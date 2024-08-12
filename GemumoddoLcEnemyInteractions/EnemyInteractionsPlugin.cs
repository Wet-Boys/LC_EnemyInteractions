using System;
using System.Reflection;
using BepInEx;
using BepInEx.Bootstrap;
using EnemyInteractions.DataStuffs;
using EnemyInteractions.Utils;
using MonoMod.RuntimeDetour;

namespace EnemyInteractions;

[BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
[BepInDependency("com.weliveinasociety.badasscompany", BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(ModGuid, ModName, ModVersion)]
public class EnemyInteractionsPlugin : BaseUnityPlugin
{
    public const string ModGuid = "com.gemumoddo.enemyinteractions";
    public const string ModName = "Enemy Interactions";
    public const string ModVersion = "1.1.6";
    public static EnemyInteractionsPlugin instance;
    private void Awake()
    {
        instance = this;
        Logging.SetLogSource(Logger);
        EnemyKillHooks.InitHooks();
        EnemyInteractionSettings.Setup();
        if (Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.badasscompany"))
        {
            EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Default Dance", 30));
            EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Take The L", 3));
            EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Orange Justice", 3));
            EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__California Gurls", 6.75f));
            EmoteOptions.onKillEmotes.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Dabstand", 30));


            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Gangnam Style", 1));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Thicc", 2));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Butt", 30));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Club Penguin", 5));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__DevilSpawn", 6));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__I NEED A MEDIC BAG", .8f));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Float", 1.2f));
            EmoteOptions.intermittentEmoteList.Add(new EnemyEmote("com.weliveinasociety.badasscompany__Bird", .5f));
        }
    }
}