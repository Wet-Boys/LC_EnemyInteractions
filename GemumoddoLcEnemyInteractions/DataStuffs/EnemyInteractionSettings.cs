using BepInEx.Configuration;
using EmotesAPI;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;
using LethalConfig;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace EnemyInteractions.DataStuffs
{
    public static class EnemyInteractionSettings
    {
        public static ConfigEntry<float> RandomEmoteFrequencyMinimum;
        public static ConfigEntry<float> RandomEmoteFrequencyMaximum;
        public static ConfigEntry<float> OnKillEmoteChance;


        internal static void Setup()
        {
            RandomEmoteFrequencyMinimum = CustomEmotesAPI.instance.Config.Bind<float>("Settings", "Random Emote Frequency Minimum Time", 60, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            RandomEmoteFrequencyMaximum = CustomEmotesAPI.instance.Config.Bind<float>("Settings", "Random Emote Frequency Maximum Time", 120, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            OnKillEmoteChance = CustomEmotesAPI.instance.Config.Bind<float>("Settings", "On Kill Emote Chance", 100, "% chance for enemies to emote upon a player dying near them");


            RandomEmoteFrequencyMinimum.SettingChanged += FrequencyChange;
            RandomEmoteFrequencyMaximum.SettingChanged += FrequencyChange;
            OnKillEmoteChance.SettingChanged += FrequencyChange;
            ClampChance();
            LethalConfig();
        }

        private static void FrequencyChange(object sender, EventArgs e)
        {
            ClampChance();
        }
        private static void ClampChance()
        {
            RandomEmoteFrequencyMaximum.Value = Mathf.Clamp(RandomEmoteFrequencyMaximum.Value, 2f, 86400f);
            RandomEmoteFrequencyMinimum.Value = Mathf.Clamp(RandomEmoteFrequencyMinimum.Value, 1f, RandomEmoteFrequencyMaximum.Value);
            OnKillEmoteChance.Value = Mathf.Clamp(OnKillEmoteChance.Value, 0, 100);
        }
        private static void LethalConfig()
        {
            LethalConfigManager.SetModDescription("Enemies can emote too!");

            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(RandomEmoteFrequencyMinimum, new FloatSliderOptions { Min = 0f, Max = 600f, RequiresRestart = false }));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(RandomEmoteFrequencyMaximum, new FloatSliderOptions { Min = 0f, Max = 600f, RequiresRestart = false }));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(OnKillEmoteChance, new FloatSliderOptions { Min = 0, Max = 100, RequiresRestart = false }));

        }
    }
}
