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


        public static ConfigEntry<bool> shyGuyEmote;
        public static ConfigEntry<bool> skibidiToiletEmote;
        public static ConfigEntry<bool> demoGorgonEmote;
        public static ConfigEntry<bool> peeperEmote;
        public static ConfigEntry<bool> radMechEmote;
        public static ConfigEntry<bool> butlerEmote;
        public static ConfigEntry<bool> tulipSnakeEmote;
        public static ConfigEntry<bool> harpGhostEmote;
        public static ConfigEntry<bool> enforcerGhostEmote;
        public static ConfigEntry<bool> bagpipeGhostEmote;
        public static ConfigEntry<bool> slendermanEmote;
        public static ConfigEntry<bool> redWoodGiantEmote;
        public static ConfigEntry<bool> driftWoodGiantEmote;
        public static ConfigEntry<bool> foxyEmote;
        public static ConfigEntry<bool> theFiendEmote;
        public static ConfigEntry<bool> sirenHeadEmote;
        public static ConfigEntry<bool> footballEmote;
        public static ConfigEntry<bool> sentinelEmote;
        public static ConfigEntry<bool> bushWolfEmote;
        public static ConfigEntry<bool> claySurgeonEmote;
        public static ConfigEntry<bool> internEmote;
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

            shyGuyEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Shy Guy can emote", true, "Lets the specified enemy emote with enemy interactions");
            skibidiToiletEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Skibidi Toilet can emote", true, "Lets the specified enemy emote with enemy interactions");
            demoGorgonEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "DemoGorgon can emote", true, "Lets the specified enemy emote with enemy interactions");
            peeperEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Peeper can emote", true, "Lets the specified enemy emote with enemy interactions");
            radMechEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Old Bird can emote", true, "Lets the specified enemy emote with enemy interactions");
            butlerEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Butler can emote", true, "Lets the specified enemy emote with enemy interactions");
            tulipSnakeEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Tulip Snake can emote", true, "Lets the specified enemy emote with enemy interactions");
            harpGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Harp Ghost can emote", true, "Lets the specified enemy emote with enemy interactions");
            enforcerGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Enforcer Ghost can emote", true, "Lets the specified enemy emote with enemy interactions");
            bagpipeGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Bagpipe Ghost can emote", true, "Lets the specified enemy emote with enemy interactions");
            slendermanEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Slenderman can emote", true, "Lets the specified enemy emote with enemy interactions");
            redWoodGiantEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Redwood Giant can emote", true, "Lets the specified enemy emote with enemy interactions");
            driftWoodGiantEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Driftwood Giant can emote", true, "Lets the specified enemy emote with enemy interactions");
            foxyEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Foxy can emote", true, "Lets the specified enemy emote with enemy interactions");
            theFiendEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "The Fiend can emote", true, "Lets the specified enemy emote with enemy interactions");
            sirenHeadEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Siren Head can emote", true, "Lets the specified enemy emote with enemy interactions");
            footballEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Football can emote", true, "Lets the specified enemy emote with enemy interactions");
            sentinelEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Modded Enemies", "Sentinel can emote", true, "Lets the specified enemy emote with enemy interactions");
            bushWolfEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Kidnapper Fox can emote", true, "Lets the specified enemy emote with enemy interactions");
            claySurgeonEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Barber can emote", true, "Lets the specified enemy emote with enemy interactions");
            internEmote = EnemyInteractionsPlugin.instance.Config.Bind<bool>("Misc", "Interns can emote", true, "Lets the specified enemy emote with enemy interactions");
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
