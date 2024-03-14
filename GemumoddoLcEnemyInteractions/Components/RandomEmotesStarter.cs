using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EnemyInteractions.Components
{
    public class RandomEmotesStarter : MonoBehaviour
    {
        internal EnemyAI enemyAI;

        public void Setup(EnemyAI enemyAI)
        {
            this.enemyAI = enemyAI;
            StartCoroutine(SetupRandomEmotes(enemyAI));
        }
        public IEnumerator SetupRandomEmotes(EnemyAI self)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();//just to be safe lol
            if (BoneMapper.playersToMappers.ContainsKey(self.gameObject))
            {
                BoneMapper b = BoneMapper.playersToMappers[self.gameObject];
                RandomEmotePlayer component = b.gameObject.AddComponent<RandomEmotePlayer>();
                component.SetupToRandomlyEmote(b, self);
            }
        }
    }
}
