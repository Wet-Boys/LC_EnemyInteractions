using EmotesAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EnemyInteractions.Components
{
    internal class EmoteStopper : MonoBehaviour
    {
        internal IEnumerator StopEmoteAfterTime(BoneMapper mapper, float time)
        {
            yield return new WaitForSeconds(time);
            CustomEmotesAPI.PlayAnimation("none", mapper);
        }
    }
}
