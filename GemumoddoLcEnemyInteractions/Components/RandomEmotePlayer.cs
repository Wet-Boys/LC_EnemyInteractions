using EmotesAPI;
using EnemyInteractions.DataStuffs;
using EnemyInteractions.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
        internal IEnumerator PlaySpecificEmote(EnemyEmote emoteName, bool skip, BoneMapper mapper, RandomEmotePlayer emotePlayer)
        {
            if (!personalAI.isEnemyDead && !mapper.emoteSkeletonAnimator.enabled && !emotePlayer.skipNextRandomPlay)
            {
                emotePlayer.skipNextRandomPlay = skip;
                mapper.preserveProps = true;
                GameObject g = new GameObject();
                EmoteStopper lmao = g.AddComponent<EmoteStopper>();
                lmao.StartCoroutine(lmao.StopEmoteAfterTime(mapper, emoteName.maxDuration));
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
                    CustomEmotesAPI.PlayAnimation(emoteName.animationName, mapper);
                }



                yield return new WaitForSeconds(UnityEngine.Random.Range(.1f, .5f));
                var nearbyEnemies = GetEnemies.ReturnAllEnemiesInRange(personalAI.gameObject, 15f);
                foreach (var item in nearbyEnemies)
                {
                    if (item == personalAI.gameObject || !BoneMapper.playersToMappers.ContainsKey(item))
                    {
                        continue;
                    }
                    BoneMapper b = BoneMapper.playersToMappers[item];
                    if (UnityEngine.Random.Range(0, 5) > 1 && !b.emoteSkeletonAnimator.enabled)
                    {
                        RandomEmotePlayer randomPlayer = b.GetComponent<RandomEmotePlayer>();
                        randomPlayer.StartCoroutine(PlaySpecificEmote(emoteName, true, b, randomPlayer));
                    }
                }
            }
        }
        internal IEnumerator PlayEmotesRandomly()
        {
            float seconds = UnityEngine.Random.Range(EnemyInteractionSettings.RandomEmoteFrequencyMinimum.Value, EnemyInteractionSettings.RandomEmoteFrequencyMaximum.Value);
            yield return new WaitForSeconds(seconds);
            if (!skipNextRandomPlay)
            {
                EnemyEmote emote = EmoteOptions.intermittentEmoteList[UnityEngine.Random.Range(0, EmoteOptions.intermittentEmoteList.Count)];
                StartCoroutine(PlaySpecificEmote(emote, false, personalMapper, this));
            }
            if (!personalAI.isEnemyDead)
            {
                StartCoroutine(PlayEmotesRandomly());
            }
            skipNextRandomPlay = false;
        }

    }
}
