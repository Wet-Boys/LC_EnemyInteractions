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
                CustomEmotesAPI.PlayAnimation(emoteName.animationName, mapper);



                yield return new WaitForSeconds(UnityEngine.Random.Range(.1f, .5f));
                var nearbyEnemies = GetEnemies.ReturnAllEnemiesInRange(personalAI.gameObject, 15f);
                foreach (var item in nearbyEnemies)
                {
                    if (item == personalAI.gameObject)
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
            yield return new WaitForSeconds(UnityEngine.Random.Range(10, 20));
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
