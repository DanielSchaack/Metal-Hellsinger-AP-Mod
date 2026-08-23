using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Archipelago.MultiClient.Net.Models;
using static Randomizer.ItemOrigin;
using static Randomizer.Locations;
using static Randomizer.Lookup;

namespace Randomizer
{
    public class ItemTracker
    {
        public Dictionary<int, ItemData> CollectedItemsByIndex = new Dictionary<int, ItemData>()
        { };

        public Dictionary<string, int> CollectedImportantItemCountsByName = new Dictionary<
            string,
            int
        >()
        {
            { "Leviathan", 1 },
            { "Hells", 1 },

            { "Coat of Arms", 32 },

            { "Progressive Hells", 8 },
            { "Tutorial", 1 },
            { "Voke", 1 },
            { "Aspect of Anger", 1 },
            { "Stygia", 1 },
            { "Aspect of the Charged", 1 },
            { "Yhelm", 1 },
            { "Aspect of the Fortress", 1 },
            { "Incaustis", 1 },
            { "Aspect of Infernal Fury", 1 },
            { "Gehenna", 1 },
            { "Aspect of the Hellstorm", 1 },
            { "Nihil", 1 },
            { "Aspect of the Doppelganger", 1 },
            { "Acheron", 1 },
            { "Aspect of the Wheel", 1 },
            { "Sheol", 1 },

            { "Progressive Killing with Rhythm", 3 },
            { "Killing with Rhythm: 1", 1 },
            { "Killing with Rhythm: 2", 1 },
            { "Killing with Rhythm: 3", 1 },
            { "Progressive Giantslayer", 3 },
            { "Giantslayer: 1", 1 },
            { "Giantslayer: 2", 1 },
            { "Giantslayer: 3", 1 },
            { "Progressive Ultimate Mastery", 3 },
            { "Ultimate Mastery: 1", 1 },
            { "Ultimate Mastery: 2", 1 },
            { "Ultimate Mastery: 3", 1 },
            { "Progressive Slaughter Mastery", 3 },
            { "Slaughter Mastery: 1", 1 },
            { "Slaughter Mastery: 2", 1 },
            { "Slaughter Mastery: 3", 1 },
            { "Progressive Relic Thief", 3 },
            { "Relic Thief: 1", 1 },
            { "Relic Thief: 2", 1 },
            { "Relic Thief: 3", 1 },
            { "Progressive Weapon Trickery", 3 },
            { "Weapon Trickery: 1", 1 },
            { "Weapon Trickery: 2", 1 },
            { "Weapon Trickery: 3", 1 },
            { "Progressive Death's Edge", 3 },
            { "Death's Edge: 1", 1 },
            { "Death's Edge: 2", 1 },
            { "Death's Edge: 3", 1 },

            { "Paz", 1 },
            { "Paz Ultimate", 1 },
            { "Terminus", 1 },
            { "Terminus Ultimate", 1 },
            { "Persephone", 1 },
            { "Persephone Ultimate", 1 },
            { "The Hounds", 1 },
            { "The Hounds Ultimate", 1 },
            { "Vulcan", 1 },
            { "Vulcan Ultimate", 1 },
            { "Hellcrow", 1 },
            { "Hellcrow Ultimate", 1 },
            { "The Red Right Hand", 1 },
            { "The Red Right Hand Ultimate", 1 },
            { "Telos", 1 },
            { "Telos Ultimate", 1 },

            { "Lost Persephone", 1 },
            { "Manifested Persephone", 1 },
            { "The Lost Hounds", 1 },
            { "Lost Vulcan", 1 },

            { "Progressive Streak Guardian", 3 },
            { "Progressive Ghost Rounds", 3 },
            { "Progressive Boon Momentum", 3 },
            { "Progressive Unyielding Fury", 3 },
            { "Progressive Last Breath Aegis", 3 },
            { "Progressive Ultimate Sovereignty", 3 },
            { "Progressive The Perfectionist", 3 },

            { "Enduring Fury", 1 },
            { "Faster Ultimate Gain", 1 },
            { "Deadlier Dash", 1 },
            { "Explosive Slaughter", 1 },

            { "Progressive Dash", 2 },
            { "Dash", 1 },
            { "Soar", 1 },
            { "Progressive Jump", 3 },
            { "Jump", 1 },
            { "Double Jump", 1 },
            { "Infinite Jump", 0 },
            { "Progressive Reload", 2 },
            { "Quick Reload", 1 },
            { "Manual Reload", 1 },
            { "Slaughter", 1 },
            { "Destructible Ammostashes", 1 },
            { "Destructible Health Crystals", 1 },
            { "Destructible Chaos Crystals", 1 },

            { "Regressive Difficulty", 4 },
            { "Archdevil", 1 },
            { "Beast", 1 },
            { "Goat", 1 },
            { "Lamb", 1 },

            { "Paz Skin", 1 },
            { "Terminus Skin", 1 },
            { "Persephone Skin", 1 },
            { "The Hounds Skin", 1 },
            { "Vulcan Skin", 1 },
            { "Hellcrow Skin", 1 },

            { "Outfit of the Unknown", 1 },
            { "Outfit of the Dark Devotee", 1 },
            { "Outfit of the Morning Star", 1 },
            { "Outfit of the Angel Eyes", 1 },
            { "Outfit of the Obsidian", 1 },
            { "Outfit of the Amethyst", 1 },
            { "Outfit of the Chromatica", 1 },
            { "Outfit of the Leviathan", 1 },

            { "This is the End", 1 },
            { "Stygia (Song)", 1 },
            { "Burial At Night", 1 },
            { "This Devastation", 1 },
            { "Poetry of Cinder", 1 },
            { "Dissolution", 1 },
            { "Acheron (Song)", 1 },
            { "Silent No More", 1 },

            { "Blood and Law", 1 },
            { "Infernal Invocation I: Hopes and Fears", 1 },
            { "Infernal Invocation II: Defiance", 1 },
            { "Infernal Invocation III: Dreaming in Distortion", 1 },
            { "No Tomorrow", 1 },

            { "Leviathan (Song)", 1 },
            { "Dream of the Beast", 1 },
            { "Swallow the Fire", 1 },
            { "Mouth of Hell", 1 },
            { "Goodbye, Morning Star", 1 },

            { "Departure to Destruction", 1 },
            { "Hand Cannon", 1 },
            { "Burn in Hell", 1 },
            { "Murder Machine Inc", 1 },
            { "Endless", 1 },
            { "Mine Control", 1 },
            { "Sacrifice", 1 },
            { "Erebus Reaction", 1 },
            { "Bleeding Out", 1 },

            { "Down With the Sickness", 1 },
            { "Uprising", 1 },
            { "Misery Business", 1 },
            { "Tsunami (Original Mix)", 1 },
            { "Runaway (U&I)", 1 },
            { "Feel Good Inc.", 1 },
            { "I Love It feat. Charli XCX", 1 },
            { "Personal Jesus", 1 },

            { "Progressive Voke Anguish Gate Skip", 0 },
            { "Voke Anguish Gate 1 Skip", 0 },
            { "Voke Anguish Gate 2 Skip", 0 },
            { "Voke Anguish Gate 3 Skip", 0 },
            { "Voke Anguish Gate 4 Skip", 0 },
            { "Progressive Stygia Anguish Gate Skip", 0 },
            { "Stygia Anguish Gate 1 Skip", 0 },
            { "Stygia Anguish Gate 2 Skip", 0 },
            { "Stygia Anguish Gate 3 Skip", 0 },
            { "Stygia Anguish Gate 4 Skip", 0 },
            { "Progressive Yhelm Anguish Gate Skip", 0 },
            { "Yhelm Anguish Gate 1 Skip", 0 },
            { "Yhelm Anguish Gate 2 Skip", 0 },
            { "Yhelm Anguish Gate 3 Skip", 0 },
            { "Yhelm Anguish Gate 4 Skip", 0 },
            { "Progressive Incaustis Anguish Gate Skip", 0 },
            { "Incaustis Anguish Gate 1 Skip", 0 },
            { "Incaustis Anguish Gate 2 Skip", 0 },
            { "Incaustis Anguish Gate 3 Skip", 0 },
            { "Incaustis Anguish Gate 4 Skip", 0 },
            { "Progressive Gehenna Anguish Gate Skip", 0 },
            { "Gehenna Anguish Gate 1 Skip", 0 },
            { "Gehenna Anguish Gate 2 Skip", 0 },
            { "Gehenna Anguish Gate 3 Skip", 0 },
            { "Gehenna Anguish Gate 4 Skip", 0 },
            { "Progressive Nihil Anguish Gate Skip", 0 },
            { "Nihil Anguish Gate 1 Skip", 0 },
            { "Nihil Anguish Gate 2 Skip", 0 },
            { "Nihil Anguish Gate 3 Skip", 0 },
            { "Nihil Anguish Gate 4 Skip", 0 },
            { "Progressive Acheron Anguish Gate Skip", 0 },
            { "Acheron Anguish Gate 1 Skip", 0 },
            { "Acheron Anguish Gate 2 Skip", 0 },
            { "Acheron Anguish Gate 3 Skip", 0 },
            { "Acheron Anguish Gate 4 Skip", 0 },
            { "Progressive Sheol Anguish Gate Skip", 0 },
            { "Sheol Anguish Gate 1 Skip", 0 },
            { "Sheol Anguish Gate 2 Skip", 0 },
            { "Sheol Anguish Gate 3 Skip", 0 },
            { "Sheol Anguish Gate 4 Skip", 0 },

            // Not in use yet
            { "Progressive Tutorial Anguish Gate Skip", 1 },
            { "Tutorial Anguish Gate 1 Skip", 1 },

            // Leviathan
            { "The Lost Unknown: Leviathan defeated", 1 },
            // Stages
            { "Garden of Chronos", 1 },
            { "Calamity", 1 },
            { "Demonitorium", 1 },
            { "Tombs of the Ancients", 1 },
            { "Necropolis", 1 },
            { "Axiom", 1 },
            { "Final Destination", 1 },
            // Dreams
            { "Progressive Dream of the Heartbeat of Leviathan", 4 },
            { "Progressive Dream of Stubborn Outrage", 3 },
            { "Dream of Ultimate Pots", 1 },
            { "Dream of Dress for Success", 1 },
            { "Dream of Extra Memory", 1 },
            { "Progressive Dream of Strategic Withdrawal", 2 },
            { "Dream of Bloodthirst", 1 },
            { "Dream of to Charge or not to Charge", 3 },
            { "Progressive Dream of Hellcrow", 5 },
            { "Progressive Dream of The Hounds", 5 },
            { "Progressive Dream of The Lost Hounds", 5 },
            { "Progressive Dream of Persephone", 5 },
            { "Progressive Dream of Lost Persephone", 5 },
            { "Progressive Dream of Manifested Persephone", 5 },
            { "Progressive Dream of Vulcan", 5 },
            { "Progressive Dream of Lost Vulcan", 5 },
            { "Progressive Dream of The Red Right Hand", 5 },
            { "Progressive Dream of Telos", 5 },
            { "Progressive Dream of Vitality", 5 },
            { "Progressive Dream of Flux Capacity", 4 },
            { "Progressive Dream of Life Manifested", 3 },
            { "Progressive Dream of no Surrender", 2 },
            { "Dream of the Memory Palace", 1 },
            { "Progressive Dream of Streak Guardian", 3 },
            { "Progressive Dream of Ghost Rounds", 3 },
            { "Progressive Dream of Boon Momentum", 3 },
            { "Progressive Dream of Unyielding Fury", 3 },
            { "Progressive Dream of Last Breath Aegis", 3 },
            { "Progressive Dream of Ultimate Sovereignty", 3 },
            { "Progressive Dream of The Perfectionist", 3 },
            // Memories
            { "Progressive Memory of Destructive Force", 3 },
            { "Progressive Memory of Sharpened Blade", 3 },
            { "Progressive Memory of Precise Focus", 3 },
            { "Progressive Memory of Equal in Death", 3 },
            { "Progressive Memory of Defensive Charm", 3 },
            // TODO: Progressive Bleed
            // TODO: Progressive Slow
            // TODO: Progressive Chaos

            { "Progressive Memory of Stubborn Outrage", 3 },
            { "Progressive Memory of Echoing Harvest", 3 },
            { "Progressive Memory of Shard Magnet", 3 },
            { "Progressive Mind over Matter Memory", 3 },
            { "Memory of Echoing Perfection", 1 },
            { "Memory of Echoing Combos", 1 },
            { "Memory of Sturdy Boots", 1 },
            { "Memory of Profane Onslaught", 1 },
            { "Memory of Cursed Blades", 1 },
            { "Memory of Demonic Precision", 1 },
            { "Memory of Cursing Headshots", 1 },
            { "Memory of Cursed Chaos", 1 },
            { "Memory of Perfect Curse Explosions", 1 },
            { "Memory of Damning Charge", 1 },
            { "Memory of Damning Marksmanship", 1 },
            { "Memory of Damning Cuts", 1 },

            { "Memory of Freezing Cannonry", 1 },
            { "Memory of Freezing Blades", 1 },
            { "Memory of Biting Precision", 1 },
            { "Memory of Chilling Headshots", 1 },
            { "Memory of Slow Chaos", 1 },
            { "Memory of Perfect Slow Explosions", 1 },
            { "Memory of Unfair Advantage", 1 },
            { "Memory of Fish in a Barrel", 1 },
            { "Memory of Cold-Seeking Blades", 1 },
            { "Memory of Heavy Consquences", 1 },

            { "Memory of Crimson Cuts", 1 },
            { "Memory of Bloody Precision", 1 },
            { "Memory of Bleeding Headshots", 1 },
            { "Memory of Bleeding Chaos", 1 },
            { "Memory of Perfect Blood Explosions", 1 },
            { "Memory of Sanguine Blade", 1 },
            { "Memory of Easy Pickings", 1 },
            { "Memory of Hunting Knives", 1 },

            { "Progressive Memory of Ultimate Urgency", 3 },
            { "Memory of Ultimate Perfection", 1 },
            { "Memory of Ultimate Combos", 1 },
            { "Memory of Ultimate Contract", 1 },
            { "Progressive Memory of Positive Mindset", 3 },
            { "Memory of Revitalizing Perfection", 1 },
            { "Memory of Revitalizing Combos", 1 },
            { "Progressive Memory of Volatile Demons", 3 },
            { "Memory of Bloodthirst", 1 },
            { "Memory of Paz Crystallization", 1 },
            { "Progressive Memory of Strategic Withdrawal", 2 },
            { "Memory of Perfect Authority", 1 },
            { "Memory of Ultimate Pots", 1 },
            { "Memory of Double Trouble", 1 },
            { "Memory of Rush of Ultimate", 1 },
            { "Memory of being Light-Footed", 1 },
            { "Memory of Crystal Attunement", 1 },

            { "Memory of Persephone", 1 },
            { "Memory of Lost Persephone", 1 },
            { "Memory of Manifested Persephone", 1 },
            { "Memory of The Hounds", 1 },
            { "Memory of The Lost Hounds", 1 },
            { "Memory of Vulcan", 1 },
            { "Memory of Lost Vulcan", 1 },
            { "Memory of Hellcrow", 1 },
            { "Memory of The Red Right Hand", 1 },
            { "Memory of Telos", 1 },

            { "Progressive Memory of Seraphs", 2 },
            { "Progressive Memory of Behemoths", 2 },
            { "Progressive Memory of Stalkers", 2 },
            { "Progressive Memory of Hierophants", 2 },
            { "Progressive Memory of Eyeless", 2 },
            { "Progressive Memory of Elites", 2 },
        };

        private Dictionary<string, List<string>> RequiredItemsForLevelUnlock = new Dictionary<
            string,
            List<string>
        >()
        { };

        private static Dictionary<string, string> LevelToLoadedLevel = new Dictionary<string, string>()
        {
            { "EndlessModeBase", "EndlessModeBase" },
            { "Tutorial", "Tutorial" },
            { "Voke", "Voke" },
            { "Stygia", "Stygia" },
            { "Yhelm", "Yhelm" },
            { "Incaustis", "Incaustis" },
            { "Gehenna", "Gehenna" },
            { "Nihil", "Nihil" },
            { "Acheron", "Acheron" },
            { "Sheol", "Sheol" },
            { "CH_Amdusias1", "CH_Amdusias1" },
            { "CH_Marbas1", "CH_Marbas1" },
            { "CH_Halphas1", "CH_Halphas1" },
            { "CH_Bune1", "CH_Bune1" },
            { "CH_Morax1", "CH_Morax1" },
            { "CH_Halphas2", "CH_Halphas2" },
            { "CH_Flauros1", "CH_Flauros1" },
            { "CH_Amdusias2", "CH_Amdusias2" },
            { "CH_Marbas2", "CH_Marbas2" },
            { "CH_Glasya1", "CH_Glasya1" },
            { "CH_Bune2", "CH_Bune2" },
            { "CH_Halphas3", "CH_Halphas3" },
            { "CH_Morax2", "CH_Morax2" },
            { "CH_Amdusias3", "CH_Amdusias3" },
            { "CH_Marbas3", "CH_Marbas3" },
            { "CH_Flauros2", "CH_Flauros2" },
            { "CH_Glasya2", "CH_Glasya2" },
            { "CH_Bune3", "CH_Bune3" },
            { "CH_Morax3", "CH_Morax3" },
            { "CH_Flauros3", "CH_Flauros3" },
            { "CH_Glasya3", "CH_Glasya3" },
        };
        public Dictionary<string, string> LoadedLevelToLevel =
            LevelToLoadedLevel.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        public void Resync(ReadOnlyCollection<ItemInfo> allItemsReceived)
        {
            Reset(Randomizer.Settings);
            for (int i = 0; i < allItemsReceived.Count; i++)
            {
                var item = allItemsReceived[i];
                var isNewItem = i > Randomizer.Archipelago.ItemIndex;
                Randomizer.ItemTracker.SetCollectedItem(item.ItemId, i, isNewItem);

                if(isNewItem)
                {
                    Randomizer.Archipelago.ItemIndex = i;
                    var gameItem = Items.ItemDataById[item.ItemId];
                    ArchipelagoConsole.Instance.LogMessage($"Received <b>{gameItem.Name}</b> while being disconnected.");
                }
            }
        }

        internal bool Has(string itemName)
        {
            return CollectedImportantItemCountsByName.TryGetValue(itemName, out int count)
                && count > 0;
        }

        private bool HasAny(IEnumerable<string> names)
        {
            foreach (var itemName in names)
                if (Has(itemName))
                    return true;
            return false;
        }

        private bool HasAll(IEnumerable<string> names)
        {
            foreach (var itemName in names)
                if (!Has(itemName))
                    return false;
            return true;
        }

        public string GetChallengeIdFromZone(EZone zone)
        {
            if (Lookup.EZoneToChallengeBaseId.TryGetValue(zone, out var id))
                return id;
            return "";
        }

        public ItemTracker()
        {
            RequiredItemsForLevelUnlock.Clear();
            var allLevelKeys = Lookup.RequiredLevelItemsBase.Keys.Union(
                Lookup.RequiredWeaponsForLevel.Keys
            );
            foreach (var level in allLevelKeys)
            {
                var combinedList = new List<string>();

                if (Lookup.RequiredLevelItemsBase.TryGetValue(level, out var items))
                    combinedList.AddRange(items);

                if (Lookup.RequiredWeaponsForLevel.TryGetValue(level, out var weapons))
                    combinedList.AddRange(weapons);

                RequiredItemsForLevelUnlock[level] = combinedList.Distinct().ToList();
            }

            CollectedItemsByIndex = new Dictionary<int, ItemData>() { };
        }

        public void Reset(Settings settings)
        {
            foreach (string Key in CollectedImportantItemCountsByName.Keys.ToList())
                CollectedImportantItemCountsByName[Key] = 0;

            if(!settings.RandomizedHellsEnabled)
                foreach(var hells in Lookup.HellsIDs)
                    CollectedImportantItemCountsByName[hells] = 1;

            RequiredItemsForLevelUnlock.Clear();
            var allLevelKeys = Lookup.RequiredLevelItemsBase.Keys.Union(
                Lookup.RequiredWeaponsForLevel.Keys
            );
            foreach (var level in allLevelKeys)
            {
                var combinedList = new List<string>();

                if (Lookup.RequiredLevelItemsBase.TryGetValue(level, out var items))
                    combinedList.AddRange(items);

                if (settings.RequireWeaponsForChallenges && Lookup.RequiredWeaponsForLevel.TryGetValue(level, out var weapons))
                    combinedList.AddRange(weapons);

                if (settings.RequireStageForChallenges && Lookup.RequiredLevelItemsChallenges.TryGetValue(level, out var hells))
                    combinedList.AddRange(hells);

                RequiredItemsForLevelUnlock[level] = combinedList.Distinct().ToList();
            }

            if(settings.RegressiveDifficultyEnabled)
                SaveDataManager.SaveData.LastUsedDifficulty = EDifficulty.VeryHard;
            else
                SaveDataManager.SaveData.LastUsedDifficulty = (EDifficulty)settings.StartingDifficulty;
            Randomizer.SelectedDifficulty = SaveDataManager.SaveData.LastUsedDifficulty;
            Randomizer.CurrentDifficulty = SaveDataManager.SaveData.LastUsedDifficulty;

            SaveDataManager.SaveData.LastPlayedLevelID = Lookup.HellsIDs[settings.StartingHells+1];

            if (!settings.RandomizedBoonsEnabled)
            {
                foreach(var boon in Lookup.BoonNameToType.Keys)
                    CollectedImportantItemCountsByName[boon] = 1;
            }

            if (!settings.RandomizedDashEnabled)
            {
                CollectedImportantItemCountsByName["Dash"] = 1;
                CollectedImportantItemCountsByName["Soar"] = 1;
            }

            if (!settings.RandomizedJumpEnabled)
            {
                CollectedImportantItemCountsByName["Jump"] = 1;
                CollectedImportantItemCountsByName["Double Jump"] = 1;
            }

            if (!settings.RandomizedReloadEnabled)
            {
                CollectedImportantItemCountsByName["Quick Reload"] = 1;
                CollectedImportantItemCountsByName["Manual Reload"] = 1;
            }

            if (!settings.RandomizedSlaughterEnabled)
            {
                CollectedImportantItemCountsByName["Slaughter"] = 1;
            }

            if (!settings.RandomizedOutfitsEnabled)
            {
                foreach(var outfit in Lookup.OutfitNameToEnum.Keys)
                    CollectedImportantItemCountsByName[outfit] = 1;
            }

            if (!settings.RandomizedSongsEnabled)
            {
                foreach(var song in Lookup.SongNameToEnum.Keys)
                    CollectedImportantItemCountsByName[song] = 1;
            }

            if (settings.WeaponUnlockMode == Settings.WeaponMode.WeaponAsOnePackage)
            {
                foreach(var weaponName in Lookup.WeaponNameToType.Keys)
                    CollectedImportantItemCountsByName[$"{weaponName} Ultimate"] = 1;
            }

            if (!settings.DestructibleAsUnlocks)
            {
                CollectedImportantItemCountsByName["Destructible Ammostashes"] = 1;
                CollectedImportantItemCountsByName["Destructible Health Crystals"] = 1;
                CollectedImportantItemCountsByName["Destructible Chaos Crystals"] = 1;
            }

            if (!settings.IncludeRandomizedWeaponSkinsChecks)
            {
                foreach(var skin in Lookup.WeaponSkinNameToType.Keys)
                    CollectedImportantItemCountsByName[skin] = 1;
            }

            if (!settings.RequireAspectForBossArena)
            {
                foreach(var aspect in Lookup.HellsNameToAspect.Values)
                    CollectedImportantItemCountsByName[aspect] = 1;
            }

            CollectedItemsByIndex = new Dictionary<int, ItemData>() { };
        }

        private readonly List<long> ProgressiveIds =
        [
            7, 12, 16, 20, 24, 28, 32,
            36, 50, 70, 72, 77, 82,
            87, 92, 97, 102, 107, 160,
            163, 167,
        ];

        private readonly List<long> BoonIds =
        [
            3, 4, 5, 6,
        ];

        private readonly List<long> WeaponIdsWithUltimates =
        [
            130, 132, 134, 138, 141, 144, 146, 148,
        ];

        public void SetCollectedItem(long itemId, int? itemIndex, bool rewardFiller, string sender = "")
        {
            ItemData item = Items.ItemDataById[itemId];
            Logger.LogInfo($"Granting item {item.Name}, rewarding filler: {rewardFiller}");

            if (itemIndex.HasValue)
            {
                CollectedItemsByIndex.Add(itemIndex.Value, item);
                Logger.LogInfo($"Item {item.Name} has index {itemIndex.Value}");
            }

            if (item.Name == "Filler")
                return;

            if(!itemIndex.HasValue)
                IngameMessagesPatches.DisplayItemReceived(item, sender);

            if (CollectedImportantItemCountsByName.ContainsKey(item.Name))
            {
                CollectedImportantItemCountsByName[item.Name]++;

                if (ProgressiveIds.Contains(item.ArchipelagoId))
                {
                    int count = CollectedImportantItemCountsByName[item.Name];
                    var newHell = Items.ItemDataById[itemId + count];
                    CollectedImportantItemCountsByName[newHell.Name]++;
                }

                // Any anguish gate unlock
                if (
                    Randomizer.CurrentGameMode == EGameMode.Stage
                    && item.ArchipelagoId >= 70
                    && item.ArchipelagoId <= 111
                )
                    Randomizer.LocationTracker.UpdateAnguishGates();

                if(BoonIds.Contains(item.ArchipelagoId))
                    AudioGameplayControllerPatches.UpdateUnlockedBeatstreaks();
            }

            if (item.Name.Equals("Coat of Arms"))
            {
                Randomizer.LocationTracker.CheckSkinUnlocks(
                    CollectedImportantItemCountsByName["Coat of Arms"]
                );
            }

            if (
                rewardFiller
                && (
                    item.Classification == ItemClassification.filler
                    || item.Classification == ItemClassification.trap
                )
            )
            {
                Logger.LogInfo("Queueing item " + item.Name + " for ingame dispension");
                Randomizer.IngameDispenser.QueueItem(item, sender);
            }
        }

        public bool HasRandomizedLevelUnlocked(string LevelID)
        {
            string randomizedLevelId = LevelID;
            if (Randomizer.Settings.RandomizedLevelsEnabled)
                randomizedLevelId = GetLevelForRandomizedLevel(LevelID);
            bool isUnlocked = GetMissingItemsUntilLevelUnlocked(LevelID).Count == 0;
            Logger.LogDebug($"Is level {LevelID} available: {isUnlocked}");
            return isUnlocked;
        }

        public bool HasLevelUnlocked(string LevelID)
        {
            bool isUnlocked = GetMissingItemsUntilLevelUnlocked(LevelID).Count == 0;
            Logger.LogDebug($"Is level {LevelID} available: {isUnlocked}");
            return isUnlocked;
        }

        internal bool HasDifficultyUnlocked(EDifficulty difficulty)
        {
            var difficultyItemName = Lookup.DifficultyTypeToName[difficulty];
            bool v = Has(difficultyItemName);
            Logger.LogDebug($"Has selected difficulty {difficulty}: {v}");
            return v;
        }

        public int GetProgressiveStagesUntilUnlock(string LevelID)
        {
            int diff =
                Lookup.RequiredProgressiveAmount[LevelID]
                - CollectedImportantItemCountsByName["Progressive Hells"];
            int result = diff >= 0 ? diff : 0;
            Logger.LogDebug($"Progressive stages until unlock for stage {LevelID}: {result}");
            return result;
        }

        public int GetProgressiveChallengesUntilUnlock(string LevelID)
        {
            int diff =
                Lookup.RequiredProgressiveAmount[LevelID]
                - CollectedImportantItemCountsByName[
                    $"Progressive {Lookup.ChallengeIdToDisplayDictionary[LevelID]}"
                ];
            int result = diff >= 0 ? diff : 0;
            Logger.LogDebug(
                $"Progressive challenges until unlock for challenge {LevelID}: {result}"
            );
            return result;
        }

        public List<string> GetMissingItemsUntilLevelUnlocked(string LevelID)
        {
            List<string> missingItems = new List<string>();
            foreach (string item in RequiredItemsForLevelUnlock[LevelID])
            {
                if(Lookup.ExtendedWeaponNameToType.TryGetValue(item, out var type))
                {
                    if(!IsWeaponUnlocked(type))
                        missingItems.Add(item);
                }
                else
                    if (!Has(item))
                        missingItems.Add(item);
            }
            Logger.LogInfo($"Items missing for {LevelID} are: {string.Join(", ", missingItems)}");
            return missingItems;
        }

        public bool HasHellOfChallenge(string levelID)
        {
            return Has(Lookup.ChallengeToHellDictionary[levelID]);
        }

        public List<PlayerWeaponType> GetAvailableWeaponTypes()
        {
            List<PlayerWeaponType> availableWeapons = new List<PlayerWeaponType>();
            foreach (var (itemName, weaponType) in Lookup.WeaponNameToType)
            {
                if (IsWeaponUnlocked(weaponType))
                {
                    // Respect DLCs
                    if (
                        (
                            weaponType != PlayerWeaponType.AssaultRifle
                            && weaponType != PlayerWeaponType.Bow
                        )
                        || (
                            weaponType == PlayerWeaponType.AssaultRifle
                            && DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                        )
                        || (
                            weaponType == PlayerWeaponType.Bow
                            && DLCPatches.Instance.HasDLC(EDLC.Purgatory)
                        )
                    )
                    {
                        availableWeapons.Add(weaponType);
                    }
                }
            }

            Logger.LogInfo($"Available weapon types are: {string.Join(", ", availableWeapons)}");
            return availableWeapons;
        }

        public bool IsWeaponUnlocked(PlayerWeaponType weaponType)
        {
            return weaponType switch
            {
                PlayerWeaponType.AssaultRifle => hasWeapon(weaponType),
                PlayerWeaponType.Bow => hasWeapon(weaponType),
                PlayerWeaponType.RhythmWeapon => hasWeapon(weaponType),
                PlayerWeaponType.Falx => hasWeapon(weaponType),
                PlayerWeaponType.Boomerang => hasWeapon(weaponType),
                PlayerWeaponType.Shotgun => HasAny(Lookup.PersephoneNames),
                PlayerWeaponType.Pistols => HasAny(Lookup.HoundsNames),
                PlayerWeaponType.Vulcan => HasAny(Lookup.VulcanNames),
                _ => false,
            };
        }

        private bool hasWeapon(PlayerWeaponType weaponType)
        {
            return Has(GetWeaponNameByType(weaponType));
        }


        public string GetWeaponNameByType(PlayerWeaponType weaponType)
        {
            return Lookup.WeaponTypeToName[weaponType];
        }

        public int GetSigilLevelByName(string name)
        {
            return Math.Min(CollectedImportantItemCountsByName.GetValueOrDefault(name, 0), 3);
        }

        public int GetSigilLevelByType(ESigilType sigilType)
        {
            if (Lookup.SigilTypeToName.TryGetValue(sigilType, out string sigilName))
            {
                int sigilLevel = CollectedImportantItemCountsByName.GetValueOrDefault(sigilName, 0);
                Logger.LogInfo($"Sigil {sigilType} ({sigilName}) has level: {sigilLevel}");
                return sigilLevel;
            }

            Logger.LogError($"Sigil type {sigilType} is not mapped to any known item.");
            return 0;
        }

        public bool HasItemByIndex(int itemIndex)
        {
            return CollectedItemsByIndex.ContainsKey(itemIndex);
        }

        public bool HasBoonByBeatSreakEffect(EBeatStreakEffect effect)
        {
            var boonName = Lookup.BoonTypeToName[effect];
            bool isAvailable = Has(boonName);
            Logger.LogInfo($"Boon {effect} is available: {isAvailable}");

            if (isAvailable && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc(boonName);

            return isAvailable;
        }

        public int GetCollectedCoatOfArms()
        {
            return Math.Min(CollectedImportantItemCountsByName["Coat of Arms"], 32);
        }

        public bool IsChallenge(string levelId)
        {
            return Lookup.ChallengeIdToDisplayDictionary.ContainsKey(levelId);
        }

        internal bool CanDash()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedDashEnabled)
                canPerform = Has("Dash");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Dash");
            Logger.LogDebug($"Can perform Dash: {canPerform}");
            return canPerform;
        }

        internal bool CanSoar()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedDashEnabled)
                canPerform = Has("Soar");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Soar");
            Logger.LogDebug($"Can perform Soar: {canPerform}");
            return canPerform;
        }

        internal bool CanJump()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedJumpEnabled)
                canPerform = Has("Jump");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Jump");
            Logger.LogDebug($"Can perform Jump: {canPerform}");
            return canPerform;
        }

        internal bool CanDoubleJump()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedJumpEnabled)
                canPerform = Has("Double Jump");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Double Jump");
            Logger.LogDebug($"Can perform Double Jump: {canPerform}");
            return canPerform;
        }

        internal bool CanInfiniteJump()
        {
            bool canPerform = false;
            if (Randomizer.Settings.RandomizedJumpEnabled)
                canPerform = Has("Infinite Jump");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Infinite Jump");
            Logger.LogDebug($"Can perform Infinite Jump: {canPerform}");
            return canPerform;
        }

        internal bool CanManualReload()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedReloadEnabled)
                canPerform = Has("Manual Reload");

            Logger.LogDebug($"Can perform Manual Reload: {canPerform}");
            return canPerform;
        }

        internal bool CanQuickReload()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedReloadEnabled)
                canPerform = Has("Quick Reload");

            if(canPerform && !Randomizer.IsPaused)
                Randomizer.LocationTracker.CheckMisc("Quick Reload");
            Logger.LogDebug($"Can perform Quick Reload: {canPerform}");
            return canPerform;
        }

        internal bool CanWeaponUltimate(PlayerWeaponType playerWeaponType)
        {
            string weaponName = GetWeaponNameByType(playerWeaponType);
            return CanWeaponUltimate(weaponName);
        }

        internal bool CanWeaponUltimate(string weaponName)
        {
            if(!Lookup.WeaponNameToType.ContainsKey(weaponName))
                return false;

            var weaponType  = Lookup.WeaponNameToType[weaponName];
            bool canPerform = IsWeaponUnlocked(weaponType) && Has($"{weaponName} Ultimate");
            Logger.LogDebug($"Can perform {weaponName}'s ultimate: {canPerform}");
            return canPerform;
        }

        internal bool CanSlaughter()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedSlaughterEnabled)
                canPerform = Has("Slaughter");

            return canPerform;
        }

        internal bool IsDestructible(string destructibleName)
        {
            if (destructibleName.Contains("Ammostash"))
            {
                bool isDestructible = Has("Destructible Ammostashes");
                if (isDestructible && !Randomizer.IsPaused)
                    Randomizer.LocationTracker.CheckMisc("Ammostash");
                return isDestructible;
            }
            else if (destructibleName.Contains("Health"))
            {
                bool isDestructible = Has("Destructible Health Crystals");
                if (isDestructible && !Randomizer.IsPaused)
                    Randomizer.LocationTracker.CheckMisc("Health Crystal");
                return isDestructible;
            }
            else if (destructibleName.Contains("Chaos"))
            {
                bool isDestructible = Has("Destructible Chaos Crystals");
                if (isDestructible && !Randomizer.IsPaused)
                    Randomizer.LocationTracker.CheckMisc("Chaos Crystal");
                return isDestructible;
            }

            return true;
        }

        internal bool HasAnguishGateSkip(string destructibleName)
        {
            bool hasAlreadyCompleted = !Randomizer.LocationTracker.IsLocationUnchecked(destructibleName);
            bool hasSkip = Has($"{destructibleName} Skip");
            return Randomizer.Settings.IncludeProgressiveAnguishGateSkips && hasAlreadyCompleted && hasSkip;
        }

        internal string GetRandomizedLevel(string levelID)
        {
            if (LevelToLoadedLevel.TryGetValue(levelID, out var randomizedLevel))
                return randomizedLevel;
            return levelID;
        }

        internal string GetLevelForRandomizedLevel(string levelID)
        {
            if (LoadedLevelToLevel.TryGetValue(levelID, out var randomizedLevel))
                return randomizedLevel;
            return levelID;
        }

        internal bool IsWeaponSkinUnlocked(PlayerWeaponType weapon)
        {
            bool hasSkin =
                Lookup.WeaponTypeToSkinName.TryGetValue(weapon, out var skinName) && Has(skinName);
            Logger.LogInfo($"Has skin for {weapon}: {hasSkin}");
            return hasSkin;
        }

        // not using Has directly to check for DLC requirements
        internal bool IsOutfitUnlocked(SkinType skinType)
        {
            bool hasSkin = false;
            var unlockedOutfits = GetUnlockedOutfits();
            if (
                Lookup.OutfitTypeToName.TryGetValue(skinType, out var outfitName)
                && unlockedOutfits.Contains(outfitName)
            )
                hasSkin = true;
            Logger.LogInfo($"Has skin {skinType}: {hasSkin}");
            return hasSkin;
        }

        internal String GetOutfitNameByType(SkinType type)
        {
            return Lookup.OutfitTypeToName[type];
        }

        internal SkinTargetType WeaponToSkin(PlayerWeaponType weaponType)
        {
            return Lookup.WeaponTypeToSkinType[weaponType];
        }

        internal SkinType GetRandomizedOutfit()
        {
            string randomizedOutfit = null;
            List<string> unlockedOutfits = GetUnlockedOutfits();

            if (
                Randomizer.Settings.RandomizedOutfitsEnabled
                && Randomizer.Configuration.skinsPrioritizeNewOutfits.Value
            )
            {
                List<string> missingOutfitChecks =
                    Randomizer.LocationTracker.GetItemsWithMissingChecks(unlockedOutfits);
                if (missingOutfitChecks.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, missingOutfitChecks.Count);
                    randomizedOutfit = missingOutfitChecks[randomIndex];
                }
            }

            if (randomizedOutfit == null)
            {
                List<string> unlockedFilteredOutfits = FilterOutfitsByConfig(unlockedOutfits);
                int randomIndex = UnityEngine.Random.Range(0, unlockedFilteredOutfits.Count);
                randomizedOutfit = unlockedFilteredOutfits[randomIndex];
            }

            Logger.LogInfo($"Randomizing Outfit to {randomizedOutfit}");
            return Lookup.OutfitNameToType[randomizedOutfit];
        }

        private List<string> FilterOutfitsByConfig(List<string> unlockedOutfits)
        {
            List<string> filteredOutfits = new List<string>(unlockedOutfits);

            foreach (var kvp in Lookup.OutfitEnumToName)
            {
                if (!Randomizer.Configuration.skinsOutfitsToInclude.Value.HasFlag(kvp.Key))
                    filteredOutfits.Remove(kvp.Value);
            }
            if (filteredOutfits.Count == 0)
                filteredOutfits = unlockedOutfits;

            Logger.LogInfo($"Returning filtered Outfits: {string.Join(", ", filteredOutfits)}");
            return filteredOutfits;
        }

        internal string GetRandomizedMainSong()
        {
            string randomizedSong = null;
            List<string> unlockedSongs = GetUnlockedMainSongs();

            if (
                Randomizer.Settings.RandomizedSongsEnabled
                && Randomizer.Configuration.songsPrioritizeNewSongs.Value
            )
            {
                List<string> missingSongChecks =
                    Randomizer.LocationTracker.GetItemsWithMissingChecks(unlockedSongs);
                if (missingSongChecks.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, missingSongChecks.Count);
                    randomizedSong = missingSongChecks[randomIndex];
                }
            }

            if (randomizedSong == null)
            {
                List<string> unlockedFilteredSongs = FilterSongsByFlags(
                    unlockedSongs,
                    Randomizer.Configuration.songsMainSongsToInclude.Value
                );
                int randomIndex = UnityEngine.Random.Range(0, unlockedFilteredSongs.Count);
                randomizedSong = unlockedFilteredSongs[randomIndex];
            }

            Logger.LogInfo($"Randomizing Main Song to {randomizedSong}");
            return Lookup.SongNameToId[randomizedSong];
        }

        internal String GetSongNameById(string songId)
        {
            return Lookup.SongIdToName.GetValueOrDefault(songId, "");
        }

        internal string GetRandomizedBossSong(string levelId = null)
        {
            string randomizedSong = null;
            List<string> unlockedSongs = GetUnlockedBossSongs();

            if (
                levelId != "Sheol"
                && Randomizer
                    .Configuration
                    .songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss
                    .Value
            )
                unlockedSongs.Remove("No Tomorrow");

            if (
                Randomizer.Settings.RandomizedSongsEnabled
                && Randomizer.Configuration.songsPrioritizeNewSongs.Value
            )
            {
                List<string> missingSongChecks =
                    Randomizer.LocationTracker.GetItemsWithMissingChecks(unlockedSongs);

                if (Randomizer.Configuration.songsApplyBossSongFilterForPrioritizedSongs.Value)
                    missingSongChecks = FilterSongsByFlags(
                        missingSongChecks,
                        Randomizer.Configuration.songsBossSongsToInclude.Value
                    );

                if (missingSongChecks.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, missingSongChecks.Count);
                    randomizedSong = missingSongChecks[randomIndex];
                }
            }

            if (randomizedSong == null)
            {
                List<string> unlockedFilteredSongs = FilterSongsByFlags(
                    unlockedSongs,
                    Randomizer.Configuration.songsBossSongsToInclude.Value
                );
                int randomIndex = UnityEngine.Random.Range(0, unlockedFilteredSongs.Count);
                randomizedSong = unlockedFilteredSongs[randomIndex];
            }

            if (
                levelId == "Sheol"
                && Randomizer
                    .Configuration
                    .songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss
                    .Value
                && unlockedSongs.Contains("No Tomorrow")
            )
                randomizedSong = "No Tomorrow";
            Logger.LogInfo($"Randomizing Boss Song to {randomizedSong}");
            return Lookup.SongNameToId[randomizedSong];
        }

        private List<string> FilterSongsByFlags(List<string> unlockedSongs, Lookup.SongId flags)
        {
            List<string> filteredSongs = new List<string>(unlockedSongs);

            foreach (var kvp in Lookup.SongEnumToName)
            {
                if (!flags.HasFlag(kvp.Key))
                    filteredSongs.Remove(kvp.Value);
            }

            if (filteredSongs.Count == 0)
                filteredSongs = unlockedSongs;

            Logger.LogInfo($"Returning filtered songs: {string.Join(", ", filteredSongs)}");
            return filteredSongs;
        }

        internal List<string> GetUnlockedOutfits()
        {
            List<string> unlockedOutfits = new List<string>();

            foreach (var kvp in Lookup.OutfitNames)
            {
                ItemOrigin origin = kvp.Key;

                if (
                    !Randomizer.Settings.RandomizedOutfitDLCs.HasFlag(origin)
                    || (origin == DreamOfTheBeast && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast))
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                )
                {
                    Logger.LogDebug(
                        $"Skipping outfit origin '{origin}': Setting disabled or DLC not owned."
                    );
                    continue;
                }

                foreach (string outfitName in kvp.Value)
                {
                    if (Has(outfitName))
                    {
                        Logger.LogDebug(
                            $"Found unlocked outfit: '{outfitName}' (Origin: {origin})"
                        );
                        unlockedOutfits.Add(outfitName);
                    }
                    else
                    {
                        Logger.LogDebug($"Outfit '{outfitName}' is not unlocked/owned.");
                    }
                }
            }

            // Default if player messes up yaml by including non-available DLCs
            if (unlockedOutfits.Count == 0)
            {
                Logger.LogDebug(
                    "No outfits were unlocked from active settings. Falling back to default 'Outfit of the Unknown'."
                );
                unlockedOutfits.Add("Outfit of the Unknown");
            }

            Logger.LogDebug($"Returning unlocked outfits: {string.Join(", ", unlockedOutfits)}");
            return unlockedOutfits;
        }

        internal List<string> GetUnlockedMainSongs()
        {
            List<string> unlockedSongs = new List<string>();

            foreach (var kvp in Lookup.MainSongNames)
            {
                ItemOrigin origin = kvp.Key;

                if (
                    !Randomizer.Settings.RandomizedSongDLCs.HasFlag(origin)
                    || (origin == DreamOfTheBeast && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast))
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                    || (origin == EssentialHits && !DLCPatches.Instance.HasDLC(EDLC.LicensedTracks1)))
                    continue;

                foreach (string songName in kvp.Value)
                {
                    if (Has(songName))
                        unlockedSongs.Add(songName);
                }
            }

            Logger.LogDebug($"Returning unlocked main songs: {string.Join(", ", unlockedSongs)}");
            return unlockedSongs;
        }

        internal List<string> GetUnlockedBossSongs()
        {
            List<string> unlockedSongs = new List<string>();

            foreach (var kvp in Lookup.BossSongNames)
            {
                ItemOrigin origin = kvp.Key;

                if (
                    !Randomizer.Settings.RandomizedSongDLCs.HasFlag(origin)
                    || (origin == DreamOfTheBeast && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast))
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                    || (origin == EssentialHits && !DLCPatches.Instance.HasDLC(EDLC.LicensedTracks1)))
                    continue;

                foreach (string songName in kvp.Value)
                {
                    if (Has(songName))
                        unlockedSongs.Add(songName);
                }
            }

            Logger.LogDebug($"Returning unlocked boss songs: {string.Join(", ", unlockedSongs)}");
            return unlockedSongs;
        }

        internal bool HasDifficultyForButtonname(string buttonName)
        {
            bool hasDifficulty = false;
            var trimmed = buttonName.Trim();
            trimmed = trimmed.Remove(trimmed.Length - 6); // remove Button from name

            if (Enum.TryParse<EDifficulty>(trimmed, ignoreCase: true, out var difficulty))
                hasDifficulty = HasDifficultyUnlocked(difficulty);

            Logger.LogInfo($"Has difficulty for button {buttonName}: {hasDifficulty}");
            return hasDifficulty;
        }

        internal List<ExtendedWeaponType> GetAvailablePersephoneTypes()
        {
            List<ExtendedWeaponType> availableTypes = new() { };

            if (Has("Persephone"))
                availableTypes.Add(ExtendedWeaponType.Regular);

            if (Has("Lost Persephone"))
                availableTypes.Add(ExtendedWeaponType.Lost);

            if (Has("Manifested Persephone"))
                availableTypes.Add(ExtendedWeaponType.Manifested);

            return availableTypes;
        }

        internal List<WeaponType> GetAvailableHoundsTypes()
        {
            List<WeaponType> availableTypes = new() { };

            if (Has("The Hounds"))
                availableTypes.Add(WeaponType.Regular);

            if (Has("The Lost Hounds"))
                availableTypes.Add(WeaponType.Lost);

            return availableTypes;
        }

        internal List<WeaponType> GetAvailableVulcanTypes()
        {
            List<WeaponType> availableTypes = new() { };
            if (Has("Vulcan"))
                availableTypes.Add(WeaponType.Regular);

            if (Has("Lost Vulcan"))
                availableTypes.Add(WeaponType.Lost);
            return availableTypes;
        }

        internal bool HasSongByLocation(string id)
        {
            return Has(ExtractName(id));
        }

        internal bool HasWeaponByLocation(string id)
        {
            return Has(ExtractName(id));
        }

        internal bool HasOutfitByLocation(string id)
        {
            return Has(ExtractName(id));
        }

        private static string ExtractName(string input)
        {
            const string prefix = "Section Cleared with: ";

            int index = input.IndexOf(prefix);
            if (index != -1)
                return input.Substring(index + prefix.Length).Trim();

            return input;
        }

        internal bool HasAspectOfLevel(string levelId)
        {
            if(Lookup.HellsNameToAspect.TryGetValue(levelId, out var aspectName))
                return Has(aspectName);
            return true;
        }

        internal List<string> GetMissingSheolItems()
        {
            List<string> missingItems = new List<string>();

            int currentCoatOfArms = CollectedImportantItemCountsByName["Coat of Arms"];
            int requiredCoatOfArms = Randomizer.Settings.RequiredCoatOfArmsForSheol;
            int missingCoatOfArms = Math.Max(requiredCoatOfArms - currentCoatOfArms, 0);
            if (Randomizer.Settings.RequireCoatOfArmsForSheol)
            {
                if (missingCoatOfArms > 0)
                {
                    Logger.LogDebug(
                        $"Coat of Arms requirement active: Missing {missingCoatOfArms} ({currentCoatOfArms}/{requiredCoatOfArms})."
                    );
                    missingItems.Add($"{missingCoatOfArms} more Coat of Arms");
                }
                else
                    Logger.LogDebug(
                        $"Coat of Arms requirement met ({currentCoatOfArms}/{requiredCoatOfArms})."
                    );
            }
            else
                Logger.LogDebug("Coat of Arms requirement disabled.");

            if (Randomizer.Settings.RequireNoTomorrowForSheol)
            {
                bool hasNoTomorrow = Randomizer.ItemTracker.Has("No Tomorrow");
                if (!hasNoTomorrow)
                {
                    Logger.LogDebug(
                        "'No Tomorrow' requirement active: Item is missing."
                    );
                    missingItems.Add("No Tomorrow");
                }
                else
                    Logger.LogDebug("'No Tomorrow' requirement met.");
            }
            else
                Logger.LogDebug("'No Tomorrow' requirement disabled.");

            if (missingItems.Count > 0)
                Logger.LogDebug(
                    $" Requirements not met. Total missing: {missingItems.Count} -> ({string.Join(", ", missingItems)})"
                );
            else
                Logger.LogDebug("All Sheol access requirements are met");

            return missingItems;
        }

        internal int GetSkipsAmount(string levelId)
        {
            int count = 0;
            for (int i = 1; i < 5; i++)
            {
                string skipName = $"{levelId} Anguish Gate {i} Skip";
                if(Has(skipName))
                    count++;
            }
            return count;

        }
    }
}
