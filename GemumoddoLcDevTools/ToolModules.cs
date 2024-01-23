using System;
using GameNetcodeStuff;
using GemumoddoLcDevTools.Components;
using GemumoddoLcDevTools.Utils;
using MonoMod.RuntimeDetour;

namespace GemumoddoLcDevTools;

internal static class ToolModules
{
    private static Hook? _playerControllerBStartHook;
    
    public static void InitHooks()
    {
        _playerControllerBStartHook =
            HookUtils.NewHook<PlayerControllerB>("Start", typeof(ToolModules), nameof(OnPlayerControllerBStart));
    }

    private static void OnPlayerControllerBStart(Action<PlayerControllerB> orig, PlayerControllerB self)
    {
        orig(self);

        Logging.Info($"Adding NoClipController to Player ({self.playerUsername})");
        self.gameObject.AddComponent<NoClipController>();
    }
}