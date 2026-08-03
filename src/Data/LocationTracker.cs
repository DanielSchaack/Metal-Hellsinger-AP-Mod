using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Outsiders.GUI;
using UnityEngine;
using static Randomizer.Locations;
using static Randomizer.Lookup;
using static Randomizer.Settings;

namespace Randomizer
{
    public class LocationTracker
    {
        public List<Location> LocationsCollected = new List<Location>();
        public Dictionary<string, bool> CheckedLocations = new Dictionary<string, bool>();

        public LocationTracker()
        {
            Reset();
        }

        private static readonly Dictionary<string, EZone> LevelIdToZoneDictionary = new Dictionary<
            string,
            EZone
        >()
        {
            { "Tutorial", EZone.Tutorial },
            { "Voke", EZone.Voke },
            { "Stygia", EZone.Stygia },
            { "Yhelm", EZone.Yhelm },
            { "Incaustis", EZone.Incaustis },
            { "Gehenna", EZone.Gehenna },
            { "Nihil", EZone.Nihil },
            { "Acheron", EZone.Acheron },
            { "Sheol", EZone.Sheol },
            { "CH_Amdusias1", EZone.KillingWithRhythm },
            { "CH_Marbas1", EZone.WeaponTrickery },
            { "CH_Halphas1", EZone.RelicThief },
            { "CH_Bune1", EZone.Giantslayer },
            { "CH_Morax1", EZone.DeathsEdge },
            { "CH_Halphas2", EZone.RelicThief },
            { "CH_Flauros1", EZone.UltimateMastery },
            { "CH_Amdusias2", EZone.KillingWithRhythm },
            { "CH_Marbas2", EZone.WeaponTrickery },
            { "CH_Glasya1", EZone.SlaughterMastery },
            { "CH_Bune2", EZone.Giantslayer },
            { "CH_Halphas3", EZone.RelicThief },
            { "CH_Morax2", EZone.DeathsEdge },
            { "CH_Amdusias3", EZone.KillingWithRhythm },
            { "CH_Marbas3", EZone.WeaponTrickery },
            { "CH_Flauros2", EZone.UltimateMastery },
            { "CH_Glasya2", EZone.SlaughterMastery },
            { "CH_Bune3", EZone.Giantslayer },
            { "CH_Morax3", EZone.DeathsEdge },
            { "CH_Flauros3", EZone.UltimateMastery },
            { "CH_Glasya3", EZone.SlaughterMastery },
        };

        private static readonly Dictionary<string, string> WorldItemToLocationName = new Dictionary<
            string,
            string
        >()
        {
            { "WI_Marionette", "Marionette discovered" },
            { "WI_Cambion", "Cambion discovered" },
            { "WI_Reaver", "Behemoth discovered" },
            { "WI_Stalker", "Stalker discovered" },
            { "WI_Eyeless", "Eyeless discovered" },
            { "WI_Hierophant", "Hierophant discovered" },
            { "WI_LesserSeraph", "Lesser Seraph discovered" },
            { "WI_Elite_Cambion", "Shield Cambion discovered" },
            { "WI_Elite_Reaver", "Siege Behemoth discovered" },
            { "WI_Elite_Stalker", "Void Stalker discovered" },
            { "WI_Elite_LesserSeraph", "Annihilator Seraph discovered" },
            { "WI_Boss_Voke", "Anger Aspect: Voke discovered" },
            { "WI_Boss_Stygia", "Charged Aspect: Stygia discovered" },
            { "WI_Boss_Yhelm", "Fortress Aspect: Yhelm discovered" },
            { "WI_Boss_Incaustis", "Infernal Fury Aspect: Incaustis discovered" },
            { "WI_Boss_Gehenna", "Hellstorm Aspect: Gehenna discovered" },
            { "WI_Boss_Nihil", "DoppelGanger Aspect: Nihil discovered" },
            { "WI_Boss_Acheron", "Wheel Aspect: Acheron discovered" },
            { "WI_Boss_Sheol", "Red Judge - Worldbreaker: Sheol discovered" },
            { "WI_LostUnknown", "The Lost Unknown: Leviathan discovered" },
        };

        private static readonly Dictionary<string, string> LevelToDefeatedBossLocationName =
            new Dictionary<string, string>()
            {
                { "Voke", "Anger Aspect: Voke defeated" },
                { "Stygia", "Charged Aspect: Stygia defeated" },
                { "Yhelm", "Fortress Aspect: Yhelm defeated" },
                { "Incaustis", "Infernal Fury Aspect: Incaustis defeated" },
                { "Gehenna", "Hellstorm Aspect: Gehenna defeated" },
                { "Nihil", "DoppelGanger Aspect: Nihil defeated" },
                { "Acheron", "Wheel Aspect: Acheron defeated" },
                { "Sheol", "Red Judge - Worldbreaker: Sheol defeated" },
            };

        private static readonly Dictionary<string, string> CoatOfArmToLocationName = new Dictionary<
            string,
            string
        >()
        {
            { "Collectible_Voke_Easy", "Voke Coat of Arms Easy" },
            { "Collectible_Voke_Medium", "Voke Coat of Arms Medium" },
            { "Collectible_Voke_Hard", "Voke Coat of Arms Hard" },
            { "Collectible_Voke_VeryHard", "Voke Coat of Arms VeryHard" },
            { "Collectible_Stygia_Easy", "Stygia Coat of Arms Easy" },
            { "Collectible_Stygia_Medium", "Stygia Coat of Arms Medium" },
            { "Collectible_Stygia_Hard", "Stygia Coat of Arms Hard" },
            { "Collectible_Stygia_VeryHard", "Stygia Coat of Arms VeryHard" },
            { "Collectible_Yhelm_Easy", "Yhelm Coat of Arms Easy" },
            { "Collectible_Yhelm_Medium", "Yhelm Coat of Arms Medium" },
            { "Collectible_Yhelm_Hard", "Yhelm Coat of Arms Hard" },
            { "Collectible_Yhelm_VeryHard", "Yhelm Coat of Arms VeryHard" },
            { "Collectible_Gehenna_Easy", "Gehenna Coat of Arms Easy" },
            { "Collectible_Gehenna_Medium", "Gehenna Coat of Arms Medium" },
            { "Collectible_Gehenna_Hard", "Gehenna Coat of Arms Hard" },
            { "Collectible_Gehenna_VeryHard", "Gehenna Coat of Arms VeryHard" },
            { "Collectible_Incaustis_Easy", "Incaustis Coat of Arms Easy" },
            { "Collectible_Incaustis_Medium", "Incaustis Coat of Arms Medium" },
            { "Collectible_Incaustis_Hard", "Incaustis Coat of Arms Hard" },
            { "Collectible_Incaustis_VeryHard", "Incaustis Coat of Arms VeryHard" },
            { "Collectible_Nihil_Easy", "Nihil Coat of Arms Easy" },
            { "Collectible_Nihil_Medium", "Nihil Coat of Arms Medium" },
            { "Collectible_Nihil_Hard", "Nihil Coat of Arms Hard" },
            { "Collectible_Nihil_VeryHard", "Nihil Coat of Arms VeryHard" },
            { "Collectible_Acheron_Easy", "Acheron Coat of Arms Easy" },
            { "Collectible_Acheron_Medium", "Acheron Coat of Arms Medium" },
            { "Collectible_Acheron_Hard", "Acheron Coat of Arms Hard" },
            { "Collectible_Acheron_VeryHard", "Acheron Coat of Arms VeryHard" },
            { "Collectible_Sheol_Easy", "Sheol Coat of Arms Easy" },
            { "Collectible_Sheol_Medium", "Sheol Coat of Arms Medium" },
            { "Collectible_Sheol_Hard", "Sheol Coat of Arms Hard" },
            { "Collectible_Sheol_VeryHard", "Sheol Coat of Arms VeryHard" },
        };

        private static readonly Dictionary<EFuryComboType, string> FuryComboToLocationName =
            new Dictionary<EFuryComboType, string>()
            {
                { EFuryComboType.ActiveReloadHealth, "Styx Reload discovered" },
                { EFuryComboType.ActiveReloadKill, "Hells's Heartbeat discovered" },
                { EFuryComboType.DashKill, "Basilisk Mode discovered" },
                { EFuryComboType.DestructDash, "Double Hit and Run discovered" },
                { EFuryComboType.DoubleDestructible, "Shatter Two discovered" },
                { EFuryComboType.DoubleJumpDash, "Devil's Flight discovered" },
                { EFuryComboType.DoubleOverkill, "Double Slaughter discovered" },
                { EFuryComboType.ExplosionSlaughter, "Chaos and Slaughter discovered" },
                { EFuryComboType.KillSlaughter, "Unholy Mess discovered" },
                { EFuryComboType.ManySlaughters, "Five Endings discovered" },
                { EFuryComboType.SlaughterKill, "Slaughter and Kill discovered" },
                { EFuryComboType.SoarJumpDestruct, "Chaos Flight discovered" },
                { EFuryComboType.SoarSlaughter, "Death from Above discovered" },
                { EFuryComboType.SwitchDamage, "Lethal Cycle discovered" },
                { EFuryComboType.TripleKill, "Kill Trio discovered" },
                { EFuryComboType.TrippleDash, "Triple Dash discovered" },
            };

        private static readonly Dictionary<string, int> LocationDestructionCountRequired =
            new Dictionary<string, int>()
            {
                { "Voke Ammostash Arena1", 3 },
                { "Voke Ammostash Arena2", 3 },
                { "Voke Ammostash Arena3", 5 },
                { "Voke Ammostash Arena4", 2 },
                { "Voke Ammostash Boss", 4 },
                { "Voke HealthCrystal Arena1", 4 },
                { "Voke HealthCrystal Arena2", 2 },
                { "Voke HealthCrystal Arena3", 3 },
                { "Voke HealthCrystal Arena4", 3 },
                { "Voke HealthCrystal Boss", 4 },
                // { "Voke ChaosCrystal Arena1", 0},
                { "Voke ChaosCrystal Arena2", 2 },
                { "Voke ChaosCrystal Arena3", 2 },
                { "Voke ChaosCrystal Arena4", 2 },
                { "Voke ChaosCrystal Boss", 4 },
                { "Stygia Ammostash Arena1", 2 },
                { "Stygia Ammostash Arena2", 6 },
                { "Stygia Ammostash Arena3", 4 },
                { "Stygia Ammostash Arena4", 7 },
                { "Stygia Ammostash Boss", 4 },
                { "Stygia HealthCrystal Arena1", 2 },
                { "Stygia HealthCrystal Arena2", 4 },
                { "Stygia HealthCrystal Arena3", 3 },
                { "Stygia HealthCrystal Arena4", 5 },
                { "Stygia HealthCrystal Boss", 5 },
                // { "Stygia ChaosCrystal Arena1", 0},
                { "Stygia ChaosCrystal Arena2", 5 },
                { "Stygia ChaosCrystal Arena3", 2 },
                { "Stygia ChaosCrystal Arena4", 4 },
                // { "Stygia ChaosCrystal Boss", 0},

                { "Yhelm Ammostash Arena1", 8 },
                { "Yhelm Ammostash Arena2", 4 },
                { "Yhelm Ammostash Arena3", 1 },
                { "Yhelm Ammostash Arena4", 7 },
                { "Yhelm Ammostash Boss", 4 },
                { "Yhelm HealthCrystal Arena1", 3 },
                { "Yhelm HealthCrystal Arena2", 4 },
                { "Yhelm HealthCrystal Arena3", 3 },
                { "Yhelm HealthCrystal Arena4", 6 },
                { "Yhelm HealthCrystal Boss", 5 },
                // { "Yhelm ChaosCrystal Arena1", 0},
                { "Yhelm ChaosCrystal Arena2", 1 },
                { "Yhelm ChaosCrystal Arena3", 2 },
                { "Yhelm ChaosCrystal Arena4", 4 },
                { "Yhelm ChaosCrystal Boss", 6 },
                { "Incaustis Ammostash Arena1", 5 },
                { "Incaustis Ammostash Arena2", 7 },
                { "Incaustis Ammostash Arena3", 4 },
                { "Incaustis Ammostash Arena4", 6 },
                { "Incaustis Ammostash Boss", 4 },
                { "Incaustis HealthCrystal Arena1", 3 },
                { "Incaustis HealthCrystal Arena2", 5 },
                { "Incaustis HealthCrystal Arena3", 4 },
                { "Incaustis HealthCrystal Arena4", 6 },
                { "Incaustis HealthCrystal Boss", 5 },
                { "Incaustis ChaosCrystal Arena1", 5 },
                { "Incaustis ChaosCrystal Arena2", 2 },
                { "Incaustis ChaosCrystal Arena3", 3 },
                { "Incaustis ChaosCrystal Arena4", 3 },
                // { "Incaustis ChaosCrystal Boss", 0},

                { "Gehenna Ammostash Arena1", 4 },
                { "Gehenna Ammostash Arena2", 4 },
                { "Gehenna Ammostash Arena3", 4 },
                { "Gehenna Ammostash Arena4", 3 },
                { "Gehenna Ammostash Boss", 4 },
                { "Gehenna HealthCrystal Arena1", 2 },
                { "Gehenna HealthCrystal Arena2", 4 },
                { "Gehenna HealthCrystal Arena3", 4 },
                { "Gehenna HealthCrystal Arena4", 4 },
                { "Gehenna HealthCrystal Boss", 4 },
                { "Gehenna ChaosCrystal Arena1", 1 },
                { "Gehenna ChaosCrystal Arena2", 1 },
                { "Gehenna ChaosCrystal Arena3", 6 },
                { "Gehenna ChaosCrystal Arena4", 2 },
                // { "Gehenna ChaosCrystal Boss", 0},

                { "Nihil Ammostash Arena1", 3 },
                { "Nihil Ammostash Arena2", 4 },
                { "Nihil Ammostash Arena3", 6 },
                { "Nihil Ammostash Arena4", 4 },
                { "Nihil Ammostash Boss", 4 },
                { "Nihil HealthCrystal Arena1", 3 },
                { "Nihil HealthCrystal Arena2", 3 },
                { "Nihil HealthCrystal Arena3", 4 },
                { "Nihil HealthCrystal Arena4", 5 },
                { "Nihil HealthCrystal Boss", 5 },
                { "Nihil ChaosCrystal Arena1", 1 },
                // { "Nihil ChaosCrystal Arena2", 0},
                { "Nihil ChaosCrystal Arena3", 2 },
                // { "Nihil ChaosCrystal Arena4", 0},
                { "Nihil ChaosCrystal Boss", 2 },
                { "Acheron Ammostash Arena1", 5 },
                { "Acheron Ammostash Arena2", 3 },
                { "Acheron Ammostash Arena3", 7 },
                { "Acheron Ammostash Arena4", 6 },
                // { "Acheron Ammostash Boss", 0},
                { "Acheron HealthCrystal Arena1", 4 },
                { "Acheron HealthCrystal Arena2", 3 },
                { "Acheron HealthCrystal Arena3", 6 },
                { "Acheron HealthCrystal Arena4", 4 },
                { "Acheron HealthCrystal Boss", 5 },
                { "Acheron ChaosCrystal Arena1", 2 },
                { "Acheron ChaosCrystal Arena2", 1 },
                { "Acheron ChaosCrystal Arena3", 5 },
                { "Acheron ChaosCrystal Arena4", 4 },
                // { "Acheron ChaosCrystal Boss", 0},

                { "Sheol Ammostash Arena1", 6 },
                { "Sheol Ammostash Arena2", 5 },
                { "Sheol Ammostash Arena3", 5 },
                { "Sheol Ammostash Arena4", 5 },
                { "Sheol Ammostash Boss", 7 },
                { "Sheol HealthCrystal Arena1", 5 },
                { "Sheol HealthCrystal Arena2", 4 },
                { "Sheol HealthCrystal Arena3", 5 },
                { "Sheol HealthCrystal Arena4", 5 },
                { "Sheol HealthCrystal Boss", 7 },
                { "Sheol ChaosCrystal Arena1", 2 },
                { "Sheol ChaosCrystal Arena2", 3 },
                { "Sheol ChaosCrystal Arena3", 3 },
                { "Sheol ChaosCrystal Arena4", 4 },
                // { "Sheol ChaosCrystal Boss", 0},
            };
        private static readonly Dictionary<string, string> LocationDestructionToCompletionId =
            new Dictionary<string, string>()
            {
                { "Voke Ammostash Arena1", "Voke Arena 1 Ammostash Destruction" },
                { "Voke Ammostash Arena2", "Voke Arena 2 Ammostash Destruction" },
                { "Voke Ammostash Arena3", "Voke Arena 3 Ammostash Destruction" },
                { "Voke Ammostash Arena4", "Voke Arena 4 Ammostash Destruction" },
                { "Voke Ammostash Boss", "Voke Boss Ammostash Destruction" },
                { "Voke HealthCrystal Arena1", "Voke Arena 1 Health Crystal Destruction" },
                { "Voke HealthCrystal Arena2", "Voke Arena 2 Health Crystal Destruction" },
                { "Voke HealthCrystal Arena3", "Voke Arena 3 Health Crystal Destruction" },
                { "Voke HealthCrystal Arena4", "Voke Arena 4 Health Crystal Destruction" },
                { "Voke HealthCrystal Boss", "Voke Boss Health Crystal Destruction" },
                // { "Voke ChaosCrystal Arena1", "Voke Arena 1 Chaos Crystal Destruction" },
                { "Voke ChaosCrystal Arena2", "Voke Arena 2 Chaos Crystal Destruction" },
                { "Voke ChaosCrystal Arena3", "Voke Arena 3 Chaos Crystal Destruction" },
                { "Voke ChaosCrystal Arena4", "Voke Arena 4 Chaos Crystal Destruction" },
                { "Voke ChaosCrystal Boss", "Voke Boss Chaos Crystal Destruction" },
                { "Stygia Ammostash Arena1", "Stygia Arena 1 Ammostash Destruction" },
                { "Stygia Ammostash Arena2", "Stygia Arena 2 Ammostash Destruction" },
                { "Stygia Ammostash Arena3", "Stygia Arena 3 Ammostash Destruction" },
                { "Stygia Ammostash Arena4", "Stygia Arena 4 Ammostash Destruction" },
                { "Stygia Ammostash Boss", "Stygia Boss Ammostash Destruction" },
                { "Stygia HealthCrystal Arena1", "Stygia Arena 1 Health Crystal Destruction" },
                { "Stygia HealthCrystal Arena2", "Stygia Arena 2 Health Crystal Destruction" },
                { "Stygia HealthCrystal Arena3", "Stygia Arena 3 Health Crystal Destruction" },
                { "Stygia HealthCrystal Arena4", "Stygia Arena 4 Health Crystal Destruction" },
                { "Stygia HealthCrystal Boss", "Stygia Boss Health Crystal Destruction" },
                // { "Stygia ChaosCrystal Arena1", "Stygia Arena 1 Chaos Crystal Destruction" },
                { "Stygia ChaosCrystal Arena2", "Stygia Arena 2 Chaos Crystal Destruction" },
                { "Stygia ChaosCrystal Arena3", "Stygia Arena 3 Chaos Crystal Destruction" },
                { "Stygia ChaosCrystal Arena4", "Stygia Arena 4 Chaos Crystal Destruction" },
                // { "Stygia ChaosCrystal Boss", "Stygia Boss Chaos Crystal Destruction" },

                { "Yhelm Ammostash Arena1", "Yhelm Arena 1 Ammostash Destruction" },
                { "Yhelm Ammostash Arena2", "Yhelm Arena 2 Ammostash Destruction" },
                { "Yhelm Ammostash Arena3", "Yhelm Arena 3 Ammostash Destruction" },
                { "Yhelm Ammostash Arena4", "Yhelm Arena 4 Ammostash Destruction" },
                { "Yhelm Ammostash Boss", "Yhelm Boss Ammostash Destruction" },
                { "Yhelm HealthCrystal Arena1", "Yhelm Arena 1 Health Crystal Destruction" },
                { "Yhelm HealthCrystal Arena2", "Yhelm Arena 2 Health Crystal Destruction" },
                { "Yhelm HealthCrystal Arena3", "Yhelm Arena 3 Health Crystal Destruction" },
                { "Yhelm HealthCrystal Arena4", "Yhelm Arena 4 Health Crystal Destruction" },
                { "Yhelm HealthCrystal Boss", "Yhelm Boss Health Crystal Destruction" },
                // { "Yhelm ChaosCrystal Arena1", "Yhelm Arena 1 Chaos Crystal Destruction" },
                { "Yhelm ChaosCrystal Arena2", "Yhelm Arena 2 Chaos Crystal Destruction" },
                { "Yhelm ChaosCrystal Arena3", "Yhelm Arena 3 Chaos Crystal Destruction" },
                { "Yhelm ChaosCrystal Arena4", "Yhelm Arena 4 Chaos Crystal Destruction" },
                { "Yhelm ChaosCrystal Boss", "Yhelm Boss Chaos Crystal Destruction" },
                { "Incaustis Ammostash Arena1", "Incaustis Arena 1 Ammostash Destruction" },
                { "Incaustis Ammostash Arena2", "Incaustis Arena 2 Ammostash Destruction" },
                { "Incaustis Ammostash Arena3", "Incaustis Arena 3 Ammostash Destruction" },
                { "Incaustis Ammostash Arena4", "Incaustis Arena 4 Ammostash Destruction" },
                { "Incaustis Ammostash Boss", "Incaustis Boss Ammostash Destruction" },
                { "Incaustis HealthCrystal Arena1", "Incaustis Arena 1 Health Crystal Destruction" },
                { "Incaustis HealthCrystal Arena2", "Incaustis Arena 2 Health Crystal Destruction" },
                { "Incaustis HealthCrystal Arena3", "Incaustis Arena 3 Health Crystal Destruction" },
                { "Incaustis HealthCrystal Arena4", "Incaustis Arena 4 Health Crystal Destruction" },
                { "Incaustis HealthCrystal Boss", "Incaustis Boss Health Crystal Destruction" },
                { "Incaustis ChaosCrystal Arena1", "Incaustis Arena 1 Chaos Crystal Destruction" },
                { "Incaustis ChaosCrystal Arena2", "Incaustis Arena 2 Chaos Crystal Destruction" },
                { "Incaustis ChaosCrystal Arena3", "Incaustis Arena 3 Chaos Crystal Destruction" },
                { "Incaustis ChaosCrystal Arena4", "Incaustis Arena 4 Chaos Crystal Destruction" },
                // { "Incaustis ChaosCrystal Boss", "Incaustis Boss Chaos Crystal Destruction" },

                { "Gehenna Ammostash Arena1", "Gehenna Arena 1 Ammostash Destruction" },
                { "Gehenna Ammostash Arena2", "Gehenna Arena 2 Ammostash Destruction" },
                { "Gehenna Ammostash Arena3", "Gehenna Arena 3 Ammostash Destruction" },
                { "Gehenna Ammostash Arena4", "Gehenna Arena 4 Ammostash Destruction" },
                { "Gehenna Ammostash Boss", "Gehenna Boss Ammostash Destruction" },
                { "Gehenna HealthCrystal Arena1", "Gehenna Arena 1 Health Crystal Destruction" },
                { "Gehenna HealthCrystal Arena2", "Gehenna Arena 2 Health Crystal Destruction" },
                { "Gehenna HealthCrystal Arena3", "Gehenna Arena 3 Health Crystal Destruction" },
                { "Gehenna HealthCrystal Arena4", "Gehenna Arena 4 Health Crystal Destruction" },
                { "Gehenna HealthCrystal Boss", "Gehenna Boss Health Crystal Destruction" },
                { "Gehenna ChaosCrystal Arena1", "Gehenna Arena 1 Chaos Crystal Destruction" },
                { "Gehenna ChaosCrystal Arena2", "Gehenna Arena 2 Chaos Crystal Destruction" },
                { "Gehenna ChaosCrystal Arena3", "Gehenna Arena 3 Chaos Crystal Destruction" },
                { "Gehenna ChaosCrystal Arena4", "Gehenna Arena 4 Chaos Crystal Destruction" },
                // { "Gehenna ChaosCrystal Boss", "Gehenna Boss Chaos Crystal Destruction" },

                { "Nihil Ammostash Arena1", "Nihil Arena 1 Ammostash Destruction" },
                { "Nihil Ammostash Arena2", "Nihil Arena 2 Ammostash Destruction" },
                { "Nihil Ammostash Arena3", "Nihil Arena 3 Ammostash Destruction" },
                { "Nihil Ammostash Arena4", "Nihil Arena 4 Ammostash Destruction" },
                { "Nihil Ammostash Boss", "Nihil Boss Ammostash Destruction" },
                { "Nihil HealthCrystal Arena1", "Nihil Arena 1 Health Crystal Destruction" },
                { "Nihil HealthCrystal Arena2", "Nihil Arena 2 Health Crystal Destruction" },
                { "Nihil HealthCrystal Arena3", "Nihil Arena 3 Health Crystal Destruction" },
                { "Nihil HealthCrystal Arena4", "Nihil Arena 4 Health Crystal Destruction" },
                { "Nihil HealthCrystal Boss", "Nihil Boss Health Crystal Destruction" },
                { "Nihil ChaosCrystal Arena1", "Nihil Arena 1 Chaos Crystal Destruction" },
                // { "Nihil ChaosCrystal Arena2", "Nihil Arena 2 Chaos Crystal Destruction" },
                { "Nihil ChaosCrystal Arena3", "Nihil Arena 3 Chaos Crystal Destruction" },
                // { "Nihil ChaosCrystal Arena4", "Nihil Arena 4 Chaos Crystal Destruction" },
                { "Nihil ChaosCrystal Boss", "Nihil Boss Chaos Crystal Destruction" },
                { "Acheron Ammostash Arena1", "Acheron Arena 1 Ammostash Destruction" },
                { "Acheron Ammostash Arena2", "Acheron Arena 2 Ammostash Destruction" },
                { "Acheron Ammostash Arena3", "Acheron Arena 3 Ammostash Destruction" },
                { "Acheron Ammostash Arena4", "Acheron Arena 4 Ammostash Destruction" },
                // { "Acheron Ammostash Boss", "Acheron Boss Ammostash Destruction" },
                { "Acheron HealthCrystal Arena1", "Acheron Arena 1 Health Crystal Destruction" },
                { "Acheron HealthCrystal Arena2", "Acheron Arena 2 Health Crystal Destruction" },
                { "Acheron HealthCrystal Arena3", "Acheron Arena 3 Health Crystal Destruction" },
                { "Acheron HealthCrystal Arena4", "Acheron Arena 4 Health Crystal Destruction" },
                { "Acheron HealthCrystal Boss", "Acheron Boss Health Crystal Destruction" },
                { "Acheron ChaosCrystal Arena1", "Acheron Arena 1 Chaos Crystal Destruction" },
                { "Acheron ChaosCrystal Arena2", "Acheron Arena 2 Chaos Crystal Destruction" },
                { "Acheron ChaosCrystal Arena3", "Acheron Arena 3 Chaos Crystal Destruction" },
                { "Acheron ChaosCrystal Arena4", "Acheron Arena 4 Chaos Crystal Destruction" },
                // { "Acheron ChaosCrystal Boss", "Acheron Boss Chaos Crystal Destruction" },

                { "Sheol Ammostash Arena1", "Sheol Arena 1 Ammostash Destruction" },
                { "Sheol Ammostash Arena2", "Sheol Arena 2 Ammostash Destruction" },
                { "Sheol Ammostash Arena3", "Sheol Arena 3 Ammostash Destruction" },
                { "Sheol Ammostash Arena4", "Sheol Arena 4 Ammostash Destruction" },
                { "Sheol Ammostash Boss", "Sheol Boss Ammostash Destruction" },
                { "Sheol HealthCrystal Arena1", "Sheol Arena 1 Health Crystal Destruction" },
                { "Sheol HealthCrystal Arena2", "Sheol Arena 2 Health Crystal Destruction" },
                { "Sheol HealthCrystal Arena3", "Sheol Arena 3 Health Crystal Destruction" },
                { "Sheol HealthCrystal Arena4", "Sheol Arena 4 Health Crystal Destruction" },
                { "Sheol HealthCrystal Boss", "Sheol Boss Health Crystal Destruction" },
                { "Sheol ChaosCrystal Arena1", "Sheol Arena 1 Chaos Crystal Destruction" },
                { "Sheol ChaosCrystal Arena2", "Sheol Arena 2 Chaos Crystal Destruction" },
                { "Sheol ChaosCrystal Arena3", "Sheol Arena 3 Chaos Crystal Destruction" },
                { "Sheol ChaosCrystal Arena4", "Sheol Arena 4 Chaos Crystal Destruction" },
                // { "Sheol ChaosCrystal Boss", "Sheol Boss Chaos Crystal Destruction" },
            };

        private Dictionary<string, List<string>> RequiredSubCompletionsForArena = new Dictionary<
            string,
            List<string>
        >()
        {
            { "Voke Arena 1 Destructible Completion", new List<string> { "Voke Arena 1 Ammostash Destruction", "Voke Arena 1 Health Crystal Destruction", } },
            { "Voke Arena 2 Destructible Completion", new List<string> { "Voke Arena 2 Ammostash Destruction", "Voke Arena 2 Health Crystal Destruction", "Voke Arena 2 Chaos Crystal Destruction", } },
            { "Voke Arena 3 Destructible Completion", new List<string> { "Voke Arena 3 Ammostash Destruction", "Voke Arena 3 Health Crystal Destruction", "Voke Arena 3 Chaos Crystal Destruction", } },
            { "Voke Arena 4 Destructible Completion", new List<string> { "Voke Arena 4 Ammostash Destruction", "Voke Arena 4 Health Crystal Destruction", "Voke Arena 4 Chaos Crystal Destruction", } },
            { "Voke Boss Destructible Completion", new List<string> { "Voke Boss Ammostash Destruction", "Voke Boss Health Crystal Destruction", "Voke Boss Chaos Crystal Destruction", } },
            { "Voke Ammostash Destruction", new List<string> { "Voke Arena 1 Ammostash Destruction", "Voke Arena 2 Ammostash Destruction", "Voke Arena 3 Ammostash Destruction", "Voke Arena 4 Ammostash Destruction", "Voke Boss Ammostash Destruction", } },
            { "Voke Health Crystal Destruction", new List<string> { "Voke Arena 1 Health Crystal Destruction", "Voke Arena 2 Health Crystal Destruction", "Voke Arena 3 Health Crystal Destruction", "Voke Arena 4 Health Crystal Destruction", "Voke Boss Health Crystal Destruction", } },
            { "Voke Chaos Crystal Destruction", new List<string> { "Voke Arena 2 Chaos Crystal Destruction", "Voke Arena 3 Chaos Crystal Destruction", "Voke Arena 4 Chaos Crystal Destruction", "Voke Boss Chaos Crystal Destruction", } },
            { "Stygia Arena 1 Destructible Completion", new List<string> { "Stygia Arena 1 Ammostash Destruction", "Stygia Arena 1 Health Crystal Destruction", } },
            { "Stygia Arena 2 Destructible Completion", new List<string> { "Stygia Arena 2 Ammostash Destruction", "Stygia Arena 2 Health Crystal Destruction", "Stygia Arena 2 Chaos Crystal Destruction", } },
            { "Stygia Arena 3 Destructible Completion", new List<string> { "Stygia Arena 3 Ammostash Destruction", "Stygia Arena 3 Health Crystal Destruction", "Stygia Arena 3 Chaos Crystal Destruction", } },
            { "Stygia Arena 4 Destructible Completion", new List<string> { "Stygia Arena 4 Ammostash Destruction", "Stygia Arena 4 Health Crystal Destruction", "Stygia Arena 4 Chaos Crystal Destruction", } },
            { "Stygia Boss Destructible Completion", new List<string> { "Stygia Boss Ammostash Destruction", "Stygia Boss Health Crystal Destruction", } },
            { "Stygia Ammostash Destruction", new List<string> { "Stygia Arena 1 Ammostash Destruction", "Stygia Arena 2 Ammostash Destruction", "Stygia Arena 3 Ammostash Destruction", "Stygia Arena 4 Ammostash Destruction", "Stygia Boss Ammostash Destruction", } },
            { "Stygia Health Crystal Destruction", new List<string> { "Stygia Arena 1 Health Crystal Destruction", "Stygia Arena 2 Health Crystal Destruction", "Stygia Arena 3 Health Crystal Destruction", "Stygia Arena 4 Health Crystal Destruction", "Stygia Boss Health Crystal Destruction", } },
            { "Stygia Chaos Crystal Destruction", new List<string> { "Stygia Arena 2 Chaos Crystal Destruction", "Stygia Arena 3 Chaos Crystal Destruction", "Stygia Arena 4 Chaos Crystal Destruction", } },
            { "Yhelm Arena 1 Destructible Completion", new List<string> { "Yhelm Arena 1 Ammostash Destruction", "Yhelm Arena 1 Health Crystal Destruction", } },
            { "Yhelm Arena 2 Destructible Completion", new List<string> { "Yhelm Arena 2 Ammostash Destruction", "Yhelm Arena 2 Health Crystal Destruction", "Yhelm Arena 2 Chaos Crystal Destruction", } },
            { "Yhelm Arena 3 Destructible Completion", new List<string> { "Yhelm Arena 3 Ammostash Destruction", "Yhelm Arena 3 Health Crystal Destruction", "Yhelm Arena 3 Chaos Crystal Destruction", } },
            { "Yhelm Arena 4 Destructible Completion", new List<string> { "Yhelm Arena 4 Ammostash Destruction", "Yhelm Arena 4 Health Crystal Destruction", "Yhelm Arena 4 Chaos Crystal Destruction", } },
            { "Yhelm Boss Destructible Completion", new List<string> { "Yhelm Boss Ammostash Destruction", "Yhelm Boss Health Crystal Destruction", "Yhelm Boss Chaos Crystal Destruction", } },
            { "Yhelm Ammostash Destruction", new List<string> { "Yhelm Arena 1 Ammostash Destruction", "Yhelm Arena 2 Ammostash Destruction", "Yhelm Arena 3 Ammostash Destruction", "Yhelm Arena 4 Ammostash Destruction", "Yhelm Boss Ammostash Destruction", } },
            { "Yhelm Health Crystal Destruction", new List<string> { "Yhelm Arena 1 Health Crystal Destruction", "Yhelm Arena 2 Health Crystal Destruction", "Yhelm Arena 3 Health Crystal Destruction", "Yhelm Arena 4 Health Crystal Destruction", "Yhelm Boss Health Crystal Destruction", } },
            { "Yhelm Chaos Crystal Destruction", new List<string> { "Yhelm Arena 2 Chaos Crystal Destruction", "Yhelm Arena 3 Chaos Crystal Destruction", "Yhelm Arena 4 Chaos Crystal Destruction", "Yhelm Boss Chaos Crystal Destruction", } },
            { "Incaustis Arena 1 Destructible Completion", new List<string> { "Incaustis Arena 1 Ammostash Destruction", "Incaustis Arena 1 Health Crystal Destruction", "Incaustis Arena 1 Chaos Crystal Destruction", } },
            { "Incaustis Arena 2 Destructible Completion", new List<string> { "Incaustis Arena 2 Ammostash Destruction", "Incaustis Arena 2 Health Crystal Destruction", "Incaustis Arena 2 Chaos Crystal Destruction", } },
            { "Incaustis Arena 3 Destructible Completion", new List<string> { "Incaustis Arena 3 Ammostash Destruction", "Incaustis Arena 3 Health Crystal Destruction", "Incaustis Arena 3 Chaos Crystal Destruction", } },
            { "Incaustis Arena 4 Destructible Completion", new List<string> { "Incaustis Arena 4 Ammostash Destruction", "Incaustis Arena 4 Health Crystal Destruction", "Incaustis Arena 4 Chaos Crystal Destruction", } },
            { "Incaustis Boss Destructible Completion", new List<string> { "Incaustis Boss Ammostash Destruction", "Incaustis Boss Health Crystal Destruction", } },
            { "Incaustis Ammostash Destruction", new List<string> { "Incaustis Arena 1 Ammostash Destruction", "Incaustis Arena 2 Ammostash Destruction", "Incaustis Arena 3 Ammostash Destruction", "Incaustis Arena 4 Ammostash Destruction", "Incaustis Boss Ammostash Destruction", } },
            { "Incaustis Health Crystal Destruction", new List<string> { "Incaustis Arena 1 Health Crystal Destruction", "Incaustis Arena 2 Health Crystal Destruction", "Incaustis Arena 3 Health Crystal Destruction", "Incaustis Arena 4 Health Crystal Destruction", "Incaustis Boss Health Crystal Destruction", } },
            { "Incaustis Chaos Crystal Destruction", new List<string> { "Incaustis Arena 1 Chaos Crystal Destruction", "Incaustis Arena 2 Chaos Crystal Destruction", "Incaustis Arena 3 Chaos Crystal Destruction", "Incaustis Arena 4 Chaos Crystal Destruction", } },
            { "Gehenna Arena 1 Destructible Completion", new List<string> { "Gehenna Arena 1 Ammostash Destruction", "Gehenna Arena 1 Health Crystal Destruction", "Gehenna Arena 1 Chaos Crystal Destruction", } },
            { "Gehenna Arena 2 Destructible Completion", new List<string> { "Gehenna Arena 2 Ammostash Destruction", "Gehenna Arena 2 Health Crystal Destruction", "Gehenna Arena 2 Chaos Crystal Destruction", } },
            { "Gehenna Arena 3 Destructible Completion", new List<string> { "Gehenna Arena 3 Ammostash Destruction", "Gehenna Arena 3 Health Crystal Destruction", "Gehenna Arena 3 Chaos Crystal Destruction", } },
            { "Gehenna Arena 4 Destructible Completion", new List<string> { "Gehenna Arena 4 Ammostash Destruction", "Gehenna Arena 4 Health Crystal Destruction", "Gehenna Arena 4 Chaos Crystal Destruction", } },
            { "Gehenna Boss Destructible Completion", new List<string> { "Gehenna Boss Ammostash Destruction", "Gehenna Boss Health Crystal Destruction", } },
            { "Gehenna Ammostash Destruction", new List<string> { "Gehenna Arena 1 Ammostash Destruction", "Gehenna Arena 2 Ammostash Destruction", "Gehenna Arena 3 Ammostash Destruction", "Gehenna Arena 4 Ammostash Destruction", "Gehenna Boss Ammostash Destruction", } },
            { "Gehenna Health Crystal Destruction", new List<string> { "Gehenna Arena 1 Health Crystal Destruction", "Gehenna Arena 2 Health Crystal Destruction", "Gehenna Arena 3 Health Crystal Destruction", "Gehenna Arena 4 Health Crystal Destruction", "Gehenna Boss Health Crystal Destruction", } },
            { "Gehenna Chaos Crystal Destruction", new List<string> { "Gehenna Arena 1 Chaos Crystal Destruction", "Gehenna Arena 2 Chaos Crystal Destruction", "Gehenna Arena 3 Chaos Crystal Destruction", "Gehenna Arena 4 Chaos Crystal Destruction", } },
            { "Nihil Arena 1 Destructible Completion", new List<string> { "Nihil Arena 1 Ammostash Destruction", "Nihil Arena 1 Health Crystal Destruction", "Nihil Arena 1 Chaos Crystal Destruction", } },
            { "Nihil Arena 2 Destructible Completion", new List<string> { "Nihil Arena 2 Ammostash Destruction", "Nihil Arena 2 Health Crystal Destruction", } },
            { "Nihil Arena 3 Destructible Completion", new List<string> { "Nihil Arena 3 Ammostash Destruction", "Nihil Arena 3 Health Crystal Destruction", "Nihil Arena 3 Chaos Crystal Destruction", } },
            { "Nihil Arena 4 Destructible Completion", new List<string> { "Nihil Arena 4 Ammostash Destruction", "Nihil Arena 4 Health Crystal Destruction", } },
            { "Nihil Boss Destructible Completion", new List<string> { "Nihil Boss Ammostash Destruction", "Nihil Boss Health Crystal Destruction", "Nihil Boss Chaos Crystal Destruction", } },
            { "Nihil Ammostash Destruction", new List<string> { "Nihil Arena 1 Ammostash Destruction", "Nihil Arena 2 Ammostash Destruction", "Nihil Arena 3 Ammostash Destruction", "Nihil Arena 4 Ammostash Destruction", "Nihil Boss Ammostash Destruction", } },
            { "Nihil Health Crystal Destruction", new List<string> { "Nihil Arena 1 Health Crystal Destruction", "Nihil Arena 2 Health Crystal Destruction", "Nihil Arena 3 Health Crystal Destruction", "Nihil Arena 4 Health Crystal Destruction", "Nihil Boss Health Crystal Destruction", } },
            { "Nihil Chaos Crystal Destruction", new List<string> { "Nihil Arena 1 Chaos Crystal Destruction", "Nihil Arena 3 Chaos Crystal Destruction", "Nihil Boss Chaos Crystal Destruction", } },
            { "Acheron Arena 1 Destructible Completion", new List<string> { "Acheron Arena 1 Ammostash Destruction", "Acheron Arena 1 Health Crystal Destruction", "Acheron Arena 1 Chaos Crystal Destruction", } },
            { "Acheron Arena 2 Destructible Completion", new List<string> { "Acheron Arena 2 Ammostash Destruction", "Acheron Arena 2 Health Crystal Destruction", "Acheron Arena 2 Chaos Crystal Destruction", } },
            { "Acheron Arena 3 Destructible Completion", new List<string> { "Acheron Arena 3 Ammostash Destruction", "Acheron Arena 3 Health Crystal Destruction", "Acheron Arena 3 Chaos Crystal Destruction", } },
            { "Acheron Arena 4 Destructible Completion", new List<string> { "Acheron Arena 4 Ammostash Destruction", "Acheron Arena 4 Health Crystal Destruction", "Acheron Arena 4 Chaos Crystal Destruction", } },
            { "Acheron Boss Destructible Completion", new List<string> { "Acheron Boss Health Crystal Destruction" } },
            { "Acheron Ammostash Destruction", new List<string> { "Acheron Arena 1 Ammostash Destruction", "Acheron Arena 2 Ammostash Destruction", "Acheron Arena 3 Ammostash Destruction", "Acheron Arena 4 Ammostash Destruction", } },
            { "Acheron Health Crystal Destruction", new List<string> { "Acheron Arena 1 Health Crystal Destruction", "Acheron Arena 2 Health Crystal Destruction", "Acheron Arena 3 Health Crystal Destruction", "Acheron Arena 4 Health Crystal Destruction", "Acheron Boss Health Crystal Destruction", } },
            { "Acheron Chaos Crystal Destruction", new List<string> { "Acheron Arena 1 Chaos Crystal Destruction", "Acheron Arena 2 Chaos Crystal Destruction", "Acheron Arena 3 Chaos Crystal Destruction", "Acheron Arena 4 Chaos Crystal Destruction", } },
            { "Sheol Arena 1 Destructible Completion", new List<string> { "Sheol Arena 1 Ammostash Destruction", "Sheol Arena 1 Health Crystal Destruction", "Sheol Arena 1 Chaos Crystal Destruction", } },
            { "Sheol Arena 2 Destructible Completion", new List<string> { "Sheol Arena 2 Ammostash Destruction", "Sheol Arena 2 Health Crystal Destruction", "Sheol Arena 2 Chaos Crystal Destruction", } },
            { "Sheol Arena 3 Destructible Completion", new List<string> { "Sheol Arena 3 Ammostash Destruction", "Sheol Arena 3 Health Crystal Destruction", "Sheol Arena 3 Chaos Crystal Destruction", } },
            { "Sheol Arena 4 Destructible Completion", new List<string> { "Sheol Arena 4 Ammostash Destruction", "Sheol Arena 4 Health Crystal Destruction", "Sheol Arena 4 Chaos Crystal Destruction", } },
            { "Sheol Boss Destructible Completion", new List<string> { "Sheol Boss Ammostash Destruction", "Sheol Boss Health Crystal Destruction", } },
            { "Sheol Ammostash Destruction", new List<string> { "Sheol Arena 1 Ammostash Destruction", "Sheol Arena 2 Ammostash Destruction", "Sheol Arena 3 Ammostash Destruction", "Sheol Arena 4 Ammostash Destruction", "Sheol Boss Ammostash Destruction", } },
            { "Sheol Health Crystal Destruction", new List<string> { "Sheol Arena 1 Health Crystal Destruction", "Sheol Arena 2 Health Crystal Destruction", "Sheol Arena 3 Health Crystal Destruction", "Sheol Arena 4 Health Crystal Destruction", "Sheol Boss Health Crystal Destruction", } },
            { "Sheol Chaos Crystal Destruction", new List<string> { "Sheol Arena 1 Chaos Crystal Destruction", "Sheol Arena 2 Chaos Crystal Destruction", "Sheol Arena 3 Chaos Crystal Destruction", "Sheol Arena 4 Chaos Crystal Destruction", } },
        };

        private List<Location> NextMultipliers = new List<Location>();
        private List<Location> MaxMultiplier = new List<Location>();
        private List<Location> SecretMultipliers = new List<Location>();
        private List<Location> CoatOfArmsPickups = new List<Location>();
        private List<Location> WeaponPickups = new List<Location>();
        private List<Location> AnguishGates = new List<Location>();
        private List<Location> Ammostashes = new List<Location>();
        private List<Location> HealthCrystals = new List<Location>();
        private List<Location> ChaosCrystals = new List<Location>();

        public void SetupLevel(string levelId)
        {
            Logger.LogInfo($"Setting up collectibles from Level {levelId}");
            ClearCollections();

            if (!LevelIdToZoneDictionary.ContainsKey(levelId))
            {
                Logger.LogInfo(
                    $"Level {levelId} does not contain any locations to pickup, skipping setup"
                );
                return;
            }

            EZone zone = LevelIdToZoneDictionary[levelId];
            var zoneLocations = Locations.LocationDataByName.Values.Where(loc => loc.Zone == zone);

            NextMultipliers = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.NextMultiplier)
                .ToList();
            MaxMultiplier = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.MaxMultiplier)
                .ToList();
            SecretMultipliers = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.SecretMultiplier)
                .ToList();
            AnguishGates = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.AnguishGate)
                .ToList();
            WeaponPickups = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.WeaponPickup)
                .ToList();
            Ammostashes = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.Ammostash)
                .ToList();
            HealthCrystals = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.HealthCrystal)
                .ToList();
            ChaosCrystals = zoneLocations
                .Where(loc => loc.LocationType == ELocationType.ChaosCrystal)
                .ToList();

            SetupCollections();
        }

        private void ClearCollections()
        {
            NextMultipliers.Clear();
            MaxMultiplier.Clear();
            SecretMultipliers.Clear();
            AnguishGates.Clear();
            WeaponPickups.Clear();
            Ammostashes.Clear();
            HealthCrystals.Clear();
            ChaosCrystals.Clear();
            CoatOfArmsPickups.Clear();
            Logger.LogInfo("Cleared all location lists");
        }

        private void SetupCollections()
        {
            SetupCollectionReferences(NextMultipliers, ELocationType.NextMultiplier);
            SetupCollectionReferences(MaxMultiplier, ELocationType.MaxMultiplier);
            SetupCollectionReferences(SecretMultipliers, ELocationType.SecretMultiplier);
            SetupCollectionReferences(AnguishGates, ELocationType.AnguishGate);
            SetupCollectionReferences(WeaponPickups, ELocationType.WeaponPickup);
            SetupCollectionReferences(Ammostashes, ELocationType.Ammostash);
            SetupCollectionReferences(HealthCrystals, ELocationType.HealthCrystal);
            SetupCollectionReferences(ChaosCrystals, ELocationType.ChaosCrystal);
            Logger.LogInfo("Setup all pickup locations");
        }

        // Used to reload on Akkeron boss to activate the mults pickups
        public void ResetUpCollections()
        {
            SetupCollectionReferences(NextMultipliers, ELocationType.NextMultiplier);
            SetupCollectionReferences(MaxMultiplier, ELocationType.MaxMultiplier);
            Logger.LogInfo("Re-setup mult pickup locations");
        }

        private void SetupCollectionReferences(List<Location> locations, ELocationType locationType)
        {
            if (locations == null)
                return;

            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];
                Logger.LogInfo($"Location {location.LocationId} is being checked for collecting");

                location.IsCollected = CheckedLocations.ContainsKey(location.LocationId);

                if (
                    Locations.GameObjectTypes.Contains(locationType)
                    && location.GameObjectName != null
                )
                {
                    location.LoadedGameObject = GameObject.Find(location.GameObjectName);

                    if (location.LoadedGameObject == null)
                    {
                        Logger.LogWarning(
                            $"Could not find main object for location: {location.LocationId}, looked at {location.GameObjectName}"
                        );
                        continue;
                    }
                    else
                    {
                        location.LoadedGameObject.name = location.LocationId;
                        Logger.LogInfo($"Location {location.LocationId} has its gameObject loaded");
                    }
                }

                if (Locations.TypesWithReferences.Contains(locationType))
                {
                    location.ReferenceGameObject = GameObject.Find(
                        location.ReferenceGameObjectName
                    );
                    if (location.ReferenceGameObject == null)
                    {
                        Logger.LogWarning(
                            $"Could not find reference object for location: {location.LocationId}, looked at {location.ReferenceGameObjectName}"
                        );
                        continue;
                    }
                    Logger.LogInfo(
                        $"Location {location.LocationId} has its reference gameObject loaded"
                    );
                }

                if (location.IsCollected)
                {
                    Logger.LogInfo($"Location {location.LocationId} is already collected");
                    locations[i] = location;
                    continue;
                }

                if (
                    IsLocationTypeRandomized(location.LocationType)
                    && !location.IsSetupForCollection
                )
                {
                    if (Locations.MultiplierTypes.Contains(location.LocationType))
                    {
                        Transform defaultSkin = location.LoadedGameObject.transform.Find(
                            "DefaultSkin"
                        );
                        Transform morningStar = location.LoadedGameObject.transform.Find(
                            "MorningStarSkin"
                        );
                        Transform blackMetal = location.LoadedGameObject.transform.Find(
                            "BlackMetalSkin"
                        );
                        if (defaultSkin != null)
                            defaultSkin.gameObject.SetActive(true);
                        if (blackMetal != null)
                            blackMetal.gameObject.SetActive(true);
                        if (morningStar != null)
                            morningStar.gameObject.SetActive(true);
                    }
                    location.IsSetupForCollection = true;
                    Logger.LogInfo($"Location {location.LocationId} is ready for collecting");
                }

                locations[i] = location;
            }
        }

        private static bool IsLocationTypeRandomized(ELocationType locationType)
        {
            return locationType switch
            {
                ELocationType.NextMultiplier => Randomizer.Settings.HellsNextMultiplierEnabled,
                ELocationType.MaxMultiplier => Randomizer.Settings.HellsMaxMultiplierEnabled,
                ELocationType.SecretMultiplier => Randomizer.Settings.HellsSecretMultiplierEnabled,
                ELocationType.ChallengePickup => false, // only for tracking
                ELocationType.WeaponPickup => true, // required for check count
                ELocationType.CoatOfArms => Randomizer.Settings.HellsCoatOfArmsEnabled,
                ELocationType.AnguishGate => true, // maybe in the future adjustable, for now for check count
                ELocationType.Ammostash => false, // only for tracking destructibles
                ELocationType.HealthCrystal => false, // only for tracking destructibles
                ELocationType.ChaosCrystal => false, // only for tracking destructibles
                ELocationType.FirstDestruction => Randomizer.Settings.DestructibleAsUnlocks,
                ELocationType.FirstMiscellaneous => false, // collection of individual locations
                ELocationType.Boon => Randomizer.Settings.RandomisedBoonsEnabled,
                ELocationType.Bestiary => true, // required for check count,
                ELocationType.Codex => true,
                ELocationType.LevelCompletion => Randomizer.Settings.RandomizedHellsEnabled,
                ELocationType.LevelAmmostashCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelHealthCrystalCompletion   => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelChaosCrystalCompletion  => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.LevelSpeed => Randomizer.Settings.HellsCoatOfArmsEnabled,
                ELocationType.ArenaAmmostashCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaHealthCrystalCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaChaosCrystalCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerDestructibleType,
                ELocationType.ArenaDestructibleCompletion => Randomizer.Settings.DestructibleLocationsEnabled && Randomizer.Settings.DestructibleLocationsMode == DestructibleMode.PerEntireArena,
                ELocationType.SectionClearMainSong => Randomizer.Settings.RandomizedSongsEnabled,
                ELocationType.SectionClearBossSong  => Randomizer.Settings.RandomizedSongsEnabled,
                ELocationType.Skin => Randomizer.Settings.HellsRandomizedWeaponSkinsEnabled,
                ELocationType.TormentBronze => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentSilver => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentGold => Randomizer.Settings.ChallengeMedaillonsEnabled,
                ELocationType.TormentCompletion => Randomizer.Settings.RandomizedChallengesEnabled,
                ELocationType.BossAchievement => Randomizer.Settings.RequireHellsCompletion,
                ELocationType.XpEgg => false, // TODO: Leviathan integration
                ELocationType.NightmareCrystal => false, // TODO: Leviathan integration
                _ => false,
            };
        }

        public void CheckMultiplierPickups()
        {
            IsPickupCollected(NextMultipliers);
            IsPickupCollected(MaxMultiplier);
            IsPickupCollected(SecretMultipliers);
        }

        public void CheckWeaponPickups(PlayerWeaponType weaponType)
        {
            string weaponName = Randomizer.ItemTracker.GetWeaponNameByType(weaponType);
            Logger.LogInfo($"Checking weapon pickup for {weaponName}");

            for (int i = 0; i < WeaponPickups.Count; i++)
            {
                Location location = WeaponPickups[i];

                if (location.IsCollected)
                    continue;

                if (location.IsSetupForCollection && location.OriginalItemName.Equals(weaponName))
                {
                    location.IsCollected = true;
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                }
                WeaponPickups[i] = location;
            }
        }

        private void IsPickupCollected(List<Location> locations)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];

                if (location.IsCollected)
                    continue;

                // if no longer active then it is collected
                if (location.IsSetupForCollection && !location.LoadedGameObject.activeSelf)
                {
                    location.IsCollected = true;
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                }
                locations[i] = location;
            }
        }

        private void CollectLocation(
            Location location,
            bool isRandomized = false,
            bool isResync = false
        )
        {
            Logger.LogInfo(
                $"Checking location {location.LocationId}, which is randomized: {isRandomized}"
            );

            if (!CheckedLocations.ContainsKey(location.LocationId))
            {
                Logger.LogInfo($"Location {location.LocationId} is new, adding and sending check");

                LocationsCollected.Add(location);
                CheckedLocations.Add(location.LocationId, true);
                Randomizer.Archipelago.CompleteLocationCheck(location);
                if (!isRandomized)
                {
                    if (!isResync)
                        Randomizer.Archipelago.SynchronizeNotRandomizedLocation(
                            LocationsCollected.ToArray()
                        );

                    if (Items.ItemDataByName.ContainsKey(location.OriginalItemName))
                        Randomizer.ItemTracker.SetCollectedItem(
                            Items.ItemDataByName[location.OriginalItemName].ArchipelagoId,
                            null,
                            false,
                            true
                        );
                }

                CheckGoalCompletion();
            }
        }

        private void CheckGoalCompletion()
        {
            bool IsHellsRelevant =
                Randomizer.Settings.RequireHellsCompletion
                || Randomizer.Settings.RequireSheolCompletion;
            bool IsLeviathanRelevant = Randomizer.Settings.RequireLeviathanCompletion;
            bool IsAspectsDone =
                Randomizer.ItemTracker.GetBossesDefeated(ItemGamemode.HELL).Count
                >= Randomizer.Settings.RequiredHellsCompletion;
            bool IsRedJudgeDefeated = Randomizer
                .ItemTracker.GetBossesDefeated(ItemGamemode.HELL)
                .Contains("Red Judge - Worldbreaker: Sheol defeated");
            bool IsHellsDone =
                (!Randomizer.Settings.RequireHellsCompletion || IsAspectsDone)
                && (!Randomizer.Settings.RequireSheolCompletion || IsRedJudgeDefeated);
            bool IsLeviathanDone =
                Randomizer.ItemTracker.GetBossesDefeated(ItemGamemode.LEVIATHAN).Count == 1;

            if (
                !Randomizer.Archipelago.sentCompletion
                && (!IsHellsRelevant || IsHellsDone)
                && (IsLeviathanRelevant || IsLeviathanDone)
            )
                Randomizer.Archipelago.SendCompletion();
        }

        public void CheckAnguishGates(string anguishGateName)
        {
            Logger.LogInfo($"Checking for anguish gate '{anguishGateName}' in {AnguishGates.Count} gates");

            for (int i = 0; i < AnguishGates.Count; i++)
            {
                Location location = AnguishGates[i];

                if (!location.LocationId.Equals(anguishGateName))
                    continue;

                Logger.LogDebug($"'{anguishGateName}'-'{location.LocationId}'");
                if (location.ReferenceGameObject != null)
                    location.ReferenceGameObject.SetActive(false);

                if (location.IsCollected)
                {
                    Logger.LogInfo($"Location '{location.LocationId}' is already collected");
                    continue;
                }

                location.IsCollected = true;
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));

                AnguishGates[i] = location;
            }
        }

        public void CheckCoatOfArms(string id)
        {
            if (CoatOfArmToLocationName.TryGetValue(id, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckWorldItem(string id)
        {
            if (WorldItemToLocationName.TryGetValue(id, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckFuryCombo(EFuryComboType combo)
        {
            if (FuryComboToLocationName.TryGetValue(combo, out var locationName))
            {
                Location location = LocationDataByName[locationName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public bool IsDestructible(DestructibleObject destructible)
        {
            bool IsDestructible = true;
            string destructibleName =
                destructible.Root != null ? destructible.Root.name : destructible.name;
            if (destructibleName.Equals("Sheol Anguish Gate 4"))
                IsDestructible = IsSheolBossUnlocked();
            return IsDestructible && Randomizer.ItemTracker.IsDestructible(destructibleName);
        }

        internal bool IsSheolBossUnlocked()
        {
            if (!Randomizer.Settings.RequireCoatOfArmsForSheolBoss && !Randomizer.Settings.RequireNoTomorrowForSheolBoss)
                return true;
            bool hasCoatOfArms =
                !Randomizer.Settings.RequireCoatOfArmsForSheolBoss
                || Randomizer.ItemTracker.GetCollectedCoatOfArms()
                    >= Randomizer.Settings.RequiredCoatOfArmsForSheolBoss;
            bool hasNoTomorrow =
                !Randomizer.Settings.RequireNoTomorrowForSheolBoss
                || Randomizer.ItemTracker.HasSongByName("No Tomorrow");
            return hasCoatOfArms && hasNoTomorrow;
        }

        public void CheckDestructible(string currentLevel, DestructibleObject destructible)
        {
            string destructibleName =
                destructible.Root != null ? destructible.Root.name : destructible.name;

            Logger.LogDebug($"Checking destructible with name {destructibleName}");

            if (destructibleName.Contains("Anguish Gate"))
                CheckAnguishGates(destructible.name);
            else if (destructibleName.Contains("Ammostash"))
                CheckDestructible(destructibleName, Ammostashes);
            else if (destructibleName.Contains("Health Crystal"))
                CheckDestructible(destructibleName, HealthCrystals);
            else if (destructibleName.Contains("Chaos Crystal"))
                CheckDestructible(destructibleName, ChaosCrystals);
        }

        private void CheckDestructible(
            string destructibleName,
            List<Location> locations
        )
        {
            Logger.LogInfo(
                $"Checking for destructible '{destructibleName}' in {locations.Count} destructibles"
            );
            for (int i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];

                if (!location.LocationId.Equals(destructibleName))
                    continue;

                if (location.IsCollected)
                {
                    Logger.LogDebug($"Location '{location.LocationId}' is already collected");
                    continue;
                }

                if(IsLocationTypeRandomized(ELocationType.FirstDestruction))
                    CheckFirstDestructions(location.LocationType);

                location.IsCollected = true;
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
                locations[i] = location;
            }

            if (locations.Count > 0)
            {
                Logger.LogDebug($"Checking for arena and level completions");
                var location = locations[0];
                CheckCompletions(location.Zone, location.Arena, location.LocationType, locations);
                CheckDestructionCompletions(location.Zone, location.Arena, location.LocationType);
                CheckLevelCompletions(location.Zone, location.LocationType);
            }
        }

        private void CheckFirstDestructions(ELocationType locationType)
        {
            if (
                !CheckedLocations.ContainsKey("Destroyed First Ammostash")
                && locationType == ELocationType.Ammostash
            )
            {
                var location = Locations.LocationDataByName["Destroyed First Ammostash"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }

            if (
                !CheckedLocations.ContainsKey("Destroyed First Health Crystal")
                && locationType == ELocationType.HealthCrystal
            )
            {
                var location = Locations.LocationDataByName["Destroyed First Health Crystal"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }

            if (
                !CheckedLocations.ContainsKey("Destroyed First Chaos Crystal")
                && locationType == ELocationType.ChaosCrystal
            )
            {
                var location = Locations.LocationDataByName["Destroyed First Chaos Crystal"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        private void CheckDestructionCompletions(
            EZone zone,
            EArena arena,
            ELocationType locationType
        )
        {
            var arenaStr = arena.ToString();
            char lastChar = arenaStr[arenaStr.Length - 1];
            var arenaName = char.IsDigit(lastChar) ? $"Arena {lastChar}" : arenaStr;

            var arenaCheckupName = $"{zone} {arenaName} Destructible Completion";

            Logger.LogDebug($"Checking if '{arenaCheckupName}'s requirements has been met");
            if (
                RequiredSubCompletionsForArena.TryGetValue(
                    arenaCheckupName,
                    out var arenaLocationIds
                )
                && HasAllCompletions(arenaLocationIds)
                && Locations.LocationDataByName.TryGetValue(arenaCheckupName, out var arenaLocation)
            )
                CollectLocation(
                    arenaLocation,
                    IsLocationTypeRandomized(arenaLocation.LocationType)
                );
        }

        private bool HasAllCompletions(List<string> arenaLocationIds)
        {
            foreach (var locationId in arenaLocationIds)
            {
                if (
                    !CheckedLocations.TryGetValue(locationId, out bool hasCollected)
                    || !hasCollected
                ){

                    Logger.LogDebug($"Requirements have not been met");
                    return false;
                }
            }
            Logger.LogDebug($"Requirements has been met");
            return true;
        }

        private void CheckLevelCompletions(EZone zone, ELocationType locationType)
        {
            var locationTypeName = locationType.ToString();
            if (locationType == ELocationType.HealthCrystal)
                locationTypeName = "Health Crystal";
            else if (locationType == ELocationType.ChaosCrystal)
                locationTypeName = "Chaos Crystal";

            var levelCompletionName = $"{zone} {locationTypeName} Destruction";
            if (
                RequiredSubCompletionsForArena.TryGetValue(
                    levelCompletionName,
                    out var levelLocationIds
                )
                && HasRequiredCompletions(levelLocationIds)
                && Locations.LocationDataByName.TryGetValue(
                    levelCompletionName,
                    out var levelLocation
                )
            )
                CollectLocation(
                    levelLocation,
                    IsLocationTypeRandomized(levelLocation.LocationType)
                );
        }

        private bool HasRequiredCompletions(List<string> levelLocationIds)
        {
            int count = 0;
            foreach (var locationId in levelLocationIds)
            {
                if (CheckedLocations.TryGetValue(locationId, out bool hasCollected) && hasCollected)
                    count++;
            }
            return count
                >= Math.Min(
                    levelLocationIds.Count,
                    Randomizer.Settings.RequiredDestructionCompletions
                );
        }

        private void CheckCompletions(
            EZone zone,
            EArena arena,
            ELocationType locationType,
            List<Location> locations
        )
        {
            var checkupName = $"{zone} {locationType} {arena}";
            Logger.LogDebug($"Checking if '{checkupName}'s requirements has been met");
            if (LocationDestructionCountRequired.TryGetValue(checkupName, out var requiredAmount))
            {
                var collectedCount = locations.Count(loc =>
                    loc.IsCollected && loc.Arena == arena && loc.LocationType == locationType
                );

                Logger.LogDebug(
                    $"Collected Amount: {collectedCount} - Required Amount: {requiredAmount}"
                );

                if (
                    collectedCount == requiredAmount
                    && Locations.LocationDataByName.TryGetValue(
                        LocationDestructionToCompletionId[checkupName],
                        out var location
                    )
                )
                    CollectLocation(location, IsLocationTypeRandomized(location.LocationType));
            }
        }

        public void CheckChallengeProgress(ChallengeTracker.ChallengeResult result, string levelId)
        {
            if (result == ChallengeTracker.ChallengeResult.Fail)
                return;

            string checkBaseName = GetChallengeBaseName(levelId);
            Logger.LogInfo($"Challenge {checkBaseName} achieved {result}");

            Location location = Locations.LocationDataByName[$"{checkBaseName} {result}"];
            CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
        }

        public string GetChallengeBaseName(string levelId)
        {
            string displayName = Randomizer.ItemTracker.GetChallengeDisplayName(levelId);
            string challengeLevel = levelId[^1].ToString();
            return $"{displayName}: {challengeLevel}";
        }

        public void CheckLevelCompletion(GameManager.EEndCause endCause, string levelId)
        {
            Logger.LogInfo($"Level {levelId} ended due to {endCause}");
            if (
                endCause != GameManager.EEndCause.ChallengePlayed
                && endCause != GameManager.EEndCause.StageCompleted
                && endCause != GameManager.EEndCause.TutorialCompleted
            )
            {
                return;
            }

            if (Randomizer.ItemTracker.IsChallenge(levelId) && HasChallengeAnyResults(levelId))
            {
                string checkBaseName = GetChallengeBaseName(levelId);

                Location location = Locations.LocationDataByName[$"{checkBaseName} Completion"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
            else if (!Randomizer.ItemTracker.IsChallenge(levelId))
            {
                Location location = Locations.LocationDataByName[$"{levelId} Completion"];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
        }

        private int ChallengeResults(string levelId)
        {
            string checkBaseName = GetChallengeBaseName(levelId);
            return CheckedLocations.Keys.Count(loc =>
                loc == $"{checkBaseName} Bronze"
                || loc == $"{checkBaseName} Silver"
                || loc == $"{checkBaseName} Gold"
            );
        }

        private bool HasChallengeAnyResults(string levelId)
        {
            string checkBaseName = GetChallengeBaseName(levelId);
            bool hasLocationsCollected = LocationsCollected.Any(loc =>
                loc.LocationId == $"{checkBaseName} Bronze"
                || loc.LocationId == $"{checkBaseName} Silver"
                || loc.LocationId == $"{checkBaseName} Gold"
            );
            return hasLocationsCollected;
        }

        public void CheckStageCompletion(
            StageUnlocksData unlocksData,
            bool bossDefeated,
            string levelId
        )
        {
            Logger.LogInfo($"Level {levelId} ended and has boss defeated: {bossDefeated}");
            if (bossDefeated)
            {
                string checkName = LevelToDefeatedBossLocationName[levelId];
                Location location = Locations.LocationDataByName[checkName];
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), false);
            }
        }

        public void CheckSectionCompletion(
                PlayerWeaponType primaryWeapon,
                PlayerWeaponType secondaryWeapon,
                SkinType equippedOutfit,
                string songName
        )
        {
            Logger.LogInfo($"Section cleared with primary {primaryWeapon}, secondary {secondaryWeapon} and song {songName}");
            if(primaryWeapon != PlayerWeaponType.None)
            {
                var pName = Lookup.GetCurrentWeaponName(primaryWeapon);
                CheckFirstSectionClear(pName);
            }

            if (secondaryWeapon != PlayerWeaponType.None){
                var sName = Lookup.GetCurrentWeaponName(secondaryWeapon);
                CheckFirstSectionClear(sName);
            }

            if(equippedOutfit != SkinType.Corrupted){
                string equippedSkinName = Randomizer.ItemTracker.GetOutfitNameByType(equippedOutfit);
                CheckFirstSectionClear(equippedSkinName);
            }

            CheckFirstSectionClear(songName);
        }


        internal void CheckFirstSectionClear(string sectionItem)
        {
            var locationId = $"Section Cleared with: {sectionItem}";
            if(Locations.LocationDataByName.TryGetValue(locationId, out var location))
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }


        public void Resync(ReadOnlyCollection<long> allLocationsChecked)
        {
            List<Location> locations = allLocationsChecked
                .Where(id => Locations.LocationDataById.ContainsKey(id))
                .Select(id => Locations.LocationDataById[id])
                .ToList();

            foreach (var location in locations)
            {
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
            }
        }

        public void Reset()
        {
            LocationsCollected.Clear();
            CheckedLocations.Clear();
        }

        internal void CheckSkinUnlocks(int coatOfArmsCount)
        {
            int skinLocationCount = GetSkinLocationAmount();
            if(coatOfArmsCount >= 1)
                GrantSkinLocation("Paz");
            if(coatOfArmsCount >= 2)
                GrantSkinLocation("Terminus");
            if(coatOfArmsCount >= 3)
                GrantSkinLocation("Persephone");
            if(coatOfArmsCount >= 4)
                GrantSkinLocation("The Hounds");
            if(coatOfArmsCount >= 5)
                GrantSkinLocation("Vulcan");
            if(coatOfArmsCount >= 6)
                GrantSkinLocation("Hellcrow");
        }

        private void GrantSkinLocation(string weaponName)
        {
            var locationId = $"{weaponName} Weapon Skin Unlock";
            var location = Locations.LocationDataByName[locationId];
            CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }



        internal void CheckMisc(string itemName)
        {
            var locationId = $"First Miscellaneous: {itemName}";
            if(Locations.LocationDataByName.TryGetValue(locationId, out var location))
                CollectLocation(location, IsLocationTypeRandomized(location.LocationType), true);
        }

        internal int GetSkinLocationAmount()
        {
            return Randomizer.ItemTracker.GetCollectedCoatOfArms() >= 2
                ? 1
                : 0 + Randomizer.ItemTracker.GetCollectedCoatOfArms() / 6;
        }

        internal List<string> GetItemsWithMissingChecks(List<string> unlockedItems)
        {
            List<string> missingItems = new List<string>();
            foreach (string unlockedItem in unlockedItems)
            {
                var checkName = $"Section Cleared with: {unlockedItem}";
                if(!CheckedLocations.ContainsKey(checkName)){
                    missingItems.Add(unlockedItem);
                    Logger.LogDebug($"Missing Check {checkName}, adding {unlockedItem} to missing items");
                }
            }
            Logger.LogInfo($"Returning missing items: {string.Join(", ", missingItems)}");
            return missingItems;
        }

        internal bool HasUncheckedSongs()
        {
            return Randomizer.Settings.RandomizedSongsEnabled
                && (
                    LocationAccessibility.CanReachAny(getUncheckedLocationsByType(ELocationType.SectionClearMainSong))
                    || LocationAccessibility.CanReachAny(getUncheckedLocationsByType(ELocationType.SectionClearBossSong))
                );
        }

        internal bool HasUncheckedOutfits()
        {
            return Randomizer.Settings.RandomizedOutfitsEnabled
                && (
                    LocationAccessibility.CanReachAny(getUncheckedLocationsByType(ELocationType.SectionClearOutfit))
                );
        }

        internal bool HasUncheckedWeapons()
        {
            return Randomizer.Settings.HellsRandomizedWeaponsEnabled
                && (
                    LocationAccessibility.CanReachAny(getUncheckedLocationsByType(ELocationType.SectionClearWeapon))
                );
        }

        internal List<PlayerWeaponType> GetUncheckedWeapons(List<PlayerWeaponType> availableWeapons)
        {
            List<PlayerWeaponType> uncheckedWeapons = new() { };

            if (!Randomizer.Settings.HellsRandomizedWeaponsEnabled)
                return uncheckedWeapons;

            foreach (var weapon in availableWeapons)
            {
                foreach (var name in Lookup.WeaponTypeToAllWeaponNames[weapon])
                {
                    var locationName = $"Section Cleared with: {name}";
                    if (
                        !CheckedLocations.ContainsKey(locationName)
                        && LocationAccessibility.CanReach(locationName)
                    )
                        uncheckedWeapons.Add(weapon);
                }
            }
            return uncheckedWeapons;
        }

        private List<Location> getUncheckedLocationsByType(ELocationType type)
        {
            return Locations
                .LocationDataByName.Where(kvp => kvp.Value.LocationType == type && !CheckedLocations.ContainsKey(kvp.Key))
                .Select(kvp => kvp.Value)
                .ToList();
        }

        internal bool HasUncheckedCodex()
        {
            return LocationAccessibility.CanReachAny(
                getUncheckedLocationsByType(ELocationType.Codex)
            );
        }

        internal bool HasUncheckedBestiary()
        {
            return LocationAccessibility.CanReachAny(
                getUncheckedLocationsByType(ELocationType.Bestiary)
            );
        }

        internal bool HasUncheckedCompanion()
        {
            return HasUncheckedBestiary() || HasUncheckedCodex();
        }

        internal bool HasChecksOpen(string levelID, bool shouldRandomizeLevel)
        {
            string actualLevelId = levelID;
            if(shouldRandomizeLevel)
                actualLevelId = Randomizer.ItemTracker.GetRandomizedLevel(levelID);
            EZone zone = Randomizer.ItemTracker.GetZoneForLevelId(actualLevelId);
            EArena arena = Randomizer.ItemTracker.GetArenasForLevelId(actualLevelId);

            var openLocationIds = Randomizer.Archipelago.GetOpenLocations();
            foreach (long id in openLocationIds)
            {
                if (
                    Locations.LocationDataById.TryGetValue(id, out Location location)
                    && location.Zone == zone
                    && arena.HasFlag(location.Arena)
                    && LocationAccessibility.CanReach(location)
                )
                    return true;
            }

            return true;
            // return false;
        }

        internal CollectiblesStageData GetCollectiblesForHells(string levelId)
        {
            Il2CppSystem.Collections.Generic.Dictionary<EDifficulty, CollectiblesStageData.CollectibleData> stageData = new () { };
            stageData.System_Collections_IDictionary_Add((int)EDifficulty.Easy, new CollectiblesStageData.CollectibleData(0,1).BoxIl2CppObject());
            stageData.System_Collections_IDictionary_Add((int)EDifficulty.Medium, new CollectiblesStageData.CollectibleData(0,1).BoxIl2CppObject());
            stageData.System_Collections_IDictionary_Add((int)EDifficulty.Hard, new CollectiblesStageData.CollectibleData(0,1).BoxIl2CppObject());
            stageData.System_Collections_IDictionary_Add((int)EDifficulty.VeryHard, new CollectiblesStageData.CollectibleData(0,1).BoxIl2CppObject());

            return new CollectiblesStageData{LevelID = levelId, DifficultyCollectibleData = stageData};
        }

        internal int GetOpenCoatOfArmsChecks(EZone zone)
        {
            int totalCount = zone == EZone.Global ? 32 : 4;

            Func<Location, bool> predicate =
                zone == EZone.Global
                    ? loc => loc.LocationType == ELocationType.CoatOfArms
                    : loc => loc.LocationType == ELocationType.CoatOfArms && loc.Zone == zone;

            int collectedCount = LocationsCollected
                .Where(predicate)
                .Select(loc => loc.ArchipelagoId)
                .Distinct()
                .Count();

            return totalCount - collectedCount;
        }

        internal bool HasClearedLevel(string levelID)
        {
            string actualLevelName = Randomizer.ItemTracker.GetActualLevelName(levelID);
            return CheckedLocations.ContainsKey($"{actualLevelName} Completion");
        }

        internal int GetReachedChallengeMedaillon(string levelID)
        {
            int results = 0;

            if(Randomizer.Settings.ChallengeMedaillonsEnabled)
                results = ChallengeResults(levelID);

            return results;

        }

        internal bool IsOutfitUnchecked(SkinType outfitType)
        {
            if(!Randomizer.ItemTracker.IsOutfitUnlocked(outfitType))
                return false;

            string outfitName = Randomizer.ItemTracker.GetOutfitNameByType(outfitType);
            string locationName = $"Section Cleared with: {outfitName}";
            if(CheckedLocations.ContainsKey(locationName))
                return false;

            return LocationAccessibility.CanReach(locationName);
        }

        internal bool IsWeaponUnchecked(PlayerWeaponType weaponType)
        {
            if(!Randomizer.ItemTracker.IsWeaponUnlocked(weaponType))
                return false;

            return GetUncheckedWeapons(new (){weaponType}).Count > 0;
        }

        internal List<ExtendedWeaponType> GetUncheckedPersephoneLocations(List<ExtendedWeaponType> availablePersephoneTypes)
        {
            List<ExtendedWeaponType> missingTypes = new (){};
            foreach (var type in availablePersephoneTypes)
            {
                string name = Lookup.PersephoneTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if(!CheckedLocations.ContainsKey(locationName) 
                        && LocationAccessibility.CanReach(locationName) )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        internal List<WeaponType> GetUncheckedHoundsLocations(List<WeaponType> availableTypes)
        {
            List<WeaponType> missingTypes = new (){};
            foreach (var type in availableTypes)
            {
                string name = Lookup.HoundsTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if(!CheckedLocations.ContainsKey(locationName) 
                        && LocationAccessibility.CanReach(locationName) )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        internal List<WeaponType> GetUncheckedVulcanLocations(List<WeaponType> availableTypes)
        {
            List<WeaponType> missingTypes = new (){};
            foreach (var type in availableTypes)
            {
                string name = Lookup.VulcanTypeToName[type];
                string locationName = $"Section Cleared with: {name}";
                if(!CheckedLocations.ContainsKey(locationName) 
                        && LocationAccessibility.CanReach(locationName) )
                    missingTypes.Add(type);
            }
            return missingTypes;
        }

        //TODO: Leviathan integration
        public void CheckLeviathanCompletion(StageUnlocksData unlocksData) { }

    }
}
