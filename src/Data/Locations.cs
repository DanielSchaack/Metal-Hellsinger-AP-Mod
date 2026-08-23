using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Randomizer
{
    public class Locations
    {
        public struct Location
        {
            public long ArchipelagoId;
            public string LocationId;
            public string Description;
            public string OriginalItemName;
            public string GameObjectName;
            public GameObject LoadedGameObject;
            public string ReferenceGameObjectName;
            public GameObject ReferenceGameObject;
            public bool IsCollected = false;
            public bool IsSetupForCollection = false;

            public EZone Zone;
            public EArena Arena;
            public ELocationType LocationType;

            public Location(Location location)
            {
                ArchipelagoId = location.ArchipelagoId;
                LocationId = location.LocationId;
                Description = location.Description;
                OriginalItemName = location.OriginalItemName;
                GameObjectName = location.GameObjectName;
                Zone = location.Zone;
                Arena = location.Arena;
                LocationType = location.LocationType;
            }
        }

        public enum EZone
        {
            Global,

            // Hells
            Tutorial,
            Voke,
            Stygia,
            Yhelm,
            Incaustis,
            Gehenna,
            Nihil,
            Acheron,
            Sheol,

            // Torments
            KillingWithRhythm,
            Giantslayer,
            UltimateMastery,
            SlaughterMastery,
            RelicThief,
            WeaponTrickery,
            DeathsEdge,

            //Leviathan
            Leviathan,

            Song,
            Weapon,
            Outfit,
        }

        [Flags]
        public enum EArena
        {
            Global = 1 << 0,
            Tutorial = 1 << 1,

            Arena1 = 1 << 2,
            Arena2 = 1 << 3,
            Arena3 = 1 << 4,
            Arena4 = 1 << 5,
            Boss = 1 << 6,

            Torment1 = 1 << 7,
            Torment2 = 1 << 8,
            Torment3 = 1 << 9,

            WalledGarden = 1 << 10,
            HighRode = 1 << 11,
            Bridge = 1 << 12,
            Pyramid = 1 << 13,
            Monument = 1 << 14,
            Ziggurat = 1 << 15,
            FinalDestination = 1 << 16,

            Basegame = 1 << 17,
            DreamOfTheBeast = 1 << 18,
            Purgatory = 1 << 19,
            EssentialHits = 1 << 20,
            DuskSoundtrack = 1 << 21,
        }

        public enum ELocationType
        {
            AnguishGate,
            WeaponPickup,
            NextMultiplier,
            MaxMultiplier,
            SecretMultiplier,
            CoatOfArms,
            Ammostash,
            HealthCrystal,
            ChaosCrystal,
            ChallengePickup,
            XpEgg,
            NightmareCrystal,
            LevelCompletion,
            LevelSpeed,
            LevelAmmostashCompletion,
            LevelHealthCrystalCompletion,
            LevelChaosCrystalCompletion,
            ArenaAmmostashCompletion,
            ArenaHealthCrystalCompletion,
            ArenaChaosCrystalCompletion,
            ArenaDestructibleCompletion,
            SectionClearWeapon,
            SectionClearOutfit,
            SectionClearMainSong,
            SectionClearBossSong,
            WeaponSkin,
            Boon,
            TormentBronze,
            TormentSilver,
            TormentGold,
            TormentCompletion,
            Codex,
            Bestiary,
            BossAchievement,
            FirstMiscellaneous
        }

        public static readonly List<ELocationType> GameObjectTypes = new List<ELocationType>
        {
            ELocationType.AnguishGate,
            ELocationType.WeaponPickup,
            ELocationType.NextMultiplier,
            ELocationType.MaxMultiplier,
            ELocationType.SecretMultiplier,
            ELocationType.CoatOfArms,
            ELocationType.Ammostash,
            ELocationType.HealthCrystal,
            ELocationType.ChaosCrystal,
            ELocationType.ChallengePickup,
        };

        public static readonly List<ELocationType> MultiplierTypes = new List<ELocationType>
        {
            ELocationType.NextMultiplier,
            ELocationType.MaxMultiplier,
            ELocationType.SecretMultiplier,
        };

        public static readonly List<ELocationType> TypesWithReferences = new List<ELocationType>
        {
            ELocationType.AnguishGate,
        };

        public static readonly Dictionary<string, Location> LocationDataByName = new Dictionary<
            string,
            Location
        >()
        {
            {
                "Tutorial Paz Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 1,
                    LocationId = "Tutorial Paz Weapon Pickup",
                    Description = "Tutorial - Paz Pickup in Arena",
                    OriginalItemName = "Paz",
                    Zone = EZone.Tutorial,
                    Arena = EArena.Tutorial,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName =
                        "Wave_Spawning/FirstAnguishGate_Encounter/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Tutorial Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 2,
                    LocationId = "Tutorial Max Multiplier 1",
                    Description = "Tutorial - Max Multiplier in the last Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Tutorial,
                    Arena = EArena.Tutorial,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "Wave_Spawning/FirstAnguishGate_Encounter/FuryBoostDisablerObject (for safety)/HUD_prompt_info_FuryBoost (enabled by PickUpFuryBoost)/MultiplierBoostMaxTier/",
                }
            },
            {
                "Tutorial Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 3,
                    LocationId = "Tutorial Anguish Gate 1",
                    Description = "Tutorial - Finished the forced Encounter",
                    OriginalItemName = "Filler",
                    Zone = EZone.Tutorial,
                    Arena = EArena.Tutorial,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/FirstAnguishGate_Encounter/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/FirstAnguishGate_Encounter/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Tutorial Completion",
                new Location
                {
                    ArchipelagoId = 4,
                    LocationId = "Tutorial Completion",
                    Description = "Tutorial - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Tutorial,
                    Arena = EArena.Tutorial,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Persephone Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 5,
                    LocationId = "Voke Persephone Weapon Pickup",
                    Description = "Voke - Weapon Pickup in Arena 1",
                    OriginalItemName = "Persephone",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_CryptGate (AnguishGate1) (CorruptedSeraphIntro)/PF_GunpickupShotgun/",
                }
            },
            {
                "Voke Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 6,
                    LocationId = "Voke Anguish Gate 1",
                    Description = "Voke - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_CryptGate (AnguishGate1) (CorruptedSeraphIntro)/PF_AnguishGate(Exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_CryptGate (AnguishGate1) (CorruptedSeraphIntro)/PF_AnguishGate(Entry)/",
                }
            },
            {
                "Voke Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 7,
                    LocationId = "Voke Anguish Gate 2",
                    Description = "Voke - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_CryptYard (AnguishGate2)/PF_AnguishGate(Exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_CryptYard (AnguishGate2)/PF_AnguishGate(Entry)/",
                }
            },
            {
                "Voke Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 8,
                    LocationId = "Voke Anguish Gate 3",
                    Description = "Voke - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_FallenChurch_ReaverIntro (AnguishGate3)/PF_AnguishGate(Exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_FallenChurch_ReaverIntro (AnguishGate3)/PF_AnguishGate(Entry)/",
                }
            },
            {
                "Voke Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 9,
                    LocationId = "Voke Anguish Gate 4",
                    Description = "Voke - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_RagingCaves (AnguishGate4)/PF_AnguishGate(Exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_RagingCaves (AnguishGate4)/PF_AnguishGate(Entry)/",
                }
            },
            {
                "Voke Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 10,
                    LocationId = "Voke Next Multiplier 1",
                    Description = "Voke - Next Multiplier in Arena 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Voke Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 11,
                    LocationId = "Voke Next Multiplier 2",
                    Description = "Voke - Next Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Voke Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 12,
                    LocationId = "Voke Next Multiplier 3",
                    Description = "Voke - Next Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Voke Boss Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 13,
                    LocationId = "Voke Boss Next Multiplier 1",
                    Description = "Voke - Next Multiplier in Boss Arena atop right Bridge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Voke Boss Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 14,
                    LocationId = "Voke Boss Next Multiplier 2",
                    Description = "Voke - Next Multiplier in Boss Arena atop left Bridge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Voke Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 15,
                    LocationId = "Voke Max Multiplier 1",
                    Description = "Voke - Max Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Voke Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 16,
                    LocationId = "Voke Secret Max Multiplier",
                    Description = "Voke - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoost_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Voke Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 17,
                    LocationId = "Voke Coat of Arms Lamb",
                    Description = "Voke - Coat of Arms in Arena 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Voke Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 18,
                    LocationId = "Voke Coat of Arms Goat",
                    Description = "Voke - Coat of Arms in Arena 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Voke Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 19,
                    LocationId = "Voke Coat of Arms Beast",
                    Description = "Voke - Coat of Arms between Arena 2 & 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Voke Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 20,
                    LocationId = "Voke Coat of Arms Archdevil",
                    Description = "Voke - Coat of Arms before Arena 1",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Voke Completion",
                new Location
                {
                    ArchipelagoId = 21,
                    LocationId = "Voke Completion",
                    Description = "Voke - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Enduring Fury Unlock",
                new Location
                {
                    ArchipelagoId = 22,
                    LocationId = "Enduring Fury Unlock",
                    Description = "Voke - Boon Completion",
                    OriginalItemName = "Enduring Fury",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Persephone Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 23,
                    LocationId = "Stygia Persephone Weapon Pickup",
                    Description = "Stygia - Weapon Pickup in Arena 1",
                    OriginalItemName = "Persephone",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName = "PF_GunpickupShotgun/",
                }
            },
            {
                "Stygia The Hounds Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 24,
                    LocationId = "Stygia The Hounds Weapon Pickup",
                    Description = "Stygia - Weapon Pickup in Arena 2",
                    OriginalItemName = "The Hounds",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_TerraceOverlook (AnguishGate2)/PF_GunPickupPistol/",
                }
            },
            {
                "Stygia Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 25,
                    LocationId = "Stygia Anguish Gate 1",
                    Description = "Stygia - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_CityGates (AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_CityGates (AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Stygia Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 26,
                    LocationId = "Stygia Anguish Gate 2",
                    Description = "Stygia - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_TerraceOverlook (AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_TerraceOverlook (AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Stygia Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 27,
                    LocationId = "Stygia Anguish Gate 3",
                    Description = "Stygia - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_MemorialSite (AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_MemorialSite (AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Stygia Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 28,
                    LocationId = "Stygia Anguish Gate 4",
                    Description = "Stygia - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_FracturedParkStaircase (AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_FracturedParkStaircase (AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Stygia Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 29,
                    LocationId = "Stygia Next Multiplier 1",
                    Description = "Stygia - Next Multiplier in Arena 1 on ground",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Stygia Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 30,
                    LocationId = "Stygia Next Multiplier 2",
                    Description = "Stygia - Next Multiplier in Arena 1 on pillar",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Stygia Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 31,
                    LocationId = "Stygia Next Multiplier 3",
                    Description = "Stygia - Next Multiplier in Arena 1 to the right",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Stygia Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 32,
                    LocationId = "Stygia Next Multiplier 4",
                    Description = "Stygia - Next Multiplier between Arena 1 & 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (3)/",
                }
            },
            {
                "Stygia Next Multiplier 5",
                new Location
                {
                    ArchipelagoId = 33,
                    LocationId = "Stygia Next Multiplier 5",
                    Description = "Stygia - Next Multiplier in Arena 2 top right",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (4)/",
                }
            },
            {
                "Stygia Next Multiplier 6",
                new Location
                {
                    ArchipelagoId = 34,
                    LocationId = "Stygia Next Multiplier 6",
                    Description = "Stygia - Next Multiplier in Arena 2 tunnel left",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (5)/",
                }
            },
            {
                "Stygia Next Multiplier 7",
                new Location
                {
                    ArchipelagoId = 35,
                    LocationId = "Stygia Next Multiplier 7",
                    Description = "Stygia - Next Multiplier in Arena 2 tunnel right",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (6)/",
                }
            },
            {
                "Stygia Next Multiplier 8",
                new Location
                {
                    ArchipelagoId = 36,
                    LocationId = "Stygia Next Multiplier 8",
                    Description = "Stygia - Next Multiplier in Arena 3 behind statue",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (7)/",
                }
            },
            {
                "Stygia Next Multiplier 9",
                new Location
                {
                    ArchipelagoId = 37,
                    LocationId = "Stygia Next Multiplier 9",
                    Description = "Stygia - Next Multiplier in Arena 3 back half",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (8)/",
                }
            },
            {
                "Stygia Next Multiplier 10",
                new Location
                {
                    ArchipelagoId = 38,
                    LocationId = "Stygia Next Multiplier 10",
                    Description = "Stygia - Next Multiplier in Arena 3 front half",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (9)/",
                }
            },
            {
                "Stygia Next Multiplier 11",
                new Location
                {
                    ArchipelagoId = 39,
                    LocationId = "Stygia Next Multiplier 11",
                    Description = "Stygia - Next Multiplier in Arena 4 high ground",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (10)/",
                }
            },
            {
                "Stygia Next Multiplier 12",
                new Location
                {
                    ArchipelagoId = 40,
                    LocationId = "Stygia Next Multiplier 12",
                    Description = "Stygia - Next Multiplier in Arena 4 low ground",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (11)/",
                }
            },
            {
                "Stygia Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 41,
                    LocationId = "Stygia Max Multiplier 1",
                    Description = "Stygia - Max Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Stygia Boss Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 42,
                    LocationId = "Stygia Boss Max Multiplier 1",
                    Description = "Stygia - Max Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Stygia Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 43,
                    LocationId = "Stygia Secret Max Multiplier",
                    Description = "Stygia - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Stygia Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 44,
                    LocationId = "Stygia Coat of Arms Lamb",
                    Description = "Stygia - Coat of Arms in Arena 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Stygia Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 45,
                    LocationId = "Stygia Coat of Arms Goat",
                    Description = "Stygia - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Stygia Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 46,
                    LocationId = "Stygia Coat of Arms Beast",
                    Description = "Stygia - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Stygia Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 47,
                    LocationId = "Stygia Coat of Arms Archdevil",
                    Description = "Stygia - Coat of Arms in Arena 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Stygia Completion",
                new Location
                {
                    ArchipelagoId = 48,
                    LocationId = "Stygia Completion",
                    Description = "Stygia - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Faster Ultimate Gain Unlock",
                new Location
                {
                    ArchipelagoId = 49,
                    LocationId = "Faster Ultimate Gain Unlock",
                    Description = "Stygia - Boon Completion",
                    OriginalItemName = "Faster Ultimate Gain",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Vulcan Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 50,
                    LocationId = "Yhelm Vulcan Weapon Pickup",
                    Description = "Yhelm - Weapon Pickup in Arena 2",
                    OriginalItemName = "Vulcan",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchCatacombs_(AnguishGate2)/PF_GunpickupVulcan/",
                }
            },
            {
                "Yhelm Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 51,
                    LocationId = "Yhelm Anguish Gate 1",
                    Description = "Yhelm - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchGarden_(AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchGarden_(AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Yhelm Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 52,
                    LocationId = "Yhelm Anguish Gate 2",
                    Description = "Yhelm - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchCatacombs_(AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchCatacombs_(AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Yhelm Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 53,
                    LocationId = "Yhelm Anguish Gate 3",
                    Description = "Yhelm - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchSewers_(AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_DamnedChurchSewers_(AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Yhelm Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 54,
                    LocationId = "Yhelm Anguish Gate 4",
                    Description = "Yhelm - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_ForestOfAnguish_(AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_ForestOfAnguish_(AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Yhelm Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 55,
                    LocationId = "Yhelm Next Multiplier 1",
                    Description = "Yhelm - Next Multiplier in Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Yhelm Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 56,
                    LocationId = "Yhelm Next Multiplier 2",
                    Description = "Yhelm - Next Multiplier in Arena 2 first on right",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Yhelm Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 57,
                    LocationId = "Yhelm Next Multiplier 3",
                    Description = "Yhelm - Next Multiplier in Arena 2 second on right",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Yhelm Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 58,
                    LocationId = "Yhelm Next Multiplier 4",
                    Description = "Yhelm - Next Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (3)/",
                }
            },
            {
                "Yhelm Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 59,
                    LocationId = "Yhelm Max Multiplier 1",
                    Description = "Yhelm - Max Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Yhelm Boss Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 60,
                    LocationId = "Yhelm Boss Next Multiplier 1",
                    Description = "Yhelm - Next Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Yhelm Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 61,
                    LocationId = "Yhelm Secret Max Multiplier",
                    Description = "Yhelm - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoost_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Yhelm Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 62,
                    LocationId = "Yhelm Coat of Arms Lamb",
                    Description = "Yhelm - Coat of Arms in Arena 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Yhelm Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 63,
                    LocationId = "Yhelm Coat of Arms Goat",
                    Description = "Yhelm - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Yhelm Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 64,
                    LocationId = "Yhelm Coat of Arms Beast",
                    Description = "Yhelm - Coat of Arms in Arena 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Yhelm Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 65,
                    LocationId = "Yhelm Coat of Arms Archdevil",
                    Description = "Yhelm - Coat of Arms in Arena 1",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Yhelm Completion",
                new Location
                {
                    ArchipelagoId = 66,
                    LocationId = "Yhelm Completion",
                    Description = "Yhelm - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Hellcrow Weapon Pickup",
                new Location
                {
                    ArchipelagoId = 67,
                    LocationId = "Incaustis Hellcrow Weapon Pickup",
                    Description = "Incaustis - Weapon Pickup in Arena 2",
                    OriginalItemName = "Hellcrow",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.WeaponPickup,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_Crow_Shrine_(AnguishGate2)/PF_GunpickupHellcrow/",
                }
            },
            {
                "Incaustis Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 68,
                    LocationId = "Incaustis Anguish Gate 1",
                    Description = "Incaustis - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_TheFirstDescent_(AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_TheFirstDescent_(AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Incaustis Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 69,
                    LocationId = "Incaustis Anguish Gate 2",
                    Description = "Incaustis - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_Crow_Shrine_(AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_Crow_Shrine_(AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Incaustis Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 70,
                    LocationId = "Incaustis Anguish Gate 3",
                    Description = "Incaustis - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_AncientBarracks_(AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_AncientBarracks_(AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Incaustis Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 71,
                    LocationId = "Incaustis Anguish Gate 4",
                    Description = "Incaustis - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_LavaFalls_(AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_LavaFalls_(AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Incaustis Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 72,
                    LocationId = "Incaustis Next Multiplier 1",
                    Description = "Incaustis - Next Multiplier in Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Incaustis Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 73,
                    LocationId = "Incaustis Next Multiplier 2",
                    Description = "Incaustis - Next Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Incaustis Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 74,
                    LocationId = "Incaustis Max Multiplier 1",
                    Description = "Incaustis - Max Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Incaustis Boss Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 75,
                    LocationId = "Incaustis Boss Max Multiplier 1",
                    Description = "Incaustis - Max Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMax/MultiplierBoostMaxTier/",
                }
            },
            {
                "Incaustis Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 76,
                    LocationId = "Incaustis Secret Max Multiplier",
                    Description = "Incaustis - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoost_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Incaustis Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 77,
                    LocationId = "Incaustis Coat of Arms Lamb",
                    Description = "Incaustis - Coat of Arms in Arena 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Incaustis Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 78,
                    LocationId = "Incaustis Coat of Arms Goat",
                    Description = "Incaustis - Coat of Arms before the boss fight",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Incaustis Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 79,
                    LocationId = "Incaustis Coat of Arms Beast",
                    Description = "Incaustis - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Incaustis Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 80,
                    LocationId = "Incaustis Coat of Arms Archdevil",
                    Description = "Incaustis - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Incaustis Completion",
                new Location
                {
                    ArchipelagoId = 81,
                    LocationId = "Incaustis Completion",
                    Description = "Incaustis - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Deadlier Dash Unlock",
                new Location
                {
                    ArchipelagoId = 82,
                    LocationId = "Deadlier Dash Unlock",
                    Description = "Incaustis - Boon Completion",
                    OriginalItemName = "Deadlier Dash",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 83,
                    LocationId = "Gehenna Anguish Gate 1",
                    Description = "Gehenna - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_CliffSide (AnguishGate1)/PF_AnguishGate_modular (exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_CliffSide (AnguishGate1)/PF_AnguishGate_modular (entry)/",
                }
            },
            {
                "Gehenna Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 84,
                    LocationId = "Gehenna Anguish Gate 2",
                    Description = "Gehenna - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_BurialChamber (AnguishGate2)/PF_AnguishGate_modular (exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_BurialChamber (AnguishGate2)/PF_AnguishGate_modular (entry)/",
                }
            },
            {
                "Gehenna Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 85,
                    LocationId = "Gehenna Anguish Gate 3",
                    Description = "Gehenna - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_SacrificialShrine (AnguishGate3)/PF_AnguishGate_modular (exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_SacrificialShrine (AnguishGate3)/PF_AnguishGate_modular (entry)/",
                }
            },
            {
                "Gehenna Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 86,
                    LocationId = "Gehenna Anguish Gate 4",
                    Description = "Gehenna - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_EmbalmingRoom (AnguishGate4)/PF_AnguishGate_modular (exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_EmbalmingRoom (AnguishGate4)/PF_AnguishGate_modular (entry)/",
                }
            },
            {
                "Gehenna Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 87,
                    LocationId = "Gehenna Next Multiplier 1",
                    Description = "Gehenna - Next Multiplier in Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Gehenna Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 88,
                    LocationId = "Gehenna Next Multiplier 2",
                    Description = "Gehenna - Next Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Gehenna Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 89,
                    LocationId = "Gehenna Max Multiplier 1",
                    Description = "Gehenna - Max Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Gehenna Boss Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 90,
                    LocationId = "Gehenna Boss Next Multiplier 1",
                    Description = "Gehenna - Next Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Gehenna Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 91,
                    LocationId = "Gehenna Secret Max Multiplier",
                    Description = "Gehenna - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostSecret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Gehenna Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 92,
                    LocationId = "Gehenna Coat of Arms Lamb",
                    Description = "Gehenna - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Gehenna Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 93,
                    LocationId = "Gehenna Coat of Arms Goat",
                    Description = "Gehenna - Coat of Arms between Arena 2 & 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Gehenna Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 94,
                    LocationId = "Gehenna Coat of Arms Beast",
                    Description = "Gehenna - Coat of Arms before the boss fight",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Gehenna Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 95,
                    LocationId = "Gehenna Coat of Arms Archdevil",
                    Description = "Gehenna - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Gehenna Completion",
                new Location
                {
                    ArchipelagoId = 96,
                    LocationId = "Gehenna Completion",
                    Description = "Gehenna - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 97,
                    LocationId = "Nihil Anguish Gate 1",
                    Description = "Nihil - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Encounter_1_(AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Encounter_1_(AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Nihil Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 98,
                    LocationId = "Nihil Anguish Gate 2",
                    Description = "Nihil - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Encounter_4_(AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Encounter_4_(AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Nihil Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 99,
                    LocationId = "Nihil Anguish Gate 3",
                    Description = "Nihil - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Encounter_6_(AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Encounter_6_(AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Nihil Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 100,
                    LocationId = "Nihil Anguish Gate 4",
                    Description = "Nihil - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Encounter_7_(AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Encounter_7_(AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Nihil Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 101,
                    LocationId = "Nihil Next Multiplier 1",
                    Description = "Nihil - Next Multiplier in Arena 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Nihil Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 102,
                    LocationId = "Nihil Next Multiplier 2",
                    Description = "Nihil - Next Multiplier between Arena 2 & 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Nihil Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 103,
                    LocationId = "Nihil Next Multiplier 3",
                    Description = "Nihil - Next Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Nihil Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 104,
                    LocationId = "Nihil Next Multiplier 4",
                    Description = "Nihil - Next Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (3)/",
                }
            },
            {
                "Nihil Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 105,
                    LocationId = "Nihil Max Multiplier 1",
                    Description = "Nihil - Max Multiplier in Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Nihil Boss Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 106,
                    LocationId = "Nihil Boss Next Multiplier 1",
                    Description = "Nihil - Next Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Nihil Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 107,
                    LocationId = "Nihil Secret Max Multiplier",
                    Description = "Nihil - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickups/MultiplierBoostMaxTier_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Nihil Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 108,
                    LocationId = "Nihil Coat of Arms Lamb",
                    Description = "Nihil - Coat of Arms between Arena 2 & 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Nihil Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 109,
                    LocationId = "Nihil Coat of Arms Goat",
                    Description = "Nihil - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Nihil Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 110,
                    LocationId = "Nihil Coat of Arms Beast",
                    Description = "Nihil - Coat of Arms before the boss fight",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Nihil Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 111,
                    LocationId = "Nihil Coat of Arms Archdevil",
                    Description = "Nihil - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Nihil Completion",
                new Location
                {
                    ArchipelagoId = 112,
                    LocationId = "Nihil Completion",
                    Description = "Nihil - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Explosive Slaughter Unlock",
                new Location
                {
                    ArchipelagoId = 113,
                    LocationId = "Explosive Slaughter Unlock",
                    Description = "Nihil - Boon Completion",
                    OriginalItemName = "Explosive Slaughter",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 114,
                    LocationId = "Acheron Anguish Gate 1",
                    Description = "Acheron - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Wave_Start_Bridge (AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Wave_Start_Bridge (AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Acheron Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 115,
                    LocationId = "Acheron Anguish Gate 2",
                    Description = "Acheron - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Wave_Start_Tunnel_Mid (AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Wave_Start_Tunnel_Mid (AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Acheron Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 116,
                    LocationId = "Acheron Anguish Gate 3",
                    Description = "Acheron - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Wave_Start_Building (AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Wave_Start_Building (AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Acheron Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 117,
                    LocationId = "Acheron Anguish Gate 4",
                    Description = "Acheron - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "EnemyEncounters/Wave_Start_Cargo_Hall (AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "EnemyEncounters/Wave_Start_Cargo_Hall (AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Acheron Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 118,
                    LocationId = "Acheron Next Multiplier 1",
                    Description = "Acheron - Next Multiplier before Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 119,
                    LocationId = "Acheron Next Multiplier 2",
                    Description = "Acheron - Next Multiplier in Arena 1 front left Containers",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Acheron Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 120,
                    LocationId = "Acheron Next Multiplier 3",
                    Description = "Acheron - Next Multiplier in Arena 1 top Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Acheron Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 121,
                    LocationId = "Acheron Next Multiplier 4",
                    Description = "Acheron - Next Multiplier in Arena 1 bottom Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (3)/",
                }
            },
            {
                "Acheron Next Multiplier 5",
                new Location
                {
                    ArchipelagoId = 122,
                    LocationId = "Acheron Next Multiplier 5",
                    Description = "Acheron - Next Multiplier in Arena 1 back right Alley",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (4)/",
                }
            },
            {
                "Acheron Next Multiplier 6",
                new Location
                {
                    ArchipelagoId = 123,
                    LocationId = "Acheron Next Multiplier 6",
                    Description = "Acheron - Next Multiplier in Arena 1 back left Containers",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (5)/",
                }
            },
            {
                "Acheron Next Multiplier 7",
                new Location
                {
                    ArchipelagoId = 124,
                    LocationId = "Acheron Next Multiplier 7",
                    Description = "Acheron - Next Multiplier in Arena 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (6)/",
                }
            },
            {
                "Acheron Next Multiplier 8",
                new Location
                {
                    ArchipelagoId = 125,
                    LocationId = "Acheron Next Multiplier 8",
                    Description = "Acheron - Next Multiplier between Arena 2 & 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (7)/",
                }
            },
            {
                "Acheron Next Multiplier 9",
                new Location
                {
                    ArchipelagoId = 126,
                    LocationId = "Acheron Next Multiplier 9",
                    Description = "Acheron - Next Multiplier in Arena 3 front left Corner",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (8)/",
                }
            },
            {
                "Acheron Next Multiplier 10",
                new Location
                {
                    ArchipelagoId = 127,
                    LocationId = "Acheron Next Multiplier 10",
                    Description = "Acheron - Next Multiplier in Arena 3 back Corner",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostNextTier/MultiplierBoostNextTier (9)/",
                }
            },
            {
                "Acheron Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 128,
                    LocationId = "Acheron Max Multiplier 1",
                    Description = "Acheron - Max Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Acheron Boss Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 129,
                    LocationId = "Acheron Boss Next Multiplier 1",
                    Description = "Acheron - Next Multiplier in Boss Arena front Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "Boss_Cogwheels/PF__CogWheel1_Boss/CogWheel01/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Boss Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 130,
                    LocationId = "Acheron Boss Next Multiplier 2",
                    Description = "Acheron - Next Multiplier in Boss Arena first back left Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "Boss_Cogwheels/PF__CogWheel2_Boss/CogWheel02/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Boss Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 131,
                    LocationId = "Acheron Boss Next Multiplier 3",
                    Description = "Acheron - Next Multiplier in Boss Arena second back left Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "Boss_Cogwheels/PF__CogWheel2_Boss/CogWheel02/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Boss Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 132,
                    LocationId = "Acheron Boss Next Multiplier 4",
                    Description = "Acheron - Next Multiplier in Boss Arena first back right Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "Boss_Cogwheels/PF__CogWheel3_Boss/CogWheel03/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Boss Next Multiplier 5",
                new Location
                {
                    ArchipelagoId = 133,
                    LocationId = "Acheron Boss Next Multiplier 5",
                    Description = "Acheron - Next Multiplier in Boss Arena second back right Cog",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "Boss_Cogwheels/PF__CogWheel3_Boss/CogWheel03/MultiplierBoostNextTier/",
                }
            },
            {
                "Acheron Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 134,
                    LocationId = "Acheron Secret Max Multiplier",
                    Description = "Acheron - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierPickups/MultiplierBoost_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Acheron Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 135,
                    LocationId = "Acheron Coat of Arms Lamb",
                    Description = "Acheron - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Acheron Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 136,
                    LocationId = "Acheron Coat of Arms Goat",
                    Description = "Acheron - Coat of Arms between Arena 2 & 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Acheron Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 137,
                    LocationId = "Acheron Coat of Arms Beast",
                    Description = "Acheron - Coat of Arms in Arena 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Acheron Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 138,
                    LocationId = "Acheron Coat of Arms Archdevil",
                    Description = "Acheron - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Acheron Completion",
                new Location
                {
                    ArchipelagoId = 139,
                    LocationId = "Acheron Completion",
                    Description = "Acheron - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Anguish Gate 1",
                new Location
                {
                    ArchipelagoId = 140,
                    LocationId = "Sheol Anguish Gate 1",
                    Description = "Sheol - Finished forced Encounter 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_TheHowlingRamparts (AnguishGate1)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_TheHowlingRamparts (AnguishGate1)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Sheol Anguish Gate 2",
                new Location
                {
                    ArchipelagoId = 141,
                    LocationId = "Sheol Anguish Gate 2",
                    Description = "Sheol - Finished forced Encounter 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_ThePaleCitadelGate (AnguishGate2)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_ThePaleCitadelGate (AnguishGate2)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Sheol Anguish Gate 3",
                new Location
                {
                    ArchipelagoId = 142,
                    LocationId = "Sheol Anguish Gate 3",
                    Description = "Sheol - Finished forced Encounter 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_HallOfDarkness (AnguishGate3)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_HallOfDarkness (AnguishGate3)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Sheol Anguish Gate 4",
                new Location
                {
                    ArchipelagoId = 143,
                    LocationId = "Sheol Anguish Gate 4",
                    Description = "Sheol - Finished forced Encounter 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.AnguishGate,
                    GameObjectName =
                        "Wave_Spawning/Wave_Start_TheEternalClimb (AnguishGate4)/PF_AnguishGate_modular(exit)/",
                    ReferenceGameObjectName =
                        "Wave_Spawning/Wave_Start_TheEternalClimb (AnguishGate4)/PF_AnguishGate_modular(entry)/",
                }
            },
            {
                "Sheol Next Multiplier 1",
                new Location
                {
                    ArchipelagoId = 144,
                    LocationId = "Sheol Next Multiplier 1",
                    Description = "Sheol - Next Multiplier before Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier/",
                }
            },
            {
                "Sheol Next Multiplier 2",
                new Location
                {
                    ArchipelagoId = 145,
                    LocationId = "Sheol Next Multiplier 2",
                    Description = "Sheol - Next Multiplier in Arena 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (1)/",
                }
            },
            {
                "Sheol Next Multiplier 3",
                new Location
                {
                    ArchipelagoId = 146,
                    LocationId = "Sheol Next Multiplier 3",
                    Description = "Sheol - Next Multiplier in Arena 2 front right Corner",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (2)/",
                }
            },
            {
                "Sheol Next Multiplier 4",
                new Location
                {
                    ArchipelagoId = 147,
                    LocationId = "Sheol Next Multiplier 4",
                    Description = "Sheol - Next Multiplier in Arena 2 on back Pillar",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (3)/",
                }
            },
            {
                "Sheol Next Multiplier 5",
                new Location
                {
                    ArchipelagoId = 148,
                    LocationId = "Sheol Next Multiplier 5",
                    Description = "Sheol - Next Multiplier in Arena 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.NextMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostNextTier/MultiplierBoostNextTier (4)/",
                }
            },
            {
                "Sheol Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 149,
                    LocationId = "Sheol Max Multiplier 1",
                    Description = "Sheol - Max Multiplier in Arena 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Sheol Boss Max Multiplier 1",
                new Location
                {
                    ArchipelagoId = 150,
                    LocationId = "Sheol Boss Max Multiplier 1",
                    Description = "Sheol - Max Multiplier in Boss Arena",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.MaxMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoostMaxTier/MultiplierBoostMaxTier/",
                }
            },
            {
                "Sheol Secret Max Multiplier",
                new Location
                {
                    ArchipelagoId = 151,
                    LocationId = "Sheol Secret Max Multiplier",
                    Description = "Sheol - Secret Max Multiplier",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.SecretMultiplier,
                    GameObjectName =
                        "MultiplierBoostPickup_Groups/MultiplierBoost_Secret/MultiplierBoostMaxTier/",
                }
            },
            {
                "Sheol Coat of Arms Lamb",
                new Location
                {
                    ArchipelagoId = 152,
                    LocationId = "Sheol Coat of Arms Lamb",
                    Description = "Sheol - Coat of Arms before Arena 1",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Easy/Coat_Of_Arms_Collectible_Easy/",
                }
            },
            {
                "Sheol Coat of Arms Goat",
                new Location
                {
                    ArchipelagoId = 153,
                    LocationId = "Sheol Coat of Arms Goat",
                    Description = "Sheol - Coat of Arms between Arena 1 & 2",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Medium/Coat_Of_Arms_Collectible_Medium/",
                }
            },
            {
                "Sheol Coat of Arms Beast",
                new Location
                {
                    ArchipelagoId = 154,
                    LocationId = "Sheol Coat of Arms Beast",
                    Description = "Sheol - Coat of Arms between Arena 2 & 3",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/Hard/Coat_Of_Arms_Collectible_Hard/",
                }
            },
            {
                "Sheol Coat of Arms Archdevil",
                new Location
                {
                    ArchipelagoId = 155,
                    LocationId = "Sheol Coat of Arms Archdevil",
                    Description = "Sheol - Coat of Arms between Arena 3 & 4",
                    OriginalItemName = "Coat of Arms",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.CoatOfArms,
                    GameObjectName =
                        "Coat_Of_Arms_Collectibles/VeryHard/Coat_Of_Arms_Collectible_VeryHard/",
                }
            },
            {
                "Sheol Completion",
                new Location
                {
                    ArchipelagoId = 156,
                    LocationId = "Sheol Completion",
                    Description = "Sheol - Completed the Hell",
                    OriginalItemName = "Progressive Hells",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Paz Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 157,
                    LocationId = "Paz Weapon Skin Unlock",
                    Description = "Collect 2 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "Terminus Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 158,
                    LocationId = "Terminus Weapon Skin Unlock",
                    Description = "Collect 8 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "Persephone Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 159,
                    LocationId = "Persephone Weapon Skin Unlock",
                    Description = "Collect 14 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "The Hounds Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 160,
                    LocationId = "The Hounds Weapon Skin Unlock",
                    Description = "Collect 20 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "Vulcan Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 161,
                    LocationId = "Vulcan Weapon Skin Unlock",
                    Description = "Collect 26 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "Hellcrow Weapon Skin Unlock",
                new Location
                {
                    ArchipelagoId = 162,
                    LocationId = "Hellcrow Weapon Skin Unlock",
                    Description = "Collect 32 Coat of Arms",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.WeaponSkin,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 163,
                    LocationId = "Killing with Rhythm: 1 Bronze",
                    Description = "Killing with Rhythm: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 1 Silver",
                new Location
                {
                    ArchipelagoId = 164,
                    LocationId = "Killing with Rhythm: 1 Silver",
                    Description = "Killing with Rhythm: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 1 Gold",
                new Location
                {
                    ArchipelagoId = 165,
                    LocationId = "Killing with Rhythm: 1 Gold",
                    Description = "Killing with Rhythm: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 1 Completion",
                new Location
                {
                    ArchipelagoId = 166,
                    LocationId = "Killing with Rhythm: 1 Completion",
                    Description = "Killing with Rhythm: 1 - Completion",
                    OriginalItemName = "Progressive Streak Guardian",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 167,
                    LocationId = "Killing with Rhythm: 2 Bronze",
                    Description = "Killing with Rhythm: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 2 Silver",
                new Location
                {
                    ArchipelagoId = 168,
                    LocationId = "Killing with Rhythm: 2 Silver",
                    Description = "Killing with Rhythm: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 2 Gold",
                new Location
                {
                    ArchipelagoId = 169,
                    LocationId = "Killing with Rhythm: 2 Gold",
                    Description = "Killing with Rhythm: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 2 Completion",
                new Location
                {
                    ArchipelagoId = 170,
                    LocationId = "Killing with Rhythm: 2 Completion",
                    Description = "Killing with Rhythm: 2 - Completion",
                    OriginalItemName = "Progressive Streak Guardian",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 171,
                    LocationId = "Killing with Rhythm: 3 Bronze",
                    Description = "Killing with Rhythm: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 3 Silver",
                new Location
                {
                    ArchipelagoId = 172,
                    LocationId = "Killing with Rhythm: 3 Silver",
                    Description = "Killing with Rhythm: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 3 Gold",
                new Location
                {
                    ArchipelagoId = 173,
                    LocationId = "Killing with Rhythm: 3 Gold",
                    Description = "Killing with Rhythm: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Killing with Rhythm: 3 Completion",
                new Location
                {
                    ArchipelagoId = 174,
                    LocationId = "Killing with Rhythm: 3 Completion",
                    Description = "Killing with Rhythm: 3 - Completion",
                    OriginalItemName = "Progressive Streak Guardian",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 175,
                    LocationId = "Giantslayer: 1 Bronze",
                    Description = "Giantslayer: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 1 Silver",
                new Location
                {
                    ArchipelagoId = 176,
                    LocationId = "Giantslayer: 1 Silver",
                    Description = "Giantslayer: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 1 Gold",
                new Location
                {
                    ArchipelagoId = 177,
                    LocationId = "Giantslayer: 1 Gold",
                    Description = "Giantslayer: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 1 Completion",
                new Location
                {
                    ArchipelagoId = 178,
                    LocationId = "Giantslayer: 1 Completion",
                    Description = "Giantslayer: 1 - Completion",
                    OriginalItemName = "Progressive Unyielding Fury",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 179,
                    LocationId = "Giantslayer: 2 Bronze",
                    Description = "Giantslayer: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 2 Silver",
                new Location
                {
                    ArchipelagoId = 180,
                    LocationId = "Giantslayer: 2 Silver",
                    Description = "Giantslayer: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 2 Gold",
                new Location
                {
                    ArchipelagoId = 181,
                    LocationId = "Giantslayer: 2 Gold",
                    Description = "Giantslayer: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 2 Completion",
                new Location
                {
                    ArchipelagoId = 182,
                    LocationId = "Giantslayer: 2 Completion",
                    Description = "Giantslayer: 2 - Completion",
                    OriginalItemName = "Progressive Unyielding Fury",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 183,
                    LocationId = "Giantslayer: 3 Bronze",
                    Description = "Giantslayer: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 3 Silver",
                new Location
                {
                    ArchipelagoId = 184,
                    LocationId = "Giantslayer: 3 Silver",
                    Description = "Giantslayer: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 3 Gold",
                new Location
                {
                    ArchipelagoId = 185,
                    LocationId = "Giantslayer: 3 Gold",
                    Description = "Giantslayer: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Giantslayer: 3 Completion",
                new Location
                {
                    ArchipelagoId = 186,
                    LocationId = "Giantslayer: 3 Completion",
                    Description = "Giantslayer: 3 - Completion",
                    OriginalItemName = "Progressive Unyielding Fury",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 187,
                    LocationId = "Ultimate Mastery: 1 Bronze",
                    Description = "Ultimate Mastery: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 1 Silver",
                new Location
                {
                    ArchipelagoId = 188,
                    LocationId = "Ultimate Mastery: 1 Silver",
                    Description = "Ultimate Mastery: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 1 Gold",
                new Location
                {
                    ArchipelagoId = 189,
                    LocationId = "Ultimate Mastery: 1 Gold",
                    Description = "Ultimate Mastery: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 1 Completion",
                new Location
                {
                    ArchipelagoId = 190,
                    LocationId = "Ultimate Mastery: 1 Completion",
                    Description = "Ultimate Mastery: 1 - Completion",
                    OriginalItemName = "Progressive Ultimate Sovereignty",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 191,
                    LocationId = "Ultimate Mastery: 2 Bronze",
                    Description = "Ultimate Mastery: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 2 Silver",
                new Location
                {
                    ArchipelagoId = 192,
                    LocationId = "Ultimate Mastery: 2 Silver",
                    Description = "Ultimate Mastery: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 2 Gold",
                new Location
                {
                    ArchipelagoId = 193,
                    LocationId = "Ultimate Mastery: 2 Gold",
                    Description = "Ultimate Mastery: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 2 Completion",
                new Location
                {
                    ArchipelagoId = 194,
                    LocationId = "Ultimate Mastery: 2 Completion",
                    Description = "Ultimate Mastery: 2 - Completion",
                    OriginalItemName = "Progressive Ultimate Sovereignty",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 195,
                    LocationId = "Ultimate Mastery: 3 Bronze",
                    Description = "Ultimate Mastery: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 3 Silver",
                new Location
                {
                    ArchipelagoId = 196,
                    LocationId = "Ultimate Mastery: 3 Silver",
                    Description = "Ultimate Mastery: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 3 Gold",
                new Location
                {
                    ArchipelagoId = 197,
                    LocationId = "Ultimate Mastery: 3 Gold",
                    Description = "Ultimate Mastery: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Ultimate Mastery: 3 Completion",
                new Location
                {
                    ArchipelagoId = 198,
                    LocationId = "Ultimate Mastery: 3 Completion",
                    Description = "Ultimate Mastery: 3 - Completion",
                    OriginalItemName = "Progressive Ultimate Sovereignty",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 199,
                    LocationId = "Slaughter Mastery: 1 Bronze",
                    Description = "Slaughter Mastery: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 1 Silver",
                new Location
                {
                    ArchipelagoId = 200,
                    LocationId = "Slaughter Mastery: 1 Silver",
                    Description = "Slaughter Mastery: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 1 Gold",
                new Location
                {
                    ArchipelagoId = 201,
                    LocationId = "Slaughter Mastery: 1 Gold",
                    Description = "Slaughter Mastery: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 1 Completion",
                new Location
                {
                    ArchipelagoId = 202,
                    LocationId = "Slaughter Mastery: 1 Completion",
                    Description = "Slaughter Mastery: 1 - Completion",
                    OriginalItemName = "Progressive The Perfectionist",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 203,
                    LocationId = "Slaughter Mastery: 2 Bronze",
                    Description = "Slaughter Mastery: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 2 Silver",
                new Location
                {
                    ArchipelagoId = 204,
                    LocationId = "Slaughter Mastery: 2 Silver",
                    Description = "Slaughter Mastery: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 2 Gold",
                new Location
                {
                    ArchipelagoId = 205,
                    LocationId = "Slaughter Mastery: 2 Gold",
                    Description = "Slaughter Mastery: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 2 Completion",
                new Location
                {
                    ArchipelagoId = 206,
                    LocationId = "Slaughter Mastery: 2 Completion",
                    Description = "Slaughter Mastery: 2 - Completion",
                    OriginalItemName = "Progressive The Perfectionist",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 207,
                    LocationId = "Slaughter Mastery: 3 Bronze",
                    Description = "Slaughter Mastery: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 3 Silver",
                new Location
                {
                    ArchipelagoId = 208,
                    LocationId = "Slaughter Mastery: 3 Silver",
                    Description = "Slaughter Mastery: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 3 Gold",
                new Location
                {
                    ArchipelagoId = 209,
                    LocationId = "Slaughter Mastery: 3 Gold",
                    Description = "Slaughter Mastery: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter Mastery: 3 Completion",
                new Location
                {
                    ArchipelagoId = 210,
                    LocationId = "Slaughter Mastery: 3 Completion",
                    Description = "Slaughter Mastery: 3 - Completion",
                    OriginalItemName = "Progressive The Perfectionist",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 211,
                    LocationId = "Relic Thief: 1 Bronze",
                    Description = "Relic Thief: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 1 Silver",
                new Location
                {
                    ArchipelagoId = 212,
                    LocationId = "Relic Thief: 1 Silver",
                    Description = "Relic Thief: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 1 Gold",
                new Location
                {
                    ArchipelagoId = 213,
                    LocationId = "Relic Thief: 1 Gold",
                    Description = "Relic Thief: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 1 Completion",
                new Location
                {
                    ArchipelagoId = 214,
                    LocationId = "Relic Thief: 1 Completion",
                    Description = "Relic Thief: 1 - Completion",
                    OriginalItemName = "Progressive Boon Momentum",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 215,
                    LocationId = "Relic Thief: 2 Bronze",
                    Description = "Relic Thief: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 2 Silver",
                new Location
                {
                    ArchipelagoId = 216,
                    LocationId = "Relic Thief: 2 Silver",
                    Description = "Relic Thief: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 2 Gold",
                new Location
                {
                    ArchipelagoId = 217,
                    LocationId = "Relic Thief: 2 Gold",
                    Description = "Relic Thief: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 2 Completion",
                new Location
                {
                    ArchipelagoId = 218,
                    LocationId = "Relic Thief: 2 Completion",
                    Description = "Relic Thief: 2 - Completion",
                    OriginalItemName = "Progressive Boon Momentum",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 219,
                    LocationId = "Relic Thief: 3 Bronze",
                    Description = "Relic Thief: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 3 Silver",
                new Location
                {
                    ArchipelagoId = 220,
                    LocationId = "Relic Thief: 3 Silver",
                    Description = "Relic Thief: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 3 Gold",
                new Location
                {
                    ArchipelagoId = 221,
                    LocationId = "Relic Thief: 3 Gold",
                    Description = "Relic Thief: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Relic Thief: 3 Completion",
                new Location
                {
                    ArchipelagoId = 222,
                    LocationId = "Relic Thief: 3 Completion",
                    Description = "Relic Thief: 3 - Completion",
                    OriginalItemName = "Progressive Boon Momentum",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 223,
                    LocationId = "Weapon Trickery: 1 Bronze",
                    Description = "Weapon Trickery: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 1 Silver",
                new Location
                {
                    ArchipelagoId = 224,
                    LocationId = "Weapon Trickery: 1 Silver",
                    Description = "Weapon Trickery: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 1 Gold",
                new Location
                {
                    ArchipelagoId = 225,
                    LocationId = "Weapon Trickery: 1 Gold",
                    Description = "Weapon Trickery: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 1 Completion",
                new Location
                {
                    ArchipelagoId = 226,
                    LocationId = "Weapon Trickery: 1 Completion",
                    Description = "Weapon Trickery: 1 - Completion",
                    OriginalItemName = "Progressive Ghost Rounds",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 227,
                    LocationId = "Weapon Trickery: 2 Bronze",
                    Description = "Weapon Trickery: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 2 Silver",
                new Location
                {
                    ArchipelagoId = 228,
                    LocationId = "Weapon Trickery: 2 Silver",
                    Description = "Weapon Trickery: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 2 Gold",
                new Location
                {
                    ArchipelagoId = 229,
                    LocationId = "Weapon Trickery: 2 Gold",
                    Description = "Weapon Trickery: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 2 Completion",
                new Location
                {
                    ArchipelagoId = 230,
                    LocationId = "Weapon Trickery: 2 Completion",
                    Description = "Weapon Trickery: 2 - Completion",
                    OriginalItemName = "Progressive Ghost Rounds",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 231,
                    LocationId = "Weapon Trickery: 3 Bronze",
                    Description = "Weapon Trickery: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 3 Silver",
                new Location
                {
                    ArchipelagoId = 232,
                    LocationId = "Weapon Trickery: 3 Silver",
                    Description = "Weapon Trickery: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 3 Gold",
                new Location
                {
                    ArchipelagoId = 233,
                    LocationId = "Weapon Trickery: 3 Gold",
                    Description = "Weapon Trickery: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Weapon Trickery: 3 Completion",
                new Location
                {
                    ArchipelagoId = 234,
                    LocationId = "Weapon Trickery: 3 Completion",
                    Description = "Weapon Trickery: 3 - Completion",
                    OriginalItemName = "Progressive Ghost Rounds",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 1 Bronze",
                new Location
                {
                    ArchipelagoId = 235,
                    LocationId = "Death's Edge: 1 Bronze",
                    Description = "Death's Edge: 1 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 1 Silver",
                new Location
                {
                    ArchipelagoId = 236,
                    LocationId = "Death's Edge: 1 Silver",
                    Description = "Death's Edge: 1 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 1 Gold",
                new Location
                {
                    ArchipelagoId = 237,
                    LocationId = "Death's Edge: 1 Gold",
                    Description = "Death's Edge: 1 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 1 Completion",
                new Location
                {
                    ArchipelagoId = 238,
                    LocationId = "Death's Edge: 1 Completion",
                    Description = "Death's Edge: 1 - Completion",
                    OriginalItemName = "Progressive Last Breath Aegis",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 2 Bronze",
                new Location
                {
                    ArchipelagoId = 239,
                    LocationId = "Death's Edge: 2 Bronze",
                    Description = "Death's Edge: 2 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 2 Silver",
                new Location
                {
                    ArchipelagoId = 240,
                    LocationId = "Death's Edge: 2 Silver",
                    Description = "Death's Edge: 2 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 2 Gold",
                new Location
                {
                    ArchipelagoId = 241,
                    LocationId = "Death's Edge: 2 Gold",
                    Description = "Death's Edge: 2 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 2 Completion",
                new Location
                {
                    ArchipelagoId = 242,
                    LocationId = "Death's Edge: 2 Completion",
                    Description = "Death's Edge: 2 - Completion",
                    OriginalItemName = "Progressive Last Breath Aegis",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 3 Bronze",
                new Location
                {
                    ArchipelagoId = 243,
                    LocationId = "Death's Edge: 3 Bronze",
                    Description = "Death's Edge: 3 - Achieve Bronze",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentBronze,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 3 Silver",
                new Location
                {
                    ArchipelagoId = 244,
                    LocationId = "Death's Edge: 3 Silver",
                    Description = "Death's Edge: 3 - Achieve Silver",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentSilver,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 3 Gold",
                new Location
                {
                    ArchipelagoId = 245,
                    LocationId = "Death's Edge: 3 Gold",
                    Description = "Death's Edge: 3 - Achieve Gold",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentGold,
                    GameObjectName = null,
                }
            },
            {
                "Death's Edge: 3 Completion",
                new Location
                {
                    ArchipelagoId = 246,
                    LocationId = "Death's Edge: 3 Completion",
                    Description = "Death's Edge: 3 - Completion",
                    OriginalItemName = "Progressive Last Breath Aegis",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.TormentCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 247,
                    LocationId = "Voke Ammostash Destruction",
                    Description = "Voke - Complete Ammostash Destruction",
                    OriginalItemName = "Killing with Rhythm: 1",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Speed Challenge",
                new Location
                {
                    ArchipelagoId = 248,
                    LocationId = "Voke Speed Challenge",
                    Description = "Voke - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Voke Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 249,
                    LocationId = "Voke Health Crystal Destruction",
                    Description = "Voke - Complete Health Crystal Destruction",
                    OriginalItemName = "Relic Thief: 1",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 250,
                    LocationId = "Voke Chaos Crystal Destruction",
                    Description = "Voke - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Weapon Trickery: 1",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 251,
                    LocationId = "Stygia Ammostash Destruction",
                    Description = "Stygia - Complete Ammostash Destruction",
                    OriginalItemName = "Giantslayer: 1",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Speed Challenge",
                new Location
                {
                    ArchipelagoId = 252,
                    LocationId = "Stygia Speed Challenge",
                    Description = "Stygia - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 253,
                    LocationId = "Stygia Health Crystal Destruction",
                    Description = "Stygia - Complete Health Crystal Destruction",
                    OriginalItemName = "Relic Thief: 2",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 254,
                    LocationId = "Stygia Chaos Crystal Destruction",
                    Description = "Stygia - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Death's Edge: 1",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 255,
                    LocationId = "Yhelm Ammostash Destruction",
                    Description = "Yhelm - Complete Ammostash Destruction",
                    OriginalItemName = "Ultimate Mastery: 1",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Speed Challenge",
                new Location
                {
                    ArchipelagoId = 256,
                    LocationId = "Yhelm Speed Challenge",
                    Description = "Yhelm - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 257,
                    LocationId = "Yhelm Health Crystal Destruction",
                    Description = "Yhelm - Complete Health Crystal Destruction",
                    OriginalItemName = "Weapon Trickery: 2",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 258,
                    LocationId = "Yhelm Chaos Crystal Destruction",
                    Description = "Yhelm - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Killing with Rhythm: 2",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 259,
                    LocationId = "Incaustis Ammostash Destruction",
                    Description = "Incaustis - Complete Ammostash Destruction",
                    OriginalItemName = "Slaughter Mastery: 1",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Speed Challenge",
                new Location
                {
                    ArchipelagoId = 260,
                    LocationId = "Incaustis Speed Challenge",
                    Description = "Incaustis - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 261,
                    LocationId = "Incaustis Health Crystal Destruction",
                    Description = "Incaustis - Complete Health Crystal Destruction",
                    OriginalItemName = "Relic Thief: 3",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 262,
                    LocationId = "Incaustis Chaos Crystal Destruction",
                    Description = "Incaustis - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Giantslayer: 2",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 263,
                    LocationId = "Gehenna Ammostash Destruction",
                    Description = "Gehenna - Complete Ammostash Destruction",
                    OriginalItemName = "Death's Edge: 2",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Speed Challenge",
                new Location
                {
                    ArchipelagoId = 264,
                    LocationId = "Gehenna Speed Challenge",
                    Description = "Gehenna - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 265,
                    LocationId = "Gehenna Health Crystal Destruction",
                    Description = "Gehenna - Complete Health Crystal Destruction",
                    OriginalItemName = "Weapon Trickery: 3",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 266,
                    LocationId = "Gehenna Chaos Crystal Destruction",
                    Description = "Gehenna - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Killing with Rhythm: 3",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 267,
                    LocationId = "Nihil Ammostash Destruction",
                    Description = "Nihil - Complete Ammostash Destruction",
                    OriginalItemName = "Ultimate Mastery: 2",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Speed Challenge",
                new Location
                {
                    ArchipelagoId = 268,
                    LocationId = "Nihil Speed Challenge",
                    Description = "Nihil - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 269,
                    LocationId = "Nihil Health Crystal Destruction",
                    Description = "Nihil - Complete Health Crystal Destruction",
                    OriginalItemName = "Giantslayer: 3",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 270,
                    LocationId = "Nihil Chaos Crystal Destruction",
                    Description = "Nihil - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Slaughter Mastery: 2",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 271,
                    LocationId = "Acheron Ammostash Destruction",
                    Description = "Acheron - Complete Ammostash Destruction",
                    OriginalItemName = "Death's Edge: 3",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Speed Challenge",
                new Location
                {
                    ArchipelagoId = 272,
                    LocationId = "Acheron Speed Challenge",
                    Description = "Acheron - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 273,
                    LocationId = "Acheron Health Crystal Destruction",
                    Description = "Acheron - Complete Health Crystal Destruction",
                    OriginalItemName = "Slaughter Mastery: 3",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 274,
                    LocationId = "Acheron Chaos Crystal Destruction",
                    Description = "Acheron - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Ultimate Mastery: 3",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Hells's Heartbeat discovered",
                new Location
                {
                    ArchipelagoId = 276,
                    LocationId = "Hells's Heartbeat discovered",
                    Description = "Fury Combo - Hells's Heartbeat discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Basilisk Mode discovered",
                new Location
                {
                    ArchipelagoId = 277,
                    LocationId = "Basilisk Mode discovered",
                    Description = "Fury Combo - Basilisk Mode discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Double Hit and Run discovered",
                new Location
                {
                    ArchipelagoId = 278,
                    LocationId = "Double Hit and Run discovered",
                    Description = "Fury Combo - Double Hit and Run discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Shatter Two discovered",
                new Location
                {
                    ArchipelagoId = 279,
                    LocationId = "Shatter Two discovered",
                    Description = "Fury Combo - Shatter Two discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Devil's Flight discovered",
                new Location
                {
                    ArchipelagoId = 280,
                    LocationId = "Devil's Flight discovered",
                    Description = "Fury Combo - Devil's Flight discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Double Slaughter discovered",
                new Location
                {
                    ArchipelagoId = 281,
                    LocationId = "Double Slaughter discovered",
                    Description = "Fury Combo - Double Slaughter discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Chaos and Slaughter discovered",
                new Location
                {
                    ArchipelagoId = 282,
                    LocationId = "Chaos and Slaughter discovered",
                    Description = "Fury Combo - Chaos and Slaughter discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Unholy Mess discovered",
                new Location
                {
                    ArchipelagoId = 283,
                    LocationId = "Unholy Mess discovered",
                    Description = "Fury Combo - Unholy Mess discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Five Endings discovered",
                new Location
                {
                    ArchipelagoId = 284,
                    LocationId = "Five Endings discovered",
                    Description = "Fury Combo - Five Endings discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Slaughter and Kill discovered",
                new Location
                {
                    ArchipelagoId = 285,
                    LocationId = "Slaughter and Kill discovered",
                    Description = "Fury Combo - Slaughter and Kill discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Chaos Flight discovered",
                new Location
                {
                    ArchipelagoId = 286,
                    LocationId = "Chaos Flight discovered",
                    Description = "Fury Combo - Chaos Flight discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Death from Above discovered",
                new Location
                {
                    ArchipelagoId = 287,
                    LocationId = "Death from Above discovered",
                    Description = "Fury Combo - Death from Above discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Lethal Cycle discovered",
                new Location
                {
                    ArchipelagoId = 288,
                    LocationId = "Lethal Cycle discovered",
                    Description = "Fury Combo - Lethal Cycle discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Kill Trio discovered",
                new Location
                {
                    ArchipelagoId = 289,
                    LocationId = "Kill Trio discovered",
                    Description = "Fury Combo - Kill Trio discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Triple Dash discovered",
                new Location
                {
                    ArchipelagoId = 290,
                    LocationId = "Triple Dash discovered",
                    Description = "Fury Combo - Triple Dash discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "Marionette discovered",
                new Location
                {
                    ArchipelagoId = 291,
                    LocationId = "Marionette discovered",
                    Description = "Tutorial - Marionette discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Cambion discovered",
                new Location
                {
                    ArchipelagoId = 292,
                    LocationId = "Cambion discovered",
                    Description = "Voke/Stygia - Cambion discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Behemoth discovered",
                new Location
                {
                    ArchipelagoId = 293,
                    LocationId = "Behemoth discovered",
                    Description = "Voke - Behemoth discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Stalker discovered",
                new Location
                {
                    ArchipelagoId = 294,
                    LocationId = "Stalker discovered",
                    Description = "Stygia - Stalker discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Eyeless discovered",
                new Location
                {
                    ArchipelagoId = 295,
                    LocationId = "Eyeless discovered",
                    Description = "Yhelm - Eyeless discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Hierophant discovered",
                new Location
                {
                    ArchipelagoId = 296,
                    LocationId = "Hierophant discovered",
                    Description = "Incaustis - Hierophant discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Lesser Seraph discovered",
                new Location
                {
                    ArchipelagoId = 297,
                    LocationId = "Lesser Seraph discovered",
                    Description = "Gehenna - Lesser Seraph discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Shield Cambion discovered",
                new Location
                {
                    ArchipelagoId = 298,
                    LocationId = "Shield Cambion discovered",
                    Description = "Yhelm - Shield Cambion discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Siege Behemoth discovered",
                new Location
                {
                    ArchipelagoId = 299,
                    LocationId = "Siege Behemoth discovered",
                    Description = "Incaustis - Siege Behemoth discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Void Stalker discovered",
                new Location
                {
                    ArchipelagoId = 300,
                    LocationId = "Void Stalker discovered",
                    Description = "Nihil - Void Stalker discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Annihilator Seraph discovered",
                new Location
                {
                    ArchipelagoId = 301,
                    LocationId = "Annihilator Seraph discovered",
                    Description = "Voke (Archdevil) - Annihilator Seraph discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Anger Aspect: Voke discovered",
                new Location
                {
                    ArchipelagoId = 302,
                    LocationId = "Anger Aspect: Voke discovered",
                    Description = "Anger Aspect: Voke discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Charged Aspect: Stygia discovered",
                new Location
                {
                    ArchipelagoId = 303,
                    LocationId = "Charged Aspect: Stygia discovered",
                    Description = "Charged Aspect: Stygia discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Fortress Aspect: Yhelm discovered",
                new Location
                {
                    ArchipelagoId = 304,
                    LocationId = "Fortress Aspect: Yhelm discovered",
                    Description = "Fortress Aspect: Yhelm discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Infernal Fury Aspect: Incaustis discovered",
                new Location
                {
                    ArchipelagoId = 305,
                    LocationId = "Infernal Fury Aspect: Incaustis discovered",
                    Description = "Infernal Fury Aspect: Incaustis discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Hellstorm Aspect: Gehenna discovered",
                new Location
                {
                    ArchipelagoId = 306,
                    LocationId = "Hellstorm Aspect: Gehenna discovered",
                    Description = "Hellstorm Aspect: Gehenna discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Doppelganger Aspect: Nihil discovered",
                new Location
                {
                    ArchipelagoId = 307,
                    LocationId = "Doppelganger Aspect: Nihil discovered",
                    Description = "Doppelganger Aspect: Nihil discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Wheel Aspect: Acheron discovered",
                new Location
                {
                    ArchipelagoId = 308,
                    LocationId = "Wheel Aspect: Acheron discovered",
                    Description = "Wheel Aspect: Acheron discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Red Judge - Worldbreaker: Sheol discovered",
                new Location
                {
                    ArchipelagoId = 309,
                    LocationId = "Red Judge - Worldbreaker: Sheol discovered",
                    Description = "Red Judge - Worldbreaker: Sheol discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "The Lost Unknown: Leviathan discovered",
                new Location
                {
                    ArchipelagoId = 310,
                    LocationId = "The Lost Unknown: Leviathan discovered",
                    Description = "The Lost Unknown: Leviathan discovered",
                    OriginalItemName = "The Lost Unknown: Leviathan discovered",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.Bestiary,
                    GameObjectName = null,
                }
            },
            {
                "Voke - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 320,
                    LocationId = "Voke - Ammostash 1",
                    Description = "Voke - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash/",
                }
            },
            {
                "Voke - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 321,
                    LocationId = "Voke - Ammostash 2",
                    Description = "Voke - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (1)/",
                }
            },
            {
                "Voke - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 322,
                    LocationId = "Voke - Ammostash 3",
                    Description = "Voke - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (2)/",
                }
            },
            {
                "Voke - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 323,
                    LocationId = "Voke - Ammostash 4",
                    Description = "Voke - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (3)/",
                }
            },
            {
                "Voke - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 324,
                    LocationId = "Voke - Ammostash 5",
                    Description = "Voke - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (4)/",
                }
            },
            {
                "Voke - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 325,
                    LocationId = "Voke - Ammostash 6",
                    Description = "Voke - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (5)/",
                }
            },
            {
                "Voke - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 326,
                    LocationId = "Voke - Ammostash 7",
                    Description = "Voke - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (6)/",
                }
            },
            {
                "Voke - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 327,
                    LocationId = "Voke - Ammostash 8",
                    Description = "Voke - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (7)/",
                }
            },
            {
                "Voke - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 328,
                    LocationId = "Voke - Ammostash 9",
                    Description = "Voke - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (8)/",
                }
            },
            {
                "Voke - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 329,
                    LocationId = "Voke - Ammostash 10",
                    Description = "Voke - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (9)/",
                }
            },
            {
                "Voke - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 330,
                    LocationId = "Voke - Ammostash 11",
                    Description = "Voke - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (10)/",
                }
            },
            {
                "Voke - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 331,
                    LocationId = "Voke - Ammostash 12",
                    Description = "Voke - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (11)/",
                }
            },
            {
                "Voke - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 332,
                    LocationId = "Voke - Ammostash 13",
                    Description = "Voke - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (12)/",
                }
            },
            {
                "Voke - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 333,
                    LocationId = "Voke - Ammostash 14",
                    Description = "Voke - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash/",
                }
            },
            {
                "Voke - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 334,
                    LocationId = "Voke - Ammostash 15",
                    Description = "Voke - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (1)/",
                }
            },
            {
                "Voke - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 335,
                    LocationId = "Voke - Ammostash 16",
                    Description = "Voke - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (2)/",
                }
            },
            {
                "Voke - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 336,
                    LocationId = "Voke - Ammostash 17",
                    Description = "Voke - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Ammostashes/PF_Ammostash (3)/",
                }
            },
            {
                "Voke - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 337,
                    LocationId = "Voke - Health Crystal 1",
                    Description = "Voke - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone/",
                }
            },
            {
                "Voke - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 338,
                    LocationId = "Voke - Health Crystal 2",
                    Description = "Voke - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (1)/",
                }
            },
            {
                "Voke - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 339,
                    LocationId = "Voke - Health Crystal 3",
                    Description = "Voke - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (2)/",
                }
            },
            {
                "Voke - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 340,
                    LocationId = "Voke - Health Crystal 4",
                    Description = "Voke - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (3)/",
                }
            },
            {
                "Voke - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 341,
                    LocationId = "Voke - Health Crystal 5",
                    Description = "Voke - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (4)/",
                }
            },
            {
                "Voke - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 342,
                    LocationId = "Voke - Health Crystal 6",
                    Description = "Voke - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (5)/",
                }
            },
            {
                "Voke - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 343,
                    LocationId = "Voke - Health Crystal 7",
                    Description = "Voke - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (6)/",
                }
            },
            {
                "Voke - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 344,
                    LocationId = "Voke - Health Crystal 8",
                    Description = "Voke - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (7)/",
                }
            },
            {
                "Voke - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 345,
                    LocationId = "Voke - Health Crystal 9",
                    Description = "Voke - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (8)/",
                }
            },
            {
                "Voke - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 346,
                    LocationId = "Voke - Health Crystal 10",
                    Description = "Voke - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (9)/",
                }
            },
            {
                "Voke - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 347,
                    LocationId = "Voke - Health Crystal 11",
                    Description = "Voke - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (10)/",
                }
            },
            {
                "Voke - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 348,
                    LocationId = "Voke - Health Crystal 12",
                    Description = "Voke - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (11)/",
                }
            },
            {
                "Voke - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 349,
                    LocationId = "Voke - Health Crystal 13",
                    Description = "Voke - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (12)/",
                }
            },
            {
                "Voke - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 350,
                    LocationId = "Voke - Health Crystal 14",
                    Description = "Voke - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (13)/",
                }
            },
            {
                "Voke - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 351,
                    LocationId = "Voke - Health Crystal 15",
                    Description = "Voke - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone/",
                }
            },
            {
                "Voke - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 352,
                    LocationId = "Voke - Health Crystal 16",
                    Description = "Voke - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (1)/",
                }
            },
            {
                "Voke - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 353,
                    LocationId = "Voke - Health Crystal 17",
                    Description = "Voke - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (2)/",
                }
            },
            {
                "Voke - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 354,
                    LocationId = "Voke - Health Crystal 18",
                    Description = "Voke - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (3)/",
                }
            },
            {
                "Voke - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 355,
                    LocationId = "Voke - Chaos Crystal 1",
                    Description = "Voke - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Voke - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 356,
                    LocationId = "Voke - Chaos Crystal 2",
                    Description = "Voke - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Voke - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 357,
                    LocationId = "Voke - Chaos Crystal 3",
                    Description = "Voke - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Voke - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 358,
                    LocationId = "Voke - Chaos Crystal 4",
                    Description = "Voke - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Voke - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 359,
                    LocationId = "Voke - Chaos Crystal 5",
                    Description = "Voke - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Voke - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 360,
                    LocationId = "Voke - Chaos Crystal 6",
                    Description = "Voke - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Voke - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 361,
                    LocationId = "Voke - Chaos Crystal 7",
                    Description = "Voke - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Voke - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 362,
                    LocationId = "Voke - Chaos Crystal 8",
                    Description = "Voke - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Voke - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 363,
                    LocationId = "Voke - Chaos Crystal 9",
                    Description = "Voke - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Voke - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 364,
                    LocationId = "Voke - Chaos Crystal 10",
                    Description = "Voke - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Killing with Rhythm: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 365,
                    LocationId = "Killing with Rhythm: 1 - Health Crystal 1",
                    Description = "Killing with Rhythm: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Killing with Rhythm: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 366,
                    LocationId = "Killing with Rhythm: 1 - Health Crystal 2",
                    Description = "Killing with Rhythm: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 367,
                    LocationId = "Weapon Trickery: 1 - Health Crystal 1",
                    Description = "Weapon Trickery: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Weapon Trickery: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 368,
                    LocationId = "Weapon Trickery: 1 - Health Crystal 2",
                    Description = "Weapon Trickery: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 1 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 369,
                    LocationId = "Weapon Trickery: 1 - Chaos Crystal 1",
                    Description = "Weapon Trickery: 1 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Relic Thief: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 370,
                    LocationId = "Relic Thief: 1 - Health Crystal 1",
                    Description = "Relic Thief: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Relic Thief: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 371,
                    LocationId = "Relic Thief: 1 - Health Crystal 2",
                    Description = "Relic Thief: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Relic Thief: 1 - Challenge Pickup 1",
                new Location
                {
                    ArchipelagoId = 372,
                    LocationId = "Relic Thief: 1 - Challenge Pickup 1",
                    Description = "Relic Thief: 1 - Challenge Pickup 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup/",
                }
            },
            {
                "Relic Thief: 1 - Challenge Pickup 2",
                new Location
                {
                    ArchipelagoId = 373,
                    LocationId = "Relic Thief: 1 - Challenge Pickup 2",
                    Description = "Relic Thief: 1 - Challenge Pickup 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (1)/",
                }
            },
            {
                "Relic Thief: 1 - Challenge Pickup 3",
                new Location
                {
                    ArchipelagoId = 374,
                    LocationId = "Relic Thief: 1 - Challenge Pickup 3",
                    Description = "Relic Thief: 1 - Challenge Pickup 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (2)/",
                }
            },
            {
                "Stygia - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 375,
                    LocationId = "Stygia - Ammostash 1",
                    Description = "Stygia - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Start/PF_AmmoStash/",
                }
            },
            {
                "Stygia - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 376,
                    LocationId = "Stygia - Ammostash 2",
                    Description = "Stygia - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Start/PF_AmmoStash (1)/",
                }
            },
            {
                "Stygia - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 377,
                    LocationId = "Stygia - Ammostash 3",
                    Description = "Stygia - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerTransition/PF_AmmoStash (2)/",
                }
            },
            {
                "Stygia - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 378,
                    LocationId = "Stygia - Ammostash 4",
                    Description = "Stygia - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerTransition/PF_AmmoStash (3)/",
                }
            },
            {
                "Stygia - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 379,
                    LocationId = "Stygia - Ammostash 5",
                    Description = "Stygia - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerView/PF_AmmoStash (4)/",
                }
            },
            {
                "Stygia - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 380,
                    LocationId = "Stygia - Ammostash 6",
                    Description = "Stygia - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerView/PF_AmmoStash (5)/",
                }
            },
            {
                "Stygia - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 381,
                    LocationId = "Stygia - Ammostash 7",
                    Description = "Stygia - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerView/PF_AmmoStash (6)/",
                }
            },
            {
                "Stygia - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 382,
                    LocationId = "Stygia - Ammostash 8",
                    Description = "Stygia - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/TowerView/PF_AmmoStash (7)/",
                }
            },
            {
                "Stygia - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 383,
                    LocationId = "Stygia - Ammostash 9",
                    Description = "Stygia - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Altar/PF_AmmoStash (8)/",
                }
            },
            {
                "Stygia - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 384,
                    LocationId = "Stygia - Ammostash 10",
                    Description = "Stygia - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Altar/PF_AmmoStash (9)/",
                }
            },
            {
                "Stygia - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 385,
                    LocationId = "Stygia - Ammostash 11",
                    Description = "Stygia - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Altar/PF_AmmoStash (10)/",
                }
            },
            {
                "Stygia - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 386,
                    LocationId = "Stygia - Ammostash 12",
                    Description = "Stygia - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/Altar/PF_AmmoStash (11)/",
                }
            },
            {
                "Stygia - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 387,
                    LocationId = "Stygia - Ammostash 13",
                    Description = "Stygia - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/FloatingDebris/PF_AmmoStash (12)/",
                }
            },
            {
                "Stygia - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 388,
                    LocationId = "Stygia - Ammostash 14",
                    Description = "Stygia - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/FloatingDebris/PF_AmmoStash (13)/",
                }
            },
            {
                "Stygia - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 389,
                    LocationId = "Stygia - Ammostash 15",
                    Description = "Stygia - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/FloatingDebris/PF_AmmoStash (14)/",
                }
            },
            {
                "Stygia - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 390,
                    LocationId = "Stygia - Ammostash 16",
                    Description = "Stygia - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/ThreeStairs/PF_AmmoStash (15)/",
                }
            },
            {
                "Stygia - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 391,
                    LocationId = "Stygia - Ammostash 17",
                    Description = "Stygia - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/ThreeStairs/PF_AmmoStash (16)/",
                }
            },
            {
                "Stygia - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 392,
                    LocationId = "Stygia - Ammostash 18",
                    Description = "Stygia - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/ThreeStairs/PF_AmmoStash (17)/",
                }
            },
            {
                "Stygia - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 393,
                    LocationId = "Stygia - Ammostash 19",
                    Description = "Stygia - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/ThreeStairs/PF_AmmoStash (18)/",
                }
            },
            {
                "Stygia - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 394,
                    LocationId = "Stygia - Ammostash 20",
                    Description = "Stygia - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes /PF_Ammostash/",
                }
            },
            {
                "Stygia - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 395,
                    LocationId = "Stygia - Ammostash 21",
                    Description = "Stygia - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes /PF_Ammostash (1)/",
                }
            },
            {
                "Stygia - Ammostash 22",
                new Location
                {
                    ArchipelagoId = 396,
                    LocationId = "Stygia - Ammostash 22",
                    Description = "Stygia - Ammostash 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes /PF_Ammostash (2)/",
                }
            },
            {
                "Stygia - Ammostash 23",
                new Location
                {
                    ArchipelagoId = 397,
                    LocationId = "Stygia - Ammostash 23",
                    Description = "Stygia - Ammostash 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes /PF_Ammostash (3)/",
                }
            },
            {
                "Stygia - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 398,
                    LocationId = "Stygia - Health Crystal 1",
                    Description = "Stygia - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/Start/PF_HealthStone/",
                }
            },
            {
                "Stygia - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 399,
                    LocationId = "Stygia - Health Crystal 2",
                    Description = "Stygia - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/Start/PF_HealthStone (1)/",
                }
            },
            {
                "Stygia - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 400,
                    LocationId = "Stygia - Health Crystal 3",
                    Description = "Stygia - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/TowerTransition/PF_HealthStone (2)/",
                }
            },
            {
                "Stygia - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 401,
                    LocationId = "Stygia - Health Crystal 4",
                    Description = "Stygia - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/TowerView/PF_HealthStone (3)/",
                }
            },
            {
                "Stygia - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 402,
                    LocationId = "Stygia - Health Crystal 5",
                    Description = "Stygia - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/TowerView/PF_HealthStone (4)/",
                }
            },
            {
                "Stygia - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 403,
                    LocationId = "Stygia - Health Crystal 6",
                    Description = "Stygia - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/TowerView/PF_HealthStone (5)/",
                }
            },
            {
                "Stygia - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 404,
                    LocationId = "Stygia - Health Crystal 7",
                    Description = "Stygia - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/Altar/PF_HealthStone (6)/",
                }
            },
            {
                "Stygia - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 405,
                    LocationId = "Stygia - Health Crystal 8",
                    Description = "Stygia - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/Altar/PF_HealthStone (7)/",
                }
            },
            {
                "Stygia - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 406,
                    LocationId = "Stygia - Health Crystal 9",
                    Description = "Stygia - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/Altar/PF_HealthStone (8)/",
                }
            },
            {
                "Stygia - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 407,
                    LocationId = "Stygia - Health Crystal 10",
                    Description = "Stygia - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/FloatingDebris/PF_HealthStone (9)/",
                }
            },
            {
                "Stygia - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 408,
                    LocationId = "Stygia - Health Crystal 11",
                    Description = "Stygia - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/FloatingDebris/PF_HealthStone (10)/",
                }
            },
            {
                "Stygia - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 409,
                    LocationId = "Stygia - Health Crystal 12",
                    Description = "Stygia - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/ThreeStairs/PF_HealthStone (11)/",
                }
            },
            {
                "Stygia - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 410,
                    LocationId = "Stygia - Health Crystal 13",
                    Description = "Stygia - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/ThreeStairs/PF_HealthStone (12)/",
                }
            },
            {
                "Stygia - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 411,
                    LocationId = "Stygia - Health Crystal 14",
                    Description = "Stygia - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/ThreeStairs/PF_HealthStone (13)/",
                }
            },
            {
                "Stygia - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 412,
                    LocationId = "Stygia - Health Crystal 15",
                    Description = "Stygia - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Stygia - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 413,
                    LocationId = "Stygia - Health Crystal 16",
                    Description = "Stygia - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Stygia - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 414,
                    LocationId = "Stygia - Health Crystal 17",
                    Description = "Stygia - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Stygia - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 415,
                    LocationId = "Stygia - Health Crystal 18",
                    Description = "Stygia - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Stygia - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 416,
                    LocationId = "Stygia - Health Crystal 19",
                    Description = "Stygia - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (4)/",
                }
            },
            {
                "Stygia - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 417,
                    LocationId = "Stygia - Chaos Crystal 1",
                    Description = "Stygia - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/TowerView/PF_SoulStoneBeat/",
                }
            },
            {
                "Stygia - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 418,
                    LocationId = "Stygia - Chaos Crystal 2",
                    Description = "Stygia - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/TowerView/PF_SoulStoneBeat (1)/",
                }
            },
            {
                "Stygia - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 419,
                    LocationId = "Stygia - Chaos Crystal 3",
                    Description = "Stygia - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/TowerView/PF_SoulStoneBeat (2)/",
                }
            },
            {
                "Stygia - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 420,
                    LocationId = "Stygia - Chaos Crystal 4",
                    Description = "Stygia - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/TowerView/PF_SoulStoneBeat (3)/",
                }
            },
            {
                "Stygia - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 421,
                    LocationId = "Stygia - Chaos Crystal 5",
                    Description = "Stygia - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/TowerView/PF_SoulStoneBeat (4)/",
                }
            },
            {
                "Stygia - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 422,
                    LocationId = "Stygia - Chaos Crystal 6",
                    Description = "Stygia - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/Altar/PF_SoulStoneBeat (5)/",
                }
            },
            {
                "Stygia - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 423,
                    LocationId = "Stygia - Chaos Crystal 7",
                    Description = "Stygia - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/Altar/PF_SoulStoneBeat (6)/",
                }
            },
            {
                "Stygia - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 424,
                    LocationId = "Stygia - Chaos Crystal 8",
                    Description = "Stygia - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/FloatingDebris/PF_SoulStoneBeat (7)/",
                }
            },
            {
                "Stygia - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 425,
                    LocationId = "Stygia - Chaos Crystal 9",
                    Description = "Stygia - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/ThreeStairs/PF_SoulStoneBeat (8)/",
                }
            },
            {
                "Stygia - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 426,
                    LocationId = "Stygia - Chaos Crystal 10",
                    Description = "Stygia - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/ThreeStairs/PF_SoulStoneBeat (9)/",
                }
            },
            {
                "Stygia - Chaos Crystal 11",
                new Location
                {
                    ArchipelagoId = 427,
                    LocationId = "Stygia - Chaos Crystal 11",
                    Description = "Stygia - Chaos Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/ThreeStairs/PF_SoulStoneBeat (10)/",
                }
            },
            {
                "Giantslayer: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 428,
                    LocationId = "Giantslayer: 1 - Health Crystal 1",
                    Description = "Giantslayer: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Giantslayer: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 429,
                    LocationId = "Giantslayer: 1 - Health Crystal 2",
                    Description = "Giantslayer: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Giantslayer: 1 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 430,
                    LocationId = "Giantslayer: 1 - Health Crystal 3",
                    Description = "Giantslayer: 1 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Giantslayer: 1 - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 431,
                    LocationId = "Giantslayer: 1 - Health Crystal 4",
                    Description = "Giantslayer: 1 - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Giantslayer: 1 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 432,
                    LocationId = "Giantslayer: 1 - Chaos Crystal 1",
                    Description = "Giantslayer: 1 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Giantslayer: 1 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 433,
                    LocationId = "Giantslayer: 1 - Chaos Crystal 2",
                    Description = "Giantslayer: 1 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Giantslayer: 1 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 434,
                    LocationId = "Giantslayer: 1 - Chaos Crystal 3",
                    Description = "Giantslayer: 1 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Giantslayer: 1 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 435,
                    LocationId = "Giantslayer: 1 - Chaos Crystal 4",
                    Description = "Giantslayer: 1 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 436,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 1",
                    Description = "Death's Edge: 1 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 437,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 2",
                    Description = "Death's Edge: 1 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 438,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 3",
                    Description = "Death's Edge: 1 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 439,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 4",
                    Description = "Death's Edge: 1 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 440,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 5",
                    Description = "Death's Edge: 1 - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 441,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 6",
                    Description = "Death's Edge: 1 - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 442,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 7",
                    Description = "Death's Edge: 1 - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Death's Edge: 1 - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 443,
                    LocationId = "Death's Edge: 1 - Chaos Crystal 8",
                    Description = "Death's Edge: 1 - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Relic Thief: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 444,
                    LocationId = "Relic Thief: 2 - Health Crystal 1",
                    Description = "Relic Thief: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Relic Thief: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 445,
                    LocationId = "Relic Thief: 2 - Health Crystal 2",
                    Description = "Relic Thief: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Relic Thief: 2 - Challenge Pickup 1",
                new Location
                {
                    ArchipelagoId = 446,
                    LocationId = "Relic Thief: 2 - Challenge Pickup 1",
                    Description = "Relic Thief: 2 - Challenge Pickup 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup/",
                }
            },
            {
                "Relic Thief: 2 - Challenge Pickup 2",
                new Location
                {
                    ArchipelagoId = 447,
                    LocationId = "Relic Thief: 2 - Challenge Pickup 2",
                    Description = "Relic Thief: 2 - Challenge Pickup 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (1)/",
                }
            },
            {
                "Relic Thief: 2 - Challenge Pickup 3",
                new Location
                {
                    ArchipelagoId = 448,
                    LocationId = "Relic Thief: 2 - Challenge Pickup 3",
                    Description = "Relic Thief: 2 - Challenge Pickup 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (2)/",
                }
            },
            {
                "Yhelm - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 449,
                    LocationId = "Yhelm - Ammostash 1",
                    Description = "Yhelm - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Yhelm - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 450,
                    LocationId = "Yhelm - Ammostash 2",
                    Description = "Yhelm - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Yhelm - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 451,
                    LocationId = "Yhelm - Ammostash 3",
                    Description = "Yhelm - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Yhelm - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 452,
                    LocationId = "Yhelm - Ammostash 4",
                    Description = "Yhelm - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Yhelm - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 453,
                    LocationId = "Yhelm - Ammostash 5",
                    Description = "Yhelm - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Yhelm - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 454,
                    LocationId = "Yhelm - Ammostash 6",
                    Description = "Yhelm - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Yhelm - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 455,
                    LocationId = "Yhelm - Ammostash 7",
                    Description = "Yhelm - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Yhelm - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 456,
                    LocationId = "Yhelm - Ammostash 8",
                    Description = "Yhelm - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Yhelm - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 457,
                    LocationId = "Yhelm - Ammostash 9",
                    Description = "Yhelm - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Yhelm - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 458,
                    LocationId = "Yhelm - Ammostash 10",
                    Description = "Yhelm - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Yhelm - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 459,
                    LocationId = "Yhelm - Ammostash 11",
                    Description = "Yhelm - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Yhelm - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 460,
                    LocationId = "Yhelm - Ammostash 12",
                    Description = "Yhelm - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Yhelm - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 461,
                    LocationId = "Yhelm - Ammostash 13",
                    Description = "Yhelm - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Yhelm - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 462,
                    LocationId = "Yhelm - Ammostash 14",
                    Description = "Yhelm - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Yhelm - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 463,
                    LocationId = "Yhelm - Ammostash 15",
                    Description = "Yhelm - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Yhelm - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 464,
                    LocationId = "Yhelm - Ammostash 16",
                    Description = "Yhelm - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (15)/",
                }
            },
            {
                "Yhelm - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 465,
                    LocationId = "Yhelm - Ammostash 17",
                    Description = "Yhelm - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (16)/",
                }
            },
            {
                "Yhelm - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 466,
                    LocationId = "Yhelm - Ammostash 18",
                    Description = "Yhelm - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (17)/",
                }
            },
            {
                "Yhelm - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 467,
                    LocationId = "Yhelm - Ammostash 19",
                    Description = "Yhelm - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (18)/",
                }
            },
            {
                "Yhelm - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 468,
                    LocationId = "Yhelm - Ammostash 20",
                    Description = "Yhelm - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (19)/",
                }
            },
            {
                "Yhelm - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 469,
                    LocationId = "Yhelm - Ammostash 21",
                    Description = "Yhelm - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Yhelm - Ammostash 22",
                new Location
                {
                    ArchipelagoId = 470,
                    LocationId = "Yhelm - Ammostash 22",
                    Description = "Yhelm - Ammostash 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Yhelm - Ammostash 23",
                new Location
                {
                    ArchipelagoId = 471,
                    LocationId = "Yhelm - Ammostash 23",
                    Description = "Yhelm - Ammostash 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Yhelm - Ammostash 24",
                new Location
                {
                    ArchipelagoId = 472,
                    LocationId = "Yhelm - Ammostash 24",
                    Description = "Yhelm - Ammostash 24",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Yhelm - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 473,
                    LocationId = "Yhelm - Health Crystal 1",
                    Description = "Yhelm - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone/",
                }
            },
            {
                "Yhelm - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 474,
                    LocationId = "Yhelm - Health Crystal 2",
                    Description = "Yhelm - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (1)/",
                }
            },
            {
                "Yhelm - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 475,
                    LocationId = "Yhelm - Health Crystal 3",
                    Description = "Yhelm - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (2)/",
                }
            },
            {
                "Yhelm - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 476,
                    LocationId = "Yhelm - Health Crystal 4",
                    Description = "Yhelm - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (3)/",
                }
            },
            {
                "Yhelm - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 477,
                    LocationId = "Yhelm - Health Crystal 5",
                    Description = "Yhelm - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (4)/",
                }
            },
            {
                "Yhelm - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 478,
                    LocationId = "Yhelm - Health Crystal 6",
                    Description = "Yhelm - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (5)/",
                }
            },
            {
                "Yhelm - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 479,
                    LocationId = "Yhelm - Health Crystal 7",
                    Description = "Yhelm - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (6)/",
                }
            },
            {
                "Yhelm - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 480,
                    LocationId = "Yhelm - Health Crystal 8",
                    Description = "Yhelm - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (7)/",
                }
            },
            {
                "Yhelm - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 481,
                    LocationId = "Yhelm - Health Crystal 9",
                    Description = "Yhelm - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (8)/",
                }
            },
            {
                "Yhelm - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 482,
                    LocationId = "Yhelm - Health Crystal 10",
                    Description = "Yhelm - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (9)/",
                }
            },
            {
                "Yhelm - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 483,
                    LocationId = "Yhelm - Health Crystal 11",
                    Description = "Yhelm - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (10)/",
                }
            },
            {
                "Yhelm - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 484,
                    LocationId = "Yhelm - Health Crystal 12",
                    Description = "Yhelm - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (11)/",
                }
            },
            {
                "Yhelm - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 485,
                    LocationId = "Yhelm - Health Crystal 13",
                    Description = "Yhelm - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (12)/",
                }
            },
            {
                "Yhelm - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 486,
                    LocationId = "Yhelm - Health Crystal 14",
                    Description = "Yhelm - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (13)/",
                }
            },
            {
                "Yhelm - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 487,
                    LocationId = "Yhelm - Health Crystal 15",
                    Description = "Yhelm - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (14)/",
                }
            },
            {
                "Yhelm - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 488,
                    LocationId = "Yhelm - Health Crystal 16",
                    Description = "Yhelm - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (15)/",
                }
            },
            {
                "Yhelm - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 489,
                    LocationId = "Yhelm - Health Crystal 17",
                    Description = "Yhelm - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (16)/",
                }
            },
            {
                "Yhelm - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 490,
                    LocationId = "Yhelm - Health Crystal 18",
                    Description = "Yhelm - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Yhelm - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 491,
                    LocationId = "Yhelm - Health Crystal 19",
                    Description = "Yhelm - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Yhelm - Health Crystal 20",
                new Location
                {
                    ArchipelagoId = 492,
                    LocationId = "Yhelm - Health Crystal 20",
                    Description = "Yhelm - Health Crystal 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Yhelm - Health Crystal 21",
                new Location
                {
                    ArchipelagoId = 493,
                    LocationId = "Yhelm - Health Crystal 21",
                    Description = "Yhelm - Health Crystal 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 494,
                    LocationId = "Yhelm - Chaos Crystal 1",
                    Description = "Yhelm - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Yhelm - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 495,
                    LocationId = "Yhelm - Chaos Crystal 2",
                    Description = "Yhelm - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 496,
                    LocationId = "Yhelm - Chaos Crystal 3",
                    Description = "Yhelm - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 497,
                    LocationId = "Yhelm - Chaos Crystal 4",
                    Description = "Yhelm - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 498,
                    LocationId = "Yhelm - Chaos Crystal 5",
                    Description = "Yhelm - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 499,
                    LocationId = "Yhelm - Chaos Crystal 6",
                    Description = "Yhelm - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 500,
                    LocationId = "Yhelm - Chaos Crystal 7",
                    Description = "Yhelm - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 501,
                    LocationId = "Yhelm - Chaos Crystal 8",
                    Description = "Yhelm - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Yhelm - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 502,
                    LocationId = "Yhelm - Chaos Crystal 9",
                    Description = "Yhelm - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 503,
                    LocationId = "Yhelm - Chaos Crystal 10",
                    Description = "Yhelm - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 11",
                new Location
                {
                    ArchipelagoId = 504,
                    LocationId = "Yhelm - Chaos Crystal 11",
                    Description = "Yhelm - Chaos Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 12",
                new Location
                {
                    ArchipelagoId = 505,
                    LocationId = "Yhelm - Chaos Crystal 12",
                    Description = "Yhelm - Chaos Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Yhelm - Chaos Crystal 13",
                new Location
                {
                    ArchipelagoId = 506,
                    LocationId = "Yhelm - Chaos Crystal 13",
                    Description = "Yhelm - Chaos Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Ultimate Mastery: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 507,
                    LocationId = "Ultimate Mastery: 1 - Health Crystal 1",
                    Description = "Ultimate Mastery: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Ultimate Mastery: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 508,
                    LocationId = "Ultimate Mastery: 1 - Health Crystal 2",
                    Description = "Ultimate Mastery: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Ultimate Mastery: 1 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 509,
                    LocationId = "Ultimate Mastery: 1 - Health Crystal 3",
                    Description = "Ultimate Mastery: 1 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Killing with Rhythm: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 510,
                    LocationId = "Killing with Rhythm: 2 - Health Crystal 1",
                    Description = "Killing with Rhythm: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Killing with Rhythm: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 511,
                    LocationId = "Killing with Rhythm: 2 - Health Crystal 2",
                    Description = "Killing with Rhythm: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 512,
                    LocationId = "Weapon Trickery: 2 - Health Crystal 1",
                    Description = "Weapon Trickery: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Weapon Trickery: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 513,
                    LocationId = "Weapon Trickery: 2 - Health Crystal 2",
                    Description = "Weapon Trickery: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 2 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 514,
                    LocationId = "Weapon Trickery: 2 - Chaos Crystal 1",
                    Description = "Weapon Trickery: 2 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Incaustis - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 515,
                    LocationId = "Incaustis - Ammostash 1",
                    Description = "Incaustis - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Incaustis - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 516,
                    LocationId = "Incaustis - Ammostash 2",
                    Description = "Incaustis - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Incaustis - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 517,
                    LocationId = "Incaustis - Ammostash 3",
                    Description = "Incaustis - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Incaustis - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 518,
                    LocationId = "Incaustis - Ammostash 4",
                    Description = "Incaustis - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Incaustis - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 519,
                    LocationId = "Incaustis - Ammostash 5",
                    Description = "Incaustis - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Incaustis - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 520,
                    LocationId = "Incaustis - Ammostash 6",
                    Description = "Incaustis - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Incaustis - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 521,
                    LocationId = "Incaustis - Ammostash 7",
                    Description = "Incaustis - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Incaustis - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 522,
                    LocationId = "Incaustis - Ammostash 8",
                    Description = "Incaustis - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Incaustis - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 523,
                    LocationId = "Incaustis - Ammostash 9",
                    Description = "Incaustis - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Incaustis - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 524,
                    LocationId = "Incaustis - Ammostash 10",
                    Description = "Incaustis - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Incaustis - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 525,
                    LocationId = "Incaustis - Ammostash 11",
                    Description = "Incaustis - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Incaustis - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 526,
                    LocationId = "Incaustis - Ammostash 12",
                    Description = "Incaustis - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Incaustis - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 527,
                    LocationId = "Incaustis - Ammostash 13",
                    Description = "Incaustis - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Incaustis - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 528,
                    LocationId = "Incaustis - Ammostash 14",
                    Description = "Incaustis - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Incaustis - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 529,
                    LocationId = "Incaustis - Ammostash 15",
                    Description = "Incaustis - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Incaustis - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 530,
                    LocationId = "Incaustis - Ammostash 16",
                    Description = "Incaustis - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (15)/",
                }
            },
            {
                "Incaustis - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 531,
                    LocationId = "Incaustis - Ammostash 17",
                    Description = "Incaustis - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (16)/",
                }
            },
            {
                "Incaustis - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 532,
                    LocationId = "Incaustis - Ammostash 18",
                    Description = "Incaustis - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (17)/",
                }
            },
            {
                "Incaustis - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 533,
                    LocationId = "Incaustis - Ammostash 19",
                    Description = "Incaustis - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (18)/",
                }
            },
            {
                "Incaustis - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 534,
                    LocationId = "Incaustis - Ammostash 20",
                    Description = "Incaustis - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (19)/",
                }
            },
            {
                "Incaustis - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 535,
                    LocationId = "Incaustis - Ammostash 21",
                    Description = "Incaustis - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (20)/",
                }
            },
            {
                "Incaustis - Ammostash 22",
                new Location
                {
                    ArchipelagoId = 536,
                    LocationId = "Incaustis - Ammostash 22",
                    Description = "Incaustis - Ammostash 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (21)/",
                }
            },
            {
                "Incaustis - Ammostash 23",
                new Location
                {
                    ArchipelagoId = 537,
                    LocationId = "Incaustis - Ammostash 23",
                    Description = "Incaustis - Ammostash 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Incaustis - Ammostash 24",
                new Location
                {
                    ArchipelagoId = 538,
                    LocationId = "Incaustis - Ammostash 24",
                    Description = "Incaustis - Ammostash 24",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Incaustis - Ammostash 25",
                new Location
                {
                    ArchipelagoId = 539,
                    LocationId = "Incaustis - Ammostash 25",
                    Description = "Incaustis - Ammostash 25",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Incaustis - Ammostash 26",
                new Location
                {
                    ArchipelagoId = 540,
                    LocationId = "Incaustis - Ammostash 26",
                    Description = "Incaustis - Ammostash 26",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Incaustis - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 541,
                    LocationId = "Incaustis - Health Crystal 1",
                    Description = "Incaustis - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone/",
                }
            },
            {
                "Incaustis - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 542,
                    LocationId = "Incaustis - Health Crystal 2",
                    Description = "Incaustis - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (1)/",
                }
            },
            {
                "Incaustis - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 543,
                    LocationId = "Incaustis - Health Crystal 3",
                    Description = "Incaustis - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (2)/",
                }
            },
            {
                "Incaustis - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 544,
                    LocationId = "Incaustis - Health Crystal 4",
                    Description = "Incaustis - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (3)/",
                }
            },
            {
                "Incaustis - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 545,
                    LocationId = "Incaustis - Health Crystal 5",
                    Description = "Incaustis - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (4)/",
                }
            },
            {
                "Incaustis - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 546,
                    LocationId = "Incaustis - Health Crystal 6",
                    Description = "Incaustis - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (5)/",
                }
            },
            {
                "Incaustis - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 547,
                    LocationId = "Incaustis - Health Crystal 7",
                    Description = "Incaustis - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (6)/",
                }
            },
            {
                "Incaustis - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 548,
                    LocationId = "Incaustis - Health Crystal 8",
                    Description = "Incaustis - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (7)/",
                }
            },
            {
                "Incaustis - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 549,
                    LocationId = "Incaustis - Health Crystal 9",
                    Description = "Incaustis - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (8)/",
                }
            },
            {
                "Incaustis - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 550,
                    LocationId = "Incaustis - Health Crystal 10",
                    Description = "Incaustis - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (9)/",
                }
            },
            {
                "Incaustis - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 551,
                    LocationId = "Incaustis - Health Crystal 11",
                    Description = "Incaustis - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (10)/",
                }
            },
            {
                "Incaustis - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 552,
                    LocationId = "Incaustis - Health Crystal 12",
                    Description = "Incaustis - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (11)/",
                }
            },
            {
                "Incaustis - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 553,
                    LocationId = "Incaustis - Health Crystal 13",
                    Description = "Incaustis - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (12)/",
                }
            },
            {
                "Incaustis - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 554,
                    LocationId = "Incaustis - Health Crystal 14",
                    Description = "Incaustis - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (13)/",
                }
            },
            {
                "Incaustis - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 555,
                    LocationId = "Incaustis - Health Crystal 15",
                    Description = "Incaustis - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (14)/",
                }
            },
            {
                "Incaustis - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 556,
                    LocationId = "Incaustis - Health Crystal 16",
                    Description = "Incaustis - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (15)/",
                }
            },
            {
                "Incaustis - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 557,
                    LocationId = "Incaustis - Health Crystal 17",
                    Description = "Incaustis - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (16)/",
                }
            },
            {
                "Incaustis - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 558,
                    LocationId = "Incaustis - Health Crystal 18",
                    Description = "Incaustis - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (17)/",
                }
            },
            {
                "Incaustis - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 559,
                    LocationId = "Incaustis - Health Crystal 19",
                    Description = "Incaustis - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (18)/",
                }
            },
            {
                "Incaustis - Health Crystal 20",
                new Location
                {
                    ArchipelagoId = 560,
                    LocationId = "Incaustis - Health Crystal 20",
                    Description = "Incaustis - Health Crystal 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Incaustis - Health Crystal 21",
                new Location
                {
                    ArchipelagoId = 561,
                    LocationId = "Incaustis - Health Crystal 21",
                    Description = "Incaustis - Health Crystal 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Incaustis - Health Crystal 22",
                new Location
                {
                    ArchipelagoId = 562,
                    LocationId = "Incaustis - Health Crystal 22",
                    Description = "Incaustis - Health Crystal 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Incaustis - Health Crystal 23",
                new Location
                {
                    ArchipelagoId = 563,
                    LocationId = "Incaustis - Health Crystal 23",
                    Description = "Incaustis - Health Crystal 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 564,
                    LocationId = "Incaustis - Chaos Crystal 1",
                    Description = "Incaustis - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Incaustis - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 565,
                    LocationId = "Incaustis - Chaos Crystal 2",
                    Description = "Incaustis - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 566,
                    LocationId = "Incaustis - Chaos Crystal 3",
                    Description = "Incaustis - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 567,
                    LocationId = "Incaustis - Chaos Crystal 4",
                    Description = "Incaustis - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 568,
                    LocationId = "Incaustis - Chaos Crystal 5",
                    Description = "Incaustis - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 569,
                    LocationId = "Incaustis - Chaos Crystal 6",
                    Description = "Incaustis - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 570,
                    LocationId = "Incaustis - Chaos Crystal 7",
                    Description = "Incaustis - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 571,
                    LocationId = "Incaustis - Chaos Crystal 8",
                    Description = "Incaustis - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 572,
                    LocationId = "Incaustis - Chaos Crystal 9",
                    Description = "Incaustis - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (8)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 573,
                    LocationId = "Incaustis - Chaos Crystal 10",
                    Description = "Incaustis - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (9)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 11",
                new Location
                {
                    ArchipelagoId = 574,
                    LocationId = "Incaustis - Chaos Crystal 11",
                    Description = "Incaustis - Chaos Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (10)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 12",
                new Location
                {
                    ArchipelagoId = 575,
                    LocationId = "Incaustis - Chaos Crystal 12",
                    Description = "Incaustis - Chaos Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (11)/",
                }
            },
            {
                "Incaustis - Chaos Crystal 13",
                new Location
                {
                    ArchipelagoId = 576,
                    LocationId = "Incaustis - Chaos Crystal 13",
                    Description = "Incaustis - Chaos Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (12)/",
                }
            },
            {
                "Slaughter Mastery: 1 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 577,
                    LocationId = "Slaughter Mastery: 1 - Health Crystal 1",
                    Description = "Slaughter Mastery: 1 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Slaughter Mastery: 1 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 578,
                    LocationId = "Slaughter Mastery: 1 - Health Crystal 2",
                    Description = "Slaughter Mastery: 1 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Slaughter Mastery: 1 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 579,
                    LocationId = "Slaughter Mastery: 1 - Health Crystal 3",
                    Description = "Slaughter Mastery: 1 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Giantslayer: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 580,
                    LocationId = "Giantslayer: 2 - Health Crystal 1",
                    Description = "Giantslayer: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Giantslayer: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 581,
                    LocationId = "Giantslayer: 2 - Health Crystal 2",
                    Description = "Giantslayer: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Giantslayer: 2 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 582,
                    LocationId = "Giantslayer: 2 - Health Crystal 3",
                    Description = "Giantslayer: 2 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Giantslayer: 2 - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 583,
                    LocationId = "Giantslayer: 2 - Health Crystal 4",
                    Description = "Giantslayer: 2 - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Giantslayer: 2 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 584,
                    LocationId = "Giantslayer: 2 - Chaos Crystal 1",
                    Description = "Giantslayer: 2 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Giantslayer: 2 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 585,
                    LocationId = "Giantslayer: 2 - Chaos Crystal 2",
                    Description = "Giantslayer: 2 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Giantslayer: 2 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 586,
                    LocationId = "Giantslayer: 2 - Chaos Crystal 3",
                    Description = "Giantslayer: 2 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Giantslayer: 2 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 587,
                    LocationId = "Giantslayer: 2 - Chaos Crystal 4",
                    Description = "Giantslayer: 2 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Relic Thief: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 588,
                    LocationId = "Relic Thief: 3 - Health Crystal 1",
                    Description = "Relic Thief: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Relic Thief: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 589,
                    LocationId = "Relic Thief: 3 - Health Crystal 2",
                    Description = "Relic Thief: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Relic Thief: 3 - Challenge Pickup 1",
                new Location
                {
                    ArchipelagoId = 590,
                    LocationId = "Relic Thief: 3 - Challenge Pickup 1",
                    Description = "Relic Thief: 3 - Challenge Pickup 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup/",
                }
            },
            {
                "Relic Thief: 3 - Challenge Pickup 2",
                new Location
                {
                    ArchipelagoId = 591,
                    LocationId = "Relic Thief: 3 - Challenge Pickup 2",
                    Description = "Relic Thief: 3 - Challenge Pickup 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (1)/",
                }
            },
            {
                "Relic Thief: 3 - Challenge Pickup 3",
                new Location
                {
                    ArchipelagoId = 592,
                    LocationId = "Relic Thief: 3 - Challenge Pickup 3",
                    Description = "Relic Thief: 3 - Challenge Pickup 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.RelicThief,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChallengePickup,
                    GameObjectName = "BoostPickups/PF_ChallengePickup (2)/",
                }
            },
            {
                "Gehenna - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 593,
                    LocationId = "Gehenna - Ammostash 1",
                    Description = "Gehenna - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Gehenna - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 594,
                    LocationId = "Gehenna - Ammostash 2",
                    Description = "Gehenna - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Gehenna - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 595,
                    LocationId = "Gehenna - Ammostash 3",
                    Description = "Gehenna - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Gehenna - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 596,
                    LocationId = "Gehenna - Ammostash 4",
                    Description = "Gehenna - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Gehenna - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 597,
                    LocationId = "Gehenna - Ammostash 5",
                    Description = "Gehenna - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Gehenna - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 598,
                    LocationId = "Gehenna - Ammostash 6",
                    Description = "Gehenna - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Gehenna - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 599,
                    LocationId = "Gehenna - Ammostash 7",
                    Description = "Gehenna - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Gehenna - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 600,
                    LocationId = "Gehenna - Ammostash 8",
                    Description = "Gehenna - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Gehenna - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 601,
                    LocationId = "Gehenna - Ammostash 9",
                    Description = "Gehenna - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Gehenna - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 602,
                    LocationId = "Gehenna - Ammostash 10",
                    Description = "Gehenna - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Gehenna - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 603,
                    LocationId = "Gehenna - Ammostash 11",
                    Description = "Gehenna - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Gehenna - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 604,
                    LocationId = "Gehenna - Ammostash 12",
                    Description = "Gehenna - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Gehenna - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 605,
                    LocationId = "Gehenna - Ammostash 13",
                    Description = "Gehenna - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Gehenna - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 606,
                    LocationId = "Gehenna - Ammostash 14",
                    Description = "Gehenna - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Gehenna - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 607,
                    LocationId = "Gehenna - Ammostash 15",
                    Description = "Gehenna - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Gehenna - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 608,
                    LocationId = "Gehenna - Ammostash 16",
                    Description = "Gehenna - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Gehenna - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 609,
                    LocationId = "Gehenna - Ammostash 17",
                    Description = "Gehenna - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Gehenna - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 610,
                    LocationId = "Gehenna - Ammostash 18",
                    Description = "Gehenna - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Gehenna - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 611,
                    LocationId = "Gehenna - Ammostash 19",
                    Description = "Gehenna - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Gehenna - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 612,
                    LocationId = "Gehenna - Health Crystal 1",
                    Description = "Gehenna - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Gehenna - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 613,
                    LocationId = "Gehenna - Health Crystal 2",
                    Description = "Gehenna - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Gehenna - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 614,
                    LocationId = "Gehenna - Health Crystal 3",
                    Description = "Gehenna - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Gehenna - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 615,
                    LocationId = "Gehenna - Health Crystal 4",
                    Description = "Gehenna - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Gehenna - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 616,
                    LocationId = "Gehenna - Health Crystal 5",
                    Description = "Gehenna - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (4)/",
                }
            },
            {
                "Gehenna - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 617,
                    LocationId = "Gehenna - Health Crystal 6",
                    Description = "Gehenna - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (5)/",
                }
            },
            {
                "Gehenna - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 618,
                    LocationId = "Gehenna - Health Crystal 7",
                    Description = "Gehenna - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (6)/",
                }
            },
            {
                "Gehenna - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 619,
                    LocationId = "Gehenna - Health Crystal 8",
                    Description = "Gehenna - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (7)/",
                }
            },
            {
                "Gehenna - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 620,
                    LocationId = "Gehenna - Health Crystal 9",
                    Description = "Gehenna - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (8)/",
                }
            },
            {
                "Gehenna - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 621,
                    LocationId = "Gehenna - Health Crystal 10",
                    Description = "Gehenna - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (9)/",
                }
            },
            {
                "Gehenna - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 622,
                    LocationId = "Gehenna - Health Crystal 11",
                    Description = "Gehenna - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (10)/",
                }
            },
            {
                "Gehenna - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 623,
                    LocationId = "Gehenna - Health Crystal 12",
                    Description = "Gehenna - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (11)/",
                }
            },
            {
                "Gehenna - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 624,
                    LocationId = "Gehenna - Health Crystal 13",
                    Description = "Gehenna - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (12)/",
                }
            },
            {
                "Gehenna - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 625,
                    LocationId = "Gehenna - Health Crystal 14",
                    Description = "Gehenna - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (13)/",
                }
            },
            {
                "Gehenna - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 626,
                    LocationId = "Gehenna - Health Crystal 15",
                    Description = "Gehenna - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Gehenna - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 627,
                    LocationId = "Gehenna - Health Crystal 16",
                    Description = "Gehenna - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Gehenna - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 628,
                    LocationId = "Gehenna - Health Crystal 17",
                    Description = "Gehenna - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Gehenna - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 629,
                    LocationId = "Gehenna - Health Crystal 18",
                    Description = "Gehenna - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 630,
                    LocationId = "Gehenna - Chaos Crystal 1",
                    Description = "Gehenna - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Gehenna - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 631,
                    LocationId = "Gehenna - Chaos Crystal 2",
                    Description = "Gehenna - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 632,
                    LocationId = "Gehenna - Chaos Crystal 3",
                    Description = "Gehenna - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 633,
                    LocationId = "Gehenna - Chaos Crystal 4",
                    Description = "Gehenna - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 634,
                    LocationId = "Gehenna - Chaos Crystal 5",
                    Description = "Gehenna - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 635,
                    LocationId = "Gehenna - Chaos Crystal 6",
                    Description = "Gehenna - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 636,
                    LocationId = "Gehenna - Chaos Crystal 7",
                    Description = "Gehenna - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 637,
                    LocationId = "Gehenna - Chaos Crystal 8",
                    Description = "Gehenna - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 638,
                    LocationId = "Gehenna - Chaos Crystal 9",
                    Description = "Gehenna - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (8)/",
                }
            },
            {
                "Gehenna - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 639,
                    LocationId = "Gehenna - Chaos Crystal 10",
                    Description = "Gehenna - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (9)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 640,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 1",
                    Description = "Death's Edge: 2 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 641,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 2",
                    Description = "Death's Edge: 2 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 642,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 3",
                    Description = "Death's Edge: 2 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 643,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 4",
                    Description = "Death's Edge: 2 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 644,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 5",
                    Description = "Death's Edge: 2 - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 645,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 6",
                    Description = "Death's Edge: 2 - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 646,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 7",
                    Description = "Death's Edge: 2 - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Death's Edge: 2 - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 647,
                    LocationId = "Death's Edge: 2 - Chaos Crystal 8",
                    Description = "Death's Edge: 2 - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Killing with Rhythm: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 648,
                    LocationId = "Killing with Rhythm: 3 - Health Crystal 1",
                    Description = "Killing with Rhythm: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Killing with Rhythm: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 649,
                    LocationId = "Killing with Rhythm: 3 - Health Crystal 2",
                    Description = "Killing with Rhythm: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.KillingWithRhythm,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 650,
                    LocationId = "Weapon Trickery: 3 - Health Crystal 1",
                    Description = "Weapon Trickery: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Weapon Trickery: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 651,
                    LocationId = "Weapon Trickery: 3 - Health Crystal 2",
                    Description = "Weapon Trickery: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Weapon Trickery: 3 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 652,
                    LocationId = "Weapon Trickery: 3 - Chaos Crystal 1",
                    Description = "Weapon Trickery: 3 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.WeaponTrickery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Nihil - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 653,
                    LocationId = "Nihil - Ammostash 1",
                    Description = "Nihil - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Nihil - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 654,
                    LocationId = "Nihil - Ammostash 2",
                    Description = "Nihil - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Nihil - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 655,
                    LocationId = "Nihil - Ammostash 3",
                    Description = "Nihil - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Nihil - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 656,
                    LocationId = "Nihil - Ammostash 4",
                    Description = "Nihil - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Nihil - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 657,
                    LocationId = "Nihil - Ammostash 5",
                    Description = "Nihil - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Nihil - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 658,
                    LocationId = "Nihil - Ammostash 6",
                    Description = "Nihil - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Nihil - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 659,
                    LocationId = "Nihil - Ammostash 7",
                    Description = "Nihil - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Nihil - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 660,
                    LocationId = "Nihil - Ammostash 8",
                    Description = "Nihil - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Nihil - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 661,
                    LocationId = "Nihil - Ammostash 9",
                    Description = "Nihil - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Nihil - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 662,
                    LocationId = "Nihil - Ammostash 10",
                    Description = "Nihil - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Nihil - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 663,
                    LocationId = "Nihil - Ammostash 11",
                    Description = "Nihil - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Nihil - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 664,
                    LocationId = "Nihil - Ammostash 12",
                    Description = "Nihil - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Nihil - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 665,
                    LocationId = "Nihil - Ammostash 13",
                    Description = "Nihil - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Nihil - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 666,
                    LocationId = "Nihil - Ammostash 14",
                    Description = "Nihil - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Nihil - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 667,
                    LocationId = "Nihil - Ammostash 15",
                    Description = "Nihil - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Nihil - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 668,
                    LocationId = "Nihil - Ammostash 16",
                    Description = "Nihil - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (15)/",
                }
            },
            {
                "Nihil - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 669,
                    LocationId = "Nihil - Ammostash 17",
                    Description = "Nihil - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (16)/",
                }
            },
            {
                "Nihil - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 670,
                    LocationId = "Nihil - Ammostash 18",
                    Description = "Nihil - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Nihil - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 671,
                    LocationId = "Nihil - Ammostash 19",
                    Description = "Nihil - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Nihil - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 672,
                    LocationId = "Nihil - Ammostash 20",
                    Description = "Nihil - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Nihil - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 673,
                    LocationId = "Nihil - Ammostash 21",
                    Description = "Nihil - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Nihil - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 674,
                    LocationId = "Nihil - Health Crystal 1",
                    Description = "Nihil - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone/",
                }
            },
            {
                "Nihil - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 675,
                    LocationId = "Nihil - Health Crystal 2",
                    Description = "Nihil - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (1)/",
                }
            },
            {
                "Nihil - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 676,
                    LocationId = "Nihil - Health Crystal 3",
                    Description = "Nihil - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (2)/",
                }
            },
            {
                "Nihil - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 677,
                    LocationId = "Nihil - Health Crystal 4",
                    Description = "Nihil - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (3)/",
                }
            },
            {
                "Nihil - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 678,
                    LocationId = "Nihil - Health Crystal 5",
                    Description = "Nihil - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (4)/",
                }
            },
            {
                "Nihil - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 679,
                    LocationId = "Nihil - Health Crystal 6",
                    Description = "Nihil - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (5)/",
                }
            },
            {
                "Nihil - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 680,
                    LocationId = "Nihil - Health Crystal 7",
                    Description = "Nihil - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (6)/",
                }
            },
            {
                "Nihil - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 681,
                    LocationId = "Nihil - Health Crystal 8",
                    Description = "Nihil - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (7)/",
                }
            },
            {
                "Nihil - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 682,
                    LocationId = "Nihil - Health Crystal 9",
                    Description = "Nihil - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (8)/",
                }
            },
            {
                "Nihil - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 683,
                    LocationId = "Nihil - Health Crystal 10",
                    Description = "Nihil - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (9)/",
                }
            },
            {
                "Nihil - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 684,
                    LocationId = "Nihil - Health Crystal 11",
                    Description = "Nihil - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (10)/",
                }
            },
            {
                "Nihil - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 685,
                    LocationId = "Nihil - Health Crystal 12",
                    Description = "Nihil - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (11)/",
                }
            },
            {
                "Nihil - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 686,
                    LocationId = "Nihil - Health Crystal 13",
                    Description = "Nihil - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (12)/",
                }
            },
            {
                "Nihil - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 687,
                    LocationId = "Nihil - Health Crystal 14",
                    Description = "Nihil - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (13)/",
                }
            },
            {
                "Nihil - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 688,
                    LocationId = "Nihil - Health Crystal 15",
                    Description = "Nihil - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (14)/",
                }
            },
            {
                "Nihil - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 689,
                    LocationId = "Nihil - Health Crystal 16",
                    Description = "Nihil - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Health_Stones/PF_HealthStone (15)/",
                }
            },
            {
                "Nihil - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 690,
                    LocationId = "Nihil - Health Crystal 17",
                    Description = "Nihil - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Nihil - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 691,
                    LocationId = "Nihil - Health Crystal 18",
                    Description = "Nihil - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Nihil - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 692,
                    LocationId = "Nihil - Health Crystal 19",
                    Description = "Nihil - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Nihil - Health Crystal 20",
                new Location
                {
                    ArchipelagoId = 693,
                    LocationId = "Nihil - Health Crystal 20",
                    Description = "Nihil - Health Crystal 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Nihil - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 694,
                    LocationId = "Nihil - Chaos Crystal 1",
                    Description = "Nihil - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Nihil - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 695,
                    LocationId = "Nihil - Chaos Crystal 2",
                    Description = "Nihil - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Nihil - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 696,
                    LocationId = "Nihil - Chaos Crystal 3",
                    Description = "Nihil - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Nihil - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 697,
                    LocationId = "Nihil - Chaos Crystal 4",
                    Description = "Nihil - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Nihil - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 698,
                    LocationId = "Nihil - Chaos Crystal 5",
                    Description = "Nihil - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Ultimate Mastery: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 699,
                    LocationId = "Ultimate Mastery: 2 - Health Crystal 1",
                    Description = "Ultimate Mastery: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Ultimate Mastery: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 700,
                    LocationId = "Ultimate Mastery: 2 - Health Crystal 2",
                    Description = "Ultimate Mastery: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Ultimate Mastery: 2 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 701,
                    LocationId = "Ultimate Mastery: 2 - Health Crystal 3",
                    Description = "Ultimate Mastery: 2 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Slaughter Mastery: 2 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 702,
                    LocationId = "Slaughter Mastery: 2 - Health Crystal 1",
                    Description = "Slaughter Mastery: 2 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Slaughter Mastery: 2 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 703,
                    LocationId = "Slaughter Mastery: 2 - Health Crystal 2",
                    Description = "Slaughter Mastery: 2 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Slaughter Mastery: 2 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 704,
                    LocationId = "Slaughter Mastery: 2 - Health Crystal 3",
                    Description = "Slaughter Mastery: 2 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Giantslayer: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 705,
                    LocationId = "Giantslayer: 3 - Health Crystal 1",
                    Description = "Giantslayer: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Giantslayer: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 706,
                    LocationId = "Giantslayer: 3 - Health Crystal 2",
                    Description = "Giantslayer: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Giantslayer: 3 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 707,
                    LocationId = "Giantslayer: 3 - Health Crystal 3",
                    Description = "Giantslayer: 3 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Giantslayer: 3 - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 708,
                    LocationId = "Giantslayer: 3 - Health Crystal 4",
                    Description = "Giantslayer: 3 - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Giantslayer: 3 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 709,
                    LocationId = "Giantslayer: 3 - Chaos Crystal 1",
                    Description = "Giantslayer: 3 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Giantslayer: 3 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 710,
                    LocationId = "Giantslayer: 3 - Chaos Crystal 2",
                    Description = "Giantslayer: 3 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Giantslayer: 3 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 711,
                    LocationId = "Giantslayer: 3 - Chaos Crystal 3",
                    Description = "Giantslayer: 3 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Giantslayer: 3 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 712,
                    LocationId = "Giantslayer: 3 - Chaos Crystal 4",
                    Description = "Giantslayer: 3 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Giantslayer,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Acheron - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 713,
                    LocationId = "Acheron - Ammostash 1",
                    Description = "Acheron - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Acheron - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 714,
                    LocationId = "Acheron - Ammostash 2",
                    Description = "Acheron - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Acheron - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 715,
                    LocationId = "Acheron - Ammostash 3",
                    Description = "Acheron - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Acheron - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 716,
                    LocationId = "Acheron - Ammostash 4",
                    Description = "Acheron - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Acheron - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 717,
                    LocationId = "Acheron - Ammostash 5",
                    Description = "Acheron - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Acheron - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 718,
                    LocationId = "Acheron - Ammostash 6",
                    Description = "Acheron - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Acheron - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 719,
                    LocationId = "Acheron - Ammostash 7",
                    Description = "Acheron - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Acheron - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 720,
                    LocationId = "Acheron - Ammostash 8",
                    Description = "Acheron - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Acheron - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 721,
                    LocationId = "Acheron - Ammostash 9",
                    Description = "Acheron - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Acheron - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 722,
                    LocationId = "Acheron - Ammostash 10",
                    Description = "Acheron - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Acheron - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 723,
                    LocationId = "Acheron - Ammostash 11",
                    Description = "Acheron - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Acheron - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 724,
                    LocationId = "Acheron - Ammostash 12",
                    Description = "Acheron - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Acheron - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 725,
                    LocationId = "Acheron - Ammostash 13",
                    Description = "Acheron - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Acheron - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 726,
                    LocationId = "Acheron - Ammostash 14",
                    Description = "Acheron - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Acheron - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 727,
                    LocationId = "Acheron - Ammostash 15",
                    Description = "Acheron - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Acheron - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 728,
                    LocationId = "Acheron - Ammostash 16",
                    Description = "Acheron - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (15)/",
                }
            },
            {
                "Acheron - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 729,
                    LocationId = "Acheron - Ammostash 17",
                    Description = "Acheron - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (16)/",
                }
            },
            {
                "Acheron - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 730,
                    LocationId = "Acheron - Ammostash 18",
                    Description = "Acheron - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (17)/",
                }
            },
            {
                "Acheron - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 731,
                    LocationId = "Acheron - Ammostash 19",
                    Description = "Acheron - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (18)/",
                }
            },
            {
                "Acheron - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 732,
                    LocationId = "Acheron - Ammostash 20",
                    Description = "Acheron - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (19)/",
                }
            },
            {
                "Acheron - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 733,
                    LocationId = "Acheron - Ammostash 21",
                    Description = "Acheron - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (20)/",
                }
            },
            {
                "Acheron - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 734,
                    LocationId = "Acheron - Health Crystal 1",
                    Description = "Acheron - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone/",
                }
            },
            {
                "Acheron - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 735,
                    LocationId = "Acheron - Health Crystal 2",
                    Description = "Acheron - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (1)/",
                }
            },
            {
                "Acheron - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 736,
                    LocationId = "Acheron - Health Crystal 3",
                    Description = "Acheron - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (2)/",
                }
            },
            {
                "Acheron - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 737,
                    LocationId = "Acheron - Health Crystal 4",
                    Description = "Acheron - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (3)/",
                }
            },
            {
                "Acheron - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 738,
                    LocationId = "Acheron - Health Crystal 5",
                    Description = "Acheron - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (4)/",
                }
            },
            {
                "Acheron - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 739,
                    LocationId = "Acheron - Health Crystal 6",
                    Description = "Acheron - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (5)/",
                }
            },
            {
                "Acheron - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 740,
                    LocationId = "Acheron - Health Crystal 7",
                    Description = "Acheron - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (6)/",
                }
            },
            {
                "Acheron - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 741,
                    LocationId = "Acheron - Health Crystal 8",
                    Description = "Acheron - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (7)/",
                }
            },
            {
                "Acheron - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 742,
                    LocationId = "Acheron - Health Crystal 9",
                    Description = "Acheron - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (8)/",
                }
            },
            {
                "Acheron - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 743,
                    LocationId = "Acheron - Health Crystal 10",
                    Description = "Acheron - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (9)/",
                }
            },
            {
                "Acheron - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 744,
                    LocationId = "Acheron - Health Crystal 11",
                    Description = "Acheron - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (10)/",
                }
            },
            {
                "Acheron - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 745,
                    LocationId = "Acheron - Health Crystal 12",
                    Description = "Acheron - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (11)/",
                }
            },
            {
                "Acheron - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 746,
                    LocationId = "Acheron - Health Crystal 13",
                    Description = "Acheron - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (12)/",
                }
            },
            {
                "Acheron - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 747,
                    LocationId = "Acheron - Health Crystal 14",
                    Description = "Acheron - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (13)/",
                }
            },
            {
                "Acheron - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 748,
                    LocationId = "Acheron - Health Crystal 15",
                    Description = "Acheron - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (14)/",
                }
            },
            {
                "Acheron - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 749,
                    LocationId = "Acheron - Health Crystal 16",
                    Description = "Acheron - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (15)/",
                }
            },
            {
                "Acheron - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 750,
                    LocationId = "Acheron - Health Crystal 17",
                    Description = "Acheron - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (16)/",
                }
            },
            {
                "Acheron - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 751,
                    LocationId = "Acheron - Health Crystal 18",
                    Description = "Acheron - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (17)/",
                }
            },
            {
                "Acheron - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 752,
                    LocationId = "Acheron - Health Crystal 19",
                    Description = "Acheron - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Acheron - Health Crystal 20",
                new Location
                {
                    ArchipelagoId = 753,
                    LocationId = "Acheron - Health Crystal 20",
                    Description = "Acheron - Health Crystal 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Acheron - Health Crystal 21",
                new Location
                {
                    ArchipelagoId = 754,
                    LocationId = "Acheron - Health Crystal 21",
                    Description = "Acheron - Health Crystal 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Acheron - Health Crystal 22",
                new Location
                {
                    ArchipelagoId = 755,
                    LocationId = "Acheron - Health Crystal 22",
                    Description = "Acheron - Health Crystal 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (3)/",
                }
            },
            {
                "Acheron - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 756,
                    LocationId = "Acheron - Chaos Crystal 1",
                    Description = "Acheron - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Acheron - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 757,
                    LocationId = "Acheron - Chaos Crystal 2",
                    Description = "Acheron - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Acheron - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 758,
                    LocationId = "Acheron - Chaos Crystal 3",
                    Description = "Acheron - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Acheron - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 759,
                    LocationId = "Acheron - Chaos Crystal 4",
                    Description = "Acheron - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Acheron - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 760,
                    LocationId = "Acheron - Chaos Crystal 5",
                    Description = "Acheron - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Acheron - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 761,
                    LocationId = "Acheron - Chaos Crystal 6",
                    Description = "Acheron - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Acheron - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 762,
                    LocationId = "Acheron - Chaos Crystal 7",
                    Description = "Acheron - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Acheron - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 763,
                    LocationId = "Acheron - Chaos Crystal 8",
                    Description = "Acheron - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Acheron - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 764,
                    LocationId = "Acheron - Chaos Crystal 9",
                    Description = "Acheron - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (8)/",
                }
            },
            {
                "Acheron - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 765,
                    LocationId = "Acheron - Chaos Crystal 10",
                    Description = "Acheron - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (9)/",
                }
            },
            {
                "Acheron - Chaos Crystal 11",
                new Location
                {
                    ArchipelagoId = 766,
                    LocationId = "Acheron - Chaos Crystal 11",
                    Description = "Acheron - Chaos Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (10)/",
                }
            },
            {
                "Acheron - Chaos Crystal 12",
                new Location
                {
                    ArchipelagoId = 767,
                    LocationId = "Acheron - Chaos Crystal 12",
                    Description = "Acheron - Chaos Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (11)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 768,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 1",
                    Description = "Death's Edge: 3 - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 769,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 2",
                    Description = "Death's Edge: 3 - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 770,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 3",
                    Description = "Death's Edge: 3 - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 771,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 4",
                    Description = "Death's Edge: 3 - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 772,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 5",
                    Description = "Death's Edge: 3 - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 773,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 6",
                    Description = "Death's Edge: 3 - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 774,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 7",
                    Description = "Death's Edge: 3 - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Death's Edge: 3 - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 775,
                    LocationId = "Death's Edge: 3 - Chaos Crystal 8",
                    Description = "Death's Edge: 3 - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.DeathsEdge,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "SoulStones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Ultimate Mastery: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 776,
                    LocationId = "Ultimate Mastery: 3 - Health Crystal 1",
                    Description = "Ultimate Mastery: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Ultimate Mastery: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 777,
                    LocationId = "Ultimate Mastery: 3 - Health Crystal 2",
                    Description = "Ultimate Mastery: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Ultimate Mastery: 3 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 778,
                    LocationId = "Ultimate Mastery: 3 - Health Crystal 3",
                    Description = "Ultimate Mastery: 3 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.UltimateMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Slaughter Mastery: 3 - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 779,
                    LocationId = "Slaughter Mastery: 3 - Health Crystal 1",
                    Description = "Slaughter Mastery: 3 - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone/",
                }
            },
            {
                "Slaughter Mastery: 3 - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 780,
                    LocationId = "Slaughter Mastery: 3 - Health Crystal 2",
                    Description = "Slaughter Mastery: 3 - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (1)/",
                }
            },
            {
                "Slaughter Mastery: 3 - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 781,
                    LocationId = "Slaughter Mastery: 3 - Health Crystal 3",
                    Description = "Slaughter Mastery: 3 - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.SlaughterMastery,
                    Arena = EArena.Torment3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "HealthStones/PF_HealthStone (2)/",
                }
            },
            {
                "Sheol - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 782,
                    LocationId = "Sheol - Ammostash 1",
                    Description = "Sheol - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash/",
                }
            },
            {
                "Sheol - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 783,
                    LocationId = "Sheol - Ammostash 2",
                    Description = "Sheol - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (1)/",
                }
            },
            {
                "Sheol - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 784,
                    LocationId = "Sheol - Ammostash 3",
                    Description = "Sheol - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (2)/",
                }
            },
            {
                "Sheol - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 785,
                    LocationId = "Sheol - Ammostash 4",
                    Description = "Sheol - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (3)/",
                }
            },
            {
                "Sheol - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 786,
                    LocationId = "Sheol - Ammostash 5",
                    Description = "Sheol - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (4)/",
                }
            },
            {
                "Sheol - Ammostash 6",
                new Location
                {
                    ArchipelagoId = 787,
                    LocationId = "Sheol - Ammostash 6",
                    Description = "Sheol - Ammostash 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (5)/",
                }
            },
            {
                "Sheol - Ammostash 7",
                new Location
                {
                    ArchipelagoId = 788,
                    LocationId = "Sheol - Ammostash 7",
                    Description = "Sheol - Ammostash 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (6)/",
                }
            },
            {
                "Sheol - Ammostash 8",
                new Location
                {
                    ArchipelagoId = 789,
                    LocationId = "Sheol - Ammostash 8",
                    Description = "Sheol - Ammostash 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (7)/",
                }
            },
            {
                "Sheol - Ammostash 9",
                new Location
                {
                    ArchipelagoId = 790,
                    LocationId = "Sheol - Ammostash 9",
                    Description = "Sheol - Ammostash 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (8)/",
                }
            },
            {
                "Sheol - Ammostash 10",
                new Location
                {
                    ArchipelagoId = 791,
                    LocationId = "Sheol - Ammostash 10",
                    Description = "Sheol - Ammostash 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (9)/",
                }
            },
            {
                "Sheol - Ammostash 11",
                new Location
                {
                    ArchipelagoId = 792,
                    LocationId = "Sheol - Ammostash 11",
                    Description = "Sheol - Ammostash 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (10)/",
                }
            },
            {
                "Sheol - Ammostash 12",
                new Location
                {
                    ArchipelagoId = 793,
                    LocationId = "Sheol - Ammostash 12",
                    Description = "Sheol - Ammostash 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (11)/",
                }
            },
            {
                "Sheol - Ammostash 13",
                new Location
                {
                    ArchipelagoId = 794,
                    LocationId = "Sheol - Ammostash 13",
                    Description = "Sheol - Ammostash 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (12)/",
                }
            },
            {
                "Sheol - Ammostash 14",
                new Location
                {
                    ArchipelagoId = 795,
                    LocationId = "Sheol - Ammostash 14",
                    Description = "Sheol - Ammostash 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (13)/",
                }
            },
            {
                "Sheol - Ammostash 15",
                new Location
                {
                    ArchipelagoId = 796,
                    LocationId = "Sheol - Ammostash 15",
                    Description = "Sheol - Ammostash 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (14)/",
                }
            },
            {
                "Sheol - Ammostash 16",
                new Location
                {
                    ArchipelagoId = 797,
                    LocationId = "Sheol - Ammostash 16",
                    Description = "Sheol - Ammostash 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (15)/",
                }
            },
            {
                "Sheol - Ammostash 17",
                new Location
                {
                    ArchipelagoId = 798,
                    LocationId = "Sheol - Ammostash 17",
                    Description = "Sheol - Ammostash 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (16)/",
                }
            },
            {
                "Sheol - Ammostash 18",
                new Location
                {
                    ArchipelagoId = 799,
                    LocationId = "Sheol - Ammostash 18",
                    Description = "Sheol - Ammostash 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (17)/",
                }
            },
            {
                "Sheol - Ammostash 19",
                new Location
                {
                    ArchipelagoId = 800,
                    LocationId = "Sheol - Ammostash 19",
                    Description = "Sheol - Ammostash 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (18)/",
                }
            },
            {
                "Sheol - Ammostash 20",
                new Location
                {
                    ArchipelagoId = 801,
                    LocationId = "Sheol - Ammostash 20",
                    Description = "Sheol - Ammostash 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (19)/",
                }
            },
            {
                "Sheol - Ammostash 21",
                new Location
                {
                    ArchipelagoId = 802,
                    LocationId = "Sheol - Ammostash 21",
                    Description = "Sheol - Ammostash 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "AmmoStashes/PF_Ammostash (20)/",
                }
            },
            {
                "Sheol - Ammostash 22",
                new Location
                {
                    ArchipelagoId = 803,
                    LocationId = "Sheol - Ammostash 22",
                    Description = "Sheol - Ammostash 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash/",
                }
            },
            {
                "Sheol - Ammostash 23",
                new Location
                {
                    ArchipelagoId = 804,
                    LocationId = "Sheol - Ammostash 23",
                    Description = "Sheol - Ammostash 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (1)/",
                }
            },
            {
                "Sheol - Ammostash 24",
                new Location
                {
                    ArchipelagoId = 805,
                    LocationId = "Sheol - Ammostash 24",
                    Description = "Sheol - Ammostash 24",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (2)/",
                }
            },
            {
                "Sheol - Ammostash 25",
                new Location
                {
                    ArchipelagoId = 806,
                    LocationId = "Sheol - Ammostash 25",
                    Description = "Sheol - Ammostash 25",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (3)/",
                }
            },
            {
                "Sheol - Ammostash 26",
                new Location
                {
                    ArchipelagoId = 807,
                    LocationId = "Sheol - Ammostash 26",
                    Description = "Sheol - Ammostash 26",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (4)/",
                }
            },
            {
                "Sheol - Ammostash 27",
                new Location
                {
                    ArchipelagoId = 808,
                    LocationId = "Sheol - Ammostash 27",
                    Description = "Sheol - Ammostash 27",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (5)/",
                }
            },
            {
                "Sheol - Ammostash 28",
                new Location
                {
                    ArchipelagoId = 809,
                    LocationId = "Sheol - Ammostash 28",
                    Description = "Sheol - Ammostash 28",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = "Resources/Ammostashes/PF_Ammostash (6)/",
                }
            },
            {
                "Sheol - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 810,
                    LocationId = "Sheol - Health Crystal 1",
                    Description = "Sheol - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone/",
                }
            },
            {
                "Sheol - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 811,
                    LocationId = "Sheol - Health Crystal 2",
                    Description = "Sheol - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (1)/",
                }
            },
            {
                "Sheol - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 812,
                    LocationId = "Sheol - Health Crystal 3",
                    Description = "Sheol - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (2)/",
                }
            },
            {
                "Sheol - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 813,
                    LocationId = "Sheol - Health Crystal 4",
                    Description = "Sheol - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (3)/",
                }
            },
            {
                "Sheol - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 814,
                    LocationId = "Sheol - Health Crystal 5",
                    Description = "Sheol - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (4)/",
                }
            },
            {
                "Sheol - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 815,
                    LocationId = "Sheol - Health Crystal 6",
                    Description = "Sheol - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (5)/",
                }
            },
            {
                "Sheol - Health Crystal 7",
                new Location
                {
                    ArchipelagoId = 816,
                    LocationId = "Sheol - Health Crystal 7",
                    Description = "Sheol - Health Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (6)/",
                }
            },
            {
                "Sheol - Health Crystal 8",
                new Location
                {
                    ArchipelagoId = 817,
                    LocationId = "Sheol - Health Crystal 8",
                    Description = "Sheol - Health Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (7)/",
                }
            },
            {
                "Sheol - Health Crystal 9",
                new Location
                {
                    ArchipelagoId = 818,
                    LocationId = "Sheol - Health Crystal 9",
                    Description = "Sheol - Health Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (8)/",
                }
            },
            {
                "Sheol - Health Crystal 10",
                new Location
                {
                    ArchipelagoId = 819,
                    LocationId = "Sheol - Health Crystal 10",
                    Description = "Sheol - Health Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (9)/",
                }
            },
            {
                "Sheol - Health Crystal 11",
                new Location
                {
                    ArchipelagoId = 820,
                    LocationId = "Sheol - Health Crystal 11",
                    Description = "Sheol - Health Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (10)/",
                }
            },
            {
                "Sheol - Health Crystal 12",
                new Location
                {
                    ArchipelagoId = 821,
                    LocationId = "Sheol - Health Crystal 12",
                    Description = "Sheol - Health Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (11)/",
                }
            },
            {
                "Sheol - Health Crystal 13",
                new Location
                {
                    ArchipelagoId = 822,
                    LocationId = "Sheol - Health Crystal 13",
                    Description = "Sheol - Health Crystal 13",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (12)/",
                }
            },
            {
                "Sheol - Health Crystal 14",
                new Location
                {
                    ArchipelagoId = 823,
                    LocationId = "Sheol - Health Crystal 14",
                    Description = "Sheol - Health Crystal 14",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (13)/",
                }
            },
            {
                "Sheol - Health Crystal 15",
                new Location
                {
                    ArchipelagoId = 824,
                    LocationId = "Sheol - Health Crystal 15",
                    Description = "Sheol - Health Crystal 15",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (14)/",
                }
            },
            {
                "Sheol - Health Crystal 16",
                new Location
                {
                    ArchipelagoId = 825,
                    LocationId = "Sheol - Health Crystal 16",
                    Description = "Sheol - Health Crystal 16",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (15)/",
                }
            },
            {
                "Sheol - Health Crystal 17",
                new Location
                {
                    ArchipelagoId = 826,
                    LocationId = "Sheol - Health Crystal 17",
                    Description = "Sheol - Health Crystal 17",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (16)/",
                }
            },
            {
                "Sheol - Health Crystal 18",
                new Location
                {
                    ArchipelagoId = 827,
                    LocationId = "Sheol - Health Crystal 18",
                    Description = "Sheol - Health Crystal 18",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (17)/",
                }
            },
            {
                "Sheol - Health Crystal 19",
                new Location
                {
                    ArchipelagoId = 828,
                    LocationId = "Sheol - Health Crystal 19",
                    Description = "Sheol - Health Crystal 19",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (18)/",
                }
            },
            {
                "Sheol - Health Crystal 20",
                new Location
                {
                    ArchipelagoId = 829,
                    LocationId = "Sheol - Health Crystal 20",
                    Description = "Sheol - Health Crystal 20",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Healthstones/PF_HealthStone (19)/",
                }
            },
            {
                "Sheol - Health Crystal 21",
                new Location
                {
                    ArchipelagoId = 830,
                    LocationId = "Sheol - Health Crystal 21",
                    Description = "Sheol - Health Crystal 21",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone/",
                }
            },
            {
                "Sheol - Health Crystal 22",
                new Location
                {
                    ArchipelagoId = 831,
                    LocationId = "Sheol - Health Crystal 22",
                    Description = "Sheol - Health Crystal 22",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone (1)/",
                }
            },
            {
                "Sheol - Health Crystal 23",
                new Location
                {
                    ArchipelagoId = 832,
                    LocationId = "Sheol - Health Crystal 23",
                    Description = "Sheol - Health Crystal 23",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone (2)/",
                }
            },
            {
                "Sheol - Health Crystal 24",
                new Location
                {
                    ArchipelagoId = 833,
                    LocationId = "Sheol - Health Crystal 24",
                    Description = "Sheol - Health Crystal 24",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone (3)/",
                }
            },
            {
                "Sheol - Health Crystal 25",
                new Location
                {
                    ArchipelagoId = 834,
                    LocationId = "Sheol - Health Crystal 25",
                    Description = "Sheol - Health Crystal 25",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone (4)/",
                }
            },
            {
                "Sheol - Health Crystal 26",
                new Location
                {
                    ArchipelagoId = 835,
                    LocationId = "Sheol - Health Crystal 26",
                    Description = "Sheol - Health Crystal 26",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = "Resources/HealtStones/PF_HealthStone (5)/",
                }
            },
            {
                "Sheol - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 836,
                    LocationId = "Sheol - Chaos Crystal 1",
                    Description = "Sheol - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat/",
                }
            },
            {
                "Sheol - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 837,
                    LocationId = "Sheol - Chaos Crystal 2",
                    Description = "Sheol - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (1)/",
                }
            },
            {
                "Sheol - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 838,
                    LocationId = "Sheol - Chaos Crystal 3",
                    Description = "Sheol - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (2)/",
                }
            },
            {
                "Sheol - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 839,
                    LocationId = "Sheol - Chaos Crystal 4",
                    Description = "Sheol - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (3)/",
                }
            },
            {
                "Sheol - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 840,
                    LocationId = "Sheol - Chaos Crystal 5",
                    Description = "Sheol - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (4)/",
                }
            },
            {
                "Sheol - Chaos Crystal 6",
                new Location
                {
                    ArchipelagoId = 841,
                    LocationId = "Sheol - Chaos Crystal 6",
                    Description = "Sheol - Chaos Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (5)/",
                }
            },
            {
                "Sheol - Chaos Crystal 7",
                new Location
                {
                    ArchipelagoId = 842,
                    LocationId = "Sheol - Chaos Crystal 7",
                    Description = "Sheol - Chaos Crystal 7",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (6)/",
                }
            },
            {
                "Sheol - Chaos Crystal 8",
                new Location
                {
                    ArchipelagoId = 843,
                    LocationId = "Sheol - Chaos Crystal 8",
                    Description = "Sheol - Chaos Crystal 8",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (7)/",
                }
            },
            {
                "Sheol - Chaos Crystal 9",
                new Location
                {
                    ArchipelagoId = 844,
                    LocationId = "Sheol - Chaos Crystal 9",
                    Description = "Sheol - Chaos Crystal 9",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (8)/",
                }
            },
            {
                "Sheol - Chaos Crystal 10",
                new Location
                {
                    ArchipelagoId = 845,
                    LocationId = "Sheol - Chaos Crystal 10",
                    Description = "Sheol - Chaos Crystal 10",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (9)/",
                }
            },
            {
                "Sheol - Chaos Crystal 11",
                new Location
                {
                    ArchipelagoId = 846,
                    LocationId = "Sheol - Chaos Crystal 11",
                    Description = "Sheol - Chaos Crystal 11",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (10)/",
                }
            },
            {
                "Sheol - Chaos Crystal 12",
                new Location
                {
                    ArchipelagoId = 847,
                    LocationId = "Sheol - Chaos Crystal 12",
                    Description = "Sheol - Chaos Crystal 12",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = "Soulstones/PF_SoulstoneBeat (11)/",
                }
            },
            {
                "Garden of Chronos - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 848,
                    LocationId = "Garden of Chronos - Ammostash 1",
                    Description = "Garden of Chronos - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 849,
                    LocationId = "Garden of Chronos - Ammostash 2",
                    Description = "Garden of Chronos - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 850,
                    LocationId = "Garden of Chronos - Ammostash 3",
                    Description = "Garden of Chronos - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 851,
                    LocationId = "Garden of Chronos - Ammostash 4",
                    Description = "Garden of Chronos - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 852,
                    LocationId = "Garden of Chronos - Ammostash 5",
                    Description = "Garden of Chronos - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 853,
                    LocationId = "Garden of Chronos - Health Crystal 1",
                    Description = "Garden of Chronos - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 854,
                    LocationId = "Garden of Chronos - Health Crystal 2",
                    Description = "Garden of Chronos - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 855,
                    LocationId = "Garden of Chronos - Health Crystal 3",
                    Description = "Garden of Chronos - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 856,
                    LocationId = "Garden of Chronos - Health Crystal 4",
                    Description = "Garden of Chronos - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 857,
                    LocationId = "Garden of Chronos - Health Crystal 5",
                    Description = "Garden of Chronos - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 858,
                    LocationId = "Garden of Chronos - Health Crystal 6",
                    Description = "Garden of Chronos - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 859,
                    LocationId = "Garden of Chronos - Chaos Crystal 1",
                    Description = "Garden of Chronos - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 860,
                    LocationId = "Garden of Chronos - Chaos Crystal 2",
                    Description = "Garden of Chronos - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 861,
                    LocationId = "Garden of Chronos - Chaos Crystal 3",
                    Description = "Garden of Chronos - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 862,
                    LocationId = "Garden of Chronos - Chaos Crystal 4",
                    Description = "Garden of Chronos - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 863,
                    LocationId = "Garden of Chronos - Chaos Crystal 5",
                    Description = "Garden of Chronos - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 864,
                    LocationId = "Garden of Chronos - XP Egg 1",
                    Description = "Garden of Chronos - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 865,
                    LocationId = "Garden of Chronos - XP Egg 2",
                    Description = "Garden of Chronos - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 866,
                    LocationId = "Garden of Chronos - XP Egg 3",
                    Description = "Garden of Chronos - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 867,
                    LocationId = "Garden of Chronos - Nightmare Crystal 1",
                    Description = "Garden of Chronos - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 868,
                    LocationId = "Garden of Chronos - Nightmare Crystal 2",
                    Description = "Garden of Chronos - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 869,
                    LocationId = "Garden of Chronos - Nightmare Crystal 3",
                    Description = "Garden of Chronos - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 870,
                    LocationId = "Garden of Chronos - Nightmare Crystal 4",
                    Description = "Garden of Chronos - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Garden of Chronos - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 871,
                    LocationId = "Garden of Chronos - Nightmare Crystal 5",
                    Description = "Garden of Chronos - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.WalledGarden,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 872,
                    LocationId = "Calamity - Ammostash 1",
                    Description = "Calamity - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 873,
                    LocationId = "Calamity - Ammostash 2",
                    Description = "Calamity - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 874,
                    LocationId = "Calamity - Ammostash 3",
                    Description = "Calamity - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 875,
                    LocationId = "Calamity - Ammostash 4",
                    Description = "Calamity - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 876,
                    LocationId = "Calamity - Ammostash 5",
                    Description = "Calamity - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 877,
                    LocationId = "Calamity - Health Crystal 1",
                    Description = "Calamity - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 878,
                    LocationId = "Calamity - Health Crystal 2",
                    Description = "Calamity - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 879,
                    LocationId = "Calamity - Health Crystal 3",
                    Description = "Calamity - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 880,
                    LocationId = "Calamity - Health Crystal 4",
                    Description = "Calamity - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 881,
                    LocationId = "Calamity - Health Crystal 5",
                    Description = "Calamity - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 882,
                    LocationId = "Calamity - Health Crystal 6",
                    Description = "Calamity - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 883,
                    LocationId = "Calamity - Chaos Crystal 1",
                    Description = "Calamity - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 884,
                    LocationId = "Calamity - Chaos Crystal 2",
                    Description = "Calamity - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 885,
                    LocationId = "Calamity - Chaos Crystal 3",
                    Description = "Calamity - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 886,
                    LocationId = "Calamity - Chaos Crystal 4",
                    Description = "Calamity - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 887,
                    LocationId = "Calamity - Chaos Crystal 5",
                    Description = "Calamity - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 888,
                    LocationId = "Calamity - XP Egg 1",
                    Description = "Calamity - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 889,
                    LocationId = "Calamity - XP Egg 2",
                    Description = "Calamity - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 890,
                    LocationId = "Calamity - XP Egg 3",
                    Description = "Calamity - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 891,
                    LocationId = "Calamity - Nightmare Crystal 1",
                    Description = "Calamity - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 892,
                    LocationId = "Calamity - Nightmare Crystal 2",
                    Description = "Calamity - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 893,
                    LocationId = "Calamity - Nightmare Crystal 3",
                    Description = "Calamity - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 894,
                    LocationId = "Calamity - Nightmare Crystal 4",
                    Description = "Calamity - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Calamity - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 895,
                    LocationId = "Calamity - Nightmare Crystal 5",
                    Description = "Calamity - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.HighRode,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 896,
                    LocationId = "Demonitorium - Ammostash 1",
                    Description = "Demonitorium - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 897,
                    LocationId = "Demonitorium - Ammostash 2",
                    Description = "Demonitorium - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 898,
                    LocationId = "Demonitorium - Ammostash 3",
                    Description = "Demonitorium - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 899,
                    LocationId = "Demonitorium - Ammostash 4",
                    Description = "Demonitorium - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 900,
                    LocationId = "Demonitorium - Ammostash 5",
                    Description = "Demonitorium - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 901,
                    LocationId = "Demonitorium - Health Crystal 1",
                    Description = "Demonitorium - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 902,
                    LocationId = "Demonitorium - Health Crystal 2",
                    Description = "Demonitorium - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 903,
                    LocationId = "Demonitorium - Health Crystal 3",
                    Description = "Demonitorium - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 904,
                    LocationId = "Demonitorium - Health Crystal 4",
                    Description = "Demonitorium - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 905,
                    LocationId = "Demonitorium - Health Crystal 5",
                    Description = "Demonitorium - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 906,
                    LocationId = "Demonitorium - Health Crystal 6",
                    Description = "Demonitorium - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 907,
                    LocationId = "Demonitorium - Chaos Crystal 1",
                    Description = "Demonitorium - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 908,
                    LocationId = "Demonitorium - Chaos Crystal 2",
                    Description = "Demonitorium - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 909,
                    LocationId = "Demonitorium - Chaos Crystal 3",
                    Description = "Demonitorium - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 910,
                    LocationId = "Demonitorium - Chaos Crystal 4",
                    Description = "Demonitorium - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 911,
                    LocationId = "Demonitorium - Chaos Crystal 5",
                    Description = "Demonitorium - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 912,
                    LocationId = "Demonitorium - XP Egg 1",
                    Description = "Demonitorium - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 913,
                    LocationId = "Demonitorium - XP Egg 2",
                    Description = "Demonitorium - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 914,
                    LocationId = "Demonitorium - XP Egg 3",
                    Description = "Demonitorium - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 915,
                    LocationId = "Demonitorium - Nightmare Crystal 1",
                    Description = "Demonitorium - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 916,
                    LocationId = "Demonitorium - Nightmare Crystal 2",
                    Description = "Demonitorium - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 917,
                    LocationId = "Demonitorium - Nightmare Crystal 3",
                    Description = "Demonitorium - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 918,
                    LocationId = "Demonitorium - Nightmare Crystal 4",
                    Description = "Demonitorium - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Demonitorium - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 919,
                    LocationId = "Demonitorium - Nightmare Crystal 5",
                    Description = "Demonitorium - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Bridge,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 920,
                    LocationId = "Tombs of the Ancients - Ammostash 1",
                    Description = "Tombs of the Ancients - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 921,
                    LocationId = "Tombs of the Ancients - Ammostash 2",
                    Description = "Tombs of the Ancients - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 922,
                    LocationId = "Tombs of the Ancients - Ammostash 3",
                    Description = "Tombs of the Ancients - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 923,
                    LocationId = "Tombs of the Ancients - Ammostash 4",
                    Description = "Tombs of the Ancients - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 924,
                    LocationId = "Tombs of the Ancients - Ammostash 5",
                    Description = "Tombs of the Ancients - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 925,
                    LocationId = "Tombs of the Ancients - Health Crystal 1",
                    Description = "Tombs of the Ancients - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 926,
                    LocationId = "Tombs of the Ancients - Health Crystal 2",
                    Description = "Tombs of the Ancients - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 927,
                    LocationId = "Tombs of the Ancients - Health Crystal 3",
                    Description = "Tombs of the Ancients - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 928,
                    LocationId = "Tombs of the Ancients - Health Crystal 4",
                    Description = "Tombs of the Ancients - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 929,
                    LocationId = "Tombs of the Ancients - Health Crystal 5",
                    Description = "Tombs of the Ancients - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 930,
                    LocationId = "Tombs of the Ancients - Health Crystal 6",
                    Description = "Tombs of the Ancients - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 931,
                    LocationId = "Tombs of the Ancients - Chaos Crystal 1",
                    Description = "Tombs of the Ancients - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 932,
                    LocationId = "Tombs of the Ancients - Chaos Crystal 2",
                    Description = "Tombs of the Ancients - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 933,
                    LocationId = "Tombs of the Ancients - Chaos Crystal 3",
                    Description = "Tombs of the Ancients - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 934,
                    LocationId = "Tombs of the Ancients - Chaos Crystal 4",
                    Description = "Tombs of the Ancients - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 935,
                    LocationId = "Tombs of the Ancients - Chaos Crystal 5",
                    Description = "Tombs of the Ancients - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 936,
                    LocationId = "Tombs of the Ancients - XP Egg 1",
                    Description = "Tombs of the Ancients - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 937,
                    LocationId = "Tombs of the Ancients - XP Egg 2",
                    Description = "Tombs of the Ancients - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 938,
                    LocationId = "Tombs of the Ancients - XP Egg 3",
                    Description = "Tombs of the Ancients - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 939,
                    LocationId = "Tombs of the Ancients - Nightmare Crystal 1",
                    Description = "Tombs of the Ancients - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 940,
                    LocationId = "Tombs of the Ancients - Nightmare Crystal 2",
                    Description = "Tombs of the Ancients - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 941,
                    LocationId = "Tombs of the Ancients - Nightmare Crystal 3",
                    Description = "Tombs of the Ancients - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 942,
                    LocationId = "Tombs of the Ancients - Nightmare Crystal 4",
                    Description = "Tombs of the Ancients - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Tombs of the Ancients - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 943,
                    LocationId = "Tombs of the Ancients - Nightmare Crystal 5",
                    Description = "Tombs of the Ancients - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Pyramid,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 944,
                    LocationId = "Necropolis - Ammostash 1",
                    Description = "Necropolis - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 945,
                    LocationId = "Necropolis - Ammostash 2",
                    Description = "Necropolis - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 946,
                    LocationId = "Necropolis - Ammostash 3",
                    Description = "Necropolis - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 947,
                    LocationId = "Necropolis - Ammostash 4",
                    Description = "Necropolis - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 948,
                    LocationId = "Necropolis - Ammostash 5",
                    Description = "Necropolis - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 949,
                    LocationId = "Necropolis - Health Crystal 1",
                    Description = "Necropolis - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 950,
                    LocationId = "Necropolis - Health Crystal 2",
                    Description = "Necropolis - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 951,
                    LocationId = "Necropolis - Health Crystal 3",
                    Description = "Necropolis - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 952,
                    LocationId = "Necropolis - Health Crystal 4",
                    Description = "Necropolis - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 953,
                    LocationId = "Necropolis - Health Crystal 5",
                    Description = "Necropolis - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 954,
                    LocationId = "Necropolis - Health Crystal 6",
                    Description = "Necropolis - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 955,
                    LocationId = "Necropolis - Chaos Crystal 1",
                    Description = "Necropolis - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 956,
                    LocationId = "Necropolis - Chaos Crystal 2",
                    Description = "Necropolis - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 957,
                    LocationId = "Necropolis - Chaos Crystal 3",
                    Description = "Necropolis - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 958,
                    LocationId = "Necropolis - Chaos Crystal 4",
                    Description = "Necropolis - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 959,
                    LocationId = "Necropolis - Chaos Crystal 5",
                    Description = "Necropolis - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 960,
                    LocationId = "Necropolis - XP Egg 1",
                    Description = "Necropolis - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 961,
                    LocationId = "Necropolis - XP Egg 2",
                    Description = "Necropolis - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 962,
                    LocationId = "Necropolis - XP Egg 3",
                    Description = "Necropolis - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 963,
                    LocationId = "Necropolis - Nightmare Crystal 1",
                    Description = "Necropolis - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 964,
                    LocationId = "Necropolis - Nightmare Crystal 2",
                    Description = "Necropolis - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 965,
                    LocationId = "Necropolis - Nightmare Crystal 3",
                    Description = "Necropolis - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 966,
                    LocationId = "Necropolis - Nightmare Crystal 4",
                    Description = "Necropolis - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Necropolis - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 967,
                    LocationId = "Necropolis - Nightmare Crystal 5",
                    Description = "Necropolis - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Monument,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Ammostash 1",
                new Location
                {
                    ArchipelagoId = 968,
                    LocationId = "Axiom - Ammostash 1",
                    Description = "Axiom - Ammostash 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Ammostash 2",
                new Location
                {
                    ArchipelagoId = 969,
                    LocationId = "Axiom - Ammostash 2",
                    Description = "Axiom - Ammostash 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Ammostash 3",
                new Location
                {
                    ArchipelagoId = 970,
                    LocationId = "Axiom - Ammostash 3",
                    Description = "Axiom - Ammostash 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Ammostash 4",
                new Location
                {
                    ArchipelagoId = 971,
                    LocationId = "Axiom - Ammostash 4",
                    Description = "Axiom - Ammostash 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Ammostash 5",
                new Location
                {
                    ArchipelagoId = 972,
                    LocationId = "Axiom - Ammostash 5",
                    Description = "Axiom - Ammostash 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.Ammostash,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 973,
                    LocationId = "Axiom - Health Crystal 1",
                    Description = "Axiom - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 974,
                    LocationId = "Axiom - Health Crystal 2",
                    Description = "Axiom - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 975,
                    LocationId = "Axiom - Health Crystal 3",
                    Description = "Axiom - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 976,
                    LocationId = "Axiom - Health Crystal 4",
                    Description = "Axiom - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 977,
                    LocationId = "Axiom - Health Crystal 5",
                    Description = "Axiom - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 978,
                    LocationId = "Axiom - Health Crystal 6",
                    Description = "Axiom - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Chaos Crystal 1",
                new Location
                {
                    ArchipelagoId = 979,
                    LocationId = "Axiom - Chaos Crystal 1",
                    Description = "Axiom - Chaos Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Chaos Crystal 2",
                new Location
                {
                    ArchipelagoId = 980,
                    LocationId = "Axiom - Chaos Crystal 2",
                    Description = "Axiom - Chaos Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Chaos Crystal 3",
                new Location
                {
                    ArchipelagoId = 981,
                    LocationId = "Axiom - Chaos Crystal 3",
                    Description = "Axiom - Chaos Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Chaos Crystal 4",
                new Location
                {
                    ArchipelagoId = 982,
                    LocationId = "Axiom - Chaos Crystal 4",
                    Description = "Axiom - Chaos Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Chaos Crystal 5",
                new Location
                {
                    ArchipelagoId = 983,
                    LocationId = "Axiom - Chaos Crystal 5",
                    Description = "Axiom - Chaos Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.ChaosCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - XP Egg 1",
                new Location
                {
                    ArchipelagoId = 984,
                    LocationId = "Axiom - XP Egg 1",
                    Description = "Axiom - XP Egg 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - XP Egg 2",
                new Location
                {
                    ArchipelagoId = 985,
                    LocationId = "Axiom - XP Egg 2",
                    Description = "Axiom - XP Egg 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - XP Egg 3",
                new Location
                {
                    ArchipelagoId = 986,
                    LocationId = "Axiom - XP Egg 3",
                    Description = "Axiom - XP Egg 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.XpEgg,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Nightmare Crystal 1",
                new Location
                {
                    ArchipelagoId = 987,
                    LocationId = "Axiom - Nightmare Crystal 1",
                    Description = "Axiom - Nightmare Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Nightmare Crystal 2",
                new Location
                {
                    ArchipelagoId = 988,
                    LocationId = "Axiom - Nightmare Crystal 2",
                    Description = "Axiom - Nightmare Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Nightmare Crystal 3",
                new Location
                {
                    ArchipelagoId = 989,
                    LocationId = "Axiom - Nightmare Crystal 3",
                    Description = "Axiom - Nightmare Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Nightmare Crystal 4",
                new Location
                {
                    ArchipelagoId = 990,
                    LocationId = "Axiom - Nightmare Crystal 4",
                    Description = "Axiom - Nightmare Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Axiom - Nightmare Crystal 5",
                new Location
                {
                    ArchipelagoId = 991,
                    LocationId = "Axiom - Nightmare Crystal 5",
                    Description = "Axiom - Nightmare Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.Ziggurat,
                    LocationType = ELocationType.NightmareCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 1",
                new Location
                {
                    ArchipelagoId = 992,
                    LocationId = "Final Destination - Health Crystal 1",
                    Description = "Final Destination - Health Crystal 1",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 2",
                new Location
                {
                    ArchipelagoId = 993,
                    LocationId = "Final Destination - Health Crystal 2",
                    Description = "Final Destination - Health Crystal 2",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 3",
                new Location
                {
                    ArchipelagoId = 994,
                    LocationId = "Final Destination - Health Crystal 3",
                    Description = "Final Destination - Health Crystal 3",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 4",
                new Location
                {
                    ArchipelagoId = 995,
                    LocationId = "Final Destination - Health Crystal 4",
                    Description = "Final Destination - Health Crystal 4",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 5",
                new Location
                {
                    ArchipelagoId = 996,
                    LocationId = "Final Destination - Health Crystal 5",
                    Description = "Final Destination - Health Crystal 5",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Final Destination - Health Crystal 6",
                new Location
                {
                    ArchipelagoId = 997,
                    LocationId = "Final Destination - Health Crystal 6",
                    Description = "Final Destination - Health Crystal 6",
                    OriginalItemName = "Filler",
                    Zone = EZone.Leviathan,
                    Arena = EArena.FinalDestination,
                    LocationType = ELocationType.HealthCrystal,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 998,
                    LocationId = "Sheol Ammostash Destruction",
                    Description = "Sheol - Complete Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Speed Challenge",
                new Location
                {
                    ArchipelagoId = 999,
                    LocationId = "Sheol Speed Challenge",
                    Description = "Sheol - Complete Speed Challenge",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelSpeed,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1000,
                    LocationId = "Sheol Health Crystal Destruction",
                    Description = "Sheol - Complete Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.LevelHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1001,
                    LocationId = "Sheol Chaos Crystal Destruction",
                    Description = "Sheol - Complete Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.LevelChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1002,
                    LocationId = "Voke Arena 1 Ammostash Destruction",
                    Description = "Voke - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1003,
                    LocationId = "Voke Arena 1 Health Crystal Destruction",
                    Description = "Voke - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1004,
                    LocationId = "Voke Arena 1 Destructible Completion",
                    Description = "Voke - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1005,
                    LocationId = "Voke Arena 2 Ammostash Destruction",
                    Description = "Voke - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1006,
                    LocationId = "Voke Arena 2 Health Crystal Destruction",
                    Description = "Voke - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1007,
                    LocationId = "Voke Arena 2 Chaos Crystal Destruction",
                    Description = "Voke - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1008,
                    LocationId = "Voke Arena 2 Destructible Completion",
                    Description = "Voke - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1009,
                    LocationId = "Voke Arena 3 Ammostash Destruction",
                    Description = "Voke - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1010,
                    LocationId = "Voke Arena 3 Health Crystal Destruction",
                    Description = "Voke - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1011,
                    LocationId = "Voke Arena 3 Chaos Crystal Destruction",
                    Description = "Voke - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1012,
                    LocationId = "Voke Arena 3 Destructible Completion",
                    Description = "Voke - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1013,
                    LocationId = "Voke Arena 4 Ammostash Destruction",
                    Description = "Voke - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1014,
                    LocationId = "Voke Arena 4 Health Crystal Destruction",
                    Description = "Voke - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1015,
                    LocationId = "Voke Arena 4 Chaos Crystal Destruction",
                    Description = "Voke - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1016,
                    LocationId = "Voke Arena 4 Destructible Completion",
                    Description = "Voke - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1017,
                    LocationId = "Voke Boss Ammostash Destruction",
                    Description = "Voke - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1018,
                    LocationId = "Voke Boss Health Crystal Destruction",
                    Description = "Voke - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Boss Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1019,
                    LocationId = "Voke Boss Chaos Crystal Destruction",
                    Description = "Voke - Boss Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Voke Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1020,
                    LocationId = "Voke Boss Destructible Completion",
                    Description = "Voke - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Voke,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1021,
                    LocationId = "Stygia Arena 1 Ammostash Destruction",
                    Description = "Stygia - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1022,
                    LocationId = "Stygia Arena 1 Health Crystal Destruction",
                    Description = "Stygia - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1023,
                    LocationId = "Stygia Arena 1 Destructible Completion",
                    Description = "Stygia - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1024,
                    LocationId = "Stygia Arena 2 Ammostash Destruction",
                    Description = "Stygia - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1025,
                    LocationId = "Stygia Arena 2 Health Crystal Destruction",
                    Description = "Stygia - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1026,
                    LocationId = "Stygia Arena 2 Chaos Crystal Destruction",
                    Description = "Stygia - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1027,
                    LocationId = "Stygia Arena 2 Destructible Completion",
                    Description = "Stygia - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1028,
                    LocationId = "Stygia Arena 3 Ammostash Destruction",
                    Description = "Stygia - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1029,
                    LocationId = "Stygia Arena 3 Health Crystal Destruction",
                    Description = "Stygia - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1030,
                    LocationId = "Stygia Arena 3 Chaos Crystal Destruction",
                    Description = "Stygia - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1031,
                    LocationId = "Stygia Arena 3 Destructible Completion",
                    Description = "Stygia - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1032,
                    LocationId = "Stygia Arena 4 Ammostash Destruction",
                    Description = "Stygia - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1033,
                    LocationId = "Stygia Arena 4 Health Crystal Destruction",
                    Description = "Stygia - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1034,
                    LocationId = "Stygia Arena 4 Chaos Crystal Destruction",
                    Description = "Stygia - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1035,
                    LocationId = "Stygia Arena 4 Destructible Completion",
                    Description = "Stygia - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1036,
                    LocationId = "Stygia Boss Ammostash Destruction",
                    Description = "Stygia - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1037,
                    LocationId = "Stygia Boss Health Crystal Destruction",
                    Description = "Stygia - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Stygia Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1038,
                    LocationId = "Stygia Boss Destructible Completion",
                    Description = "Stygia - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Stygia,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1039,
                    LocationId = "Yhelm Arena 1 Ammostash Destruction",
                    Description = "Yhelm - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1040,
                    LocationId = "Yhelm Arena 1 Health Crystal Destruction",
                    Description = "Yhelm - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1041,
                    LocationId = "Yhelm Arena 1 Destructible Completion",
                    Description = "Yhelm - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1042,
                    LocationId = "Yhelm Arena 2 Ammostash Destruction",
                    Description = "Yhelm - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1043,
                    LocationId = "Yhelm Arena 2 Health Crystal Destruction",
                    Description = "Yhelm - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1044,
                    LocationId = "Yhelm Arena 2 Chaos Crystal Destruction",
                    Description = "Yhelm - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1045,
                    LocationId = "Yhelm Arena 2 Destructible Completion",
                    Description = "Yhelm - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1046,
                    LocationId = "Yhelm Arena 3 Ammostash Destruction",
                    Description = "Yhelm - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1047,
                    LocationId = "Yhelm Arena 3 Health Crystal Destruction",
                    Description = "Yhelm - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1048,
                    LocationId = "Yhelm Arena 3 Chaos Crystal Destruction",
                    Description = "Yhelm - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1049,
                    LocationId = "Yhelm Arena 3 Destructible Completion",
                    Description = "Yhelm - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1050,
                    LocationId = "Yhelm Arena 4 Ammostash Destruction",
                    Description = "Yhelm - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1051,
                    LocationId = "Yhelm Arena 4 Health Crystal Destruction",
                    Description = "Yhelm - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1052,
                    LocationId = "Yhelm Arena 4 Chaos Crystal Destruction",
                    Description = "Yhelm - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1053,
                    LocationId = "Yhelm Arena 4 Destructible Completion",
                    Description = "Yhelm - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1054,
                    LocationId = "Yhelm Boss Ammostash Destruction",
                    Description = "Yhelm - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1055,
                    LocationId = "Yhelm Boss Health Crystal Destruction",
                    Description = "Yhelm - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Boss Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1056,
                    LocationId = "Yhelm Boss Chaos Crystal Destruction",
                    Description = "Yhelm - Boss Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Yhelm Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1057,
                    LocationId = "Yhelm Boss Destructible Completion",
                    Description = "Yhelm - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Yhelm,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1058,
                    LocationId = "Incaustis Arena 1 Ammostash Destruction",
                    Description = "Incaustis - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1059,
                    LocationId = "Incaustis Arena 1 Health Crystal Destruction",
                    Description = "Incaustis - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 1 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1060,
                    LocationId = "Incaustis Arena 1 Chaos Crystal Destruction",
                    Description = "Incaustis - Arena 1 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1061,
                    LocationId = "Incaustis Arena 1 Destructible Completion",
                    Description = "Incaustis - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1062,
                    LocationId = "Incaustis Arena 2 Ammostash Destruction",
                    Description = "Incaustis - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1063,
                    LocationId = "Incaustis Arena 2 Health Crystal Destruction",
                    Description = "Incaustis - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1064,
                    LocationId = "Incaustis Arena 2 Chaos Crystal Destruction",
                    Description = "Incaustis - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1065,
                    LocationId = "Incaustis Arena 2 Destructible Completion",
                    Description = "Incaustis - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1066,
                    LocationId = "Incaustis Arena 3 Ammostash Destruction",
                    Description = "Incaustis - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1067,
                    LocationId = "Incaustis Arena 3 Health Crystal Destruction",
                    Description = "Incaustis - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1068,
                    LocationId = "Incaustis Arena 3 Chaos Crystal Destruction",
                    Description = "Incaustis - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1069,
                    LocationId = "Incaustis Arena 3 Destructible Completion",
                    Description = "Incaustis - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1070,
                    LocationId = "Incaustis Arena 4 Ammostash Destruction",
                    Description = "Incaustis - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1071,
                    LocationId = "Incaustis Arena 4 Health Crystal Destruction",
                    Description = "Incaustis - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1072,
                    LocationId = "Incaustis Arena 4 Chaos Crystal Destruction",
                    Description = "Incaustis - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1073,
                    LocationId = "Incaustis Arena 4 Destructible Completion",
                    Description = "Incaustis - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1074,
                    LocationId = "Incaustis Boss Ammostash Destruction",
                    Description = "Incaustis - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1075,
                    LocationId = "Incaustis Boss Health Crystal Destruction",
                    Description = "Incaustis - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Incaustis Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1076,
                    LocationId = "Incaustis Boss Destructible Completion",
                    Description = "Incaustis - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Incaustis,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1077,
                    LocationId = "Gehenna Arena 1 Ammostash Destruction",
                    Description = "Gehenna - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1078,
                    LocationId = "Gehenna Arena 1 Health Crystal Destruction",
                    Description = "Gehenna - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 1 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1079,
                    LocationId = "Gehenna Arena 1 Chaos Crystal Destruction",
                    Description = "Gehenna - Arena 1 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1080,
                    LocationId = "Gehenna Arena 1 Destructible Completion",
                    Description = "Gehenna - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1081,
                    LocationId = "Gehenna Arena 2 Ammostash Destruction",
                    Description = "Gehenna - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1082,
                    LocationId = "Gehenna Arena 2 Health Crystal Destruction",
                    Description = "Gehenna - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1083,
                    LocationId = "Gehenna Arena 2 Chaos Crystal Destruction",
                    Description = "Gehenna - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1084,
                    LocationId = "Gehenna Arena 2 Destructible Completion",
                    Description = "Gehenna - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1085,
                    LocationId = "Gehenna Arena 3 Ammostash Destruction",
                    Description = "Gehenna - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1086,
                    LocationId = "Gehenna Arena 3 Health Crystal Destruction",
                    Description = "Gehenna - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1087,
                    LocationId = "Gehenna Arena 3 Chaos Crystal Destruction",
                    Description = "Gehenna - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1088,
                    LocationId = "Gehenna Arena 3 Destructible Completion",
                    Description = "Gehenna - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1089,
                    LocationId = "Gehenna Arena 4 Ammostash Destruction",
                    Description = "Gehenna - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1090,
                    LocationId = "Gehenna Arena 4 Health Crystal Destruction",
                    Description = "Gehenna - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1091,
                    LocationId = "Gehenna Arena 4 Chaos Crystal Destruction",
                    Description = "Gehenna - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1092,
                    LocationId = "Gehenna Arena 4 Destructible Completion",
                    Description = "Gehenna - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1093,
                    LocationId = "Gehenna Boss Ammostash Destruction",
                    Description = "Gehenna - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1094,
                    LocationId = "Gehenna Boss Health Crystal Destruction",
                    Description = "Gehenna - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Gehenna Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1095,
                    LocationId = "Gehenna Boss Destructible Completion",
                    Description = "Gehenna - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Gehenna,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1096,
                    LocationId = "Nihil Arena 1 Ammostash Destruction",
                    Description = "Nihil - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1097,
                    LocationId = "Nihil Arena 1 Health Crystal Destruction",
                    Description = "Nihil - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 1 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1098,
                    LocationId = "Nihil Arena 1 Chaos Crystal Destruction",
                    Description = "Nihil - Arena 1 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1099,
                    LocationId = "Nihil Arena 1 Destructible Completion",
                    Description = "Nihil - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1100,
                    LocationId = "Nihil Arena 2 Ammostash Destruction",
                    Description = "Nihil - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1101,
                    LocationId = "Nihil Arena 2 Health Crystal Destruction",
                    Description = "Nihil - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1102,
                    LocationId = "Nihil Arena 2 Destructible Completion",
                    Description = "Nihil - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1103,
                    LocationId = "Nihil Arena 3 Ammostash Destruction",
                    Description = "Nihil - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1104,
                    LocationId = "Nihil Arena 3 Health Crystal Destruction",
                    Description = "Nihil - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1105,
                    LocationId = "Nihil Arena 3 Chaos Crystal Destruction",
                    Description = "Nihil - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1106,
                    LocationId = "Nihil Arena 3 Destructible Completion",
                    Description = "Nihil - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1107,
                    LocationId = "Nihil Arena 4 Ammostash Destruction",
                    Description = "Nihil - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1108,
                    LocationId = "Nihil Arena 4 Health Crystal Destruction",
                    Description = "Nihil - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1109,
                    LocationId = "Nihil Arena 4 Destructible Completion",
                    Description = "Nihil - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1110,
                    LocationId = "Nihil Boss Ammostash Destruction",
                    Description = "Nihil - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1111,
                    LocationId = "Nihil Boss Health Crystal Destruction",
                    Description = "Nihil - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Boss Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1112,
                    LocationId = "Nihil Boss Chaos Crystal Destruction",
                    Description = "Nihil - Boss Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Nihil Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1113,
                    LocationId = "Nihil Boss Destructible Completion",
                    Description = "Nihil - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Nihil,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1114,
                    LocationId = "Acheron Arena 1 Ammostash Destruction",
                    Description = "Acheron - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1115,
                    LocationId = "Acheron Arena 1 Health Crystal Destruction",
                    Description = "Acheron - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 1 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1116,
                    LocationId = "Acheron Arena 1 Chaos Crystal Destruction",
                    Description = "Acheron - Arena 1 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1117,
                    LocationId = "Acheron Arena 1 Destructible Completion",
                    Description = "Acheron - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1118,
                    LocationId = "Acheron Arena 2 Ammostash Destruction",
                    Description = "Acheron - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1119,
                    LocationId = "Acheron Arena 2 Health Crystal Destruction",
                    Description = "Acheron - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1120,
                    LocationId = "Acheron Arena 2 Chaos Crystal Destruction",
                    Description = "Acheron - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1121,
                    LocationId = "Acheron Arena 2 Destructible Completion",
                    Description = "Acheron - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1122,
                    LocationId = "Acheron Arena 3 Ammostash Destruction",
                    Description = "Acheron - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1123,
                    LocationId = "Acheron Arena 3 Health Crystal Destruction",
                    Description = "Acheron - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1124,
                    LocationId = "Acheron Arena 3 Chaos Crystal Destruction",
                    Description = "Acheron - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1125,
                    LocationId = "Acheron Arena 3 Destructible Completion",
                    Description = "Acheron - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1126,
                    LocationId = "Acheron Arena 4 Ammostash Destruction",
                    Description = "Acheron - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1127,
                    LocationId = "Acheron Arena 4 Health Crystal Destruction",
                    Description = "Acheron - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1128,
                    LocationId = "Acheron Arena 4 Chaos Crystal Destruction",
                    Description = "Acheron - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1129,
                    LocationId = "Acheron Arena 4 Destructible Completion",
                    Description = "Acheron - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1130,
                    LocationId = "Acheron Boss Health Crystal Destruction",
                    Description = "Acheron - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Acheron Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1131,
                    LocationId = "Acheron Boss Destructible Completion",
                    Description = "Acheron - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Acheron,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 1 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1132,
                    LocationId = "Sheol Arena 1 Ammostash Destruction",
                    Description = "Sheol - Arena 1 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 1 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1133,
                    LocationId = "Sheol Arena 1 Health Crystal Destruction",
                    Description = "Sheol - Arena 1 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 1 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1134,
                    LocationId = "Sheol Arena 1 Chaos Crystal Destruction",
                    Description = "Sheol - Arena 1 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 1 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1135,
                    LocationId = "Sheol Arena 1 Destructible Completion",
                    Description = "Sheol - Arena 1 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena1,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 2 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1136,
                    LocationId = "Sheol Arena 2 Ammostash Destruction",
                    Description = "Sheol - Arena 2 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 2 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1137,
                    LocationId = "Sheol Arena 2 Health Crystal Destruction",
                    Description = "Sheol - Arena 2 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 2 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1138,
                    LocationId = "Sheol Arena 2 Chaos Crystal Destruction",
                    Description = "Sheol - Arena 2 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 2 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1139,
                    LocationId = "Sheol Arena 2 Destructible Completion",
                    Description = "Sheol - Arena 2 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena2,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 3 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1140,
                    LocationId = "Sheol Arena 3 Ammostash Destruction",
                    Description = "Sheol - Arena 3 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 3 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1141,
                    LocationId = "Sheol Arena 3 Health Crystal Destruction",
                    Description = "Sheol - Arena 3 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 3 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1142,
                    LocationId = "Sheol Arena 3 Chaos Crystal Destruction",
                    Description = "Sheol - Arena 3 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 3 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1144,
                    LocationId = "Sheol Arena 3 Destructible Completion",
                    Description = "Sheol - Arena 3 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena3,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 4 Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1145,
                    LocationId = "Sheol Arena 4 Ammostash Destruction",
                    Description = "Sheol - Arena 4 Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 4 Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1146,
                    LocationId = "Sheol Arena 4 Health Crystal Destruction",
                    Description = "Sheol - Arena 4 Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 4 Chaos Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1147,
                    LocationId = "Sheol Arena 4 Chaos Crystal Destruction",
                    Description = "Sheol - Arena 4 Chaos Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaChaosCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Arena 4 Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1148,
                    LocationId = "Sheol Arena 4 Destructible Completion",
                    Description = "Sheol - Arena 4 Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Arena4,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Boss Ammostash Destruction",
                new Location
                {
                    ArchipelagoId = 1149,
                    LocationId = "Sheol Boss Ammostash Destruction",
                    Description = "Sheol - Boss Ammostash Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaAmmostashCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Boss Health Crystal Destruction",
                new Location
                {
                    ArchipelagoId = 1150,
                    LocationId = "Sheol Boss Health Crystal Destruction",
                    Description = "Sheol - Boss Health Crystal Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaHealthCrystalCompletion,
                    GameObjectName = null,
                }
            },
            {
                "Sheol Boss Destructible Completion",
                new Location
                {
                    ArchipelagoId = 1151,
                    LocationId = "Sheol Boss Destructible Completion",
                    Description = "Sheol - Boss Destructible Completion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Sheol,
                    Arena = EArena.Boss,
                    LocationType = ELocationType.ArenaDestructibleCompletion,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Ammostash",
                new Location
                {
                    ArchipelagoId = 1152,
                    LocationId = "First Miscellaneous - Ammostash",
                    Description = "First Miscellaneous - Ammostash",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Health Crystal",
                new Location
                {
                    ArchipelagoId = 1153,
                    LocationId = "First Miscellaneous - Health Crystal",
                    Description = "First Miscellaneous - Health Crystal",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Chaos Crystal",
                new Location
                {
                    ArchipelagoId = 1154,
                    LocationId = "First Miscellaneous - Chaos Crystal",
                    Description = "First Miscellaneous - Chaos Crystal",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Paz",
                new Location
                {
                    ArchipelagoId = 1155,
                    LocationId = "Section Cleared with: Paz",
                    Description = "Section Cleared with: Paz",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Terminus",
                new Location
                {
                    ArchipelagoId = 1156,
                    LocationId = "Section Cleared with: Terminus",
                    Description = "Section Cleared with: Terminus",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Persephone",
                new Location
                {
                    ArchipelagoId = 1157,
                    LocationId = "Section Cleared with: Persephone",
                    Description = "Section Cleared with: Persephone",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: The Hounds",
                new Location
                {
                    ArchipelagoId = 1158,
                    LocationId = "Section Cleared with: The Hounds",
                    Description = "Section Cleared with: The Hounds",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Vulcan",
                new Location
                {
                    ArchipelagoId = 1159,
                    LocationId = "Section Cleared with: Vulcan",
                    Description = "Section Cleared with: Vulcan",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Hellcrow",
                new Location
                {
                    ArchipelagoId = 1160,
                    LocationId = "Section Cleared with: Hellcrow",
                    Description = "Section Cleared with: Hellcrow",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: The Red Right Hand",
                new Location
                {
                    ArchipelagoId = 1161,
                    LocationId = "Section Cleared with: The Red Right Hand",
                    Description = "Section Cleared with: The Red Right Hand",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Telos",
                new Location
                {
                    ArchipelagoId = 1162,
                    LocationId = "Section Cleared with: Telos",
                    Description = "Section Cleared with: Telos",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Lost Persephone",
                new Location
                {
                    ArchipelagoId = 1163,
                    LocationId = "Section Cleared with: Lost Persephone",
                    Description = "Section Cleared with: Lost Persephone",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Manifested Persephone",
                new Location
                {
                    ArchipelagoId = 1164,
                    LocationId = "Section Cleared with: Manifested Persephone",
                    Description = "Section Cleared with: Manifested Persephone",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: The Lost Hounds",
                new Location
                {
                    ArchipelagoId = 1165,
                    LocationId = "Section Cleared with: The Lost Hounds",
                    Description = "Section Cleared with: The Lost Hounds",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Lost Vulcan",
                new Location
                {
                    ArchipelagoId = 1166,
                    LocationId = "Section Cleared with: Lost Vulcan",
                    Description = "Section Cleared with: Lost Vulcan",
                    OriginalItemName = "Filler",
                    Zone = EZone.Weapon,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearWeapon,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Unknown",
                new Location
                {
                    ArchipelagoId = 1167,
                    LocationId = "Section Cleared with: Outfit of the Unknown",
                    Description = "Section Cleared with: Outfit of the Unknown",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Leviathan",
                new Location
                {
                    ArchipelagoId = 1168,
                    LocationId = "Section Cleared with: Outfit of the Leviathan",
                    Description = "Section Cleared with: Outfit of the Leviathan",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Dark Devotee",
                new Location
                {
                    ArchipelagoId = 1169,
                    LocationId = "Section Cleared with: Outfit of the Dark Devotee",
                    Description = "Section Cleared with: Outfit of the Dark Devotee",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Morning Star",
                new Location
                {
                    ArchipelagoId = 1170,
                    LocationId = "Section Cleared with: Outfit of the Morning Star",
                    Description = "Section Cleared with: Outfit of the Morning Star",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Angel Eyes",
                new Location
                {
                    ArchipelagoId = 1171,
                    LocationId = "Section Cleared with: Outfit of the Angel Eyes",
                    Description = "Section Cleared with: Outfit of the Angel Eyes",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Obsidian",
                new Location
                {
                    ArchipelagoId = 1172,
                    LocationId = "Section Cleared with: Outfit of the Obsidian",
                    Description = "Section Cleared with: Outfit of the Obsidian",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Amethyst",
                new Location
                {
                    ArchipelagoId = 1173,
                    LocationId = "Section Cleared with: Outfit of the Amethyst",
                    Description = "Section Cleared with: Outfit of the Amethyst",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Outfit of the Chromatica",
                new Location
                {
                    ArchipelagoId = 1174,
                    LocationId = "Section Cleared with: Outfit of the Chromatica",
                    Description = "Section Cleared with: Outfit of the Chromatica",
                    OriginalItemName = "Filler",
                    Zone = EZone.Outfit,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearOutfit,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: This is the End",
                new Location
                {
                    ArchipelagoId = 1175,
                    LocationId = "Section Cleared with: This is the End",
                    Description = "Section Cleared with: This is the End",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Stygia (Song)",
                new Location
                {
                    ArchipelagoId = 1176,
                    LocationId = "Section Cleared with: Stygia (Song)",
                    Description = "Section Cleared with: Stygia (Song)",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Burial At Night",
                new Location
                {
                    ArchipelagoId = 1177,
                    LocationId = "Section Cleared with: Burial At Night",
                    Description = "Section Cleared with: Burial At Night",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: This Devastation",
                new Location
                {
                    ArchipelagoId = 1178,
                    LocationId = "Section Cleared with: This Devastation",
                    Description = "Section Cleared with: This Devastation",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Poetry of Cinder",
                new Location
                {
                    ArchipelagoId = 1179,
                    LocationId = "Section Cleared with: Poetry of Cinder",
                    Description = "Section Cleared with: Poetry of Cinder",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Dissolution",
                new Location
                {
                    ArchipelagoId = 1180,
                    LocationId = "Section Cleared with: Dissolution",
                    Description = "Section Cleared with: Dissolution",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Acheron (Song)",
                new Location
                {
                    ArchipelagoId = 1181,
                    LocationId = "Section Cleared with: Acheron (Song)",
                    Description = "Section Cleared with: Acheron (Song)",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Silent No More",
                new Location
                {
                    ArchipelagoId = 1182,
                    LocationId = "Section Cleared with: Silent No More",
                    Description = "Section Cleared with: Silent No More",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Blood and Law",
                new Location
                {
                    ArchipelagoId = 1183,
                    LocationId = "Section Cleared with: Blood and Law",
                    Description = "Section Cleared with: Blood and Law",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearBossSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Infernal Invocation I: Hopes and Fears",
                new Location
                {
                    ArchipelagoId = 1184,
                    LocationId = "Section Cleared with: Infernal Invocation I: Hopes and Fears",
                    Description = "Section Cleared with: Infernal Invocation I: Hopes and Fears",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearBossSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Infernal Invocation II: Defiance",
                new Location
                {
                    ArchipelagoId = 1185,
                    LocationId = "Section Cleared with: Infernal Invocation II: Defiance",
                    Description = "Section Cleared with: Infernal Invocation II: Defiance",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearBossSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Infernal Invocation III: Dreaming in Distortion",
                new Location
                {
                    ArchipelagoId = 1186,
                    LocationId = "Section Cleared with: Infernal Invocation III: Dreaming in Distortion",
                    Description = "Section Cleared with: Infernal Invocation III: Dreaming in Distortion",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearBossSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: No Tomorrow",
                new Location
                {
                    ArchipelagoId = 1187,
                    LocationId = "Section Cleared with: No Tomorrow",
                    Description = "Section Cleared with: No Tomorrow",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Basegame,
                    LocationType = ELocationType.SectionClearBossSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Leviathan (Song)",
                new Location
                {
                    ArchipelagoId = 1188,
                    LocationId = "Section Cleared with: Leviathan (Song)",
                    Description = "Section Cleared with: Leviathan (Song)",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Dream of the Beast",
                new Location
                {
                    ArchipelagoId = 1189,
                    LocationId = "Section Cleared with: Dream of the Beast",
                    Description = "Section Cleared with: Dream of the Beast",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DreamOfTheBeast,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Swallow the Fire",
                new Location
                {
                    ArchipelagoId = 1190,
                    LocationId = "Section Cleared with: Swallow the Fire",
                    Description = "Section Cleared with: Swallow the Fire",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Mouth of Hell",
                new Location
                {
                    ArchipelagoId = 1191,
                    LocationId = "Section Cleared with: Mouth of Hell",
                    Description = "Section Cleared with: Mouth of Hell",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Goodbye, Morning Star",
                new Location
                {
                    ArchipelagoId = 1192,
                    LocationId = "Section Cleared with: Goodbye, Morning Star",
                    Description = "Section Cleared with: Goodbye, Morning Star",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.Purgatory,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Departure to Destruction",
                new Location
                {
                    ArchipelagoId = 1193,
                    LocationId = "Section Cleared with: Departure to Destruction",
                    Description = "Section Cleared with: Departure to Destruction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Hand Cannon",
                new Location
                {
                    ArchipelagoId = 1194,
                    LocationId = "Section Cleared with: Hand Cannon",
                    Description = "Section Cleared with: Hand Cannon",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Burn in Hell",
                new Location
                {
                    ArchipelagoId = 1195,
                    LocationId = "Section Cleared with: Burn in Hell",
                    Description = "Section Cleared with: Burn in Hell",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Murder Machine Inc",
                new Location
                {
                    ArchipelagoId = 1196,
                    LocationId = "Section Cleared with: Murder Machine Inc",
                    Description = "Section Cleared with: Murder Machine Inc",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Endless",
                new Location
                {
                    ArchipelagoId = 1197,
                    LocationId = "Section Cleared with: Endless",
                    Description = "Section Cleared with: Endless",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Mine Control",
                new Location
                {
                    ArchipelagoId = 1198,
                    LocationId = "Section Cleared with: Mine Control",
                    Description = "Section Cleared with: Mine Control",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Sacrifice",
                new Location
                {
                    ArchipelagoId = 1199,
                    LocationId = "Section Cleared with: Sacrifice",
                    Description = "Section Cleared with: Sacrifice",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Erebus Reaction",
                new Location
                {
                    ArchipelagoId = 1200,
                    LocationId = "Section Cleared with: Erebus Reaction",
                    Description = "Section Cleared with: Erebus Reaction",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Bleeding Out",
                new Location
                {
                    ArchipelagoId = 1201,
                    LocationId = "Section Cleared with: Bleeding Out",
                    Description = "Section Cleared with: Bleeding Out",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.DuskSoundtrack,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Down With the Sickness",
                new Location
                {
                    ArchipelagoId = 1202,
                    LocationId = "Section Cleared with: Down With the Sickness",
                    Description = "Section Cleared with: Down With the Sickness",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Uprising",
                new Location
                {
                    ArchipelagoId = 1203,
                    LocationId = "Section Cleared with: Uprising",
                    Description = "Section Cleared with: Uprising",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Misery Business",
                new Location
                {
                    ArchipelagoId = 1204,
                    LocationId = "Section Cleared with: Misery Business",
                    Description = "Section Cleared with: Misery Business",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Tsunami (Original Mix)",
                new Location
                {
                    ArchipelagoId = 1205,
                    LocationId = "Section Cleared with: Tsunami (Original Mix)",
                    Description = "Section Cleared with: Tsunami (Original Mix)",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Runaway (U&I)",
                new Location
                {
                    ArchipelagoId = 1206,
                    LocationId = "Section Cleared with: Runaway (U&I)",
                    Description = "Section Cleared with: Runaway (U&I)",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Feel Good Inc.",
                new Location
                {
                    ArchipelagoId = 1207,
                    LocationId = "Section Cleared with: Feel Good Inc.",
                    Description = "Section Cleared with: Feel Good Inc.",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: I Love It feat. Charli XCX",
                new Location
                {
                    ArchipelagoId = 1208,
                    LocationId = "Section Cleared with: I Love It feat. Charli XCX",
                    Description = "Section Cleared with: I Love It feat. Charli XCX",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "Section Cleared with: Personal Jesus",
                new Location
                {
                    ArchipelagoId = 1209,
                    LocationId = "Section Cleared with: Personal Jesus",
                    Description = "Section Cleared with: Personal Jesus",
                    OriginalItemName = "Filler",
                    Zone = EZone.Song,
                    Arena = EArena.EssentialHits,
                    LocationType = ELocationType.SectionClearMainSong,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Slaughter",
                new Location
                {
                    ArchipelagoId = 1210,
                    LocationId = "First Miscellaneous - Slaughter",
                    Description = "First Miscellaneous - Slaughter",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "Styx Reload discovered",
                new Location
                {
                    ArchipelagoId = 1211,
                    LocationId = "Styx Reload discovered",
                    Description = "Fury Combo - Styx Reload discovered",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Codex,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Jump",
                new Location
                {
                    ArchipelagoId = 1212,
                    LocationId = "First Miscellaneous - Jump",
                    Description = "First Miscellaneous - Jump",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Double Jump",
                new Location
                {
                    ArchipelagoId = 1213,
                    LocationId = "First Miscellaneous - Double Jump",
                    Description = "First Miscellaneous - Double Jump",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Infinite Jump",
                new Location
                {
                    ArchipelagoId = 1214,
                    LocationId = "First Miscellaneous - Infinite Jump",
                    Description = "First Miscellaneous - Infinite Jump",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Quick Reload",
                new Location
                {
                    ArchipelagoId = 1215,
                    LocationId = "First Miscellaneous - Quick Reload",
                    Description = "First Miscellaneous - Quick Reload",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Dash",
                new Location
                {
                    ArchipelagoId = 1216,
                    LocationId = "First Miscellaneous - Dash",
                    Description = "First Miscellaneous - Dash",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Soar",
                new Location
                {
                    ArchipelagoId = 1217,
                    LocationId = "First Miscellaneous - Soar",
                    Description = "First Miscellaneous - Soar",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Enduring Fury",
                new Location
                {
                    ArchipelagoId = 1218,
                    LocationId = "First Miscellaneous - Enduring Fury",
                    Description = "Activate Enduring Fury for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Faster Ultimate Gain",
                new Location
                {
                    ArchipelagoId = 1219,
                    LocationId = "First Miscellaneous - Faster Ultimate Gain",
                    Description = "Activate Faster Ultimate Gain for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Deadlier Dash",
                new Location
                {
                    ArchipelagoId = 1220,
                    LocationId = "First Miscellaneous - Deadlier Dash",
                    Description = "Activate Deadlier Dash for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Explosive Slaughter",
                new Location
                {
                    ArchipelagoId = 1221,
                    LocationId = "First Miscellaneous - Explosive Slaughter",
                    Description = "Activate Explosive Slaughter for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.Boon,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Paz Ultimate",
                new Location
                {
                    ArchipelagoId = 1222,
                    LocationId = "First Miscellaneous - Paz Ultimate",
                    Description = "Activate Paz' Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Terminus Ultimate",
                new Location
                {
                    ArchipelagoId = 1223,
                    LocationId = "First Miscellaneous - Terminus Ultimate",
                    Description = "Activate Terminus' Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Persephone Ultimate",
                new Location
                {
                    ArchipelagoId = 1224,
                    LocationId = "First Miscellaneous - Persephone Ultimate",
                    Description = "Activate Persephones Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - The Hounds Ultimate",
                new Location
                {
                    ArchipelagoId = 1225,
                    LocationId = "First Miscellaneous - The Hounds Ultimate",
                    Description = "Activate the Hounds Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Vulcan Ultimate",
                new Location
                {
                    ArchipelagoId = 1226,
                    LocationId = "First Miscellaneous - Vulcan Ultimate",
                    Description = "Activate Vulcans Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Hellcrow Ultimate",
                new Location
                {
                    ArchipelagoId = 1227,
                    LocationId = "First Miscellaneous - Hellcrow Ultimate",
                    Description = "Activate Hellcrows Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - The Red Right Hand Ultimate",
                new Location
                {
                    ArchipelagoId = 1228,
                    LocationId = "First Miscellaneous - The Red Right Hand Ultimate",
                    Description = "Activate the Red Right Hands Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
            {
                "First Miscellaneous - Telos Ultimate",
                new Location
                {
                    ArchipelagoId = 1229,
                    LocationId = "First Miscellaneous - Telos Ultimate",
                    Description = "Activate Telos' Ultimate for the first time",
                    OriginalItemName = "Filler",
                    Zone = EZone.Global,
                    Arena = EArena.Global,
                    LocationType = ELocationType.FirstMiscellaneous,
                    GameObjectName = null,
                }
            },
        };
        public static readonly Dictionary<long, Location> LocationDataById =
            LocationDataByName.Values.ToDictionary(loc => loc.ArchipelagoId, loc => loc);
    }
}
