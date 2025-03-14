using EmotesAPI;
using EnemyInteractions.DataStuffs;
using EnemyInteractions.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace EnemyInteractions.Components
{
    internal class RandomEmotePlayer : MonoBehaviour
    {
        internal BoneMapper? personalMapper;
        internal EnemyAI? personalAI;
        internal bool skipNextRandomPlay = false;
        internal void SetupToRandomlyEmote(BoneMapper personalMapper, EnemyAI personalAI)
        {
            if (CustomEmotesAPI.localMapper.isServer)
            {
                this.personalMapper = personalMapper;
                this.personalAI = personalAI;
                StartCoroutine(PlayEmotesRandomly());
            }
        }
        internal IEnumerator PlaySpecificEmote(EnemyEmote emoteName, bool skip, BoneMapper mapper, RandomEmotePlayer emotePlayer, int recursionDepth = 0)
        {
            if (recursionDepth >= 10) // 限制递归深度
            {
                Logging.Info("Recursion depth limit reached in PlaySpecificEmote.");
                yield break;
            }

            // 检查 personalAI 和 personalMapper 是否为空
            if (personalAI == null || personalMapper == null)
            {
                Logging.Error("personalAI or personalMapper is null in PlaySpecificEmote.");
                yield break;
            }

            // 检查 mapper 是否为空
            if (mapper == null)
            {
                Logging.Error("mapper is null in PlaySpecificEmote.");
                yield break;
            }

            // 检查 enemyController 是否为空
            if (mapper.enemyController == null)
            {
                Logging.Error("mapper.enemyController is null in PlaySpecificEmote.");
                yield break;
            }

            // 检查 enemyType 是否为空
            if (mapper.enemyController.enemyType == null)
            {
                Logging.Error("mapper.enemyController.enemyType is null in PlaySpecificEmote.");
                yield break;
            }
            /*
            // 检查 props 是否为空
            if (mapper.props == null)
            {
                Logging.Error("mapper.props is null in PlaySpecificEmote.");
                yield break;
            }
            */
            // 检查其他条件
            if (personalAI.isEnemyDead || mapper.emoteSkeletonAnimator.enabled || emotePlayer.skipNextRandomPlay)
            {
                Logging.Info("Skipping emote due to conditions: isEnemyDead, emoteSkeletonAnimator.enabled, or skipNextRandomPlay.");
                yield break;
            }

            emotePlayer.skipNextRandomPlay = skip;
            mapper.preserveProps = true;

            // 创建 GameObject 并添加 EmoteStopper 组件
            GameObject g = new GameObject();
            EmoteStopper lmao = g.AddComponent<EmoteStopper>();
            lmao.StartCoroutine(lmao.StopEmoteAfterTime(mapper, emoteName.maxDuration));
            mapper.props.Add(g);

            // 检查是否可以播放表情
            if (CanEmoteChecker.CanEmote(mapper.enemyController.enemyType.enemyName))
            {
                Logging.Info($"Playing emote: {emoteName.animationName}");
                CustomEmotesAPI.PlayAnimation(emoteName.animationName, mapper);
            }
            else
            {
                Logging.Info($"Cannot play emote for enemy: {mapper.enemyController.enemyType.enemyName}");
            }

            // 等待一段时间
            yield return new WaitForSeconds(0.1f);

            // 获取附近的敌人
            List<GameObject> nearbyEnemies = GetEnemies.ReturnAllEnemiesInRange(personalAI.gameObject, 15f);
            foreach (GameObject item in nearbyEnemies)
            {
                if (item == null || item == personalAI.gameObject)
                {
                    continue;
                }

                // 检查 BoneMapper 是否包含该敌人
                if (!BoneMapper.playersToMappers.ContainsKey(item))
                {
                    continue;
                }

                // 获取 BoneMapper 和 EnemyAI
                BoneMapper b = BoneMapper.playersToMappers[item];
                EnemyAI enemyAI = item.GetComponent<EnemyAI>();

                if (enemyAI == null || enemyAI.isEnemyDead)
                {
                    continue;
                }

                // 检查表情动画器是否启用
                if (b.emoteSkeletonAnimator.enabled)
                {
                    continue;
                }

                // 随机播放表情
                if (Random.Range(0, 5) > 1)
                {
                    RandomEmotePlayer randomPlayer = b.GetComponent<RandomEmotePlayer>();
                    if (randomPlayer != null)
                    {
                        Logging.Info($"Triggering emote on nearby enemy: {b.enemyController.enemyType.enemyName}");
                        randomPlayer.StartCoroutine(PlaySpecificEmote(emoteName, skip: true, b, randomPlayer, recursionDepth + 1));
                    }
                }
            }
        }
        internal IEnumerator PlayEmotesRandomly()
        {
            while (!personalAI.isEnemyDead) // 移除超时机制，确保协程持续运行
            {
                float seconds = Random.Range(EnemyInteractionSettings.RandomEmoteFrequencyMinimum.Value, EnemyInteractionSettings.RandomEmoteFrequencyMaximum.Value);
                yield return new WaitForSeconds(seconds);

                if (!skipNextRandomPlay)
                {
                    EnemyEmote emote = EmoteOptions.intermittentEmoteList[Random.Range(0, EmoteOptions.intermittentEmoteList.Count)];
                    Logging.Info($"Playing random emote: {emote.animationName}");
                    StartCoroutine(PlaySpecificEmote(emote, false, personalMapper, this));
                }
                else
                {
                    Logging.Info("Skipping random emote due to skipNextRandomPlay.");
                }

                // 重置 skipNextRandomPlay
                skipNextRandomPlay = false;
            }

            Logging.Info("PlayEmotesRandomly coroutine ended because the enemy is dead.");
        }
    }
}
