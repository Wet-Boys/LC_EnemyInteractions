using EmotesAPI;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using LethalInternship.AI;
using EnemyInteractions.Utils;
using EnemyInteractions.Components;


namespace EnemyInteractions
{
    internal class InternCompat
    {
        public static void SetupInternStartHook()
        {
            StartHook = HookUtils.NewHook<InternAI>("Start", typeof(InternCompat), nameof(Start));
        }
        private static void Start(Action<InternAI> orig, InternAI self)
        {
            orig(self);
            self.gameObject.AddComponent<RandomEmotesStarter>().Setup(self);
        }
        private static Hook? StartHook;
    }
}
