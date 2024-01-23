using System;
using GameNetcodeStuff;
using GemumoddoLcDevTools.Utils;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GemumoddoLcDevTools.Components;

public class NoClipController : MonoBehaviour
{
    private PlayerControllerB? _playerController;
    private CharacterController? _characterController;
    private Rigidbody? _rigidBody;

    private Hook? _damagePlayerHook;
    private ILHook? _playerUpdateILHook;
    private bool _noClipActive;
    private LayerMask _origExcludeLayers;
    private bool _hooksActive;
    
    private Vector3 _noClipForces = Vector3.zero;
    private float _speed = 3000f;

    private void Start()
    {
        Setup();
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Update()
    {
        Setup();
        
        HandleNoClipForces();
    }

    private void OnDisable()
    {
        CleanUp();
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void Setup()
    {
        EnsureComponentReferences();

        if (_hooksActive || _playerController != StartOfRound.Instance.localPlayerController)
            return;
        
        SetupHooks();
        InputActions.NoClip.performed += NoClipOnPerformed;

        _origExcludeLayers = _characterController!.excludeLayers;
        _noClipActive = false;
        _hooksActive = true;
    }

    private void CleanUp()
    {
        if (!_hooksActive || _playerController != StartOfRound.Instance.localPlayerController)
            return;
        
        InputActions.NoClip.performed -= NoClipOnPerformed;
        
        DisableNoClip();
        DisposeHooks();
        _hooksActive = false;
    }

    private void HandleNoClipForces()
    {
        if (_playerController is null)
            return;

        var movement = _playerController.moveInputVector;
        var camTransform = _playerController.gameplayCamera.transform;

        var right = camTransform.right;
        var forward = Vector3.Cross(right, camTransform.up);
        
        _noClipForces = (right * movement.x + forward * movement.y) * (_speed * Time.deltaTime);
    }

    private void EnableNoClip()
    {
        if (_playerController is null || _characterController is null || _rigidBody is null)
            return;
        
        Logging.Info($"Enabling NoClip for Player ({_playerController.playerUsername})");
        
        _playerController.ResetFallGravity();
        _noClipActive = true;

        _origExcludeLayers = _characterController.excludeLayers;
        _characterController.excludeLayers = -1;
    }

    private void DisableNoClip()
    {
        if (_playerController is null || _characterController is null || _rigidBody is null)
            return;
        
        Logging.Info($"Disabling NoClip for Player ({_playerController.playerUsername})");
        
        _playerController.ResetFallGravity();
        _noClipActive = false;

        _characterController.excludeLayers = _origExcludeLayers;
    }

    private void OnDamagePlayer(Action<PlayerControllerB, int, bool, bool, CauseOfDeath, int, bool, Vector3> orig,
        PlayerControllerB self, int damageNumber, bool hasDamageSfx, bool callRpc, CauseOfDeath causeOfDeath,
        int deathAnimation, bool fallDamage, Vector3 force)
    {
        if (self != _playerController || !_noClipActive)
        {
            orig(self, damageNumber, hasDamageSfx, callRpc, causeOfDeath, deathAnimation, fallDamage, force);
            return;
        }

        if (causeOfDeath == CauseOfDeath.Gravity || fallDamage)
            return;
        
        _playerController.ResetFallGravity();
        orig(self, damageNumber, hasDamageSfx, callRpc, causeOfDeath, deathAnimation, fallDamage, force);
    }
    
    private void NoClipUpdateILManipulator(ILContext ctx)
    {
        var cursor = new ILCursor(ctx);
        
        ILLabel? ifGroundedLabel = null;
        
        // Match the following:
        // if (!this.inSpecialInteractAnimation || this.inShockingMinigame)
        //     if (!this.thisController.isGrounded)
        cursor.GotoNext(
            MoveType.After,
            x => x.MatchLdarg(0),
            x => x.MatchLdfld<PlayerControllerB>("inSpecialInteractAnimation"),
            x => x.MatchBrfalse(out _),
            x => x.MatchLdarg(0),
            x => x.MatchLdfld<PlayerControllerB>("inShockingMinigame"),
            x => x.MatchBrfalse(out ifGroundedLabel),
            x => x.MatchLdarg(0),
            x => x.MatchLdfld<PlayerControllerB>("thisController"),
            x => x.MatchCallvirt<CharacterController>("get_isGrounded"),
            x => x.MatchBrtrue(out _)
        );
        
        // Create Label for original instruction
        var origIsGroundedLabel = cursor.DefineLabel();
        
        // Load instance of PlayerControllerB
        cursor.Emit(OpCodes.Ldarg, 0);
        // Ensure that we are on the correct instance
        cursor.EmitDelegate<Func<PlayerControllerB?, bool>>(IsNoClipActive);
        // If NoClip isn't active, jump to original instruction label
        cursor.Emit(OpCodes.Brfalse_S, origIsGroundedLabel);
        // Otherwise Reset the fallValue and fallValueUncapped to prevent gravity from being calculated
        cursor.EmitDelegate<Action>(ApplyNoClipForces);
        cursor.Emit(OpCodes.Br, ifGroundedLabel);

        cursor.MarkLabel(origIsGroundedLabel);
        
        // Match the following:
        // this.walkForce = Vector3.MoveTowards(this.walkForce, base.transform.right * this.moveInputVector.x + base.transform.forward * this.moveInputVector.y, num6 * Time.deltaTime);
        cursor.GotoNext(
            x => x.MatchLdarg(0),
            x => x.MatchLdarg(0),
            x => x.MatchLdfld<PlayerControllerB>("walkForce"),
            x => x.MatchLdarg(0),
            x => x.MatchCall<Component>("get_transform"),
            x => x.MatchCallvirt<Transform>("get_right"),
            x => x.MatchLdarg(0),
            x => x.MatchLdflda<PlayerControllerB>("moveInputVector"),
            x => x.MatchLdfld<Vector2>("x"),
            x => x.MatchCall<Vector3>("op_Multiply"),
            x => x.MatchLdarg(0),
            x => x.MatchCall<Component>("get_transform"),
            x => x.MatchCallvirt<Transform>("get_forward"),
            x => x.MatchLdarg(0),
            x => x.MatchLdflda<PlayerControllerB>("moveInputVector"),
            x => x.MatchLdfld<Vector2>("y"),
            x => x.MatchCall<Vector3>("op_Multiply"),
            x => x.MatchCall<Vector3>("op_Addition"),
            x => x.MatchLdloc(9),
            x => x.MatchCall<Time>("get_deltaTime"),
            x => x.MatchMul(),
            x => x.MatchCall<Vector3>("MoveTowards"),
            x => x.MatchStfld<PlayerControllerB>("walkForce")
        );

        // Create Label for original instruction
        var origInstrLabel = cursor.DefineLabel();

        // Load instance of PlayerControllerB
        cursor.Emit(OpCodes.Ldarg, 0);
        // Ensure that we are on the correct instance
        cursor.EmitDelegate<Func<PlayerControllerB?, bool>>(IsNoClipActive);
        // If NoClip isn't active, jump to original instruction label
        cursor.Emit(OpCodes.Brfalse_S, origInstrLabel);
        // Otherwise Reset the fallValue and fallValueUncapped to prevent gravity from being calculated
        cursor.EmitDelegate<Action>(ApplyNoClipForces);
        // Set walkForce to zero
        cursor.Emit(OpCodes.Ldarg_0);
        cursor.Emit(OpCodes.Call, AccessTools.Method(typeof(Vector3), "get_zero"));
        cursor.Emit(OpCodes.Stfld, AccessTools.Field(typeof(PlayerControllerB), "walkForce"));

        cursor.MarkLabel(origInstrLabel);
    }

    private bool IsNoClipActive(PlayerControllerB? other)
    {
        if (other is null || _playerController is null || other != _playerController)
            return false;

        return _noClipActive;
    }

    private void ApplyNoClipForces()
    {
        if (_playerController is null)
            return;

        _playerController.ResetFallGravity();
        _playerController.externalForces = _noClipForces;
    }

    private void SetupHooks()
    {
        if (_playerController is null)
            return;
        
        Logging.Info($"NoClipController: {_playerController}, StartOfRound: {StartOfRound.Instance.localPlayerController}");

        if (_playerController != StartOfRound.Instance.localPlayerController)
            return;
        
        _damagePlayerHook =
            HookUtils.NewHook<PlayerControllerB, NoClipController>(nameof(PlayerControllerB.DamagePlayer),
                nameof(OnDamagePlayer), this);

        _playerUpdateILHook = HookUtils.NewILHook<PlayerControllerB>("Update", NoClipUpdateILManipulator);
    }

    private void DisposeHooks()
    {
        _damagePlayerHook?.Dispose();
        _playerUpdateILHook?.Dispose();
    }

    private void NoClipOnPerformed(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
            return;
        
        if (_noClipActive)
            DisableNoClip();
        else
            EnableNoClip();
    }

    private void EnsureComponentReferences()
    {
        if (_playerController is null)
            _playerController = GetComponent<PlayerControllerB>();

        if (_characterController is null)
            _characterController = GetComponent<CharacterController>();

        if (_rigidBody is null)
            _rigidBody = GetComponent<Rigidbody>();
    }
}