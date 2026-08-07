using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using static OutsidersButton;
using static Randomizer.Locations;

namespace Randomizer
{
    [HarmonyPatch(typeof(TitleState))]
    public class TitleStatePatches
    {
        public static TitleState Instance;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.CreateTitleScreen))]
        static bool CreateTitleScreenPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState CreateTitleScreen Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.CreateTitleScreen))]
        static void CreateTitleScreenPostfix(ref TitleState __instance)
        {
            Logger.LogInfo($"TitleState CreateTitleScreen Postfix called");
            if (Instance == null)
                Instance = __instance;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OpenTitleScreen))]
        static bool OpenTitleScreenPrefix(TitleState __instance, bool skipLogo)
        {
            Logger.LogInfo($"TitleState OpenTitleScreen Prefix called and skips logo: {skipLogo}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OpenTitleScreen))]
        static void OpenTitleScreenPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenTitleScreen Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OpenMainMenu))]
        static bool OpenMainMenuPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenMainMenu Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OpenMainMenu))]
        static void OpenMainMenuPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OpenMainMenu Postfix called");
            if (__instance.m_endlessLobbyController != null)
                Logger.LogInfo($"TitleState Has endless lobby controller");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.OnMenuOptionSelected))]
        static bool OnMenuOptionSelectedPrefix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OnMenuOptionSelected Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.OnMenuOptionSelected))]
        static void OnMenuOptionSelectedPostfix(TitleState __instance)
        {
            Logger.LogInfo($"TitleState OnMenuOptionSelected Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleState.GetMenuItems))]
        static bool GetMenuItemsPrefix(TitleState __instance)
        {
            // Logger.LogInfo($"TitleState GetMenuItems Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleState.GetMenuItems))]
        static void GetMenuItemsPostfix(TitleState __instance)
        {
            // Logger.LogInfo($"TitleState GetMenuItems Postfix called");
        }
    }

    [HarmonyPatch(typeof(CompanionController))]
    public class CompanionControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.CreateCompanion))]
        static bool CreateCompanionPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController CreateCompanion Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.CreateCompanion))]
        static void CreateCompanionPostfix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController CreateCompanion Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.Show))]
        static bool ShowPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController Show Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.Show))]
        static void ShowPostfix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController Show Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionController.IsAnyCompanionItemUnviewed))]
        static bool IsAnyCompanionItemUnviewedPrefix(CompanionController __instance)
        {
            Logger.LogInfo($"CompanionController IsAnyCompanionItemUnviewed Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionController.IsAnyCompanionItemUnviewed))]
        static void IsAnyCompanionItemUnviewedPostfix(
            CompanionController __instance,
            ref bool __result
        )
        {
            Logger.LogInfo($"CompanionController IsAnyCompanionItemUnviewed Postfix called");
            __result = Randomizer.LocationTracker.HasUncheckedCompanion();
        }
    }

    [HarmonyPatch(typeof(CompanionItemRow))]
    public class CompanionItemRowPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionItemRow.SetIsLocked))]
        static bool SetIsLockedPrefix(CompanionItemRow __instance, ref bool isLocked)
        {
            Logger.LogDebug($"CompanionItemRow SetIsLocked Prefix called for {__instance.m_label.text}");

            string difficulties = __instance.m_label.text switch
            {
                "LAMB" => "Lamb",
                "GOAT" => "Goat",
                "BEAST" => "Beast",
                "ARCHDEVIL" => "Archdevil",
                _ => "",
            };
            if(!string.IsNullOrEmpty(difficulties))
            {
                isLocked = !Randomizer.ItemTracker.Has(difficulties);
                return true;
            }

            var hells = __instance.m_label.text switch
            {
                "VOKE" => EZone.Voke,
                "STYGIA" => EZone.Stygia,
                "YHELM" => EZone.Yhelm,
                "INCAUSTIS" => EZone.Incaustis,
                "GEHENNA" => EZone.Gehenna,
                "NIHIL" => EZone.Nihil,
                "ACHERON" => EZone.Acheron,
                "SHEOL" => EZone.Sheol,
                _ => EZone.Global,
            };
            if(hells != EZone.Global)
            {
                isLocked = !LocationAccessibility.CanAccessRegion(hells);
                return true;
            }

            string leviathan = __instance.m_label.text switch
            {
                "THE LEVIATHAN" => "The Leviathan",
                "ALTAR OF ECHOES" => "Altar Of Echoes",
                "MEMORIES" => "Memories",
                "DREAMS" => "Dreams",
                "VOID ECHOES" => "Void Echoes",
                "LEVIATHAN WEAPONS" => "Leviathan Weapons",
                "WEAPON TYPES" => "Weapon Types",
                "AFFLICTIONS" => "Afflictions",
                "VOID TOUCHED" => "Void Touched",
                "ULTIMATE POTS" => "Ultimate Pots",
                "NIGHTMARE CRYSTAL" => "Nightmare Crystal",
                _ => "",
            };
            if(!string.IsNullOrEmpty(leviathan))
            {
                isLocked = !LocationAccessibility.CanAccessZone(EZone.Leviathan, EArena.Global);
                return true;
            }

            var torments = __instance.m_label.text switch
            {
                "KILLING WITH RHYTHM" => EZone.KillingWithRhythm,
                "WEAPON TRICKERY" => EZone.WeaponTrickery,
                "RELIC THIEF" => EZone.RelicThief,
                "GIANTSLAYER" => EZone.Giantslayer,
                "DEATH'S EDGE" => EZone.DeathsEdge,
                "ULTIMATE MASTERY" => EZone.UltimateMastery,
                "SLAUGHTER MASTERY" => EZone.SlaughterMastery,
                _ => EZone.Global,
            };
            if (torments != EZone.Global)
            {
                isLocked = !(
                    LocationAccessibility.CanAccessZone(torments, EArena.Torment1)
                    | LocationAccessibility.CanAccessZone(torments, EArena.Torment2)
                    | LocationAccessibility.CanAccessZone(torments, EArena.Torment3)
                );
                return true;
            }

            var weapons = __instance.m_label.text switch
            {
                "TERMINUS" => PlayerWeaponType.Falx,
                "PAZ" => PlayerWeaponType.RhythmWeapon,
                "PERSEPHONE" => PlayerWeaponType.Shotgun,
                "THE HOUNDS" => PlayerWeaponType.Pistols,
                "VULCAN" => PlayerWeaponType.Vulcan,
                "HELLCROW" => PlayerWeaponType.Boomerang,
                "THE RED RIGHT HAND" => PlayerWeaponType.AssaultRifle,
                "TELOS" => PlayerWeaponType.Bow,
                _ => PlayerWeaponType.None,
            };
            if(weapons != PlayerWeaponType.None)
            {
                isLocked = !Randomizer.ItemTracker.IsWeaponUnlocked(weapons);
                return true;
            }

            string sigils = __instance.m_label.text switch
            {
                "STREAK GUARDIAN" => "Progressive Streak Guardian",
                "GHOST ROUNDS" => "Progressive Ghost Rounds",
                "BOON MOMENTUM" => "Progressive Boon Momentum",
                "UNYIELDING FURY" => "Progressive Unyielding Fury",
                "LAST BREATH AEGIS" => "Progressive Last Breath Aegis",
                "ULTIMATE SOVEREIGNTY" => "Progressive Ultimate Sovereignty",
                "THE PERFECTIONIST" => "Progressive The Perfectionist",
                _ => "",
            };
            if(!string.IsNullOrEmpty(sigils))
            {
                isLocked = !Randomizer.ItemTracker.Has(sigils);
                return true;
            }

            string hitStreakBoons = __instance.m_label.text switch
            {
                "ENDURING FURY" => "Enduring Fury Unlock",
                "FASTER ULTIMATE GAIN" => "Faster Ultimate Gain Unlock",
                "DEADLIER DASH" => "Deadlier Dash Unlock",
                "EXPLOSIVE SLAUGHTERS" => "Explosive Slaughter Unlock",
                _ => "",
            };
            if(!string.IsNullOrEmpty(hitStreakBoons))
            {
                isLocked = !LocationAccessibility.CanReach(hitStreakBoons);
                return true;
            }

            string combos = __instance.m_label.text switch
            {
                "TRIPLE DASH" => "Triple Dash discovered",
                "DOUBLE SLAUGHTER" => "Double Slaughter discovered",
                "DEVIL'S FLIGHT" => "Devil's Flight discovered",
                "STYX RELOAD" => "Styx Reload discovered",
                "UNHOLY MESS" => "Unholy Mess discovered",
                "HELL'S HEARTBEAT" => "Hells's Heartbeat discovered",
                "SLAUGHTER AND KILL" => "Slaughter and Kill discovered",
                "DEATH FROM ABOVE" => "Death from Above discovered",
                "FIVE ENDINGS" => "Five Endings discovered",
                "SHATTER TWO" => "Shatter Two discovered",
                "CHAOS FLIGHT" => "Chaos Flight discovered",
                "LETHAL CYCLE" => "Lethal Cycle discovered",
                "KILL TRIO" => "Kill Trio discovered",
                "CHAOS AND SLAUGHTER" => "Chaos and Slaughter discovered",
                "BASILISK MODE" => "Basilisk Mode discovered",
                "DOUBLE HIT AND RUN" => "Double Hit and Run discovered",
                _ => "",
            };
            if(!string.IsNullOrEmpty(combos))
            {
                isLocked = !LocationAccessibility.CanReach(combos);
                return true;
            }


            isLocked = false;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionItemRow.SetIsLocked))]
        static void SetIsLockedPostfix(CompanionItemRow __instance, bool isLocked)
        {
            Logger.LogInfo($"CompanionItemRow SetIsLocked Postfix called for {__instance.m_label.text}, and is locked: {isLocked}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(CompanionItemRow.SetUnViewed))]
        static bool SetUnViewedPrefix(CompanionItemRow __instance, ref bool unViewed)
        {
            Logger.LogInfo($"CompanionItemRow SetUnViewed Prefix called for {__instance.m_label.text}");

            string difficulties = __instance.m_label.text switch
            {
                "LAMB" => "Lamb",
                "GOAT" => "Goat",
                "BEAST" => "Beast",
                "ARCHDEVIL" => "Archdevil",
                _ => "",
            };
            if(!string.IsNullOrEmpty(difficulties))
            {
                unViewed = false;
                return true;
            }

            var hells = __instance.m_label.text switch
            {
                "VOKE" => EZone.Voke,
                "STYGIA" => EZone.Stygia,
                "YHELM" => EZone.Yhelm,
                "INCAUSTIS" => EZone.Incaustis,
                "GEHENNA" => EZone.Gehenna,
                "NIHIL" => EZone.Nihil,
                "ACHERON" => EZone.Acheron,
                "SHEOL" => EZone.Sheol,
                _ => EZone.Global,
            };
            if(hells != EZone.Global)
            {
                unViewed = Randomizer.LocationTracker.IsRegionUnchecked(hells);
                return true;
            }

            string leviathan = __instance.m_label.text switch
            {
                "THE LEVIATHAN" => "The Leviathan",
                "ALTAR OF ECHOES" => "Altar Of Echoes",
                "MEMORIES" => "Memories",
                "DREAMS" => "Dreams",
                "VOID ECHOES" => "Void Echoes",
                "LEVIATHAN WEAPONS" => "Leviathan Weapons",
                "WEAPON TYPES" => "Weapon Types",
                "AFFLICTIONS" => "Afflictions",
                "VOID TOUCHED" => "Void Touched",
                "ULTIMATE POTS" => "Ultimate Pots",
                "NIGHTMARE CRYSTAL" => "Nightmare Crystal",
                _ => "",
            };
            if(!string.IsNullOrEmpty(leviathan))
            {
                unViewed = Randomizer.LocationTracker.IsRegionUnchecked(EZone.Leviathan);
                return true;
            }

            var torments = __instance.m_label.text switch
            {
                "KILLING WITH RHYTHM" => "CH_Amdusias",
                "WEAPON TRICKERY" => "CH_Marbas",
                "RELIC THIEF" => "CH_Halphas",
                "GIANTSLAYER" => "CH_Bune",
                "DEATH'S EDGE" => "CH_Morax",
                "ULTIMATE MASTERY" => "CH_Flauros",
                "SLAUGHTER MASTERY" => "CH_Glasya",
                _ => "",
            };
            if (!string.IsNullOrEmpty(torments))
            {
                unViewed = (
                    Randomizer.LocationTracker.HasChecksOpen($"{torments}1", false)
                    | Randomizer.LocationTracker.HasChecksOpen($"{torments}2", false)
                    | Randomizer.LocationTracker.HasChecksOpen($"{torments}3", false)
                );
                return true;
            }

            var weapons = __instance.m_label.text switch
            {
                "TERMINUS" => PlayerWeaponType.Falx,
                "PAZ" => PlayerWeaponType.RhythmWeapon,
                "PERSEPHONE" => PlayerWeaponType.Shotgun,
                "THE HOUNDS" => PlayerWeaponType.Pistols,
                "VULCAN" => PlayerWeaponType.Vulcan,
                "HELLCROW" => PlayerWeaponType.Boomerang,
                "THE RED RIGHT HAND" => PlayerWeaponType.AssaultRifle,
                "TELOS" => PlayerWeaponType.Bow,
                _ => PlayerWeaponType.None,
            };
            if(weapons != PlayerWeaponType.None)
            {
                unViewed = Randomizer.LocationTracker.IsWeaponUnchecked(weapons);
                return true;
            }

            string sigils = __instance.m_label.text switch
            {
                "STREAK GUARDIAN" => "Progressive Streak Guardian",
                "GHOST ROUNDS" => "Progressive Ghost Rounds",
                "BOON MOMENTUM" => "Progressive Boon Momentum",
                "UNYIELDING FURY" => "Progressive Unyielding Fury",
                "LAST BREATH AEGIS" => "Progressive Last Breath Aegis",
                "ULTIMATE SOVEREIGNTY" => "Progressive Ultimate Sovereignty",
                "THE PERFECTIONIST" => "Progressive The Perfectionist",
                _ => "",
            };
            if(!string.IsNullOrEmpty(sigils))
            {
                unViewed = !Randomizer.ItemTracker.Has(sigils);
                return true;
            }

            string hitStreakBoons = __instance.m_label.text switch
            {
                "ENDURING FURY" => "Enduring Fury Unlock",
                "FASTER ULTIMATE GAIN" => "Faster Ultimate Gain Unlock",
                "DEADLIER DASH" => "Deadlier Dash Unlock",
                "EXPLOSIVE SLAUGHTERS" => "Explosive Slaughter Unlock",
                _ => "",
            };
            if(!string.IsNullOrEmpty(hitStreakBoons))
            {
                unViewed =
                    LocationAccessibility.CanReach(hitStreakBoons)
                    && !Randomizer.LocationTracker.CheckedLocations.ContainsKey(hitStreakBoons);
                return true;
            }

            string combos = __instance.m_label.text switch
            {
                "TRIPLE DASH" => "Triple Dash discovered",
                "DOUBLE SLAUGHTER" => "Double Slaughter discovered",
                "DEVIL'S FLIGHT" => "Devil's Flight discovered",
                "STYX RELOAD" => "Styx Reload discovered",
                "UNHOLY MESS" => "Unholy Mess discovered",
                "HELL'S HEARTBEAT" => "Hells's Heartbeat discovered",
                "SLAUGHTER AND KILL" => "Slaughter and Kill discovered",
                "DEATH FROM ABOVE" => "Death from Above discovered",
                "FIVE ENDINGS" => "Five Endings discovered",
                "SHATTER TWO" => "Shatter Two discovered",
                "CHAOS FLIGHT" => "Chaos Flight discovered",
                "LETHAL CYCLE" => "Lethal Cycle discovered",
                "KILL TRIO" => "Kill Trio discovered",
                "CHAOS AND SLAUGHTER" => "Chaos and Slaughter discovered",
                "BASILISK MODE" => "Basilisk Mode discovered",
                "DOUBLE HIT AND RUN" => "Double Hit and Run discovered",
                _ => "",
            };
            if(!string.IsNullOrEmpty(combos))
            {

                unViewed = LocationAccessibility.CanReach(combos)
                    && !Randomizer.LocationTracker.CheckedLocations.ContainsKey(combos);
                return true;
            }

            unViewed = false;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(CompanionItemRow.SetUnViewed))]
        static void SetUnViewedPostfix(CompanionItemRow __instance, bool unViewed)
        {
            Logger.LogInfo($"CompanionItemRow SetIsLocked Postfix called for {__instance.m_label.text} and is unviewed: {unViewed}");
        }
    }

    [HarmonyPatch(typeof(BestiaryEnemyRow))]
    public class BestiaryEnemyRowPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BestiaryEnemyRow.SetIsLocked))]
        static bool SetIsLockedPrefix(BestiaryEnemyRow __instance, ref bool isLocked)
        {
            isLocked = !Randomizer.LocationTracker.IsBestiaryReachable(__instance.EnemyType);
            Logger.LogDebug($"BestiaryEnemyRow SetIsLocked Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BestiaryEnemyRow.SetIsLocked))]
        static void SetIsLockedPostfix(BestiaryEnemyRow __instance)
        {
            Logger.LogDebug($"BestiaryEnemyRow SetIsLocked Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(BestiaryEnemyRow.SetUnViewed))]
        static bool SetUnViewedPrefix(BestiaryEnemyRow __instance, ref bool unViewed)
        {
            unViewed = Randomizer.LocationTracker.IsBestiaryUnchecked(__instance.EnemyType);
            Logger.LogInfo(
                $"BestiaryEnemyRow SetUnViewed Prefix called and is unViewed: {unViewed}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BestiaryEnemyRow.SetUnViewed))]
        static void SetUnViewedPostfix(BestiaryEnemyRow __instance)
        {
            Logger.LogDebug($"BestiaryEnemyRow SetIsLocked Postfix called");
        }
    }

    [HarmonyPatch(typeof(PauseScreenController))]
    public class PauseScreenControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PauseScreenController.TrySetPaused))]
        static bool TrySetPausedPrefix(PauseScreenController __instance, bool paused)
        {
            Logger.LogInfo(
                $"PauseScreenController TrySetPaused Prefix called and is paused: {paused}"
            );

            if (paused)
                Randomizer.IsPaused = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(PauseScreenController.TrySetPaused))]
        static void TrySetPausedPostfix(PauseScreenController __instance)
        {
            Logger.LogInfo($"PauseScreenController TrySetPaused Postfix called");
            ArchipelagoConnectorGui.TryInjectPause();
        }
    }

    [HarmonyPatch(typeof(PauseScreenView))]
    public class PauseScreenViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PauseScreenView.OnClosed))]
        static bool OnClosedPrefix(PauseScreenView __instance)
        {
            Logger.LogInfo($"PauseScreenView OnClosed Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(PauseScreenView.OnClosed))]
        static void OnClosedPostfix(PauseScreenView __instance)
        {
            Logger.LogInfo($"PauseScreenView OnClosed Postfix called");
            Randomizer.IsPaused = false;
        }
    }

    [HarmonyPatch(typeof(TitleScreenView))]
    public class TitleScreenViewPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(TitleScreenView.PopulateMenu))]
        static bool PopulateMenuPrefix(TitleScreenView __instance)
        {
            Logger.LogInfo($"TitleScreenView PopulateMenu Prefix called");
            Randomizer.IsPaused = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TitleScreenView.PopulateMenu))]
        static void PopulateMenuPostfix(TitleScreenView __instance)
        {
            Logger.LogInfo($"TitleScreenView PopulateMenu Postfix called");
            AddTitleItem(__instance);
        }

        private static void AddTitleItem(TitleScreenView __instance)
        {
            TextMenuRowItem newRow = UnityEngine.Object.Instantiate(
                __instance.m_rowPrefab,
                __instance.m_verticalMenuLayoutGroup.transform,
                false
            );
            newRow.gameObject.name = "AP Connector";
            newRow.Setup("ARCHIPELAGO");
            newRow.SetViewedIconVisible(!Randomizer.Archipelago.connected);
            newRow.transform.SetAsFirstSibling();

            var btn = newRow.GetButton();
            btn.onClick.RemoveAllListeners();
            btn.add_onSelectionChanged(
                new System.Action<SelectionEvent, Il2CppSystem.Object>(
                    ArchipelagoConnectorGui.OnArchipelagoClick
                )
            );

            var oldButtons = __instance.m_menuButtons;
            int oldLength = oldButtons != null ? oldButtons.Length : 0;

            TextMenuRowItem[] newArray = new TextMenuRowItem[oldLength + 1];
            newArray[0] = newRow;
            for (int i = 0; i < oldLength; i++)
            {
                newArray[i + 1] = oldButtons[i];
            }

            __instance.m_menuButtons = new Il2CppReferenceArray<TextMenuRowItem>(newArray);
        }
    }

    [HarmonyPatch(typeof(TextMenuRowItem))]
    public class TextMenuRowItemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(TextMenuRowItem.Setup))]
        static bool SetupPrefix(TextMenuRowItem __instance)
        {
            Logger.LogInfo($"TextMenuRowItem Setup Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(TextMenuRowItem.Setup))]
        static void SetupPostfix(TextMenuRowItem __instance, string textToDisplay)
        {
            Logger.LogInfo($"TextMenuRowItem Setup Postfix called for {textToDisplay}");
            if (textToDisplay == "CODEX")
                __instance.SetViewedIconVisible(Randomizer.LocationTracker.HasUncheckedCodex());
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(TextMenuRowItem.SetViewedIconVisible))]
        static bool SetViewedIconVisiblePrefix(TextMenuRowItem __instance, ref bool visible)
        {
            if (
                __instance.m_button != null
                && __instance.m_button.TextComponent != null
                && __instance.m_button.TextComponent.text == "CODEX"
            )
                visible = Randomizer.LocationTracker.HasUncheckedCodex();
            Logger.LogInfo(
                $"TextMenuRowItem SetViewedIconVisible Prefix called and should be visible: {visible}"
            );
            return true;
        }
    }
}
