using EmotesAPI;
using EnemyInteractions.Utils;
using System.Collections;
using UnityEngine;

namespace EnemyInteractions.Components
{
    internal class EmoteStopper : MonoBehaviour
    {
        private Coroutine _stopCoroutine;

        internal IEnumerator StopEmoteAfterTime(BoneMapper mapper, float time)
        {
            _stopCoroutine = StartCoroutine(StopEmoteAfterTimeInternal(mapper, time));
            yield return _stopCoroutine;
        }

        private IEnumerator StopEmoteAfterTimeInternal(BoneMapper mapper, float time)
        {
            yield return new WaitForSeconds(time);

            // 检查 mapper 和 mapper.gameObject 是否为空
            if (mapper == null || mapper.gameObject == null)
            {
                Logging.Warn("Mapper or its GameObject is null in StopEmoteAfterTime. Skipping emote stop.");
                yield break;
            }

            // 检查 mapper 是否仍然有效
            if (mapper.emoteSkeletonAnimator == null)
            {
                Logging.Warn("Mapper's emoteSkeletonAnimator is null in StopEmoteAfterTime. Skipping emote stop.");
                yield break;
            }

            // 停止表情动画
            CustomEmotesAPI.PlayAnimation("none", mapper);

            // 销毁当前 GameObject
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            // 在对象销毁时停止协程
            if (_stopCoroutine != null)
            {
                StopCoroutine(_stopCoroutine);
            }
        }
    }
}
