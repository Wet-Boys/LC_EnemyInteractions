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

namespace EnemyInteractions
{
    internal class EnemyKillHooks
    {
        private static Hook? _enemyAiStartHook;

        private static Hook? _caveDwellerTransform;

        private static Hook? _killPlayerHook;

        private static Hook? _maskedAiStartHook;

        private static Hook? _enemyDeathHook; 

        public static void InitHooks()
        {
            _maskedAiStartHook = HookUtils.NewHook<MaskedPlayerEnemy>("Start", typeof(EnemyKillHooks), "OnMaskedAiStart");
            _enemyAiStartHook = HookUtils.NewHook<EnemyAI>("Start", typeof(EnemyKillHooks), "OnEnemyAiStart");
            _killPlayerHook = HookUtils.NewHook<PlayerControllerB>("KillPlayer", typeof(EnemyKillHooks), "OnKillPlayer");
            if (Chainloader.PluginInfos.ContainsKey("Szumi57.LethalInternship"))
            {
                InternCompat.SetupInternStartHook();
            }
            _enemyAiStartHook = HookUtils.NewHook<EnemyAI>("KillEnemy", typeof(EnemyKillHooks), "OnKillEnemy");
            _caveDwellerTransform = HookUtils.NewHook<CaveDwellerAI>("StartTransformationAnim", typeof(EnemyKillHooks), "CaveDwellerStartTransformationAnim");
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

        private static void OnKillEnemy(Action<EnemyAI, bool> orig, EnemyAI self, bool destroy = false)
        {
            orig(self, destroy);
            if (!destroy && EnemyInteractionsPlugin.BadAssCompanyPresent && EnemyInteractionSettings.emoteOnDeath.Value && BoneMapper.playersToMappers.ContainsKey(self.gameObject)&& EnemyInteractionSettings.useBadAssCompany.Value)
            {
                BoneMapper boneMapper = BoneMapper.playersToMappers[self.gameObject];
                boneMapper.preserveProps = true;
                EnemyEmote enemyEmote = new EnemyEmote("com.weliveinasociety.badasscompany__I NEED A MEDIC BAG", 5.5f);
                GameObject gameObject = new GameObject();
                EmoteStopper emoteStopper = gameObject.AddComponent<EmoteStopper>();
                emoteStopper.StartCoroutine(emoteStopper.StopEmoteAfterTime(boneMapper, enemyEmote.maxDuration));
                boneMapper.props.Add(gameObject);
                if (CanEmoteChecker.CanEmote(boneMapper.enemyController.enemyType.enemyName))
                {
                    CustomEmotesAPI.PlayAnimation(enemyEmote.animationName, boneMapper);
                }
            }
        }
        private static void CaveDwellerStartTransformationAnim(Action<CaveDwellerAI> orig, CaveDwellerAI self)
        {
            if ((bool)self.GetComponent<RandomEmotesStarter>())
            {
                CustomEmotesAPI.localMapper.StartCoroutine(SetupManEaterAdult(self));
            }
            orig(self);
        }
        private static IEnumerator SetupManEaterAdult(CaveDwellerAI self)
        {
            yield return new WaitForSeconds(2.5f);
            self.gameObject.GetComponent<RandomEmotesStarter>().Setup(self);
        }
    }
}
