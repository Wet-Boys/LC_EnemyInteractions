using EmotesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EnemyInteractions.Utils
{
    internal class GetEnemies
    {
        private static readonly object _lock = new object();

        internal static List<GameObject> ReturnAllEnemiesInRange(GameObject self, float range)
        {
            lock (_lock)
            {
                try
                {
                    if (CustomEmotesAPI.localMapper.isServer)
                    {
                        Collider[] hitColliders = Physics.OverlapSphere(self.transform.position, range);
                        List<GameObject> hitEnemies = new List<GameObject>();
                        foreach (var hitCollider in hitColliders)
                        {
                            EnemyAI enemyAi = hitCollider.GetComponentInParent<EnemyAI>();
                            if (enemyAi is not null && !hitEnemies.Contains(enemyAi.gameObject))
                            {
                                hitEnemies.Add(enemyAi.gameObject);
                            }
                        }
                        return hitEnemies;
                    }
                }
                catch (Exception ex)
                {
                    Logging.Error($"Exception in ReturnAllEnemiesInRange: {ex.Message}");
                }
                return [];
            }
        }
    }
}
