using System.Linq;
using System.Collections.Generic;
using static Randomizer.ItemGamemode;
using static Randomizer.ItemClassification;
using static Randomizer.ItemOrigin;
using static Randomizer.ItemType;
using Archipelago.MultiClient.Net.Models;
using System;

namespace Randomizer
{
    public enum ItemGamemode
    {
        HELL,
        LEVIATHAN,
        ALL,
    }

    [Flags]
    public enum ItemClassification
    {
        none = 0,
        trap = 1 << 0,
        filler = 1 << 1,
        useful = 1 << 2,
        progression = 1 << 3,
    }

    [Flags]
    public enum ItemOrigin
    {
        Base = 1,
        Dusk = 2,
        DreamOfTheBeast = 4,
        Purgatory = 8,
        EssentialHits = 16,
    }

    public enum ItemType{
        Gamemode,
        Completion,
        Difficulty,
        Hells,
        Torment,
        Sigil,
        Ability,
        Weapon,
        WeaponUpgrade,
        Skin,
        AnguishGate,
        Collectible,
        Dream,
        Memory,
        Song,
        Outfit,
        Combat,
        Global,
        Aspect,
    }

    public class ItemData
    {
        public long ArchipelagoId { get; set; }
        public string Name { get; set; }
        public ItemClassification Classification { get; set; }
        public ItemGamemode Gamemode { get; set; }
        public ItemOrigin Origin { get; set; }
        public ItemType Type { get; set; }
        public int QuantityToGive { get; set; }

        public ItemData() { }

        public ItemData(
            long id,
            string name,
            ItemClassification classification,
            ItemGamemode gamemode,
            ItemType itemType,
            int quantityToGive
        )
        {
            ArchipelagoId = id;
            Name = name;
            Classification = classification;
            Gamemode = gamemode;
            Origin = Base;
            Type = itemType;
            QuantityToGive = quantityToGive;
        }

        public ItemData(
            long id,
            string name,
            ItemClassification classification,
            ItemGamemode gamemode,
            ItemOrigin origin,
            ItemType itemType,
            int quantityToGive
        )
        {
            ArchipelagoId = id;
            Name = name;
            Classification = classification;
            Gamemode = gamemode;
            Origin = origin;
            Type = itemType;
            QuantityToGive = quantityToGive;
        }
    }

    public class Items
    {

        public static Dictionary<long, ItemInfo> ItemList = new Dictionary<long, ItemInfo>() { };
        public static readonly Dictionary<long, ItemData> ItemDataById = new Dictionary<long, ItemData>()
        {
            // Hells
            { 1, new ItemData(1, "Hells", progression, HELL, Gamemode, 1) },
            { 2, new ItemData(2, "Leviathan", progression, HELL, Gamemode, 1) },

            { 3, new ItemData(3, "Enduring Fury", useful, HELL, Ability, 1) },
            { 4, new ItemData(4, "Faster Ultimate Gain", useful, HELL, Ability, 1) },
            { 5, new ItemData(5, "Deadlier Dash Fury", useful, HELL, Ability, 1) },
            { 6, new ItemData(6, "Explosive Slaughter", useful, HELL, Ability, 1) },

            { 7, new ItemData(7, "Regressive Difficulty", progression, HELL, Difficulty, 1) },
            { 8, new ItemData(8, "Archdevil", progression, HELL, Difficulty, 1) },
            { 9, new ItemData(9, "Beast", progression, HELL, Difficulty, 1) },
            { 10, new ItemData(10, "Goat", progression, HELL, Difficulty, 1) },
            { 11, new ItemData(11, "Lamb", progression, HELL, Difficulty, 1) },

            { 12, new ItemData(12, "Progressive Killing With Rhythm", progression, HELL, Torment, 3) },
            { 13, new ItemData(13, "Killing With Rhythm 1", progression, HELL, Torment, 1) },
            { 14, new ItemData(14, "Killing With Rhythm 2", progression, HELL, Torment, 1) },
            { 15, new ItemData(15, "Killing With Rhythm 3", progression, HELL, Torment, 1) },

            { 16, new ItemData(16, "Progressive Weapon Trickery", progression, HELL, Torment, 3) },
            { 17, new ItemData(17, "Weapon Trickery: 1", progression, HELL, Torment, 1) },
            { 18, new ItemData(18, "Weapon Trickery: 2", progression, HELL, Torment, 1) },
            { 19, new ItemData(19, "Weapon Trickery: 3", progression, HELL, Torment, 1) },

            { 20, new ItemData(20, "Progressive Relic Thief", progression, HELL, Torment, 3) },
            { 21, new ItemData(21, "Relic Thief: 1", progression, HELL, Torment, 1) },
            { 22, new ItemData(22, "Relic Thief: 2", progression, HELL, Torment, 1) },
            { 23, new ItemData(23, "Relic Thief: 3", progression, HELL, Torment, 1) },

            { 24, new ItemData(24, "Progressive Giantslayer", progression, HELL, Torment, 3) },
            { 25, new ItemData(25, "Giantslayer: 1", progression, HELL, Torment, 1) },
            { 26, new ItemData(26, "Giantslayer: 2", progression, HELL, Torment, 1) },
            { 27, new ItemData(27, "Giantslayer: 3", progression, HELL, Torment, 1) },

            { 28, new ItemData(28, "Progressive Death's Edge", progression, HELL, Torment, 3) },
            { 29, new ItemData(29, "Death's Edge: 1", progression, HELL, Torment, 1) },
            { 30, new ItemData(30, "Death's Edge: 2", progression, HELL, Torment, 1) },
            { 31, new ItemData(31, "Death's Edge: 3", progression, HELL, Torment, 1) },

            { 32, new ItemData(32, "Progressive Ultimate Mastery", progression, HELL, Torment, 3) },
            { 33, new ItemData(33, "Ultimate Mastery: 1", progression, HELL, Torment, 1) },
            { 34, new ItemData(34, "Ultimate Mastery: 2", progression, HELL, Torment, 1) },
            { 35, new ItemData(35, "Ultimate Mastery: 3", progression, HELL, Torment, 1) },

            { 36, new ItemData(36, "Progressive Slaughter Mastery", progression, HELL, Torment, 3) },
            { 37, new ItemData(37, "Slaughter Mastery: 1", progression, HELL, Torment, 1) },
            { 38, new ItemData(38, "Slaughter Mastery: 2", progression, HELL, Torment, 1) },
            { 39, new ItemData(39, "Slaughter Mastery: 3", progression, HELL, Torment, 1) },

            { 40, new ItemData(40, "Anger Aspect: Voke defeated", progression, HELL, Completion, 1) },
            { 41, new ItemData(41, "Charged Aspect: Stygia defeated", progression, HELL, Completion, 1) },
            { 42, new ItemData(42, "Fortress Aspect: Yhelm defeated", progression, HELL, Completion, 1) },
            { 43, new ItemData(43, "Infernal Fury Aspect: Incaustis defeated", progression, HELL, Completion, 1) },
            { 44, new ItemData(44, "Hellstorm Aspect: Gehenna defeated", progression, HELL, Completion, 1) },
            { 45, new ItemData(45, "Doppelganger Aspect: Nihil defeated", progression, HELL, Completion, 1) },
            { 46, new ItemData(46, "Wheel Aspect: Acheron defeated", progression, HELL, Completion, 1) },
            { 47, new ItemData(47, "Red Judge - Worldbreaker: Sheol defeated", progression, HELL, Completion, 1) },
            { 48, new ItemData(48, "The Lost Unknown: Leviathan defeated", progression, LEVIATHAN, Completion, 1) },

            { 50, new ItemData(50, "Progressive Hells", progression, HELL, Hells, 8) },
            { 51, new ItemData(51, "Tutorial", progression, HELL, Hells, 1) },
            { 52, new ItemData(52, "Voke", progression, HELL, Hells, 1) },
            { 53, new ItemData(53, "Stygia", progression, HELL, Hells, 1) },
            { 54, new ItemData(54, "Yhelm", progression, HELL, Hells, 1) },
            { 55, new ItemData(55, "Incaustis", progression, HELL, Hells, 1) },
            { 56, new ItemData(56, "Gehenna", progression, HELL, Hells, 1) },
            { 57, new ItemData(57, "Nihil", progression, HELL, Hells, 1) },
            { 58, new ItemData(58, "Acheron", progression, HELL, Hells, 1) },
            { 59, new ItemData(59, "Sheol", progression, HELL, Hells, 1) },
            { 60, new ItemData(60, "Coat of Arms", progression, HELL, Collectible, 1) },
            { 61, new ItemData(61, "Coat of Arms Fill", useful, HELL, Collectible, 1) },

            { 62, new ItemData(62, "Garden of Chronos", progression, LEVIATHAN, Hells, 1) },
            { 63, new ItemData(63, "Calamity", progression, LEVIATHAN, Hells, 1) },
            { 64, new ItemData(64, "Demonitorium", progression, LEVIATHAN, Hells, 1) },
            { 65, new ItemData(65, "Tombs of the Ancients", progression, LEVIATHAN, Hells, 1) },
            { 66, new ItemData(66, "Necropolis", progression, LEVIATHAN, Hells, 1) },
            { 67, new ItemData(67, "Axiom", progression, LEVIATHAN, Hells, 1) },
            { 68, new ItemData(68, "Final Destination", progression, LEVIATHAN, Hells, 1) },

            { 70, new ItemData(70, "Progressive Tutorial Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 71, new ItemData(71, "Tutorial Anguish Gate 1", progression, HELL, AnguishGate, 1) },

            { 72, new ItemData(72, "Progressive Voke Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 73, new ItemData(73, "Voke Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 74, new ItemData(74, "Voke Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 75, new ItemData(75, "Voke Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 76, new ItemData(76, "Voke Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 77, new ItemData(77, "Progressive Stygia Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 78, new ItemData(78, "Stygia Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 79, new ItemData(79, "Stygia Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 80, new ItemData(80, "Stygia Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 81, new ItemData(81, "Stygia Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 82, new ItemData(82, "Progressive Yhelm Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 83, new ItemData(83, "Yhelm Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 84, new ItemData(84, "Yhelm Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 85, new ItemData(85, "Yhelm Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 86, new ItemData(86, "Yhelm Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 87, new ItemData(87, "Progressive Incaustis Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 88, new ItemData(88, "Incaustis Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 89, new ItemData(89, "Incaustis Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 90, new ItemData(90, "Incaustis Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 91, new ItemData(91, "Incaustis Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 92, new ItemData(92, "Progressive Gehenna Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 93, new ItemData(93, "Gehenna Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 94, new ItemData(94, "Gehenna Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 95, new ItemData(95, "Gehenna Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 96, new ItemData(96, "Gehenna Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 97, new ItemData(97, "Progressive Nihil Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 98, new ItemData(98, "Nihil Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 99, new ItemData(99, "Nihil Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 100, new ItemData(100, "Nihil Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 101, new ItemData(101, "Nihil Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 102, new ItemData(102, "Progressive Acheron Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 103, new ItemData(103, "Acheron Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 104, new ItemData(104, "Acheron Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 105, new ItemData(105, "Acheron Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 106, new ItemData(106, "Acheron Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 107, new ItemData(107, "Progressive Sheol Anguish Gate", progression, HELL, AnguishGate, 4) },
            { 108, new ItemData(108, "Sheol Anguish Gate 1", progression, HELL, AnguishGate, 1) },
            { 109, new ItemData(109, "Sheol Anguish Gate 2", progression, HELL, AnguishGate, 1) },
            { 110, new ItemData(110, "Sheol Anguish Gate 3", progression, HELL, AnguishGate, 1) },
            { 111, new ItemData(111, "Sheol Anguish Gate 4", progression, HELL, AnguishGate, 1) },

            { 120, new ItemData(120, "Progressive Streak Guardian", useful, HELL, Sigil, 3) },
            { 121, new ItemData(121, "Progressive Ghost Rounds", useful, HELL, Sigil, 3) },
            { 122, new ItemData(122, "Progressive Boon Momentum", useful, HELL, Sigil, 3) },
            { 123, new ItemData(123, "Progressive Unyielding Fury", useful, HELL, Sigil, 3) },
            { 124, new ItemData(124, "Progressive Last Breath Aegis", useful, HELL, Sigil, 3) },
            { 125, new ItemData(125, "Progressive Ultimate Sovereignty", useful, HELL, Sigil, 3) },
            { 126, new ItemData(126, "Progressive The Perfectionist", useful, HELL, Sigil, 3) },

            { 130, new ItemData(130, "Paz", progression, HELL, Weapon, 1) },
            { 131, new ItemData(131, "Paz Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 132, new ItemData(132, "Terminus", progression, HELL, Weapon, 1) },
            { 133, new ItemData(133, "Terminus Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 134, new ItemData(134, "Persephone", progression, HELL, Weapon, 1) },
            { 135, new ItemData(135, "Persephone Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 136, new ItemData(136, "Lost Persephone", progression, HELL, Weapon, 1) },
            { 137, new ItemData(137, "Manifested Persephone", progression, HELL, Weapon, 1) },
            { 138, new ItemData(138, "The Hounds", progression, HELL, Weapon, 1) },
            { 139, new ItemData(139, "The Hounds Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 140, new ItemData(140, "The Lost Hounds", progression, HELL, Weapon, 1) },
            { 141, new ItemData(141, "Vulcan", progression, HELL, Weapon, 1) },
            { 142, new ItemData(142, "Vulcan Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 143, new ItemData(143, "Lost Vulcan", progression, HELL, Weapon, 1) },
            { 144, new ItemData(144, "Hellcrow", progression, HELL, Weapon, 1) },
            { 145, new ItemData(145, "Hellcrow Ultimate", progression, HELL, WeaponUpgrade, 1) },
            { 146, new ItemData(146, "The Red Right Hand", progression, HELL, DreamOfTheBeast, Weapon, 1) },
            { 147, new ItemData(147, "The Red Right Hand Ultimate", progression, HELL, DreamOfTheBeast, WeaponUpgrade, 1) },
            { 148, new ItemData(148, "Telos", progression, HELL, Weapon, 1) },
            { 149, new ItemData(149, "Telos Ultimate", progression, HELL, Purgatory, WeaponUpgrade, 1) },

            { 150, new ItemData(150, "Paz Skin", filler, HELL, Skin, 1) },
            { 151, new ItemData(151, "Terminus Skin", filler, HELL, Skin, 1) },
            { 152, new ItemData(152, "Persephone Skin", filler, HELL, Skin, 1) },
            { 153, new ItemData(153, "The Hounds Skin", filler, HELL, Skin, 1) },
            { 154, new ItemData(154, "Vulcan Skin", filler, HELL, Skin, 1) },
            { 155, new ItemData(155, "Hellcrow Skin", filler, HELL, Skin, 1) },

            { 160, new ItemData(160, "Progressive Dash", progression, HELL, Ability, 2) },
            { 161, new ItemData(161, "Dash", progression, HELL, Ability, 1) },
            { 162, new ItemData(162, "Soar", progression, HELL, Ability, 1) },
            { 163, new ItemData(163, "Progressive Jump", progression, HELL, Ability, 3) },
            { 164, new ItemData(164, "Jump", progression, HELL, Ability, 1) },
            { 165, new ItemData(165, "Double Jump", progression, HELL, Ability, 1) },
            { 166, new ItemData(166, "Infinite Jump", progression, HELL, Ability, 1) },
            { 167, new ItemData(167, "Progressive Reload", progression, HELL, Ability, 1) },
            { 168, new ItemData(168, "Quick Reload", progression, HELL, Ability, 1) },
            { 169, new ItemData(169, "Manual Reload", progression, HELL, Ability, 1) },
            { 170, new ItemData(170, "Destructible Ammostashes", progression, HELL, Ability, 1) },
            { 171, new ItemData(171, "Destructible Health Crystals", progression, HELL, Ability, 1) },
            { 172, new ItemData(172, "Destructible Chaos Crystals", progression, HELL, Ability, 1) },
            { 173, new ItemData(173, "Slaughter", progression, HELL, Ability, 1) },

            { 180, new ItemData(180, "Aspect of Anger", progression, HELL, Aspect, 1) },
            { 181, new ItemData(181, "Aspect of the Charged", progression, HELL, Aspect, 1) },
            { 182, new ItemData(182, "Aspect of the Fortress", progression, HELL, Aspect, 1) },
            { 183, new ItemData(183, "Aspect of Infernal Fury", progression, HELL, Aspect, 1) },
            { 184, new ItemData(184, "Aspect of the Hellstorm", progression, HELL, Aspect, 1) },
            { 185, new ItemData(185, "Aspect of the Doppelganger", progression, HELL, Aspect, 1) },
            { 186, new ItemData(186, "Aspect of the Wheel", progression, HELL, Aspect, 1) },

            // Leviathan
            { 310, new ItemData(310, "Progressive Dream of the Heartbeat of Leviathan", useful, LEVIATHAN, Dream, 4) },
            { 311, new ItemData(311, "Progressive Dream of Stubborn Outrage", useful, LEVIATHAN, Dream, 3) },
            { 312, new ItemData(312, "Dream of Ultimate Pots", useful, LEVIATHAN, Dream, 1) },
            { 313, new ItemData(313, "Dream of Dress for Success", useful, LEVIATHAN, Dream, 1) },
            { 314, new ItemData(314, "Dream of Extra Memory", useful, LEVIATHAN, Dream, 1) },

            { 320, new ItemData(320, "Progressive Dream of Strategic Withdrawal", progression, LEVIATHAN, Dream, 2) },
            { 321, new ItemData(321, "Dream of Bloodthirst", progression, LEVIATHAN, Dream, 1) },
            { 322, new ItemData(322, "Dream of to Charge or not to Charge", useful, LEVIATHAN, Dream, 3) },

            { 323, new ItemData(323, "Progressive Dream of Hellcrow", progression, LEVIATHAN, Dream, 5) },
            { 324, new ItemData(324, "Progressive Dream of The Hounds", progression, LEVIATHAN, Dream, 5) },
            { 325, new ItemData(325, "Progressive Dream of The Lost Hounds", progression, LEVIATHAN, Dream, 5) },
            { 326, new ItemData(326, "Progressive Dream of Persephone", progression, LEVIATHAN, Dream, 5) },
            { 327, new ItemData(327, "Progressive Dream of Lost Persephone", progression, LEVIATHAN, Dream, 5) },
            { 328, new ItemData(328, "Progressive Dream of Manifested Persephone", progression, LEVIATHAN, Dream, 5) },
            { 329, new ItemData(329, "Progressive Dream of Vulcan", progression, LEVIATHAN, Dream, 5) },
            { 330, new ItemData(330, "Progressive Dream of Lost Vulcan", progression, LEVIATHAN, Dream, 5) },
            { 331, new ItemData(331, "Progressive Dream of The Red Right Hand", progression, LEVIATHAN, DreamOfTheBeast, Dream, 5) },
            { 332, new ItemData(332, "Progressive Dream of Telos", progression, LEVIATHAN, Purgatory, Dream, 5) },

            { 340, new ItemData(340, "Progressive Dream of Vitality", progression, LEVIATHAN, Dream, 5) },
            { 341, new ItemData(341, "Progressive Dream of Flux Capacity", useful, LEVIATHAN, Dream, 4) },
            { 342, new ItemData(342, "Progressive Dream of Life Manifested", progression, LEVIATHAN, Dream, 3) },
            { 343, new ItemData(343, "Progressive Dream of no Surrender", progression, LEVIATHAN, Dream, 2) },
            { 344, new ItemData(344, "Dream of the Memory Palace", useful, LEVIATHAN, Dream, 1) },

            { 350, new ItemData(350, "Progressive Dream of Streak Guardian", useful, LEVIATHAN, Dream, 3) },
            { 351, new ItemData(351, "Progressive Dream of Ghost Rounds", useful, LEVIATHAN, Dream, 3) },
            { 352, new ItemData(352, "Progressive Dream of Boon Momentum", useful, LEVIATHAN, Dream, 3) },
            { 353, new ItemData(353, "Progressive Dream of Unyielding Fury", useful, LEVIATHAN, Dream, 3) },
            { 354, new ItemData(354, "Progressive Dream of Last Breath Aegis", useful, LEVIATHAN, Dream, 3) },
            { 355, new ItemData(355, "Progressive Dream of Ultimate Sovereignty", useful, LEVIATHAN, Dream, 3) },
            { 356, new ItemData(356, "Progressive Dream of The Perfectionist", useful, LEVIATHAN, Dream, 3) },

            { 360, new ItemData(360, "Progressive Memory of Destructive Force", progression, LEVIATHAN, Memory, 3) },
            { 361, new ItemData(361, "Progressive Memory of Sharpened Blade", progression, LEVIATHAN, Memory, 3) },
            { 362, new ItemData(362, "Progressive Memory of Precise Focus", progression, LEVIATHAN, Memory, 3) },
            { 363, new ItemData(363, "Progressive Memory of Equal in Death", progression, LEVIATHAN, Memory, 3) },
            { 364, new ItemData(364, "Progressive Memory of Defensive Charm", progression, LEVIATHAN, Memory, 3) },
            { 365, new ItemData(365, "Memory of Bounty of Void Echoes", useful, LEVIATHAN, Memory, 0) },
            // { 366, new ItemData(366, "Memory of Bounty of Void Echoes", progression, LEVIATHAN, MEMORY, 0) },
            // { 367, new ItemData(367, "Memory of Bounty of Void Echoes", progression, LEVIATHAN, MEMORY, 0) },
            // { 368, new ItemData(368, "Memory of Bounty of Void Echoes", progression, LEVIATHAN, MEMORY, 0) },
            // TODO: Progressive Bleed
            // TODO: Progressive Slow
            // TODO: Progressive Chaos

            { 369, new ItemData(369, "Progressive Memory of Stubborn Outrage", useful, LEVIATHAN, Memory, 3) },
            { 370, new ItemData(370, "Progressive Memory of Echoing Harvest", useful, LEVIATHAN, Memory, 3) },
            { 371, new ItemData(371, "Progressive Memory of Shard Magnet", useful, LEVIATHAN, Memory, 3) },
            { 372, new ItemData(372, "Progressive Mind over Matter Memory", useful, LEVIATHAN, Memory, 3) },
            { 373, new ItemData(373, "Memory of Echoing Perfection", useful, LEVIATHAN, Memory, 1) },
            { 374, new ItemData(374, "Memory of Echoing Combos", useful, LEVIATHAN, Memory, 1) },
            { 375, new ItemData(375, "Memory of Sturdy Boots", useful, LEVIATHAN, Memory, 1) },

            { 376, new ItemData(376, "Memory of Profane Onslaught", useful, LEVIATHAN, Memory, 1) },
            { 377, new ItemData(377, "Memory of Cursed Blades", useful, LEVIATHAN, Memory, 1) },
            { 378, new ItemData(378, "Memory of Demonic Precision", useful, LEVIATHAN, Memory, 1) },
            { 379, new ItemData(379, "Memory of Cursing Headshots", useful, LEVIATHAN, Memory, 1) },
            { 380, new ItemData(380, "Memory of Cursed Chaos", useful, LEVIATHAN, Memory, 1) },
            { 381, new ItemData(381, "Memory of Perfect Curse Explosions", useful, LEVIATHAN, Memory, 1) },

            { 382, new ItemData(382, "Memory of Damning Charge", useful, LEVIATHAN, Memory, 1) },
            { 383, new ItemData(383, "Memory of Damning Marksmanship", useful, LEVIATHAN, Memory, 1) },
            { 384, new ItemData(384, "Memory of Damning Cuts", useful, LEVIATHAN, Memory, 1) },

            { 385, new ItemData(385, "Memory of Freezing Cannonry", useful, LEVIATHAN, Memory, 1) },
            { 386, new ItemData(386, "Memory of Freezing Blades", useful, LEVIATHAN, Memory, 1) },
            { 387, new ItemData(387, "Memory of Biting Precision", useful, LEVIATHAN, Memory, 1) },
            { 388, new ItemData(388, "Memory of Chilling Headshots", useful, LEVIATHAN, Memory, 1) },
            { 389, new ItemData(389, "Memory of Slow Chaos", useful, LEVIATHAN, Memory, 1) },
            { 390, new ItemData(390, "Memory of Perfect Slow Explosions", useful, LEVIATHAN, Memory, 1) },

            { 391, new ItemData(391, "Memory of Unfair Advantage", useful, LEVIATHAN, Memory, 1) },
            { 392, new ItemData(392, "Memory of Fish in a Barrel", useful, LEVIATHAN, Memory, 1) },
            { 393, new ItemData(393, "Memory of Cold-Seeking Blades", useful, LEVIATHAN, Memory, 1) },


            { 394, new ItemData(394, "Memory of Heavy Consquences", useful, LEVIATHAN, Memory, 1) },
            { 395, new ItemData(395, "Memory of Crimson Cuts", useful, LEVIATHAN, Memory, 1) },
            { 396, new ItemData(396, "Memory of Bloody Precision", useful, LEVIATHAN, Memory, 1) },
            { 397, new ItemData(397, "Memory of Bleeding Headshots", useful, LEVIATHAN, Memory, 1) },
            { 398, new ItemData(398, "Memory of Bleeding Chaos", useful, LEVIATHAN, Memory, 1) },
            { 399, new ItemData(399, "Memory of Perfect Blood Explosions", useful, LEVIATHAN, Memory, 1) },

            { 400, new ItemData(400, "Memory of Sanguine Blade", useful, LEVIATHAN, Memory, 1) },
            { 401, new ItemData(401, "Memory of Easy Pickings", useful, LEVIATHAN, Memory, 1) },
            { 402, new ItemData(402, "Memory of Hunting Knives", useful, LEVIATHAN, Memory, 1) },

            { 403, new ItemData(403, "Progressive Memory of Ultimate Urgency", filler, LEVIATHAN, Memory, 3) },
            { 404, new ItemData(404, "Memory of Ultimate Perfection", filler, LEVIATHAN, Memory, 1) },
            { 405, new ItemData(405, "Memory of Ultimate Combos", filler, LEVIATHAN, Memory, 1) },
            { 406, new ItemData(406, "Memory of Ultimate Contract", filler, LEVIATHAN, Memory, 1) },
            { 407, new ItemData(407, "Progressive Memory of Positive Mindset", filler, LEVIATHAN, Memory, 3) },
            { 408, new ItemData(408, "Memory of Revitalizing Perfection", filler, LEVIATHAN, Memory, 1) },
            { 409, new ItemData(409, "Memory of Revitalizing Combos", filler, LEVIATHAN, Memory, 1) },
            { 410, new ItemData(410, "Progressive Memory of Volatile Demons", filler, LEVIATHAN, Memory, 3) },

            { 411, new ItemData(411, "Memory of Bloodthirst", useful, LEVIATHAN, Memory, 1) },
            { 412, new ItemData(412, "Memory of Paz Crystallization", useful, LEVIATHAN, Memory, 1) }, 
            { 413, new ItemData(413, "Progressive Memory of Strategic Withdrawal", useful, LEVIATHAN, Memory, 2) },
            { 414, new ItemData(414, "Memory of Perfect Authority", filler, LEVIATHAN, Memory, 1) },
            { 415, new ItemData(415, "Memory of Ultimate Pots", useful, LEVIATHAN, Memory, 1) },
            { 416, new ItemData(416, "Memory of Double Trouble", useful, LEVIATHAN, Memory, 1) },
            { 417, new ItemData(417, "Memory of Rush of Ultimate", filler, LEVIATHAN, Memory, 1) },
            { 418, new ItemData(418, "Memory of being Light-Footed", filler, LEVIATHAN, Memory, 1) },
            { 419, new ItemData(419, "Memory of Crystal Attunement", filler, LEVIATHAN, Memory, 1) },

            { 420, new ItemData(420, "Memory of Hellcrow", progression, LEVIATHAN, Memory, 1) },
            { 421, new ItemData(421, "Memory of The Hounds", progression, LEVIATHAN, Memory, 1) },
            { 422, new ItemData(422, "Memory of The Lost Hounds", progression, LEVIATHAN, Memory, 1) },
            { 423, new ItemData(423, "Memory of Persephone", progression, LEVIATHAN, Memory, 1) },
            { 424, new ItemData(424, "Memory of Lost Persephone", progression, LEVIATHAN, Memory, 1) },
            { 425, new ItemData(425, "Memory of Manifested Persephone", progression, LEVIATHAN, Memory, 1) },
            { 426, new ItemData(426, "Memory of Vulcan", progression, LEVIATHAN, Memory, 1) },
            { 427, new ItemData(427, "Memory of Lost Vulcan", progression, LEVIATHAN, Memory, 1) },
            { 428, new ItemData(428, "Memory of The Red Right Hand", progression, LEVIATHAN, DreamOfTheBeast, Memory, 1) },
            { 429, new ItemData(429, "Memory of Telos", progression, LEVIATHAN, Purgatory, Memory, 1) },

            { 430, new ItemData(430, "Progressive Memory of Seraphs", useful, LEVIATHAN, Memory, 2) },
            { 431, new ItemData(431, "Progressive Memory of Behemoths", useful, LEVIATHAN, Memory, 2) },
            { 432, new ItemData(432, "Progressive Memory of Stalkers", useful, LEVIATHAN, Memory, 2) },
            { 433, new ItemData(433, "Progressive Memory of Hierophants", useful, LEVIATHAN, Memory, 2) },
            { 434, new ItemData(434, "Progressive Memory of Eyeless", useful, LEVIATHAN, Purgatory, Memory, 2) },
            { 435, new ItemData(435, "Progressive Memory of Elites", useful, LEVIATHAN, Purgatory, Memory, 2) },

            // Generic
            { 500, new ItemData(500, "Outfit of the Unknown", progression, ALL, Outfit, 1) },
            { 501, new ItemData(501, "Outfit of the Leviathan", progression, ALL, Outfit, 1) },
            { 502, new ItemData(502, "Outfit of the Dark Devotee", progression, ALL, DreamOfTheBeast, Outfit, 1) },
            { 503, new ItemData(503, "Outfit of the Morning Star", progression, ALL, DreamOfTheBeast, Outfit, 1) },
            { 504, new ItemData(504, "Outfit of the Angel Eyes", progression, ALL, DreamOfTheBeast, Outfit, 1) },
            { 505, new ItemData(505, "Outfit of the Obsidian", progression, ALL, Purgatory, Outfit, 1) },
            { 506, new ItemData(506, "Outfit of the Amethyst", progression, ALL, Purgatory, Outfit, 1) },
            { 507, new ItemData(507, "Outfit of the Chromatica", progression, ALL, Purgatory, Outfit, 1) },


            { 510, new ItemData(510, "This is the End", progression, ALL, Song, 1) },
            { 511, new ItemData(511, "Stygia (Song)", progression, ALL, Song, 1) },
            { 512, new ItemData(512, "Burial At Night", progression, ALL, Song, 1) },
            { 513, new ItemData(513, "This Devastation", progression, ALL, Song, 1) },
            { 514, new ItemData(514, "Poetry of Cinder", progression, ALL, Song, 1) },
            { 515, new ItemData(515, "Dissolution", progression, ALL, Song, 1) },
            { 517, new ItemData(517, "Acheron (Song)", progression, ALL, Song, 1) },
            { 518, new ItemData(518, "Silent No More", progression, ALL, Song, 1) },

            { 519, new ItemData(519, "Blood and Law", progression, ALL, Song, 1) },
            { 520, new ItemData(520, "Infernal Invocation I: Hopes and Fears", progression, ALL, Song, 1) },
            { 521, new ItemData(521, "Infernal Invocation II: Defiance", progression, ALL, Song, 1) },
            { 522, new ItemData(522, "Infernal Invocation III: Dreaming in Distortion", progression, ALL, Song, 1) },
            { 523, new ItemData(523, "No Tomorrow", progression, ALL, Song, 1) },

            { 524, new ItemData(524, "Departure to Destruction", filler, ALL, Dusk, Song, 1) },
            { 525, new ItemData(525, "Hand Cannon", filler, ALL, Dusk, Song, 1) },
            { 526, new ItemData(526, "Burn in Hell", filler, ALL, Dusk, Song, 1) },
            { 527, new ItemData(527, "Murder Machine Inc", filler, ALL, Dusk, Song, 1) },
            { 528, new ItemData(528, "Endless", filler, ALL, Dusk, Song, 1) },
            { 529, new ItemData(529, "Mine Control", filler, ALL, Dusk, Song, 1) },
            { 530, new ItemData(530, "Sacrifice", filler, ALL, Dusk, Song, 1) },
            { 531, new ItemData(531, "Erebus Reaction", filler, ALL, Dusk, Song, 1) },
            { 532, new ItemData(532, "Bleeding Out", filler, ALL, Dusk, Song, 1) },

            { 533, new ItemData(533, "Leviathan (Song)", progression, ALL, DreamOfTheBeast, Song, 1) },
            { 534, new ItemData(534, "Dream of the Beast", progression, ALL, DreamOfTheBeast, Song, 1) },
            { 535, new ItemData(535, "Swallow the Fire", progression, ALL, Purgatory, Song, 1) },
            { 536, new ItemData(536, "Mouth of Hell", progression, ALL, Purgatory, Song, 1) },
            { 537, new ItemData(537, "Goodbye, Morning Star", progression, ALL, Purgatory, Song, 1) },

            { 538, new ItemData(538, "Down With the Sickness", filler, ALL, EssentialHits, Song, 1) },
            { 539, new ItemData(539, "Uprising", filler, ALL, EssentialHits, Song, 1) },
            { 540, new ItemData(540, "Misery Business", filler, ALL, EssentialHits, Song, 1) },
            { 541, new ItemData(541, "Tsunami (Original Mix)", filler, ALL, EssentialHits, Song, 1) },
            { 542, new ItemData(542, "Runaway (U&I)", filler, ALL, EssentialHits, Song, 1) },
            { 543, new ItemData(543, "Feel Good Inc.", filler, ALL, EssentialHits, Song, 1) },
            { 544, new ItemData(544, "I Love It feat. Charli XCX", filler, ALL, EssentialHits, Song, 1) },
            { 545, new ItemData(545, "Personal Jesus", filler, ALL, EssentialHits, Song, 1) },

            // Battle
            { 550, new ItemData(550, "Next Multiplier", filler, ALL, Combat, 0) },
            { 551, new ItemData(551, "Max Multiplier", filler, ALL, Combat, 0) },
            { 552, new ItemData(552, "Always on Beat", filler, ALL, Combat, 0) },
            { 553, new ItemData(553, "Complement", filler, ALL, Combat, 0) },
            { 554, new ItemData(554, "Encouragement", filler, ALL, Combat, 0) },
            { 555, new ItemData(555, "Failure", filler, ALL, Combat, 0) },

            { 560, new ItemData(560, "Reset Multiplier", trap, ALL, Combat, 0) },
            { 561, new ItemData(561, "Double Time", trap, ALL, Combat, 0) },
            { 562, new ItemData(562, "Half Time", trap, ALL, Combat, 0) },
            { 563, new ItemData(563, "Invisible Weapons", trap, ALL, Combat, 0) },
            { 564, new ItemData(564, "Weapon Trickery", trap, ALL, Combat, 0) },
            { 565, new ItemData(565, "Trigger Ultimate", trap, ALL, Combat, 0) },
            { 566, new ItemData(566, "Death", trap, ALL, Combat, 0) },

            { 666, new ItemData(666, "Filler", filler, ALL, Global, 0) },
        };

        public static readonly Dictionary<string, ItemData> ItemDataByName =
            ItemDataById.Values.ToDictionary(loc => loc.Name, loc => loc);

    }
}
