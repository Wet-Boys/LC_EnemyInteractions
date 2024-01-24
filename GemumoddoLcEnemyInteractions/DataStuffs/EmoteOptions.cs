using System;
using System.Collections.Generic;
using System.Text;

namespace EnemyInteractions.DataStuffs
{
    public class EmoteOptions
    {
        public static List<EnemyEmote> onKillEmotes = new List<EnemyEmote>();
        public static List<EnemyEmote> intermittentEmoteList = new List<EnemyEmote>();
    }
    public struct EnemyEmote{
        public EnemyEmote(string animationName, float maxDuration)
        {
            this.animationName = animationName;
            this.maxDuration = maxDuration;
        }
        public string animationName;
        public float maxDuration;
    }
}
