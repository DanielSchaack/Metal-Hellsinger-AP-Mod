using System.Collections.Generic;
using static Randomizer.ItemOrigin;
using static Randomizer.Locations.EZone;
using static Randomizer.Locations.EArena;
using static Randomizer.Locations;
using System;
using System.Linq;
using Outsiders.GUI;

namespace Randomizer
{
    public class Lookup
    {
        public static readonly Dictionary<string, EZone> LevelIdToEZone = new Dictionary<string, EZone>()
        {
            { "EndlessModeBase", Leviathan },
            { "Tutorial", EZone.Tutorial },
            { "Voke", Voke },
            { "Stygia", Stygia },
            { "Yhelm", Yhelm },
            { "Incaustis", Incaustis },
            { "Gehenna", Gehenna },
            { "Nihil", Nihil },
            { "Acheron", Acheron },
            { "Sheol", Sheol },

            { "CH_Amdusias1", KillingWithRhythm },
            { "CH_Amdusias2", KillingWithRhythm },
            { "CH_Amdusias3", KillingWithRhythm },

            { "CH_Marbas1", WeaponTrickery },
            { "CH_Marbas2", WeaponTrickery },
            { "CH_Marbas3", WeaponTrickery },

            { "CH_Halphas1", RelicThief },
            { "CH_Halphas2", RelicThief },
            { "CH_Halphas3", RelicThief },

            { "CH_Bune1", Giantslayer },
            { "CH_Bune2", Giantslayer },
            { "CH_Bune3", Giantslayer },

            { "CH_Morax1", DeathsEdge },
            { "CH_Morax2", DeathsEdge },
            { "CH_Morax3", DeathsEdge },

            { "CH_Flauros1", UltimateMastery },
            { "CH_Flauros2", UltimateMastery },
            { "CH_Flauros3", UltimateMastery },

            { "CH_Glasya1", SlaughterMastery },
            { "CH_Glasya2", SlaughterMastery },
            { "CH_Glasya3", SlaughterMastery },
        };

        public static readonly Dictionary<EZone, string> EZoneToChallengeBaseId = new Dictionary<EZone, string>()
        {
            { KillingWithRhythm, "CH_Amdusias" },
            { WeaponTrickery,    "CH_Marbas" },
            { RelicThief,        "CH_Halphas" },
            { Giantslayer,       "CH_Bune" },
            { DeathsEdge,        "CH_Morax" },
            { UltimateMastery,   "CH_Flauros" },
            { SlaughterMastery,  "CH_Glasya" },
        };

        public static readonly Dictionary<EZone, List<(EZone, EArena)>> EZoneToIndividualLevels =
            new Dictionary<EZone, List<(EZone, EArena)>>()
            {
                { EZone.Tutorial, [(EZone.Tutorial, EArena.Tutorial)] },
                {
                    Voke,
                    [
                        (Voke, Arena1),
                        (KillingWithRhythm, Torment1),
                        (WeaponTrickery, Torment1),
                        (RelicThief, Torment1),
                    ]
                },
                {
                    Stygia,
                    [
                        (Stygia, Arena1),
                        (Giantslayer, Torment1),
                        (DeathsEdge, Torment1),
                        (RelicThief, Torment2),
                    ]
                },
                {
                    Yhelm,
                    [
                        (Yhelm, Arena1),
                        (UltimateMastery, Torment1),
                        (KillingWithRhythm, Torment2),
                        (WeaponTrickery, Torment2),
                    ]
                },
                {
                    Incaustis,
                    [
                        (Incaustis, Arena1),
                        (SlaughterMastery, Torment1),
                        (Giantslayer, Torment2),
                        (RelicThief, Torment3),
                    ]
                },
                {
                    Gehenna,
                    [
                        (Gehenna, Arena1),
                        (DeathsEdge, Torment2),
                        (KillingWithRhythm, Torment3),
                        (WeaponTrickery, Torment3),
                    ]
                },
                {
                    Nihil,
                    [
                        (Nihil, Arena1),
                        (UltimateMastery, Torment2),
                        (SlaughterMastery, Torment2),
                        (Giantslayer, Torment3),
                    ]
                },
                {
                    Acheron,
                    [
                        (Acheron, Arena1),
                        (DeathsEdge, Torment3),
                        (UltimateMastery, Torment3),
                        (SlaughterMastery, Torment3),
                    ]
                },
                {
                    Sheol,
                    [
                        (Sheol, Arena1),
                    ]
                },
                {
                    EZone.Leviathan,
                    [
                        (Leviathan, EArena.Bridge),
                        (Leviathan, EArena.Ziggurat),
                        (Leviathan, EArena.WalledGarden),
                        (Leviathan, EArena.Pyramid),
                        (Leviathan, EArena.Monument),
                        (Leviathan, EArena.HighRode),
                        (Leviathan, EArena.FinalDestination),
                    ]
                },
            };

        public static readonly Dictionary<string, EArena> LevelIdToArena = new Dictionary<string, EArena>()
        {
            { "Tutorial", EArena.Global | EArena.Tutorial },
            { "EndlessModeBase", EArena.Global | WalledGarden | HighRode | Bridge | Pyramid | Monument | Ziggurat | FinalDestination},

            { "Voke", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Stygia", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Yhelm", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Incaustis", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Gehenna", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Nihil", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Acheron", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },
            { "Sheol", EArena.Global | Arena1 | Arena2 | Arena3 | Arena4 | Boss },

            { "CH_Amdusias1", EArena.Global | Torment1 },
            { "CH_Marbas1", EArena.Global | Torment1 },
            { "CH_Halphas1", EArena.Global | Torment1 },
            { "CH_Bune1", EArena.Global | Torment1 },
            { "CH_Morax1", EArena.Global | Torment1 },
            { "CH_Flauros1", EArena.Global | Torment1 },
            { "CH_Glasya1", EArena.Global | Torment1 },

            { "CH_Halphas2", EArena.Global | Torment2 },
            { "CH_Amdusias2", EArena.Global | Torment2 },
            { "CH_Marbas2", EArena.Global | Torment2 },
            { "CH_Bune2", EArena.Global | Torment2 },
            { "CH_Morax2", EArena.Global | Torment2 },
            { "CH_Flauros2", EArena.Global | Torment2 },
            { "CH_Glasya2", EArena.Global | Torment2 },

            { "CH_Halphas3", EArena.Global | Torment3 },
            { "CH_Amdusias3", EArena.Global | Torment3 },
            { "CH_Marbas3", EArena.Global | Torment3 },
            { "CH_Bune3", EArena.Global | Torment3 },
            { "CH_Morax3", EArena.Global | Torment3 },
            { "CH_Flauros3", EArena.Global | Torment3 },
            { "CH_Glasya3", EArena.Global | Torment3 },
        };

        public static readonly Dictionary<string, string> LevelIdToActualName = new Dictionary<string, string>()
        {
            { "EndlessModeBase", "Leviathan" },
            { "Tutorial", "Tutorial" },
            { "Voke", "Voke" },
            { "Stygia", "Stygia" },
            { "Yhelm", "Yhelm" },
            { "Incaustis", "Incaustis" },
            { "Gehenna", "Gehenna" },
            { "Nihil", "Nihil" },
            { "Acheron", "Acheron" },
            { "Sheol", "Sheol" },
            { "CH_Amdusias1", "Killing with Rhythm: 1" },
            { "CH_Marbas1", "Weapon Trickery: 1" },
            { "CH_Halphas1", "Relic Thief: 1" },
            { "CH_Bune1", "Giantslayer: 1" },
            { "CH_Morax1", "Death's Edge: 1" },
            { "CH_Halphas2", "Relic Thief: 2" },
            { "CH_Flauros1", "Ultimate Mastery: 1" },
            { "CH_Amdusias2", "Killing with Rhythm: 2" },
            { "CH_Marbas2", "Weapon Trickery: 2" },
            { "CH_Glasya1", "Slaughter Mastery: 1" },
            { "CH_Bune2", "Giantslayer: 2" },
            { "CH_Halphas3", "Relic Thief: 3" },
            { "CH_Morax2", "Death's Edge: 2" },
            { "CH_Amdusias3", "Killing with Rhythm: 3" },
            { "CH_Marbas3", "Weapon Trickery: 3" },
            { "CH_Flauros2", "Ultimate Mastery: 2" },
            { "CH_Glasya2", "Slaughter Mastery: 2" },
            { "CH_Bune3", "Giantslayer: 3" },
            { "CH_Morax3", "Death's Edge: 3" },
            { "CH_Flauros3", "Ultimate Mastery: 3" },
            { "CH_Glasya3", "Slaughter Mastery: 3" },
        };

        public static readonly Dictionary<string, LevelCode> LevelIdToLevelCode = new ()
        {
            // { "EndlessModeBase", "Leviathan" },
            { "Tutorial", new LevelCode(0,0) },
            { "Voke", new LevelCode(1,0) },
            { "Stygia", new LevelCode(2,0) },
            { "Yhelm", new LevelCode(3,0) },
            { "Incaustis", new LevelCode(4,0) },
            { "Gehenna", new LevelCode(5,0) },
            { "Nihil", new LevelCode(6,0) },
            { "Acheron", new LevelCode(7,0) },
            { "Sheol", new LevelCode(8,0) },
            { "CH_Amdusias1", new LevelCode(1,1) },
            { "CH_Marbas1", new LevelCode(1,2) },
            { "CH_Halphas1", new LevelCode(1,3) },
            { "CH_Bune1", new LevelCode(2,1) },
            { "CH_Morax1", new LevelCode(2,2) },
            { "CH_Halphas2", new LevelCode(2,3) },
            { "CH_Flauros1", new LevelCode(3,1) },
            { "CH_Amdusias2", new LevelCode(3,2) },
            { "CH_Marbas2", new LevelCode(3,3) },
            { "CH_Glasya1", new LevelCode(4,1) },
            { "CH_Bune2", new LevelCode(4,2) },
            { "CH_Halphas3", new LevelCode(4,3) },
            { "CH_Morax2", new LevelCode(5,1) },
            { "CH_Amdusias3", new LevelCode(5,2) },
            { "CH_Marbas3", new LevelCode(5,3) },
            { "CH_Flauros2", new LevelCode(6,1) },
            { "CH_Glasya2", new LevelCode(6,2) },
            { "CH_Bune3", new LevelCode(6,3) },
            { "CH_Morax3", new LevelCode(7,1) },
            { "CH_Flauros3", new LevelCode(7,2) },
            { "CH_Glasya3", new LevelCode(7,3) },
        };
        public static readonly Dictionary<LevelCode, string> LevelCodeToLevelId =
            LevelIdToLevelCode.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, List<string>> RequiredLevelItems = new Dictionary<
            string,
            List<string>
        >()
        {
            { "EndlessModeBase", new List<string> { "Leviathan" } },
            { "Tutorial", new List<string> { "Hells", "Tutorial" } },
            { "Voke", new List<string> { "Hells", "Voke" } },
            { "Stygia", new List<string> { "Hells", "Stygia" } },
            { "Yhelm", new List<string> { "Hells", "Yhelm" } },
            { "Incaustis", new List<string> { "Hells", "Incaustis" } },
            { "Gehenna", new List<string> { "Hells", "Gehenna" } },
            { "Nihil", new List<string> { "Hells", "Nihil" } },
            { "Acheron", new List<string> { "Hells", "Acheron" } },
            { "Sheol", new List<string> { "Hells", "Sheol" } },
            { "CH_Amdusias1", new List<string> { "Hells", "Voke", "Killing with Rhythm: 1" } },
            { "CH_Marbas1", new List<string> { "Hells", "Voke", "Weapon Trickery: 1" } },
            { "CH_Halphas1", new List<string> { "Hells", "Voke", "Relic Thief: 1" } },
            { "CH_Bune1", new List<string> { "Hells", "Stygia", "Giantslayer: 1" } },
            { "CH_Morax1", new List<string> { "Hells", "Stygia", "Death's Edge: 1" } },
            { "CH_Halphas2", new List<string> { "Hells", "Stygia", "Relic Thief: 2" } },
            { "CH_Flauros1", new List<string> { "Hells", "Yhelm", "Ultimate Mastery: 1" } },
            { "CH_Amdusias2", new List<string> { "Hells", "Yhelm", "Killing with Rhythm: 2" } },
            { "CH_Marbas2", new List<string> { "Hells", "Yhelm", "Weapon Trickery: 2" } },
            { "CH_Glasya1", new List<string> { "Hells", "Incaustis", "Slaughter Mastery: 1" } },
            { "CH_Bune2", new List<string> { "Hells", "Incaustis", "Giantslayer: 2" } },
            { "CH_Halphas3", new List<string> { "Hells", "Incaustis", "Relic Thief: 3" } },
            { "CH_Morax2", new List<string> { "Hells", "Gehenna", "Death's Edge: 2" } },
            { "CH_Amdusias3", new List<string> { "Hells", "Gehenna", "Killing with Rhythm: 3" } },
            { "CH_Marbas3", new List<string> { "Hells", "Gehenna", "Weapon Trickery: 3" } },
            { "CH_Flauros2", new List<string> { "Hells", "Nihil", "Ultimate Mastery: 2" } },
            { "CH_Glasya2", new List<string> { "Hells", "Nihil", "Slaughter Mastery: 2" } },
            { "CH_Bune3", new List<string> { "Hells", "Nihil", "Giantslayer: 3" } },
            { "CH_Morax3", new List<string> { "Hells", "Acheron", "Death's Edge: 3" } },
            { "CH_Flauros3", new List<string> { "Hells", "Acheron", "Ultimate Mastery: 3" } },
            { "CH_Glasya3", new List<string> { "Hells", "Acheron", "Slaughter Mastery: 3" } },
        };

        public static readonly Dictionary<string, List<string>> RequiredWeaponsForLevel = new Dictionary<
            string,
            List<string>
        >()
        {
            { "Tutorial", new List<string> { } },
            { "Voke", new List<string> { } },
            { "Stygia", new List<string> { } },
            { "Yhelm", new List<string> { } },
            { "Incaustis", new List<string> { } },
            { "Gehenna", new List<string> { } },
            { "Nihil", new List<string> { } },
            { "Acheron", new List<string> { } },
            { "Sheol", new List<string> { } },
            { "CH_Amdusias1", new List<string> { "Paz", "Persephone" } },
            { "CH_Marbas1", new List<string> { "Terminus", "Persephone" } },
            { "CH_Halphas1", new List<string> { "Terminus", "Persephone" } },
            { "CH_Bune1", new List<string> { "Terminus", "Persephone", "The Hounds" } },
            { "CH_Morax1", new List<string> { "Paz", "Terminus", "Persephone", "The Hounds" } },
            { "CH_Halphas2", new List<string> { "Terminus", "Persephone", "The Hounds" } },
            { "CH_Flauros1", new List<string> { "Terminus", "Persephone" } },
            { "CH_Amdusias2", new List<string> { "Paz", "Terminus", "The Hounds" } },
            { "CH_Marbas2", new List<string> { "Terminus", "Persephone", "The Hounds", "Vulcan" } },
            { "CH_Glasya1", new List<string> { "Paz", "Terminus", "The Hounds" } },
            { "CH_Bune2", new List<string> { "Terminus", "The Hounds", "Hellcrow" } },
            { "CH_Halphas3", new List<string> { "Terminus", "Vulcan", "Hellcrow" } },
            { "CH_Morax2", new List<string> { "Paz", "Terminus", "Persephone", "The Hounds" } },
            { "CH_Amdusias3", new List<string> { "Paz", "Terminus", "Hellcrow" } },
            { "CH_Marbas3", new List<string> { "Terminus", "Persephone", "The Hounds", "Vulcan", "Hellcrow" } },
            { "CH_Flauros2", new List<string> { "Terminus", "Persephone", "Hellcrow" } },
            { "CH_Glasya2", new List<string> { "Paz", "Terminus", "The Hounds" } },
            { "CH_Bune3", new List<string> { "Terminus", "Persephone", "The Hounds" } },
            { "CH_Morax3", new List<string> { "Paz", "Terminus", "Vulcan", "Hellcrow" } },
            { "CH_Flauros3", new List<string> { "Terminus", "Persephone", "The Hounds" } },
            { "CH_Glasya3", new List<string> { "Paz", "Terminus", "The Hounds" } },
        };

        public static readonly Dictionary<string, int> RequiredProgressiveAmount = new Dictionary<
            string,
            int
        >()
        {
            { "Tutorial", 0 },
            { "Voke", 1 },
            { "Stygia", 2 },
            { "Yhelm", 3 },
            { "Incaustis", 4 },
            { "Gehenna", 5 },
            { "Nihil", 6 },
            { "Acheron", 7 },
            { "Sheol", 8 },
            { "CH_Amdusias1", 1 },
            { "CH_Amdusias2", 2 },
            { "CH_Amdusias3", 3 },
            { "CH_Bune1", 1 },
            { "CH_Bune2", 2 },
            { "CH_Bune3", 3 },
            { "CH_Flauros1", 1 },
            { "CH_Flauros2", 2 },
            { "CH_Flauros3", 3 },
            { "CH_Glasya1", 1 },
            { "CH_Glasya2", 2 },
            { "CH_Glasya3", 3 },
            { "CH_Halphas1", 1 },
            { "CH_Halphas2", 2 },
            { "CH_Halphas3", 3 },
            { "CH_Marbas1", 1 },
            { "CH_Marbas2", 2 },
            { "CH_Marbas3", 3 },
            { "CH_Morax1", 1 },
            { "CH_Morax2", 2 },
            { "CH_Morax3", 3 },
        };

        public static readonly Dictionary<string, string> ChallengeToHellDictionary =
            new Dictionary<string, string>()
            {
                { "Tutorial", "Tutorial" },
                { "Voke", "Voke" },
                { "Stygia", "Stygia" },
                { "Yhelm", "Yhelm" },
                { "Incaustis", "Incaustis" },
                { "Gehenna", "Gehenna" },
                { "Nihil", "Nihil" },
                { "Acheron", "Acheron" },
                { "Sheol", "Sheol" },
                { "CH_Amdusias1", "Voke" },
                { "CH_Marbas1", "Voke" },
                { "CH_Halphas1", "Voke" },
                { "CH_Bune1", "Stygia" },
                { "CH_Morax1", "Stygia" },
                { "CH_Halphas2", "Stygia" },
                { "CH_Flauros1", "Yhelm" },
                { "CH_Amdusias2", "Yhelm" },
                { "CH_Marbas2", "Yhelm" },
                { "CH_Glasya1", "Incaustis" },
                { "CH_Bune2", "Incaustis" },
                { "CH_Halphas3", "Incaustis" },
                { "CH_Morax2", "Gehenna" },
                { "CH_Amdusias3", "Gehenna" },
                { "CH_Marbas3", "Gehenna" },
                { "CH_Flauros2", "Nihil" },
                { "CH_Glasya2", "Nihil" },
                { "CH_Bune3", "Nihil" },
                { "CH_Morax3", "Acheron" },
                { "CH_Flauros3", "Acheron" },
                { "CH_Glasya3", "Acheron" },
            };

        public static readonly Dictionary<string, string> ChallengeIdToDisplayDictionary =
            new Dictionary<string, string>()
            {
                { "CH_Amdusias1", "Killing with Rhythm" },
                { "CH_Marbas1", "Weapon Trickery" },
                { "CH_Halphas1", "Relic Thief" },
                { "CH_Bune1", "Giantslayer" },
                { "CH_Morax1", "Death's Edge" },
                { "CH_Halphas2", "Relic Thief" },
                { "CH_Flauros1", "Ultimate Mastery" },
                { "CH_Amdusias2", "Killing with Rhythm" },
                { "CH_Marbas2", "Weapon Trickery" },
                { "CH_Glasya1", "Slaughter Mastery" },
                { "CH_Bune2", "Giantslayer" },
                { "CH_Halphas3", "Relic Thief" },
                { "CH_Morax2", "Death's Edge" },
                { "CH_Amdusias3", "Killing with Rhythm" },
                { "CH_Marbas3", "Weapon Trickery" },
                { "CH_Flauros2", "Ultimate Mastery" },
                { "CH_Glasya2", "Slaughter Mastery" },
                { "CH_Bune3", "Giantslayer" },
                { "CH_Morax3", "Death's Edge" },
                { "CH_Flauros3", "Ultimate Mastery" },
                { "CH_Glasya3", "Slaughter Mastery" },
            };

        public static readonly Dictionary<string, ESigilType> ChallengeIdToSigilType =
            new ()
            {
                { "CH_Amdusias1", ESigilType.BeatStreakSave },
                { "CH_Marbas1", ESigilType.WeaponSwitchBonus },
                { "CH_Halphas1", ESigilType.BeatStreakThreshold },
                { "CH_Bune1", ESigilType.MultiplierTierPostRezz },
                { "CH_Morax1", ESigilType.ExtraHp },
                { "CH_Halphas2", ESigilType.BeatStreakThreshold },
                { "CH_Flauros1", ESigilType.UltimateAutoRefill },
                { "CH_Amdusias2", ESigilType.BeatStreakSave },
                { "CH_Marbas2", ESigilType.WeaponSwitchBonus },
                { "CH_Glasya1", ESigilType.LongerSlaughter },
                { "CH_Bune2", ESigilType.MultiplierTierPostRezz },
                { "CH_Halphas3", ESigilType.BeatStreakThreshold },
                { "CH_Morax2", ESigilType.ExtraHp },
                { "CH_Amdusias3", ESigilType.BeatStreakSave },
                { "CH_Marbas3", ESigilType.WeaponSwitchBonus },
                { "CH_Flauros2", ESigilType.UltimateAutoRefill },
                { "CH_Glasya2", ESigilType.LongerSlaughter },
                { "CH_Bune3", ESigilType.MultiplierTierPostRezz },
                { "CH_Morax3", ESigilType.ExtraHp },
                { "CH_Flauros3", ESigilType.UltimateAutoRefill },
                { "CH_Glasya3", ESigilType.LongerSlaughter },
            };

        public static readonly Dictionary<string, int> ChallengeIdToUnlockOrder =
            new ()
            {
                { "CH_Amdusias1", 0 },
                { "CH_Marbas1", 0 },
                { "CH_Halphas1", 0 },
                { "CH_Bune1", 0 },
                { "CH_Morax1", 0 },
                { "CH_Halphas2", 1 },
                { "CH_Flauros1", 0 },
                { "CH_Amdusias2", 1 },
                { "CH_Marbas2", 1 },
                { "CH_Glasya1", 0 },
                { "CH_Bune2", 1 },
                { "CH_Halphas3", 2 },
                { "CH_Morax2", 1 },
                { "CH_Amdusias3", 2 },
                { "CH_Marbas3", 2 },
                { "CH_Flauros2", 1 },
                { "CH_Glasya2", 1 },
                { "CH_Bune3", 2 },
                { "CH_Morax3", 2 },
                { "CH_Flauros3", 2 },
                { "CH_Glasya3", 2 },
            };

        public static readonly Dictionary<string, PlayerWeaponType> WeaponNameToType = new()
        {
            { "Paz", PlayerWeaponType.RhythmWeapon },
            { "Terminus", PlayerWeaponType.Falx },
            { "Persephone", PlayerWeaponType.Shotgun },
            { "The Hounds", PlayerWeaponType.Pistols },
            { "Vulcan", PlayerWeaponType.Vulcan },
            { "Hellcrow", PlayerWeaponType.Boomerang },
            { "The Red Right Hand", PlayerWeaponType.AssaultRifle },
            { "Telos", PlayerWeaponType.Bow },
        };

        public static readonly Dictionary<PlayerWeaponType, string> WeaponTypeToName =
            WeaponNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, PlayerWeaponType> ExtendedWeaponNameToType = new()
        {
            { "Paz", PlayerWeaponType.RhythmWeapon },
            { "Terminus", PlayerWeaponType.Falx },
            { "Persephone", PlayerWeaponType.Shotgun },
            { "Lost Persephone", PlayerWeaponType.Shotgun },
            { "Manifested Persephone", PlayerWeaponType.Shotgun },
            { "The Hounds", PlayerWeaponType.Pistols },
            { "The Lost Hounds", PlayerWeaponType.Pistols },
            { "Vulcan", PlayerWeaponType.Vulcan },
            { "Lost Vulcan", PlayerWeaponType.Vulcan },
            { "Hellcrow", PlayerWeaponType.Boomerang },
            { "The Red Right Hand", PlayerWeaponType.AssaultRifle },
            { "Telos", PlayerWeaponType.Bow },
        };


        public static readonly Dictionary<string, string> WeaponNameToConfig = new()
        {
            { "Paz", "RhythmWeaponData" },
            { "Terminus", "FalxWeaponData" },
            { "Persephone", "ShotgunWeaponData" },
            { "Lost Persephone", "ShotgunCrowWeaponData" },
            { "Manifested Persephone", "ShotgunTwoShotWeaponData" },
            { "The Hounds", "PistolWeaponData" },
            { "The Lost Hounds", "PistolVulcanWeaponData" },
            { "Vulcan", "VulcanWeaponData" },
            { "Lost Vulcan", "VulcanBowWeaponData" },
            { "Hellcrow", "KawWeaponData" },
            { "The Red Right Hand", "AssaultRifleWeaponData" },
            { "Telos", "BowWeaponData" },
        };

        public static string GetCurrentWeaponName(PlayerWeaponType weapon)
        {
            string weaponName = weapon switch
            {
                PlayerWeaponType.AssaultRifle => Lookup.WeaponTypeToName[weapon],
                PlayerWeaponType.Bow => Lookup.WeaponTypeToName[weapon],
                PlayerWeaponType.RhythmWeapon => Lookup.WeaponTypeToName[weapon],
                PlayerWeaponType.Falx => Lookup.WeaponTypeToName[weapon],
                PlayerWeaponType.Shotgun => Lookup.PersephoneTypeToName[Randomizer.CurrentPersephoneConfig],
                PlayerWeaponType.Pistols => Lookup.HoundsTypeToName[Randomizer.CurrentHoundsConfig],
                PlayerWeaponType.Vulcan => Lookup.VulcanTypeToName[Randomizer.CurrentVulcanConfig],
                PlayerWeaponType.Boomerang => Lookup.WeaponTypeToName[weapon],
                _ => "",
            };
            Logger.LogDebug($"Returning for {weapon} the current weapon {weaponName}");
            return weaponName;
        }

        public enum WeaponType
        {
            Regular,
            Lost
        }

        public enum ExtendedWeaponType
        {
            Regular,
            Lost,
            Manifested
        }

        public static readonly Dictionary<ExtendedWeaponType, string> PersephoneTypeToName = new()
        {
            { ExtendedWeaponType.Regular, "Persephone" },
            { ExtendedWeaponType.Lost, "Lost Persephone" },
            { ExtendedWeaponType.Manifested, "Manifested Persephone" },
        };

        public static readonly Dictionary<WeaponType, string> HoundsTypeToName = new()
        {
            { WeaponType.Regular, "The Hounds" },
            { WeaponType.Lost, "The Lost Hounds" },
        };

        public static readonly Dictionary<WeaponType, string> VulcanTypeToName = new()
        {
            { WeaponType.Regular, "Vulcan" },
            { WeaponType.Lost, "Lost Vulcan" },
        };

        public static readonly List<string> PersephoneNames = new List<string>()
        {
            "Persephone",
            "Lost Persephone",
            "Manifested Persephone",
        };

        public static readonly List<string> HoundsNames = new List<string>()
        {
            "The Hounds",
            "The Lost Hounds",
        };

        public static readonly List<string> VulcanNames = new List<string>()
        {
            "Vulcan",
            "Lost Vulcan",
        };

        public static readonly Dictionary<PlayerWeaponType, List<string>> WeaponTypeToAllWeaponNames = new (){
            {PlayerWeaponType.AssaultRifle,  ["The Red Right Hand"]},
            {PlayerWeaponType.Bow,  ["Telos"]},
            {PlayerWeaponType.RhythmWeapon,  ["Paz"]},
            {PlayerWeaponType.Falx,  ["Terminus"]},
            {PlayerWeaponType.Boomerang,  ["Hellcrow"]},
            {PlayerWeaponType.Shotgun,  PersephoneNames},
            {PlayerWeaponType.Pistols,  HoundsNames},
            {PlayerWeaponType.Vulcan,  VulcanNames},
            {PlayerWeaponType.None,  []},
        };

        public static readonly Dictionary<SkinTargetType, PlayerWeaponType> SkinTypeToWeaponType =
            new()
            {
                { SkinTargetType.RhythmWeapon, PlayerWeaponType.RhythmWeapon },
                { SkinTargetType.Falx, PlayerWeaponType.Falx },
                { SkinTargetType.Shotgun, PlayerWeaponType.Shotgun },
                { SkinTargetType.Pistols, PlayerWeaponType.Pistols },
                { SkinTargetType.Vulcan, PlayerWeaponType.Vulcan },
                { SkinTargetType.Boomerang, PlayerWeaponType.Boomerang },
            };
        public static readonly Dictionary<PlayerWeaponType, SkinTargetType> WeaponTypeToSkinType =
            SkinTypeToWeaponType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, PlayerWeaponType> WeaponSkinNameToType = new()
        {
            { "Paz Skin", PlayerWeaponType.RhythmWeapon },
            { "Terminus Skin", PlayerWeaponType.Falx },
            { "Persephone Skin", PlayerWeaponType.Shotgun },
            { "The Hounds Skin", PlayerWeaponType.Pistols },
            { "Vulcan Skin", PlayerWeaponType.Vulcan },
            { "Hellcrow Skin", PlayerWeaponType.Boomerang },
        };
        public static readonly Dictionary<PlayerWeaponType, string> WeaponTypeToSkinName =
            WeaponSkinNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, ESigilType> SigilNameToType = new()
        {
            { "Progressive Streak Guardian", ESigilType.BeatStreakSave },
            { "Progressive Ghost Rounds", ESigilType.WeaponSwitchBonus },
            { "Progressive Boon Momentum", ESigilType.BeatStreakThreshold },
            { "Progressive Unyielding Fury", ESigilType.MultiplierTierPostRezz },
            { "Progressive Last Breath Aegis", ESigilType.ExtraHp },
            { "Progressive Ultimate Sovereignty", ESigilType.UltimateAutoRefill },
            { "Progressive The Perfectionist", ESigilType.LongerSlaughter },
        };
        public static readonly Dictionary<ESigilType, string> SigilTypeToName =
            SigilNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, EBeatStreakEffect> BoonNameToType = new()
        {
            { "Enduring Fury", EBeatStreakEffect.SlowerFuryDecay },
            { "Faster Ultimate Gain", EBeatStreakEffect.IncreasedUltimateBuildSpeed },
            { "Deadlier Dash", EBeatStreakEffect.IncreasedDashDamage },
            { "Explosive Slaughter", EBeatStreakEffect.ExplosiveSlaughters },
        };
        public static readonly Dictionary<EBeatStreakEffect, string> BoonTypeToName =
            BoonNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<string, EDifficulty> DifficultyNameToType = new()
        {
            { "Lamb", EDifficulty.Easy },
            { "Goat", EDifficulty.Medium },
            { "Beast", EDifficulty.Hard },
            { "Archdevil", EDifficulty.VeryHard },
        };
        public static readonly Dictionary<EDifficulty, string> DifficultyTypeToName =
            DifficultyNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<ItemOrigin, List<string>> OutfitNames = new()
        {
            { Base, new() { "Outfit of the Unknown", "Outfit of the Leviathan" } },
            { DreamOfTheBeast, new() { "Outfit of the Dark Devotee", "Outfit of the Morning Star", "Outfit of the Angel Eyes" } },
            { Purgatory, new() { "Outfit of the Obsidian", "Outfit of the Amethyst", "Outfit of the Chromatica" } },
        };
        public static readonly Dictionary<string, SkinType> OutfitNameToType = new()
        {
            { "Outfit of the Unknown", SkinType.None },
            { "Outfit of the Dark Devotee", SkinType.Outfit1 },
            { "Outfit of the Morning Star", SkinType.Outfit2 },
            { "Outfit of the Angel Eyes", SkinType.Outfit3 },
            { "Outfit of the Obsidian", SkinType.Outfit4 },
            { "Outfit of the Amethyst", SkinType.Outfit5 },
            { "Outfit of the Chromatica", SkinType.Outfit6 },
            { "Outfit of the Leviathan", SkinType.Outfit7 },
        };
        public static readonly Dictionary<SkinType, string> OutfitTypeToName =
            OutfitNameToType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly Dictionary<ItemOrigin, List<string>> SongNames = new()
        {
            {
                Base,
                new()
                {
                    "This is the End",
                    "Stygia (Song)",
                    "Burial At Night",
                    "This Devastation",
                    "Poetry of Cinder",
                    "Dissolution",
                    "Acheron (Song)",
                    "Silent No More",
                    "Blood and Law",
                    "Infernal Invocation I: Hopes and Fears",
                    "Infernal Invocation II: Defiance",
                    "Infernal Invocation III: Dreaming in Distortion",
                    "No Tomorrow",
                }
            },
            {
                Dusk,
                new()
                {
                    "Departure to Destruction",
                    "Hand Cannon",
                    "Burn in Hell",
                    "Murder Machine Inc",
                    "Endless",
                    "Mine Control",
                    "Sacrifice",
                    "Erebus Reaction",
                    "Bleeding Out",
                }
            },
            {
                EssentialHits,
                new()
                {
                    "Down With the Sickness",
                    "Uprising",
                    "Misery Business",
                    "Tsunami (Original Mix)",
                    "Runaway (U&I)",
                    "Feel Good Inc.",
                    "I Love It feat. Charli XCX",
                    "Personal Jesus",
                }
            },
            {
                DreamOfTheBeast,
                new()
                {
                    "Leviathan (Song)",
                    "Dream of the Beast",
                }
            },
            {
                Purgatory,
                new()
                {
                    "Swallow the Fire",
                    "Mouth of Hell",
                    "Goodbye, Morning Star",
                }
            },
        };
        public static readonly Dictionary<ItemOrigin, List<string>> MainSongNames = new()
        {
            {
                Base,
                new()
                {
                    "This is the End",
                    "Stygia (Song)",
                    "Burial At Night",
                    "This Devastation",
                    "Poetry of Cinder",
                    "Dissolution",
                    "Acheron (Song)",
                    "Silent No More",
                }
            },
            {
                Dusk,
                new()
                {
                    "Departure to Destruction",
                    "Hand Cannon",
                    "Burn in Hell",
                    "Murder Machine Inc",
                    "Endless",
                    "Mine Control",
                    "Sacrifice",
                    "Erebus Reaction",
                    "Bleeding Out",
                }
            },
            {
                EssentialHits,
                new()
                {
                    "Down With the Sickness",
                    "Uprising",
                    "Misery Business",
                    "Tsunami (Original Mix)",
                    "Runaway (U&I)",
                    "Feel Good Inc.",
                    "I Love It feat. Charli XCX",
                    "Personal Jesus",
                }
            },
            {
                DreamOfTheBeast,
                new()
                {
                    "Leviathan (Song)",
                    "Dream of the Beast",
                }
            },
            {
                Purgatory,
                new()
                {
                    "Swallow the Fire",
                    "Mouth of Hell",
                    "Goodbye, Morning Star",
                }
            },
        };

        public static readonly Dictionary<ItemOrigin, List<string>> BossSongNames = new()
        {
            {
                Base,
                new()
                {
                    "Blood and Law",
                    "Infernal Invocation I: Hopes and Fears",
                    "Infernal Invocation II: Defiance",
                    "Infernal Invocation III: Dreaming in Distortion",
                    "No Tomorrow",
                }
            },
            {
                Dusk,
                new()
                {
                    "Departure to Destruction",
                    "Hand Cannon",
                    "Burn in Hell",
                    "Murder Machine Inc",
                    "Endless",
                    "Mine Control",
                    "Sacrifice",
                    "Erebus Reaction",
                    "Bleeding Out",
                }
            },
            {
                EssentialHits,
                new()
                {
                    "Down With the Sickness",
                    "Uprising",
                    "Misery Business",
                    "Tsunami (Original Mix)",
                    "Runaway (U&I)",
                    "Feel Good Inc.",
                    "I Love It feat. Charli XCX",
                    "Personal Jesus",
                }
            },
        };

        public static readonly Dictionary<string, string> SongNameToId = new()
        {
            { "This is the End", "VokeSong" },
            { "Stygia (Song)", "StygiaSong" },
            { "Burial At Night", "YhelmSong" },
            { "This Devastation", "IncaustisSong" },
            { "Poetry of Cinder", "GehennaSong" },
            { "Dissolution", "NihilSong" },
            { "Acheron (Song)", "AcheronSong" },
            { "Silent No More", "SheolSong" },

            { "Blood and Law", "BossSong" },
            { "Infernal Invocation I: Hopes and Fears", "BossVariation01Song" },
            { "Infernal Invocation II: Defiance", "BossVariation02Song" },
            { "Infernal Invocation III: Dreaming in Distortion", "BossVariation03Song" },
            { "No Tomorrow", "SheolBossSong" },

            { "Leviathan (Song)", "DLC01Song" },
            { "Dream of the Beast", "DLC02Song" },
            { "Swallow the Fire", "PurgatorySong01" },
            { "Mouth of Hell", "PurgatorySong02" },
            { "Goodbye, Morning Star", "PurgatorySong03" },

            { "Down With the Sickness", "LicensedPack01_Song01" },
            { "Uprising", "LicensedPack01_Song02" },
            { "Misery Business", "LicensedPack01_Song03" },
            { "Tsunami (Original Mix)", "LicensedPack01_Song04" },
            { "Runaway (U&I)", "LicensedPack01_Song05" },
            { "Feel Good Inc.", "LicensedPack01_Song06" },
            { "I Love It feat. Charli XCX", "LicensedPack01_Song07" },
            { "Personal Jesus", "LicensedPack01_Song08" },

            { "Departure to Destruction", "LicensedPack02_Song01" },
            { "Hand Cannon", "LicensedPack02_Song02" },
            { "Burn in Hell", "LicensedPack02_Song03" },
            { "Murder Machine Inc", "LicensedPack02_Song04" },
            { "Endless", "LicensedPack02_Song05" },
            { "Mine Control", "LicensedPack02_Song06" },
            { "Sacrifice", "LicensedPack02_Song07" },
            { "Erebus Reaction", "LicensedPack02_Song08" },
            { "Bleeding Out", "LicensedPack02_Song09" },
        };
        public static readonly Dictionary<string, string> SongIdToName =
            SongNameToId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        [Flags]
        public enum SongId : long
        {
            ThisIsTheEnd = 1L << 0,
            Stygia = 1L << 1,
            BurialAtNight = 1L << 2,
            ThisDevastation = 1L << 3,
            PoetryOfCinder = 1L << 4,
            Dissolution = 1L << 5,
            Acheron = 1L << 6,
            SilentNoMore = 1L << 7,

            BloodAndLaw = 1L << 8,
            InfernalInvocation1_HopesAndFears = 1L << 9,
            InfernalInvocation2_Defiance = 1L << 10,
            InfernalInvocation3_DreamingInDistortion = 1L << 11,
            NoTomorrow = 1L << 12,

            LeviathanSong = 1L << 13,
            DreamOfTheBeast = 1L << 14,
            SwallowTheFire = 1L << 15,
            MouthOfHell = 1L << 16,
            GoodbyeMorningStar = 1L << 17,

            DownWithTheSickness = 1L << 18,
            Uprising = 1L << 19,
            MiseryBusiness = 1L << 20,
            Tsunami_OriginalMix = 1L << 21,
            Runaway_UI = 1L << 22,
            FeelGoodInc = 1L << 23,
            ILoveIt = 1L << 24,
            PersonalJesus = 1L << 25,

            DepartureToDestruction = 1L << 26,
            HandCannon = 1L << 27,
            BurnInHell = 1L << 28,
            MurderMachineInc = 1L << 29,
            Endless = 1L << 30,
            MineControl = 1L << 31,
            Sacrifice = 1L << 32,
            ErebusReaction = 1L << 33,
            BleedingOut = 1L << 34,
        }

        public static readonly Dictionary<string, SongId> SongNameToEnum = new()
        {
            { "This is the End", SongId.ThisIsTheEnd },
            { "Stygia (Song)", SongId.Stygia },
            { "Burial At Night", SongId.BurialAtNight },
            { "This Devastation", SongId.ThisDevastation },
            { "Poetry of Cinder", SongId.PoetryOfCinder },
            { "Dissolution", SongId.Dissolution },
            { "Acheron (Song)", SongId.Acheron },
            { "Silent No More", SongId.SilentNoMore },

            { "Blood and Law", SongId.BloodAndLaw },
            { "Infernal Invocation I: Hopes and Fears", SongId.InfernalInvocation1_HopesAndFears },
            { "Infernal Invocation II: Defiance", SongId.InfernalInvocation2_Defiance },
            { "Infernal Invocation III: Dreaming in Distortion", SongId.InfernalInvocation3_DreamingInDistortion },
            { "No Tomorrow", SongId.NoTomorrow },

            { "Leviathan (Song)", SongId.LeviathanSong },
            { "Dream of the Beast", SongId.DreamOfTheBeast },
            { "Swallow the Fire", SongId.SwallowTheFire },
            { "Mouth of Hell", SongId.MouthOfHell },
            { "Goodbye, Morning Star", SongId.GoodbyeMorningStar },

            { "Down With the Sickness", SongId.DownWithTheSickness },
            { "Uprising", SongId.Uprising },
            { "Misery Business", SongId.MiseryBusiness },
            { "Tsunami (Original Mix)", SongId.Tsunami_OriginalMix },
            { "Runaway (U&I)", SongId.Runaway_UI },
            { "Feel Good Inc.", SongId.FeelGoodInc },
            { "I Love It feat. Charli XCX", SongId.ILoveIt },
            { "Personal Jesus", SongId.PersonalJesus },

            { "Departure to Destruction", SongId.DepartureToDestruction },
            { "Hand Cannon", SongId.HandCannon },
            { "Burn in Hell", SongId.BurnInHell },
            { "Murder Machine Inc", SongId.MurderMachineInc },
            { "Endless", SongId.Endless },
            { "Mine Control", SongId.MineControl },
            { "Sacrifice", SongId.Sacrifice },
            { "Erebus Reaction", SongId.ErebusReaction },
            { "Bleeding Out", SongId.BleedingOut }
        };

        public static readonly Dictionary<SongId, string> SongEnumToName = 
            SongNameToEnum.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        [Flags]
        public enum OutfitId
        {
            TheUnknown = 1 << 0,
            LeviathanOutfit = 1 << 1,

            DarkDevotee = 1 << 2,
            MorningStar = 1 << 3,
            AngelEyes = 1 << 4,

            Obsidian = 1 << 5,
            Amethyst = 1 << 6,
            Chromatica = 1 << 7,
        }

        public static readonly Dictionary<string, OutfitId> OutfitNameToEnum = new()
        {
            { "Outfit of the Unknown", OutfitId.TheUnknown },
            { "Outfit of the Leviathan", OutfitId.LeviathanOutfit },
            { "Outfit of the Dark Devotee", OutfitId.DarkDevotee },
            { "Outfit of the Morning Star", OutfitId.MorningStar },
            { "Outfit of the Angel Eyes", OutfitId.AngelEyes },
            { "Outfit of the Obsidian", OutfitId.Obsidian },
            { "Outfit of the Amethyst", OutfitId.Amethyst },
            { "Outfit of the Chromatica", OutfitId.Chromatica },
        };

        public static readonly Dictionary<OutfitId, string> OutfitEnumToName =
            OutfitNameToEnum.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public static readonly List<string> HellsIDs = new ()
        {
            "Tutorial",
            "Voke",
            "Stygia",
            "Yhelm",
            "Incaustis",
            "Gehenna",
            "Nihil",
            "Acheron",
            "Sheol",
        };

        public static readonly List<string> ChallengeIDs = new()
        {
            "CH_Amdusias1",
            "CH_Marbas1",
            "CH_Halphas1",
            "CH_Bune1",
            "CH_Morax1",
            "CH_Halphas2",
            "CH_Flauros1",
            "CH_Amdusias2",
            "CH_Marbas2",
            "CH_Glasya1",
            "CH_Bune2",
            "CH_Halphas3",
            "CH_Morax2",
            "CH_Amdusias3",
            "CH_Marbas3",
            "CH_Flauros2",
            "CH_Glasya2",
            "CH_Bune3",
            "CH_Morax3",
            "CH_Flauros3",
            "CH_Glasya3",
        };

        public static readonly Dictionary<string, string> LevelToDefeatedBossLocationName =
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

        public static string EnemyClassToLocationName(EnemyClassType enemyClass) =>
            enemyClass switch
            {
                EnemyClassType.Marionette => "Marionette discovered",
                EnemyClassType.Cambion => "Cambion discovered",
                EnemyClassType.Stalker => "Stalker discovered",
                EnemyClassType.Reaver => "Behemoth discovered",
                EnemyClassType.LesserSeraph => "Lesser Seraph discovered",
                EnemyClassType.Eyeless => "Eyeless discovered",
                EnemyClassType.Hierophant => "Hierophant discovered",
                EnemyClassType.Elite_Cambion => "Shield Cambion discovered",
                EnemyClassType.Elite_Stalker => "Void Stalker discovered",
                EnemyClassType.Elite_Reaver => "Siege Behemoth discovered",
                EnemyClassType.Elite_Seraph => "Annihilator Seraph discovered",
                EnemyClassType.LostUnknown => "The Lost Unknown: Leviathan discovered",
                EnemyClassType.Boss_Voke => "Anger Aspect: Voke discovered",
                EnemyClassType.Boss_Stygia => "Charged Aspect: Stygia discovered",
                EnemyClassType.Boss_Yhelm => "Fortress Aspect: Yhelm discovered",
                EnemyClassType.Boss_Incaustis => "Infernal Fury Aspect: Incaustis discovered",
                EnemyClassType.Boss_Gehenna => "Hellstorm Aspect: Gehenna discovered",
                EnemyClassType.Boss_Nihil => "DoppelGanger Aspect: Nihil discovered",
                EnemyClassType.Boss_Acheron => "Wheel Aspect: Acheron discovered",
                EnemyClassType.Boss_Sheol => "Red Judge - Worldbreaker: Sheol discovered",
                _ => string.Empty,
            };

        public static readonly Dictionary<string, string> CoatOfArmToLocationName = new Dictionary<
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

        public static readonly Dictionary<string, EZone> LevelIdToZoneDictionary = new Dictionary<
            string,
            EZone
        >()
        {
            { "EndlessModeBase", EZone.Leviathan },
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

        public static readonly Dictionary<string, string> WorldItemToLocationName = new Dictionary<
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

        public static readonly List<string> WorldItemIDs = new()
        {
            "IP_BeatMatching",
            "IP_Slaughter",
            "IP_Fury",
            "IP_FuryBoost",
            "IP_Ultimates",
            "IP_QuickReload",
            "IP_Dashing",
            "WI_Paz",
            "WI_Pistols",
            "WI_Shotgun",
            "WI_Vulcan",
            "WI_Hellcrow",
            "WI_Marionette",
            "WI_Cambion",
            "WI_Elite_Cambion",
            "WI_Reaver",
            "WI_Elite_Reaver",
            "WI_Stalker",
            "WI_Elite_Stalker",
            "WI_Eyeless",
            "WI_Hierophant",
            "WI_LesserSeraph",
            "WI_Elite_LesserSeraph",
            "WI_Boss_Voke",
            "WI_Boss_Stygia",
            "WI_Boss_Yhelm",
            "WI_Boss_Incaustis",
            "WI_Boss_Gehenna",
            "WI_Boss_Nihil",
            "WI_Boss_Acheron",
            "WI_Boss_Sheol",
            "WI_LostUnknown",
        };

        public static readonly List<string> InstructionIDs = new ()
        {
            "HUD_prompt_info_UltimateWhat",
            "HUD_prompt_info_Ultimates",
            "HUD_prompt_info_HitStreakHowGained",
            "HUD_prompt_info_HitStreakHowLost",
            "HUD_prompt_objective_ReloadPrompt",
            "HUD_prompt_objective_QuickReload",
            "HUD_prompt_objective_UltimateReady",
            "HUD_prompt_objective_Dash",
            "HUD_prompt_info_FailedBeats",
        };

        public static readonly List<string> CompanionIds = new()
        {
            "Collectibles",
            "Combos",
            "BeatMatching",
            "Slaughter",
            "Fury",
            "HitStreak",
            "Score",
            "Ultimate",
            "QuickReload",
            "Dashing",
            "HitStreakBoons",
            "SongSelect",
            "Outfits",
            "Terminus",
            "Paz",
            "Persephone",
            "TheHounds",
            "Vulcan",
            "Hellcrow",
            "AssaultRifle",
            "Bow",
            "RhythmsEssence",
            "JugglersFeint",
            "DevilsAvarice",
            "FireOfVengeance",
            "HeartsAegis",
            "UltimateSovereignty",
            "SlaughtersKey",
            "SlowerFuryDecay",
            "IncreasedUltimateBuildSpeed",
            "IncreasedDashDamage",
            "ExplosiveSlaughters",
            "TrippleDash",
            "DoubleSlaughter",
            "DevilsFlight",
            "StyxReload",
            "UnholyMess",
            "HellsHeartbeat",
            "SlaughterAndKill",
            "DeathFromAbove",
            "FiveEndings",
            "ShatterTwo",
            "ChaosFlight",
            "SwitchDamage",
            "TripleKill",
            "ExplosionSlaughter",
            "DashKill",
            "DestructDash",
            "Easy",
            "Medium",
            "Hard",
            "VeryHard",
            "Voke",
            "Stygia",
            "Yhelm",
            "Incaustis",
            "Gehenna",
            "Nihil",
            "Acheron",
            "Sheol",
            "Amdusias",
            "Marbas",
            "Halphas",
            "Bune",
            "Morax",
            "Flauros",
            "Glasya",
            "Lore1",
            "Lore2",
            "Lore3",
            "Lore4",
            "Lore5",
            "Lore6",
            "LeviathanMode",
            "AltarOfEchoes",
            "Memories",
            "Dreams",
            "VoidEchoes",
            "LeviathanWeapons",
            "WeaponTypes",
            "Afflictions",
            "VoidTouched",
            "UltimateVessel",
            "NightmareCrystal",
        };

        public static readonly List<string> BossStartScenarioNames = new () {
            "Phase_0_Intro", // Voke
            "12-Phase0_Intro", // Stygia
            "Phase0_Intro", // Yhelm, Incaustis, Gehenna, Nihil, Acheron
            "Setup Boss", // Sheol
        };

        public static readonly List<string> BossEndScenarioNames = new () {
            "Tutorial_Outro",
            "CompleteStage",
            "Phase_CompleteStage", // Sheol
        };

        public static readonly Dictionary<EFuryComboType, string> FuryComboToLocationName =
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

        public static readonly Dictionary<string, int> LocationDestructionCountRequired =
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

        public static readonly Dictionary<string, string> LocationDestructionToCompletionId =
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

        public static readonly Dictionary<string, List<string>> RequiredSubCompletionsForArena = new Dictionary<
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

        internal static bool IsHellsLevelId(string actualLevelId)
        {
            return HellsIDs.Contains(actualLevelId);
        }

        internal static bool IsChallengeLevelId(string actualLevelId)
        {
            return ChallengeIDs.Contains(actualLevelId);
        }

        internal static bool IsLeviathanLevelId(string actualLevelId)
        {
            return !IsHellsLevelId(actualLevelId) && !IsChallengeLevelId(actualLevelId);
        }

        internal static ChallengeData GetChallengeDataByLevelId(string actualLevelId)
        {
            return new ChallengeData(
                LevelIdToLevelCode[actualLevelId],
                actualLevelId,
                0,
                ChallengeIdToUnlockOrder[actualLevelId],
                true,
                "",
                "",
                true,
                ChallengeIdToSigilType[actualLevelId],
                true
            );
        }

        internal static StageData GetStageDataByLevelId(string actualLevelId)
        {
            return new StageData(
                LevelIdToLevelCode[actualLevelId],
                actualLevelId,
                false,
                true,
                true,
                null,
                true,
                actualLevelId == "Tutorial",
                EDifficulty.VeryHard,
                true
            );
        }

        [Flags]
        public enum FillerId : long
        {
            NextMultiplier = 1 << 0,
            MaxMultiplier = 1 << 1,
            ResetMultiplier = 1 << 2,
            UltimateTrigger = 1 << 3,
            AlwaysOnBeat = 1 << 4,
            Complement = 1 << 5,
            Encouragement = 1 << 6,
            Failure = 1 << 7,
            DoubleTime = 1 << 8,
            HalfTime = 1 << 9,
            InvisibleWeapons = 1 << 10,
            WeaponTrickery = 1 << 11,
            Death = 1 << 12,
        }

        public static readonly Dictionary<string, FillerId> FillerNameToId = new()
        {
            { "Next Multiplier", FillerId.NextMultiplier },
            { "Max Multiplier", FillerId.MaxMultiplier },
            { "Reset Multiplier", FillerId.ResetMultiplier },
            { "Trigger Ultimate", FillerId.UltimateTrigger },
            { "Always on Beat", FillerId.AlwaysOnBeat },
            { "Complement", FillerId.Complement },
            { "Encouragement", FillerId.Encouragement },
            { "Failure", FillerId.Failure },
            { "Double Time", FillerId.DoubleTime },
            { "Half Time", FillerId.HalfTime },
            { "Invisible Weapons", FillerId.InvisibleWeapons },
            { "Weapon Trickery", FillerId.WeaponTrickery },
            { "Death", FillerId.Death},
        };
        public static readonly Dictionary<FillerId, string> FillerIdToName =
            FillerNameToId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);


        internal static FillerId GetTrapItems()
        {
            return FillerId.WeaponTrickery
                | FillerId.InvisibleWeapons
                | FillerId.HalfTime
                | FillerId.DoubleTime
                | FillerId.UltimateTrigger
                | FillerId.Death;
        }

        internal static FillerId GetFillerItems()
        {
            return FillerId.Complement
                | FillerId.Encouragement
                | FillerId.Failure
                | FillerId.NextMultiplier
                | FillerId.MaxMultiplier
                | FillerId.AlwaysOnBeat;
        }

        internal static string ZoneArenaToName(EZone item1, EArena item2)
        {
            bool isHells = item1 switch
            {
                EZone.Tutorial => true,
                Voke => true,
                Stygia => true,
                Yhelm => true,
                Incaustis => true,
                Gehenna => true,
                Nihil => true,
                Acheron => true,
                Sheol => true,
                _ => false,
            };

            if(isHells)
                return item1.ToString();

            if(item1 == Leviathan)
                return "EndlessModeBase";

            if(item1 == EZone.Global)
                return "";

            var baseString = EZoneToChallengeBaseId[item1];
            var number = item2 switch
            {
                Torment1 => "1",
                Torment2 => "2",
                Torment3 => "3",
                _ => "",
            };

            return $"{baseString}{number}";
        }
    }
}
