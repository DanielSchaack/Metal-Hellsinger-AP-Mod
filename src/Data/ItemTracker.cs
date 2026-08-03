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
            { "Anger Aspect: Voke defeated", 1 },
            { "Stygia", 1 },
            { "Charged Aspect: Stygia defeated", 1 },
            { "Yhelm", 1 },
            { "Fortress Aspect: Yhelm defeated", 1 },
            { "Incaustis", 1 },
            { "Infernal Fury Aspect: Incaustis defeated", 1 },
            { "Gehenna", 1 },
            { "Hellstorm Aspect: Gehenna defeated", 1 },
            { "Nihil", 1 },
            { "DoppelGanger Aspect: Nihil defeated", 1 },
            { "Acheron", 1 },
            { "Wheel Aspect: Acheron defeated", 1 },
            { "Sheol", 1 },
            { "Red Judge - Worldbreaker: Sheol defeated", 1 },

            { "Progressive Killing with Rhythm", 3 },
            { "Killing with Rhythm: 1", 0 },
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
            { "Vulcan", 0 },
            { "Vulcan Ultimate", 1 },
            { "Hellcrow", 1 },
            { "Hellcrow Ultimate", 1 },
            { "The Red Right Hand", 1 },
            { "The Red Right Hand Ultimate", 1 },
            { "Telos", 1 },
            { "Telos Ultimate", 1 },

            { "Streak Guardian", 3 },
            { "Ghost Rounds", 3 },
            { "Boon Momentum", 3 },
            { "Unyielding Fury", 3 },
            { "Last Breath Aegis", 3 },
            { "Ultimate Sovereignty", 3 },
            { "The Perfectionist", 3 },

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

            { "Lamb", 1 },
            { "Goat", 1 },
            { "Beast", 1 },
            { "Archdevil", 1 },

            { "Paz Skin", 0 },
            { "Terminus Skin", 1 },
            { "Persephone Skin", 1 },
            { "The Hounds Skin", 0 },
            { "Vulcan Skin", 1 },
            { "Hellcrow Skin", 1 },

            { "Outfit of the Unknown", 1 },
            { "Outfit of the Dark Devotee", 1 },
            { "Outfit of the Morning Star", 1 },
            { "Outfit of the Angel Eyes", 1 },
            { "Obisidan Outfit", 0 },
            { "Outfit of the Amethyst", 1 },
            { "Outfit of the Chromatica", 1 },
            { "Outfit of the Leviathan", 0 },

            { "This is the End", 1 },
            { "Stygia (Song)", 0 },
            { "Burial At Night", 1 },
            { "This Devastation", 1 },
            { "Poetry of Cinder", 1 },
            { "Dissolution", 0 },
            { "Acheron (Song)", 1 },
            { "Silent No More", 1 },

            { "Blood and Law", 1 },
            { "Infernal Invocation I: Hopes and Fears", 0 },
            { "Infernal Invocation II: Defiance", 1 },
            { "Infernal Invocation III: Dreaming in Distortion", 1 },
            { "No Tomorrow", 1 },

            { "Leviathan (Song)", 0 },
            { "Dream of the Beast", 1 },
            { "Swallow the Fire", 1 },
            { "Mouth of Hell", 0 },
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

            // TODO:
            { "Lost Persephone", 1 },
            { "Manifested Persephone", 1 },
            { "The Lost Hounds", 1 },
            { "Lost Vulcan", 1 },

            // Not in use yet
            // Anguish Gates
            { "Progressive Tutorial Anguish Gate", 1 },
            { "Tutorial Anguish Gate 1", 1 },
            { "Progressive Voke Anguish Gate", 4 },
            { "Voke Anguish Gate 1", 1 },
            { "Voke Anguish Gate 2", 1 },
            { "Voke Anguish Gate 3", 1 },
            { "Voke Anguish Gate 4", 1 },
            { "Progressive Stygia Anguish Gate", 4 },
            { "Stygia Anguish Gate 1", 1 },
            { "Stygia Anguish Gate 2", 1 },
            { "Stygia Anguish Gate 3", 1 },
            { "Stygia Anguish Gate 4", 1 },
            { "Progressive Yhelm Anguish Gate", 4 },
            { "Yhelm Anguish Gate 1", 1 },
            { "Yhelm Anguish Gate 2", 1 },
            { "Yhelm Anguish Gate 3", 1 },
            { "Yhelm Anguish Gate 4", 1 },
            { "Progressive Incaustis Anguish Gate", 4 },
            { "Incaustis Anguish Gate 1", 1 },
            { "Incaustis Anguish Gate 2", 1 },
            { "Incaustis Anguish Gate 3", 1 },
            { "Incaustis Anguish Gate 4", 1 },
            { "Progressive Gehenna Anguish Gate", 4 },
            { "Gehenna Anguish Gate 1", 1 },
            { "Gehenna Anguish Gate 2", 1 },
            { "Gehenna Anguish Gate 3", 1 },
            { "Gehenna Anguish Gate 4", 1 },
            { "Progressive Nihil Anguish Gate", 4 },
            { "Nihil Anguish Gate 1", 1 },
            { "Nihil Anguish Gate 2", 1 },
            { "Nihil Anguish Gate 3", 1 },
            { "Nihil Anguish Gate 4", 1 },
            { "Progressive Acheron Anguish Gate", 4 },
            { "Acheron Anguish Gate 1", 1 },
            { "Acheron Anguish Gate 2", 1 },
            { "Acheron Anguish Gate 3", 1 },
            { "Acheron Anguish Gate 4", 1 },
            { "Progressive Sheol Anguish Gate", 4 },
            { "Sheol Anguish Gate 1", 1 },
            { "Sheol Anguish Gate 2", 1 },
            { "Sheol Anguish Gate 3", 1 },
            { "Sheol Anguish Gate 4", 1 },

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
            { "EndlessModeBase", "Voke" },
            // { "EndlessModeBase", "EndlessModeBase" },
            { "Tutorial", "CH_Bune1" },
            // { "Tutorial", "Tutorial" },
            { "Voke", "Stygia" },
            // { "Voke", "Voke" },
            { "Stygia", "EndlessModeBase" },
            // { "Stygia", "Stygia" },
            { "Yhelm", "CH_Halphas1" },
            // { "Yhelm", "Yhelm" },
            { "Incaustis", "Incaustis" },
            { "Gehenna", "Gehenna" },
            { "Nihil", "Nihil" },
            { "Acheron", "Acheron" },
            { "Sheol", "Sheol" },
            { "CH_Amdusias1", "CH_Amdusias1" },
            { "CH_Marbas1", "CH_Marbas3" },
            // { "CH_Halphas1", "CH_Halphas1" },
            { "CH_Halphas1", "Yhelm" },
            { "CH_Bune1", "Tutorial" },
            // { "CH_Bune1", "CH_Bune1" },
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
            { "CH_Marbas3", "CH_Marbas1" },
            { "CH_Flauros2", "CH_Flauros2" },
            { "CH_Glasya2", "CH_Glasya2" },
            { "CH_Bune3", "CH_Bune3" },
            { "CH_Morax3", "CH_Morax3" },
            { "CH_Flauros3", "CH_Flauros3" },
            { "CH_Glasya3", "CH_Glasya3" },
        };
        public Dictionary<string, string> LoadedLevelToLevel =
            LevelToLoadedLevel.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        private bool Has(string itemName)
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
            var allLevelKeys = Lookup.RequiredLevelItems.Keys.Union(
                Lookup.RequiredWeaponsForLevel.Keys
            );
            foreach (var level in allLevelKeys)
            {
                var combinedList = new List<string>();

                if (Lookup.RequiredLevelItems.TryGetValue(level, out var items))
                    combinedList.AddRange(items);

                if (Lookup.RequiredWeaponsForLevel.TryGetValue(level, out var weapons))
                    combinedList.AddRange(weapons);

                RequiredItemsForLevelUnlock[level] = combinedList.Distinct().ToList();
            }

            CollectedItemsByIndex = new Dictionary<int, ItemData>() { };
        }

        public void Reset()
        {
            foreach (string Key in CollectedImportantItemCountsByName.Keys.ToList())
                CollectedImportantItemCountsByName[Key] = 0;

            RequiredItemsForLevelUnlock.Clear();
            var allLevelKeys = Lookup.RequiredLevelItems.Keys.Union(
                Lookup.RequiredWeaponsForLevel.Keys
            );
            foreach (var level in allLevelKeys)
            {
                var combinedList = new List<string>();

                if (Lookup.RequiredLevelItems.TryGetValue(level, out var items))
                    combinedList.AddRange(items);

                if (Lookup.RequiredWeaponsForLevel.TryGetValue(level, out var weapons))
                    combinedList.AddRange(weapons);

                RequiredItemsForLevelUnlock[level] = combinedList.Distinct().ToList();
            }

            CollectedItemsByIndex = new Dictionary<int, ItemData>() { };
            Items.ItemList.Clear();
        }

        private readonly List<long> ProgressiveIds =
        [
            13, 17, 21, 25, 29, 33,
            37, 50, 70, 72, 77, 82,
            87, 92, 97, 102, 107, 160,
            163, 167,
        ];

        private readonly List<long> WeaponIds =
        [
            130, 132, 134, 136, 138, 140,
            140, 142, 144, 146, 148, 150,
            152,
        ];

        private readonly List<long> DispensibleProgressiveItems =
        [
            130, 132, 134, 136, 138, 140,
            140, 142, 144, 146, 148, 150,
            152, 301, 302, 303, 304,
        ];

        // TODO:
        public void SetCollectedItem(long itemId, int? itemIndex, bool rewardFiller, bool isResync)
        {
            ItemData item = Items.ItemDataById[itemId];
            Logger.LogInfo("Granting item " + item.Name);

            if (itemIndex.HasValue)
                CollectedItemsByIndex.Add(itemIndex.Value, item);

            if (item.Name == "Filler")
                return;

            if (CollectedImportantItemCountsByName.ContainsKey(item.Name))
            {
                CollectedImportantItemCountsByName[item.Name]++;

                if (ProgressiveIds.Contains(item.ArchipelagoId))
                {
                    int count = CollectedImportantItemCountsByName[item.Name];
                    var newHell = Items.ItemDataById[itemId + count];
                    CollectedImportantItemCountsByName[newHell.Name]++;
                }

                if (
                    Randomizer.Settings.WeaponUnlockMode == Settings.WeaponMode.WeaponAsOnePackage
                    && WeaponIds.Contains(item.ArchipelagoId)
                )
                {
                    var weaponUltimateUnlock = Items.ItemDataById[itemId + 1];
                    CollectedImportantItemCountsByName[weaponUltimateUnlock.Name]++;
                    Logger.LogInfo(
                        $"Weapons are unlocked as one package, also granting {weaponUltimateUnlock.Name}"
                    );
                }
            }

            // the filler Coat of Arm item
            if (item.Name == "Coat of Arms Fill")
            {
                var progressiveCoatOfArm = Items.ItemDataByName["Coat of Arms"];
                CollectedImportantItemCountsByName[progressiveCoatOfArm.Name]++;
                Logger.LogInfo($"Adding a Coat of Arm filler, item: {progressiveCoatOfArm.Name}");
            }

            if (item.Name.StartsWith("Coat of Arms Fill"))
            {
                Randomizer.LocationTracker.CheckSkinUnlocks(
                    CollectedImportantItemCountsByName["Coat of Arms"]
                );
            }
            if (
                rewardFiller
                && !isResync
                && (
                    item.Classification == ItemClassification.filler
                    || item.Classification == ItemClassification.trap
                )
            )
            {
                Logger.LogInfo("Queueing item " + item.Name + " for ingame dispension");
                Randomizer.IngameDispenser.QueueItem(item);
            }

            if (!isResync && DispensibleProgressiveItems.Contains(itemId))
            {
                Logger.LogInfo("Queueing item " + item.Name + " for ingame dispension");
                Randomizer.IngameDispenser.QueueItem(item);
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
            return Has(difficultyItemName);
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
                if (!Has(item))
                    missingItems.Add(item);
            }
            Logger.LogInfo($"Items missing for {LevelID} are: {string.Join(", ", missingItems)}");
            return missingItems;
        }

        public string GetChallengeDisplayName(string LevelID)
        {
            return Lookup.ChallengeIdToDisplayDictionary.GetValueOrDefault(LevelID, LevelID);
        }

        public string GetActualLevelName(string LevelID)
        {
            return Lookup.LevelIdToActualName[LevelID];
        }

        public bool HasHellOfChallenge(string levelID)
        {
            return Has(Lookup.ChallengeToHellDictionary[levelID]);
        }

        public string GetHellOfChallenge(string levelID)
        {
            return Lookup.ChallengeToHellDictionary[levelID];
        }

        public bool HasWeaponsForLevel(string levelID)
        {
            return GetMissingWeaponsForLevel(levelID).Count == 0;
        }

        public List<string> GetWeaponsForLevel(string levelID)
        {
            var weapons = Lookup.RequiredWeaponsForLevel[levelID];
            return weapons;
        }

        public List<string> GetMissingWeaponsForLevel(string levelID)
        {
            List<string> missingWeapons = new List<string>();
            var requiredWeapons = Lookup.RequiredWeaponsForLevel[levelID];
            foreach (string weapon in requiredWeapons)
            {
                if (!Has(weapon))
                    missingWeapons.Add(weapon);
            }
            Logger.LogDebug(
                $"Weapons missing for {levelID} are: {string.Join(", ", missingWeapons)}"
            );
            return missingWeapons;
        }

        public List<PlayerWeaponType> GetAvailableWeaponTypes()
        {
            List<PlayerWeaponType> availableWeapons = new List<PlayerWeaponType>();
            foreach (var (itemName, weaponType) in Lookup.WeaponNameToType)
            {
                if (hasWeapon(weaponType))
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

        // TODO:
        public void Resync(ReadOnlyCollection<ItemInfo> allItemsReceived)
        {
            throw new NotImplementedException();
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
            return isAvailable;
        }

        public int GetCollectedCoatOfArms()
        {
            return Math.Min(CollectedImportantItemCountsByName["Coat of Arms"], 32);
        }

        private readonly List<string> HellsBosses =
        [
            "Anger Aspect: Voke defeated",
            "Charged Aspect: Stygia defeated",
            "Fortress Aspect: Yhelm defeated",
            "Infernal Fury Aspect: Incaustis defeated",
            "Hellstorm Aspect: Gehenna defeated",
            "DoppelGanger Aspect: Nihil defeated",
            "Wheel Aspect: Acheron defeated",
            "Red Judge - Worldbreaker: Sheol defeated",
        ];

        private readonly List<string> LeviathanBosses = ["The Lost Unknown: Leviathan defeated"];

        public List<string> GetBossesDefeated(ItemGamemode gamemode)
        {
            List<string> DefeatedBosses = new List<string>() { };
            if (gamemode == ItemGamemode.HELL)
                foreach (string boss in HellsBosses)
                {
                    if (Has(boss))
                        DefeatedBosses.Add(boss);
                }
            else if (gamemode == ItemGamemode.LEVIATHAN)
                foreach (string boss in LeviathanBosses)
                {
                    if (Has(boss))
                        DefeatedBosses.Add(boss);
                }
            return DefeatedBosses;
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

            Logger.LogDebug($"Can perform Dash: {canPerform}");
            return canPerform;
        }

        internal bool CanSoar()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedDashEnabled && CanDash())
                canPerform = Has("Soar");

            Logger.LogDebug($"Can perform Soar: {canPerform}");
            return canPerform;
        }

        internal bool CanJump()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedJumpEnabled)
                canPerform = Has("Jump");

            Logger.LogDebug($"Can perform Jump: {canPerform}");
            return canPerform;
        }

        internal bool CanDoubleJump()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedJumpEnabled && CanJump())
                canPerform = Has("Double Jump");

            Logger.LogDebug($"Can perform Double Jump: {canPerform}");
            return canPerform;
        }

        internal bool CanInfiniteJump()
        {
            bool canPerform = false;
            if (Randomizer.Settings.RandomizedJumpEnabled && CanJump() && CanDoubleJump())
                canPerform = Has("Infinite Jump");

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
            bool canPerform = Has($"{weaponName} Ultimate");
            Logger.LogDebug($"Can perform {weaponName}'s ultimate: {canPerform}");
            return true;
        }

        internal bool CanSlaughter()
        {
            bool canPerform = true;
            if (Randomizer.Settings.RandomizedSlaughterEnabled)
                canPerform = Has("Slaughter");

            Logger.LogDebug($"Can perform Slaughter: {canPerform}");
            return canPerform;
        }

        internal bool IsDestructible(string destructibleName)
        {
            if (destructibleName.Contains("Ammostash"))
                return Has("Destructible Ammostashes");
            else if (destructibleName.Contains("Health"))
                return Has("Destructible Health Crystals");
            else if (destructibleName.Contains("Chaos"))
                return Has("Destructible Chaos Crystals");

            return true;
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

        internal string GetRandomizedBossSong()
        {
            string randomizedSong = null;
            List<string> unlockedSongs = GetUnlockedBossSongs();

            if (
                !Randomizer.IsFinalLevel
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
                Randomizer.IsFinalLevel
                && Randomizer
                    .Configuration
                    .songsRestrictAndEnforceNoTomorrowToOnlyTheFinalBoss
                    .Value
                && unlockedSongs.Contains("No Tomorrow")
            )
                randomizedSong = "No Tomorrow";
            Logger.LogInfo($"Randomizing Main Song to {randomizedSong}");
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
                    || (
                        origin == DreamOfTheBeast
                        && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                    )
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                )
                    continue;

                foreach (string outfitName in kvp.Value)
                {
                    if (Has(outfitName))
                        unlockedOutfits.Add(outfitName);
                }
            }

            // Default if player messes up yaml by including non-available DLCs
            if (unlockedOutfits.Count == 0)
                unlockedOutfits.Add("Outfit of the Unknown");

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
                    || (
                        origin == DreamOfTheBeast
                        && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                    )
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                    || (
                        origin == EssentialHits && !DLCPatches.Instance.HasDLC(EDLC.LicensedTracks1)
                    )
                )
                    continue;

                foreach (string songName in kvp.Value)
                {
                    if (HasSongByName(songName))
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
                    || (
                        origin == DreamOfTheBeast
                        && !DLCPatches.Instance.HasDLC(EDLC.DreamOfTheBeast)
                    )
                    || (origin == Purgatory && !DLCPatches.Instance.HasDLC(EDLC.Purgatory))
                    || (
                        origin == EssentialHits && !DLCPatches.Instance.HasDLC(EDLC.LicensedTracks1)
                    )
                )
                    continue;

                foreach (string songName in kvp.Value)
                {
                    if (HasSongByName(songName))
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

        internal bool HasSongByName(string songName)
        {
            return Has(songName);
        }

        //TODO:
        internal EZone GetZoneForLevelId(string levelId)
        {
            return Lookup.LevelIdToEZone[levelId];
        }

        //TODO:
        internal EArena GetArenasForLevelId(string levelId)
        {
            return Lookup.LevelIdToArena[levelId];
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
    }
}
