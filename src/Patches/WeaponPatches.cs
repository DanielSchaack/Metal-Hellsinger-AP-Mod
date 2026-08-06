using System.Collections.Generic;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using static Randomizer.Lookup;

namespace Randomizer
{
    [HarmonyPatch(typeof(WeaponAbilityBase))]
    public class WeaponAbilityBasePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityBase.CanReload))]
        static bool CanReloadPrefix(WeaponAbilityBase __instance)
        {
            Logger.LogDebug($"WeaponAbilityBase CanReload Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityBase.CanReload))]
        static void CanReloadPostfix(WeaponAbilityBase __instance, ref bool __result)
        {
            __result =
                __result
                && (
                    (__instance.HasAmmo() && Randomizer.ItemTracker.CanManualReload())
                    || !__instance.HasAmmo()
                );
            Logger.LogInfo(
                $"WeaponAbilityBase CanReload Postfix called and is available: {__result} "
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityBase.TriggerUltimate))]
        static bool TriggerUltimatePrefix(WeaponAbilityBase __instance)
        {
            Logger.LogInfo($"WeaponAbilityBase TriggerUltimate Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityBase.TriggerUltimate))]
        static void TriggerUltimatePostfix(WeaponAbilityBase __instance)
        {
            Logger.LogDebug($"WeaponAbilityBase TriggerUltimate Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityBase.GetWeaponType))]
        static bool GetWeaponTypePrefix(WeaponAbilityBase __instance)
        {
            // Logger.LogDebug($"WeaponAbilityBase GetWeaponType Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityBase.GetWeaponType))]
        static void GetWeaponTypePostfix(
            WeaponAbilityBase __instance,
            ref PlayerWeaponType __result
        )
        {
            // Logger.LogDebug(
            //     $"WeaponAbilityBase GetWeaponType Postfix called, returning {__result}"
            // );
        }
    }

    [HarmonyPatch(typeof(RangedWeaponReloadMovementState))]
    public class RangedWeaponReloadMovementStatePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(RangedWeaponReloadMovementState.DoReload))]
        static bool DoReloadPrefix(ref RangedWeaponReloadMovementState __instance)
        {
            __instance.HasTriedBeatReload = !Randomizer.ItemTracker.CanQuickReload();
            Logger.LogInfo(
                $"RangedWeaponReloadMovementState DoReload Prefix called, can perform quick reload: {!__instance.HasTriedBeatReload}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(RangedWeaponReloadMovementState.DoReload))]
        static void DoReloadPostfix(RangedWeaponReloadMovementState __instance)
        {
            Logger.LogDebug($"RangedWeaponReloadMovementState DoReload Postfix called");
        }
    }

    [HarmonyPatch(typeof(WeaponAbilityController))]
    public class WeaponAbilityControllerPatches
    {
        public static WeaponAbilityController Instance;

        private static bool weaponInvisibleTrapActive = false;
        public static void ToggleWeaponInvisibility(bool turnInvisible)
        {
            weaponInvisibleTrapActive = turnInvisible;
        }

        private static bool weaponTrickeryTrapActive = false;
        public static void ToggleWeaponTrickery(bool turnWeaponTrickeryOn)
        {
            weaponTrickeryTrapActive = turnWeaponTrickeryOn;
        }

        public static void TriggerUltimate()
        {
            Instance.GetActiveWeaponAbility().UpdateUltimate(1, false);
            InputReaderPatches.TriggerUltimate();
            IngameMessagesPatches.DisplayItemActivated($"Trigger Ultimate");
        }

        internal static void GiveWeapon(PlayerWeaponType type, string weaponName)
        {
            if (
                !Instance.m_carriedWeapons.Contains(type)
                && Instance.m_favoriteWeapon2 == PlayerWeaponType.None
            )
            {
                Instance.PickUpWeapon(type, true, true, false);
                IngameMessagesPatches.DisplayItemActivated($"Death");
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.ActivateWeaponAbility))]
        static bool ActivateWeaponAbilityPrefix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController ActivateWeaponAbility Prefix called for {weapon}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.ActivateWeaponAbility))]
        static void ActivateWeaponAbilityPostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController ActivateWeaponAbility Postfix called for {weapon}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.SwitchToWeapon))]
        static bool SwitchToWeaponPrefix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon,
            bool fastDeploy,
            ref bool explicitlyTriggered,
            bool ignoreUiTimeScale
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController SwitchToWeapon Prefix called, weapon trickery is active: {IsWeaponTrickeryActive()}"
            );
            if (explicitlyTriggered && IsWeaponTrickeryActive())
            {
                Logger.LogInfo(
                    $"WeaponAbilityController SwitchToWeapon called while WeaponTrickery is active, won't switch"
                );
                return false;
            }
            return true;
        }

        private static bool IsWeaponTrickeryActive()
        {
            Logger.LogDebug(
                $"WeaponAbilityController weapon trickery trap active: {Randomizer.IngameDispenser.WeaponTrickeryTrapActive}, config active: {Randomizer.Configuration.gameplayWeaponTrickeryModeActive.Value}"
            );
            return weaponTrickeryTrapActive
                || Randomizer.Configuration.gameplayWeaponTrickeryModeActive.Value;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.SwitchToWeapon))]
        static void SwitchToWeaponPostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon,
            bool fastDeploy,
            bool explicitlyTriggered,
            bool ignoreUiTimeScale
        )
        {
            Logger.LogDebug($"WeaponAbilityController SwitchToWeapon Postfix called for {weapon}");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.AddWeaponAbility))]
        static bool AddWeaponAbilityPrefix(
            WeaponAbilityController __instance,
            PlayerWeaponType weaponType
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController AddWeaponAbility Prefix called for {weaponType}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.AddWeaponAbility))]
        static void AddWeaponAbilityPostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType weaponType
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController AddWeaponAbility Postfix called for {weaponType}"
            );
        }

        // WARN: Triggered on every shot
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(WeaponAbilityController.CanWeaponPerform))]
        // static bool CanWeaponPerformPrefix(
        //     WeaponAbilityController __instance,
        //     PlayerWeaponType weapon
        // )
        // {
        //     Logger.LogInfo($"WeaponAbilityController CanWeaponPerform Prefix called for {weapon}");
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(WeaponAbilityController.CanWeaponPerform))]
        // static void CanWeaponPerformPostfix(
        //     WeaponAbilityController __instance,
        //     PlayerWeaponType weapon,
        //     bool __result
        // )
        // {
        //     Logger.LogInfo($"WeaponAbilityController CanWeaponPerform Postfix called for {weapon}");
        // }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.CarriesWeapon))]
        static bool CarriesWeaponPrefix(WeaponAbilityController __instance, PlayerWeaponType weapon)
        {
            Logger.LogDebug($"WeaponAbilityController CarriesWeapon Prefix called for {weapon}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.CarriesWeapon))]
        static void CarriesWeaponPostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon,
            bool __result
        )
        {
            Logger.LogInfo(
                $"WeaponAbilityController CarriesWeapon Postfix called for {weapon}: {__result}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChangeLoadout))]
        static bool ChangeLoadoutPrefix(
            WeaponAbilityController __instance,
            Il2CppStructArray<PlayerWeaponType> loadout
        )
        {
            Logger.LogInfo($"WeaponAbilityController ChangeLoadout Prefix called");
            for (int i = 0; i < loadout.Length; i++)
                Logger.LogInfo($"Changing loadout to and including {loadout[i]}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChangeLoadout))]
        static void ChangeLoadoutPostfix(
            WeaponAbilityController __instance,
            Il2CppStructArray<PlayerWeaponType> loadout
        )
        {
            Logger.LogDebug($"WeaponAbilityController ChangeLoadout Postfix called ");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChangeWeaponDataConfiguration))]
        static bool ChangeWeaponDataConfigurationPrefix(
            WeaponAbilityController __instance,
            WeaponDataConfiguration config
        )
        {
            Logger.LogDebug($"WeaponAbilityController ChangeWeaponDataConfiguration Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChangeWeaponDataConfiguration))]
        static void ChangeWeaponDataConfigurationPostfix(
            WeaponAbilityController __instance,
            WeaponDataConfiguration config
        )
        {
            Logger.LogInfo($"WeaponAbilityController ChangeWeaponDataConfiguration Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChargeUltimate))]
        static bool ChargeUltimatePrefix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon
        )
        {
            Logger.LogDebug($"WeaponAbilityController ChargeUltimate Prefix called for {weapon}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.ChargeUltimate))]
        static void ChargeUltimatePostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType weapon
        )
        {
            Logger.LogDebug($"WeaponAbilityController ChargeUltimate Postfix called for {weapon}");
        }

        // WARN: Called every frame
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponAbility))]
        // static bool GetActiveWeaponAbilityPrefix(
        //     WeaponAbilityController __instance
        // )
        // {
        //     Logger.LogInfo(
        //         $"WeaponAbilityController GetActiveWeaponAbility Prefix called"
        //     );
        //     return true;
        // }
        //
        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponAbility))]
        static void GetActiveWeaponAbilityPostfix(
            WeaponAbilityController __instance,
            WeaponAbilityBase __result
        )
        {
            // Logger.LogInfo(
            //     $"WeaponAbilityController GetActiveWeaponAbility Postfix called for {__result.m_weaponConfig.WeaponType}"
            // );
            bool invisibleWeaponActive =
                weaponInvisibleTrapActive
                || Randomizer.Configuration.gameplayInvisibleWeaponsActive.Value;
            if (__result.Weapon != null && invisibleWeaponActive)
                __result.Weapon.SetVisible(false);
            else if (__result.Weapon != null && !invisibleWeaponActive)
                __result.Weapon.SetVisible(true);
        }

        // WARN: Called on every hit
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponConfiguration))]
        // static bool GetActiveWeaponConfigurationPrefix(
        //     WeaponAbilityController __instance
        // )
        // {
        //     Logger.LogInfo(
        //         $"WeaponAbilityController GetActiveWeaponConfiguration Prefix called"
        //     );
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponConfiguration))]
        // static void GetActiveWeaponConfigurationPostfix(
        //     WeaponAbilityController __instance,
        //     ref WeaponDataConfiguration __result
        // )
        // {
        //     Logger.LogInfo(
        //         $"WeaponAbilityController GetActiveWeaponConfiguration Postfix called for {__result.WeaponType}"
        //     );
        // }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponType))]
        static bool GetActiveWeaponTypePrefix(WeaponAbilityController __instance)
        {
            Logger.LogDebug($"WeaponAbilityController GetActiveWeaponType Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.GetActiveWeaponType))]
        static void GetActiveWeaponTypePostfix(
            WeaponAbilityController __instance,
            PlayerWeaponType __result
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController GetActiveWeaponType Postfix called for {__result}"
            );
        }

        // WARN: Analogue to active methods, called on every hit
        // [HarmonyPrefix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetWeaponAbility))]
        // static bool GetWeaponAbilityPrefix(
        //     WeaponAbilityController __instance,
        //     PlayerWeaponType type
        // )
        // {
        //     Logger.LogInfo($"WeaponAbilityController GetWeaponAbility Prefix called for {type}");
        //     return true;
        // }
        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetWeaponAbility))]
        // static void GetWeaponAbilityPostfix(
        //     WeaponAbilityController __instance,
        //     PlayerWeaponType type,
        //     WeaponAbilityBase __result
        // )
        // {
        //     Logger.LogInfo($"WeaponAbilityController GetWeaponAbility Postfix called for {type}");
        // }
        //
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.GetWeaponConfig))]
        static bool GetWeaponConfigPrefix(
            WeaponAbilityController __instance,
            ref PlayerWeaponType weaponType
        )
        {
            Logger.LogDebug(
                $"WeaponAbilityController GetWeaponConfig Prefix called for {weaponType}"
            );
            return true;
        }

        //
        // [HarmonyPostfix]
        // [HarmonyPatch(nameof(WeaponAbilityController.GetWeaponConfig))]
        // static void GetWeaponConfigPostfix(
        //     WeaponAbilityController __instance,
        //     PlayerWeaponType weaponType,
        //     WeaponDataConfiguration __result
        // )
        // {
        //     Logger.LogInfo(
        //         $"WeaponAbilityController GetWeaponConfig Postfix called for {weaponType}"
        //     );
        // }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.PickUpLoadOut))]
        static bool PickUpLoadOutPrefix(
            ref WeaponAbilityController __instance,
            Il2CppStructArray<PlayerWeaponType> loadout
        )
        {
            Instance = __instance;
            Logger.LogInfo($"WeaponAbilityController PickUpLoadOut Prefix called");
            for (int i = 0; i < loadout.Length; i++)
                Logger.LogInfo($"Changing loadout to and including {loadout[i]}");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.PickUpLoadOut))]
        static void PickUpLoadOutPostfix(
            WeaponAbilityController __instance,
            Il2CppStructArray<PlayerWeaponType> loadout
        )
        {
            Logger.LogDebug($"WeaponAbilityController PickUpLoadOut Postfix called ");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeaponAbilityController.PickUpWeapon))]
        static bool PickUpWeaponPrefix(
            ref WeaponAbilityController __instance,
            PlayerWeaponType weapon,
            ref bool isPickup,
            ref bool chargePickedUpWeaponUltimate,
            ref bool showHUDMessage,
            ref bool ultimateUnlocked
        )
        {
            Logger.LogInfo(
                $"WeaponAbilityController PickUpWeapon Prefix for {weapon} called and charges Ultimate: {chargePickedUpWeaponUltimate}, is Pickup: {isPickup}, show HUD Message: {showHUDMessage}"
            );
            if (Randomizer.CurrentGameState != GameStateController.GameStateName.InGame)
                return false;

            ultimateUnlocked = Randomizer.ItemTracker.CanWeaponUltimate(weapon);
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeaponAbilityController.PickUpWeapon))]
        static void PickUpWeaponPostfix(
            ref WeaponAbilityController __instance,
            PlayerWeaponType weapon,
            bool isPickup,
            bool chargePickedUpWeaponUltimate,
            bool showHUDMessage,
            bool ultimateUnlocked
        )
        {
            Logger.LogInfo(
                $"WeaponAbilityController PickUpWeapon Postfix for {weapon} called and charges Ultimate: {chargePickedUpWeaponUltimate}, is Pickup: {isPickup}, show HUD Message: {showHUDMessage}"
            );
            if (
                Randomizer.CurrentGameMode == EGameMode.Stage
                || Randomizer.CurrentGameMode == EGameMode.Tutorial
            )
            {
                var baseConfig = weapon switch
                {
                    PlayerWeaponType.AssaultRifle => WeaponDataConfigurationCache.Get(
                        Lookup.WeaponNameToConfig["The Red Right Hand"]
                    ),
                    PlayerWeaponType.Bow => WeaponDataConfigurationCache.Get(
                        Lookup.WeaponNameToConfig["Telos"]
                    ),
                    PlayerWeaponType.RhythmWeapon => WeaponDataConfigurationCache.Get(
                        Lookup.WeaponNameToConfig["Paz"]
                    ),
                    PlayerWeaponType.Falx => WeaponDataConfigurationCache.Get(
                        Lookup.WeaponNameToConfig["Terminus"]
                    ),
                    PlayerWeaponType.Shotgun => getPersephoneConfig(),
                    PlayerWeaponType.Pistols => getHoundsConfig(),
                    PlayerWeaponType.Vulcan => getVulcanConfig(),
                    PlayerWeaponType.Boomerang => WeaponDataConfigurationCache.Get(
                        Lookup.WeaponNameToConfig["Hellcrow"]
                    ),
                    _ => null,
                };

                __instance.ChangeWeaponDataConfiguration(baseConfig);
            }
        }

        private static WeaponDataConfiguration getPersephoneConfig()
        {
            var wantedConfig = Randomizer.Configuration.weaponPersephoneType.Value;
            if (Randomizer.Configuration.weaponRandomizePersephoneType.Value)
            {
                List<ExtendedWeaponType> availablePersephoneTypes =
                    Randomizer.ItemTracker.GetAvailablePersephoneTypes();
                Logger.LogDebug(
                    $"Available Persephone types: {string.Join(",", availablePersephoneTypes)}"
                );

                List<ExtendedWeaponType> uncheckedTypes =
                    Randomizer.LocationTracker.GetUncheckedPersephoneLocations(
                        availablePersephoneTypes
                    );
                Logger.LogDebug($"Unchecked Persephone types: {string.Join(",", uncheckedTypes)}");

                if (uncheckedTypes.Count > 0)
                    availablePersephoneTypes = uncheckedTypes;

                int randomIndex = UnityEngine.Random.Range(0, availablePersephoneTypes.Count);
                ExtendedWeaponType extendedWeaponType = availablePersephoneTypes[randomIndex];
                Randomizer.CurrentPersephoneConfig = extendedWeaponType;
                Logger.LogInfo($"Returning randomized config {extendedWeaponType} for Persephone");
                return WeaponDataConfigurationCache.Get(
                    Lookup.WeaponNameToConfig[Lookup.PersephoneTypeToName[extendedWeaponType]]
                );
            }

            Randomizer.CurrentPersephoneConfig = wantedConfig;
            Logger.LogInfo($"Returning wanted config {wantedConfig} for Persephone");
            string weaponName = Lookup.PersephoneTypeToName[wantedConfig];
            Logger.LogInfo($"Returning wanted weapon name {weaponName} for Persephone");
            string configName = Lookup.WeaponNameToConfig[weaponName];
            Logger.LogInfo($"Returning wanted weapon config {configName} for Persephone");
            return WeaponDataConfigurationCache.Get(configName);
        }

        private static WeaponDataConfiguration getHoundsConfig()
        {
            Logger.LogDebug($"Getting weapon config for the Hounds");
            var wantedConfig = Randomizer.Configuration.weaponHoundsType.Value;
            if (Randomizer.Configuration.weaponRandomizeHoundsType.Value)
            {
                List<WeaponType> availableHoundsTypes =
                    Randomizer.ItemTracker.GetAvailableHoundsTypes();
                Logger.LogDebug(
                    $"Available Hounds types: {string.Join(",", availableHoundsTypes)}"
                );

                List<WeaponType> uncheckedTypes =
                    Randomizer.LocationTracker.GetUncheckedHoundsLocations(availableHoundsTypes);
                Logger.LogDebug($"Unchecked Hounds types: {string.Join(",", uncheckedTypes)}");
                if (uncheckedTypes.Count > 0)
                    availableHoundsTypes = uncheckedTypes;
                int randomIndex = UnityEngine.Random.Range(0, availableHoundsTypes.Count);
                WeaponType weaponType = availableHoundsTypes[randomIndex];
                Randomizer.CurrentHoundsConfig = weaponType;
                Logger.LogInfo($"Returning randomized config {weaponType} for the Hounds");
                return WeaponDataConfigurationCache.Get(
                    Lookup.WeaponNameToConfig[Lookup.HoundsTypeToName[weaponType]]
                );
            }

            Randomizer.CurrentHoundsConfig = wantedConfig;
            Logger.LogInfo($"Returning wanted config {wantedConfig} for the Hounds");
            string weaponName = Lookup.HoundsTypeToName[wantedConfig];
            Logger.LogInfo($"Returning wanted weapon name {weaponName} for Hounds");
            string configName = Lookup.WeaponNameToConfig[weaponName];
            Logger.LogInfo($"Returning wanted weapon config {configName} for Hounds");
            return WeaponDataConfigurationCache.Get(configName);
        }

        private static WeaponDataConfiguration getVulcanConfig()
        {
            var wantedConfig = Randomizer.Configuration.weaponVulcanType.Value;
            if (Randomizer.Configuration.weaponRandomizeVulcanType.Value)
            {
                List<WeaponType> availableVulcanTypes =
                    Randomizer.ItemTracker.GetAvailableVulcanTypes();
                Logger.LogDebug(
                    $"Available Vulcan types: {string.Join(",", availableVulcanTypes)}"
                );

                List<WeaponType> uncheckedTypes =
                    Randomizer.LocationTracker.GetUncheckedVulcanLocations(availableVulcanTypes);
                Logger.LogDebug($"Unchecked Vulcan types: {string.Join(",", uncheckedTypes)}");

                if (uncheckedTypes.Count > 0)
                    availableVulcanTypes = uncheckedTypes;
                int randomIndex = UnityEngine.Random.Range(0, availableVulcanTypes.Count);
                WeaponType weaponType = availableVulcanTypes[randomIndex];
                Randomizer.CurrentVulcanConfig = weaponType;
                Logger.LogInfo($"Returning randomized config {weaponType} for Vulcan");
                return WeaponDataConfigurationCache.Get(
                    Lookup.WeaponNameToConfig[Lookup.VulcanTypeToName[weaponType]]
                );
            }

            Randomizer.CurrentVulcanConfig = wantedConfig;
            Logger.LogInfo($"Returning wanted config {wantedConfig} for Vulcan");
            string weaponName = Lookup.VulcanTypeToName[wantedConfig];
            Logger.LogInfo($"Returning wanted weapon name {weaponName} for Vulcan");
            string configName = Lookup.WeaponNameToConfig[weaponName];
            Logger.LogInfo($"Returning wanted weapon config {configName} for Vulcan");
            return WeaponDataConfigurationCache.Get(configName);
        }
    }

    [HarmonyPatch(typeof(PlayerWeapon))]
    public class PlayerWeaponPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PlayerWeapon.SetUltimateUnlocked))]
        static bool SetUltimateUnlockedPrefix(ref PlayerWeapon __instance, ref bool unlock)
        {
            Logger.LogInfo(
                $"PlayerWeapon SetUltimateUnlocked Prefix called, attack {__instance.AttackID} is active: {unlock}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(PlayerWeapon.SetUltimateUnlocked))]
        static void SetUltimateUnlockedPostfix(PlayerWeapon __instance)
        {
            Logger.LogDebug($"PlayerWeapon SetUltimateUnlocked Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(PlayerWeapon.SetVisible))]
        static bool SetVisiblePrefix(ref PlayerWeapon __instance, ref bool isVisible)
        {
            // Logger.LogInfo(
            //     $"PlayerWeapon SetVisible Prefix called, attack {__instance.AttackID} is visible: {isVisible}"
            // );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(PlayerWeapon.SetVisible))]
        static void SetVisiblePostfix(PlayerWeapon __instance)
        {
            // Logger.LogDebug($"PlayerWeapon SetVisible Postfix called");
        }
    }

    [HarmonyPatch(typeof(FirstPersonController))]
    public class FirstPersonControllerPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(FirstPersonController.SetAttackIDForWeapon))]
        static bool SetAttackIDForWeaponPrefix(
            ref FirstPersonController __instance,
            PlayerWeaponType weaponType,
            AttackID attackID
        )
        {
            Logger.LogInfo(
                $"FirstPersonController SetAttackIDForWeapon Prefix called, weapon {weaponType} with attack {attackID}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(FirstPersonController.SetAttackIDForWeapon))]
        static void SetAttackIDForWeaponPostfix(FirstPersonController __instance)
        {
            Logger.LogDebug($"FirstPersonController SetAttackIDForWeapon Postfix called");
        }
    }

    [HarmonyPatch(typeof(InputReader))]
    public class InputReaderPatches
    {
        public static InputReader Instance;

        public static void TriggerUltimate()
        {
            Instance.TriggeredUltimate = true;
            Instance.ConsumeAttackInput();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputReader.Update))]
        static bool UpdatePrefix(
            ref InputReader __instance
        )
        {
            if(Instance == null)
                Instance = __instance;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(InputReader.Update))]
        static void UpdatePostfix(InputReader __instance)
        {
        }
    }
}
