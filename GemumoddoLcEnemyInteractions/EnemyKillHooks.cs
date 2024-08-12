using BepInEx.Bootstrap;
using EmotesAPI;
using EnemyInteractions.Components;
using EnemyInteractions.DataStuffs;
using EnemyInteractions.Utils;
using GameNetcodeStuff;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.UIElements.StylePropertyAnimationSystem;

namespace EnemyInteractions
{
    internal class EnemyKillHooks
    {
        private static Hook? _enemyAiStartHook;
        private static Hook? _killPlayerHook;
        private static Hook? _maskedAiStartHook;
        public static void InitHooks()
        {
            _enemyAiStartHook = HookUtils.NewHook<MaskedPlayerEnemy>("Start", typeof(EnemyKillHooks), nameof(OnMaskedAiStart));
            _enemyAiStartHook = HookUtils.NewHook<EnemyAI>("Start", typeof(EnemyKillHooks), nameof(OnEnemyAiStart));
            _killPlayerHook = HookUtils.NewHook<PlayerControllerB>("KillPlayer", typeof(EnemyKillHooks), nameof(OnKillPlayer));
            if (Chainloader.PluginInfos.ContainsKey("Szumi57.LethalInternship"))
            {
                InternCompat.SetupInternStartHook();
            }
        }
        private static void OnMaskedAiStart(Action<MaskedPlayerEnemy> orig, MaskedPlayerEnemy self)
        {
            orig(self);
            try
            {
                if (CustomEmotesAPI.localMapper.isServer)
                {
                    self.gameObject.AddComponent<RandomEmotesStarter>().Setup(self);
                }
            }
            catch (Exception)
            {
            }
        }
        private static void OnEnemyAiStart(Action<EnemyAI> orig, EnemyAI self)
        {
            orig(self);
            try
            {
                if (CustomEmotesAPI.localMapper.isServer)
                {
                    self.gameObject.AddComponent<RandomEmotesStarter>().Setup(self);
                }
            }
            catch (Exception)
            {
            }
        }
        private static void OnKillPlayer(Action<PlayerControllerB, Vector3, bool, CauseOfDeath, int, Vector3> orig, PlayerControllerB self, Vector3 bodyVelocity, bool spawnBody = true, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown, int deathAnimation = 0, Vector3 positionOffset = default(Vector3))
        {
            try
            {
                if (CustomEmotesAPI.localMapper.isServer && UnityEngine.Random.Range(0f, 100f) < EnemyInteractionSettings.OnKillEmoteChance.Value)
                {
                    List<GameObject> hitEnemies = GetEnemies.ReturnAllEnemiesInRange(self.gameObject, 30f);
                    foreach (var item in hitEnemies)
                    {
                        if (BoneMapper.playersToMappers.ContainsKey(item))
                        {
                            BoneMapper mapper = BoneMapper.playersToMappers[item];
                            if (!mapper.emoteSkeletonAnimator.enabled)
                            {
                                mapper.preserveProps = true;
                                EnemyEmote emote = EmoteOptions.onKillEmotes[UnityEngine.Random.Range(0, EmoteOptions.onKillEmotes.Count)];
                                GameObject g = new GameObject();
                                EmoteStopper stopper = g.AddComponent<EmoteStopper>();
                                stopper.StartCoroutine(stopper.StopEmoteAfterTime(mapper, emote.maxDuration));
                                mapper.props.Add(g); 
                                bool canEmote = CanEmoteChecker.CanEmote(mapper.enemyController.enemyType.enemyName);
                                if (canEmote)
                                {
                                    CustomEmotesAPI.PlayAnimation(emote.animationName, mapper);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"couldn't play on kill effects properly for EnemyInteractions");
            }
            orig(self, bodyVelocity, spawnBody, causeOfDeath, deathAnimation, positionOffset);
        }
    }
}
