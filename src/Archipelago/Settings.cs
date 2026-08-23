using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using static Randomizer.ItemOrigin;

namespace Randomizer
{
    public class Settings
    {

        public enum HellsMode
        {
            Progressive,
            UnlockAsCollectible,
        }

        public enum ChallengeMode
        {
            Progressive,
            UnlockAsCollectible,
        }

        public enum WeaponMode
        {
            WeaponAsOnePackage,
            IndividualAbilities,
        }

        public enum DestructibleMode
        {
            PerEntireArena,
            PerDestructibleType,
        }

        public bool RequireSheolCompletion { get; set; } = false;
        public bool RequireHellsCompletion { get; set; } = false;
        public int RequiredHellsCompletion { get; set; } = 5;
        public bool RequireLeviathanCompletion { get; set; } = false;

        public bool RandomizedHellsEnabled { get; set; } = true;
        public bool RequireAspectForBossArena { get; set; } = false;
        public int StartingHells { get; set; } = 0;
        public HellsMode HellsUnlockMode { get; set; } = HellsMode.UnlockAsCollectible;

        public bool RegressiveDifficultyEnabled { get; set; } = false;
        public int StartingDifficulty { get; set; } = 0;

        public bool RequireNoTomorrowForSheol { get; set; } = false;
        public bool RequireCoatOfArmsForSheol { get; set; } = false;
        public int RequiredCoatOfArmsForSheol { get; set; } = 26;

        public bool RandomizedChallengesEnabled { get; set; } = true;
        public bool ChallengeMedaillonsEnabled { get; set; } = true;
        public ChallengeMode ChallengeUnlockMode { get; set; } = ChallengeMode.UnlockAsCollectible;
        public bool RequireStageForChallenges { get; set; } = false;
        public bool RequireWeaponsForChallenges { get; set; } = false;

        public WeaponMode WeaponUnlockMode { get; set; } = WeaponMode.WeaponAsOnePackage;

        public bool RandomizedOutfitsEnabled { get; set; } = false;
        public ItemOrigin RandomizedOutfitDLCs { get; set; } = Base;

        public bool RandomizedSongsEnabled { get; set; } = false;
        public ItemOrigin RandomizedSongDLCs { get; set; } = Base;

        public bool DestructibleLocationsEnabled { get; set; } = false;
        public bool DestructibleAsUnlocks { get; set; } = false;
        public DestructibleMode DestructibleLocationsMode { get; set; } = DestructibleMode.PerEntireArena;
        public bool LevelDestructibleLocationsEnabled { get; set; } = false;


        public bool RandomizedBoonsEnabled { get; set; } = false;
        public bool RandomizedDashEnabled { get; set; } = false;
        public bool RandomizedJumpEnabled { get; set; } = false;
        public bool RandomizedReloadEnabled { get; set; } = false;
        public bool RandomizedSlaughterEnabled { get; set; } = false;

        public bool IncludeRandomizedWeaponSkinsChecks { get; set; } = false;
        public bool IncludeSecretMultiplierChecks { get; set; } = false;
        public bool IncludeCoatOfArmsChecks { get; set; } = false;
        public bool IncludeSectionWeaponCheck { get; set; } = false;
        public bool IncludeSectionOutfitCheck { get; set; } = false;
        public bool IncludeSectionSongCheck { get; set; } = false;
        public bool IncludeMiscellaneousChecks { get; set; } = false;
        public bool IncludeProgressiveAnguishGateSkips { get; set; } = false;

        public bool RandomizedLevelsEnabled { get; set; } = false;

        public Settings(Dictionary<string, object> slotData)
        {
            var options = ((JToken)slotData["options"]).ToObject<Dictionary<string, object>>();
            foreach (var kvp in options)
            {
                Logger.LogInfo($"SlotData entry - Key: {kvp.Key}, Value: {kvp.Value}");

                switch (kvp.Key)
                {
                    case "win_condition":
                        switch (GetInt(kvp.Value))
                        {
                            case 0:
                                RequireSheolCompletion = true;
                                break;
                            case 1:
                                RequireHellsCompletion = true;
                                break;
                            default:
                                break;
                        }
                        break;
                    case "required_hells_completion":
                        RequiredHellsCompletion = GetInt(kvp.Value);
                        break;
                    case "randomized_hells_enabled":
                        RandomizedHellsEnabled = GetBool(kvp.Value);
                        break;
                    case "require_aspect_for_boss_arena":
                        RequireAspectForBossArena = GetBool(kvp.Value);
                        break;
                    case "starting_hells":
                        StartingHells = GetInt(kvp.Value);
                        break;
                    case "regressive_difficulty":
                        RegressiveDifficultyEnabled = GetBool(kvp.Value);
                        break;
                    case "starting_difficulty":
                        StartingDifficulty = GetInt(kvp.Value);
                        break;
                    case "require_no_tomorrow_for_sheol":
                        RequireNoTomorrowForSheol = GetBool(kvp.Value);
                        break;
                    case "require_coat_of_arms_for_sheol":
                        RequireCoatOfArmsForSheol = GetBool(kvp.Value);
                        break;
                    case "required_coat_of_arms_for_sheol":
                        RequiredCoatOfArmsForSheol = GetInt(kvp.Value);
                        break;

                    case "randomized_torments_enabled":
                        RandomizedChallengesEnabled = GetBool(kvp.Value);
                        break;
                    case "torment_medaillons_enabled":
                        ChallengeMedaillonsEnabled = GetBool(kvp.Value);
                        break;
                    case "torment_unlocks_as_progressive":
                        ChallengeUnlockMode = GetBool(kvp.Value)
                            ? ChallengeMode.Progressive
                            : ChallengeMode.UnlockAsCollectible;
                        break;
                    case "require_stage_for_torments":
                        RequireStageForChallenges = GetBool(kvp.Value);
                        break;
                    case "require_weapons_for_torments":
                        RequireWeaponsForChallenges = GetBool(kvp.Value);
                        break;

                    case "randomized_weapon_ultimates_enabled":
                        WeaponUnlockMode = GetBool(kvp.Value)
                            ? WeaponMode.IndividualAbilities
                            : WeaponMode.WeaponAsOnePackage;
                        break;

                    case "randomized_boons_enabled":
                        RandomizedBoonsEnabled = GetBool(kvp.Value);
                        break;
                    case "randomized_dash_enabled":
                        RandomizedDashEnabled = GetBool(kvp.Value);
                        break;
                    case "randomized_jump_enabled":
                        RandomizedJumpEnabled = GetBool(kvp.Value);
                        break;
                    case "randomized_reload_enabled":
                        RandomizedReloadEnabled = GetBool(kvp.Value);
                        break;
                    case "randomized_slaughter_enabled":
                        RandomizedSlaughterEnabled = GetBool(kvp.Value);
                        break;
                    case "destructible_as_unlocks":
                        DestructibleAsUnlocks = GetBool(kvp.Value);
                        break;

                    case "randomized_outfits_enabled":
                        RandomizedOutfitsEnabled = GetBool(kvp.Value);
                        break;
                    case "include_dream_of_the_beast_outfits":
                        if(GetBool(kvp.Value))
                            RandomizedOutfitDLCs |= ItemOrigin.DreamOfTheBeast;
                        break;
                    case "include_purgatory_outfits":
                        if(GetBool(kvp.Value))
                            RandomizedOutfitDLCs |= ItemOrigin.Purgatory;
                        break;

                    case "randomized_songs_enabled":
                        RandomizedSongsEnabled = GetBool(kvp.Value);
                        break;
                    case "include_dusk_soundtrack_songs":
                        if(GetBool(kvp.Value))
                            RandomizedSongDLCs |= ItemOrigin.Dusk;
                        break;
                    case "include_dream_of_the_beast_songs":
                        if(GetBool(kvp.Value))
                            RandomizedSongDLCs |= ItemOrigin.DreamOfTheBeast;
                        break;
                    case "include_purgatory_songs":
                        if(GetBool(kvp.Value))
                            RandomizedSongDLCs |= ItemOrigin.Purgatory;
                        break;
                    case "include_essential_hits_soundtrack_songs":
                        if(GetBool(kvp.Value))
                            RandomizedSongDLCs |= ItemOrigin.EssentialHits;
                        break;

                    case "destructible_locations_enabled":
                        DestructibleLocationsEnabled = GetBool(kvp.Value);
                        break;
                    case "singular_destructible_locations_enabled":
                        DestructibleLocationsMode = GetBool(kvp.Value)
                            ? DestructibleMode.PerDestructibleType
                            : DestructibleLocationsMode = DestructibleMode.PerEntireArena;
                        break;
                    case "hells_destructible_locations_enabled":
                        LevelDestructibleLocationsEnabled = GetBool(kvp.Value);
                        break;

                    case "include_section_clears_with_weapons_checks":
                        IncludeSectionWeaponCheck = GetBool(kvp.Value);
                        break;
                    case "include_section_clears_with_outfits_checks":
                        IncludeSectionOutfitCheck = GetBool(kvp.Value);
                        break;
                    case "include_section_clears_with_songs_checks":
                        IncludeSectionSongCheck = GetBool(kvp.Value);
                        break;
                    case "include_randomized_weapon_skins_checks":
                        IncludeRandomizedWeaponSkinsChecks = GetBool(kvp.Value);
                        break;
                    case "include_secret_multiplier_checks":
                        IncludeSecretMultiplierChecks = GetBool(kvp.Value);
                        break;
                    case "include_coat_of_arms_checks":
                        IncludeCoatOfArmsChecks = GetBool(kvp.Value);
                        break;
                    case "include_miscellaneous_checks":
                        IncludeMiscellaneousChecks = GetBool(kvp.Value);
                        break;
                    case "include_progressive_anguish_gate_skips":
                        IncludeProgressiveAnguishGateSkips = GetInt(kvp.Value) > 0;
                        break;

                    default:
                        Logger.LogInfo($"Unmapped SlotData key encountered: {kvp.Key}");
                        break;
                }
            }
        }

        private static bool GetBool(object val)
        {
            if (val == null) return false;
            if (val is bool b) return b;
            return Convert.ToInt64(val) != 0;
        }

        private static int GetInt(object val)
        {
            if (val == null) return 0;
            return Convert.ToInt32(val);
        }

        public Settings() { }
    }
}
