using EnemyInteractions.DataStuffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnemyInteractions.Utils
{
    internal class CanEmoteChecker
    {
        public static bool CanEmote(string enemyName)
        {
            switch (enemyName)
            {
                case "Bunker Spider":
                    return  EnemyInteractionSettings.bunkerSpiderEmote.Value;
                    break;
                case "Hoarding bug":
                    return  EnemyInteractionSettings.hoardingBugEmote.Value;
                    break;
                case "Earth Leviathan":
                    return  EnemyInteractionSettings.earthLeviathanEmote.Value;
                    break;
                case "Crawler":
                    return  EnemyInteractionSettings.thumperEmote.Value;
                    break;
                case "Blob":
                    return  EnemyInteractionSettings.hygrodereEmote.Value;
                    break;
                case "Centipede":
                    return  EnemyInteractionSettings.snareFleaEmote.Value;
                    break;
                case "Nutcracker":
                    return  EnemyInteractionSettings.nutcrackerEmote.Value;
                    break;
                case "Baboon hawk":
                    return  EnemyInteractionSettings.baboonHawkEmote.Value;
                    break;
                case "Puffer":
                    return  EnemyInteractionSettings.sporeLizardEmote.Value;
                    break;
                case "Spring":
                    return  EnemyInteractionSettings.coilHeadEmote.Value;
                    break;
                case "Jester":
                    return  EnemyInteractionSettings.jesterEmote.Value;
                    break;
                case "Flowerman":
                    return  EnemyInteractionSettings.brackenEmote.Value;
                    break;
                case "Girl":
                    return  EnemyInteractionSettings.ghostGirlEmote.Value;
                    break;
                case "MouthDog":
                    return  EnemyInteractionSettings.eyelessDogEmote.Value;
                    break;
                case "ForestGiant":
                    return  EnemyInteractionSettings.forestKeeperEmote.Value;
                    break;
                case "Masked":
                    return  EnemyInteractionSettings.maskedEmote.Value;
                    break;
                case "Shy guy":
                    return  EnemyInteractionSettings.shyGuyEmote.Value;
                    break;
                case "SkibidiToilet":
                    return  EnemyInteractionSettings.skibidiToiletEmote.Value;
                    break;
                case "Demogorgon":
                    return  EnemyInteractionSettings.demoGorgonEmote.Value;
                    break;
                case "Peeper":
                    return  EnemyInteractionSettings.peeperEmote.Value;
                    break;
                case "RadMech":
                    return  EnemyInteractionSettings.radMechEmote.Value;
                    break;
                case "Butler":
                    return  EnemyInteractionSettings.butlerEmote.Value;
                    break;
                case "Tulip Snake":
                    return  EnemyInteractionSettings.tulipSnakeEmote.Value;
                    break;
                case "HarpGhost":
                    return  EnemyInteractionSettings.harpGhostEmote.Value;
                    break;
                case "EnforcerGhost":
                    return  EnemyInteractionSettings.enforcerGhostEmote.Value;
                    break;
                case "BagpipeGhost":
                    return  EnemyInteractionSettings.bagpipeGhostEmote.Value;
                    break;
                case "SlendermanEnemy":
                    return  EnemyInteractionSettings.slendermanEmote.Value;
                    break;
                case "RedWoodGiant":
                    return  EnemyInteractionSettings.redWoodGiantEmote.Value;
                    break;
                case "DriftWoodGiant":
                    return  EnemyInteractionSettings.driftWoodGiantEmote.Value;
                    break;
                case "Foxy":
                    return  EnemyInteractionSettings.foxyEmote.Value;
                    break;
                case "The Fiend":
                    return  EnemyInteractionSettings.theFiendEmote.Value;
                    break;
                case "Siren Head":
                    return  EnemyInteractionSettings.sirenHeadEmote.Value;
                    break;
                case "Football":
                    return  EnemyInteractionSettings.footballEmote.Value;
                    break;
                case "Sentinel":
                    return  EnemyInteractionSettings.sentinelEmote.Value;
                    break;
                case "Bush Wolf":
                    return  EnemyInteractionSettings.bushWolfEmote.Value;
                    break;
                case "Clay Surgeon":
                    return  EnemyInteractionSettings.claySurgeonEmote.Value;
                    break;
                case "InternNPC":
                    return EnemyInteractionSettings.internEmote.Value;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}
