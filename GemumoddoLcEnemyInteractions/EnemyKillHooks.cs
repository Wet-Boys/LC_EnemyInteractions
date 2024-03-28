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
        private static void OnKillPlayer(Action<PlayerControllerB, Vector3, bool, CauseOfDeath, int> orig, PlayerControllerB self, Vector3 bodyVelocity, bool spawnBody = true, CauseOfDeath causeOfDeath = CauseOfDeath.Unknown, int deathAnimation = 0)
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
                                bool canEmote = true;
                                switch (mapper.enemyController.enemyType.enemyName)
                                {
                                    case "Bunker Spider":
                                        canEmote = EnemyInteractionSettings.bunkerSpiderEmote.Value;
                                        break;
                                    case "Hoarding bug":
                                        canEmote = EnemyInteractionSettings.hoardingBugEmote.Value;
                                        break;
                                    case "Earth Leviathan":
                                        canEmote = EnemyInteractionSettings.earthLeviathanEmote.Value;
                                        break;
                                    case "Crawler":
                                        canEmote = EnemyInteractionSettings.thumperEmote.Value;
                                        break;
                                    case "Blob":
                                        canEmote = EnemyInteractionSettings.hygrodereEmote.Value;
                                        break;
                                    case "Centipede":
                                        canEmote = EnemyInteractionSettings.snareFleaEmote.Value;
                                        break;
                                    case "Nutcracker":
                                        canEmote = EnemyInteractionSettings.nutcrackerEmote.Value;
                                        break;
                                    case "Baboon hawk":
                                        canEmote = EnemyInteractionSettings.baboonHawkEmote.Value;
                                        break;
                                    case "Puffer":
                                        canEmote = EnemyInteractionSettings.sporeLizardEmote.Value;
                                        break;
                                    case "Spring":
                                        canEmote = EnemyInteractionSettings.coilHeadEmote.Value;
                                        break;
                                    case "Jester":
                                        canEmote = EnemyInteractionSettings.jesterEmote.Value;
                                        break;
                                    case "Flowerman":
                                        canEmote = EnemyInteractionSettings.brackenEmote.Value;
                                        break;
                                    case "Girl":
                                        canEmote = EnemyInteractionSettings.ghostGirlEmote.Value;
                                        break;
                                    case "MouthDog":
                                        canEmote = EnemyInteractionSettings.eyelessDogEmote.Value;
                                        break;
                                    case "ForestGiant":
                                        canEmote = EnemyInteractionSettings.forestKeeperEmote.Value;
                                        break;
                                    case "Masked":
                                        canEmote = EnemyInteractionSettings.maskedEmote.Value;
                                        break;
                                    default:
                                        break;
                                }
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
            orig(self, bodyVelocity, spawnBody, causeOfDeath, deathAnimation);
        }
    }
}
