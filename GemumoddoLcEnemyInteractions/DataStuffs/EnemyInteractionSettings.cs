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
        public static ConfigEntry<bool> snareFleaEmote;
        public static ConfigEntry<bool> bunkerSpiderEmote;
        public static ConfigEntry<bool> hoardingBugEmote;
        public static ConfigEntry<bool> brackenEmote;
        public static ConfigEntry<bool> thumperEmote;
        public static ConfigEntry<bool> hygrodereEmote;
        public static ConfigEntry<bool> ghostGirlEmote;
        public static ConfigEntry<bool> sporeLizardEmote;
        public static ConfigEntry<bool> nutcrackerEmote;
        public static ConfigEntry<bool> coilHeadEmote;
        public static ConfigEntry<bool> jesterEmote;
        public static ConfigEntry<bool> maskedEmote;
        public static ConfigEntry<bool> eyelessDogEmote;
        public static ConfigEntry<bool> forestKeeperEmote;
        public static ConfigEntry<bool> earthLeviathanEmote;
        public static ConfigEntry<bool> baboonHawkEmote;

        internal static void Setup()
        {
            RandomEmoteFrequencyMinimum = EnemyInteractionsPlugin.instance.Config.Bind<float>("Settings", "Random Emote Frequency Minimum Time", 60, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            RandomEmoteFrequencyMaximum = EnemyInteractionsPlugin.instance.Config.Bind<float>("Settings", "Random Emote Frequency Maximum Time", 120, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            OnKillEmoteChance = EnemyInteractionsPlugin.instance.Config.Bind<float>("Settings", "On Kill Emote Chance", 100, "% chance for enemies to emote upon a player dying near them");


            snareFleaEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Snare Flea can emote", true, "Lets the specified enemy emote with enemy interactions");
            bunkerSpiderEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Bunker Spider can emote", true, "Lets the specified enemy emote with enemy interactions");
            hoardingBugEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Hoarding Bug can emote", true, "Lets the specified enemy emote with enemy interactions");
            brackenEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Bracken can emote", true, "Lets the specified enemy emote with enemy interactions");
            thumperEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Thumper can emote", true, "Lets the specified enemy emote with enemy interactions");
            hygrodereEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Hygrodere can emote", true, "Lets the specified enemy emote with enemy interactions");
            ghostGirlEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Ghost Girl can emote", true, "Lets the specified enemy emote with enemy interactions");
            sporeLizardEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Spore Lizard can emote", true, "Lets the specified enemy emote with enemy interactions");
            nutcrackerEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Nutcracker can emote", true, "Lets the specified enemy emote with enemy interactions");
            coilHeadEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Coil-Head can emote", true, "Lets the specified enemy emote with enemy interactions");
            jesterEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Jester can emote", true, "Lets the specified enemy emote with enemy interactions");
            maskedEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Masked can emote", true, "Lets the specified enemy emote with enemy interactions");
            eyelessDogEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Eyeless Dog can emote", true, "Lets the specified enemy emote with enemy interactions");
            forestKeeperEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Forest Keeper can emote", true, "Lets the specified enemy emote with enemy interactions");
            earthLeviathanEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Earth Leviathan can emote", true, "Lets the specified enemy emote (technically) with enemy interactions");
            baboonHawkEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Baboon Hawk can emote", true, "Lets the specified enemy emote with enemy interactions");

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
