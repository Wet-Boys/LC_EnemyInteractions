using EnemyInteractions.DataStuffs;

namespace EnemyInteractions.Utils
{
    internal class CanEmoteChecker
    {
        public static bool CanEmote(string enemyName)
        {
            return enemyName switch
            {
                "Bunker Spider" => EnemyInteractionSettings.bunkerSpiderEmote.Value,
                "Hoarding bug" => EnemyInteractionSettings.hoardingBugEmote.Value,
                "Earth Leviathan" => EnemyInteractionSettings.earthLeviathanEmote.Value,
                "Crawler" => EnemyInteractionSettings.thumperEmote.Value,
                "Blob" => EnemyInteractionSettings.hygrodereEmote.Value,
                "Centipede" => EnemyInteractionSettings.snareFleaEmote.Value,
                "Nutcracker" => EnemyInteractionSettings.nutcrackerEmote.Value,
                "Baboon hawk" => EnemyInteractionSettings.baboonHawkEmote.Value,
                "Puffer" => EnemyInteractionSettings.sporeLizardEmote.Value,
                "Spring" => EnemyInteractionSettings.coilHeadEmote.Value,
                "Jester" => EnemyInteractionSettings.jesterEmote.Value,
                "Flowerman" => EnemyInteractionSettings.brackenEmote.Value,
                "Girl" => EnemyInteractionSettings.ghostGirlEmote.Value,
                "MouthDog" => EnemyInteractionSettings.eyelessDogEmote.Value,
                "ForestGiant" => EnemyInteractionSettings.forestKeeperEmote.Value,
                "Masked" => EnemyInteractionSettings.maskedEmote.Value,
                "Shy guy" => EnemyInteractionSettings.shyGuyEmote.Value,
                "SkibidiToilet" => EnemyInteractionSettings.skibidiToiletEmote.Value,
                "Demogorgon" => EnemyInteractionSettings.demoGorgonEmote.Value,
                "Peeper" => EnemyInteractionSettings.peeperEmote.Value,
                "RadMech" => EnemyInteractionSettings.radMechEmote.Value,
                "Butler" => EnemyInteractionSettings.butlerEmote.Value,
                "Tulip Snake" => EnemyInteractionSettings.tulipSnakeEmote.Value,
                "HarpGhost" => EnemyInteractionSettings.harpGhostEmote.Value,
                "EnforcerGhost" => EnemyInteractionSettings.enforcerGhostEmote.Value,
                "BagpipeGhost" => EnemyInteractionSettings.bagpipeGhostEmote.Value,
                "SlendermanEnemy" => EnemyInteractionSettings.slendermanEmote.Value,
                "RedWoodGiant" => EnemyInteractionSettings.redWoodGiantEmote.Value,
                "DriftWoodGiant" => EnemyInteractionSettings.driftWoodGiantEmote.Value,
                "Foxy" => EnemyInteractionSettings.foxyEmote.Value,
                "The Fiend" => EnemyInteractionSettings.theFiendEmote.Value,
                "Siren Head" => EnemyInteractionSettings.sirenHeadEmote.Value,
                "Football" => EnemyInteractionSettings.footballEmote.Value,
                "Sentinel" => EnemyInteractionSettings.sentinelEmote.Value,
                "Bush Wolf" => EnemyInteractionSettings.bushWolfEmote.Value,
                "Clay Surgeon" => EnemyInteractionSettings.claySurgeonEmote.Value,
                "InternNPC" => EnemyInteractionSettings.internEmote.Value,
                "Maneater" => EnemyInteractionSettings.caveDwellerEmote.Value,
                _ => false,
            };
        }
    }
}
