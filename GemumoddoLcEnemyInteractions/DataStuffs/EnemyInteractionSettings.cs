using BepInEx.Configuration;
using EmotesAPI;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;
using LethalConfig;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BepInEx.Bootstrap;

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

        public static ConfigEntry<bool> caveDwellerEmote;

        public static ConfigEntry<bool> internEmote;

        public static ConfigEntry<bool> emoteOnDeath;

        public static ConfigEntry<bool> useBadAssCompany;


        internal static void Setup()
        {
            RandomEmoteFrequencyMinimum = EnemyInteractionsPlugin.instance.Config.Bind("Settings", "Random Emote Frequency Minimum Time", 60f, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            RandomEmoteFrequencyMaximum = EnemyInteractionsPlugin.instance.Config.Bind("Settings", "Random Emote Frequency Maximum Time", 120f, "Enemies will emote at random between the minimum and maximum amount of time specified in seconds");
            OnKillEmoteChance = EnemyInteractionsPlugin.instance.Config.Bind("Settings", "On Kill Emote Chance", 100f, "% chance for enemies to emote upon a player dying near them");
            snareFleaEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Snare Flea can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            bunkerSpiderEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Bunker Spider can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            hoardingBugEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Hoarding Bug can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            brackenEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Bracken can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            thumperEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Thumper can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            hygrodereEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Hygrodere can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            ghostGirlEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Ghost Girl can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            sporeLizardEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Spore Lizard can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            nutcrackerEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Nutcracker can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            coilHeadEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Coil-Head can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            jesterEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Jester can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            maskedEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Masked can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            eyelessDogEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Eyeless Dog can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            forestKeeperEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Forest Keeper can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            earthLeviathanEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Earth Leviathan can emote", defaultValue: true, "Lets the specified enemy emote (technically) with enemy interactions");
            baboonHawkEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Baboon Hawk can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            shyGuyEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Shy Guy can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            skibidiToiletEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Skibidi Toilet can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            demoGorgonEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "DemoGorgon can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            peeperEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Peeper can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            radMechEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Old Bird can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            butlerEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Butler can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            tulipSnakeEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Tulip Snake can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            harpGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Harp Ghost can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            enforcerGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Enforcer Ghost can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            bagpipeGhostEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Bagpipe Ghost can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            slendermanEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Slenderman can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            redWoodGiantEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Redwood Giant can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            driftWoodGiantEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Driftwood Giant can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            foxyEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Foxy can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            theFiendEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "The Fiend can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            sirenHeadEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Siren Head can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            footballEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Football can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            sentinelEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Sentinel can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            bushWolfEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Kidnapper Fox can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            claySurgeonEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Barber can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            caveDwellerEmote = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Maneater can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            internEmote = EnemyInteractionsPlugin.instance.Config.Bind("Modded Enemies", "Interns can emote", defaultValue: true, "Lets the specified enemy emote with enemy interactions");
            emoteOnDeath = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Enemies Need a Medic Bag", defaultValue: true, "...when they die (Requires BadAssEmotes for the emote)");
            useBadAssCompany = EnemyInteractionsPlugin.instance.Config.Bind("Misc", "Use BadAssCompany", defaultValue: true, "Use BadAssCompany?");
            RandomEmoteFrequencyMinimum.SettingChanged += FrequencyChange;
            RandomEmoteFrequencyMaximum.SettingChanged += FrequencyChange;
            OnKillEmoteChance.SettingChanged += FrequencyChange;
            ClampChance();
            if (Chainloader.PluginInfos.ContainsKey("ainavt.lc.lethalconfig"))
            {
                LethalConfig();
            }
        }

        private static void FrequencyChange(object sender, EventArgs e)
        {
            ClampChance();
        }

        private static void ClampChance()
        {
            RandomEmoteFrequencyMaximum.Value = Mathf.Clamp(RandomEmoteFrequencyMaximum.Value, 2f, 86400f);
            RandomEmoteFrequencyMinimum.Value = Mathf.Clamp(RandomEmoteFrequencyMinimum.Value, 1f, RandomEmoteFrequencyMaximum.Value);
            OnKillEmoteChance.Value = Mathf.Clamp(OnKillEmoteChance.Value, 0f, 100f);
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
