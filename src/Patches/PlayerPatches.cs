using System.Collections.Generic;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Outsiders.Messages;
using UnityEngine;

namespace Randomizer
{
    [HarmonyPatch(typeof(Player))]
    public class PlayerPatches
    {
        public static Player Instance;
        public static AudioGameplayController m_AudioGameplayController;

        public static void KillPlayer(string sender)
        {
            if (Instance == null)
                return;
            IngameMessagesPatches.DisplayItemActivated($"Death");
            Randomizer.LevelActiveTime = -5f;
            Instance.KillPlayer(AttackID.PlayerShieldUltimateBashAttack);
        }

        public static void ToggleAssistMode(bool isItemActive)
        {
            var message = new AssistModeChangedMessage(isItemActive);
            if(m_AudioGameplayController != null)
                m_AudioGameplayController.OnAssistModeChanged(ref message);
        }

        // Used in Hells
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.SetLoadout))]
        static bool SetLoadoutPrefix(
            ref Player __instance,
            ref Il2CppStructArray<PlayerWeaponType> weapons,
            PlayerWeaponType fav1,
            PlayerWeaponType fav2
        )
        {
            Logger.LogDebug($"Player SetLoadout Prefix called, fav1: {fav1}, fav2: {fav2}");
            foreach (PlayerWeaponType weaponType in weapons)
            {
                Logger.LogDebug($"Player tries to load with weapon {weaponType} in loadout");
            }

            //Only overwrite when loading into a hell
            if (
                Randomizer.CurrentGameMode == EGameMode.Stage
                || Randomizer.CurrentGameMode == EGameMode.Tutorial
            )
            {
                List<PlayerWeaponType> availableWeaponTypes =
                    Randomizer.ItemTracker.GetAvailableWeaponTypes();

                if (!availableWeaponTypes.Contains(fav1) && fav2 == PlayerWeaponType.None)
                {
                    if (Randomizer.Configuration.weaponExcludePazFromLoadout.Value)
                        availableWeaponTypes.Remove(PlayerWeaponType.RhythmWeapon);
                    if (Randomizer.Configuration.weaponExcludeTerminusFromLoadout.Value)
                        availableWeaponTypes.Remove(PlayerWeaponType.RhythmWeapon);

                    var randomWeapon = PlayerWeaponType.None;
                    var uncheckedWeapons = Randomizer.LocationTracker.GetUncheckedWeapons(
                        availableWeaponTypes
                    );
                    if (uncheckedWeapons.Count > 0)
                        randomWeapon = uncheckedWeapons[
                            UnityEngine.Random.Range(0, uncheckedWeapons.Count)
                        ];
                    else
                        randomWeapon = availableWeaponTypes[
                            UnityEngine.Random.Range(0, availableWeaponTypes.Count)
                        ];

                    Logger.LogInfo(
                        $"Primary weapon 1 ({fav1}) is unavailable. Resetting to {randomWeapon}."
                    );
                    fav1 = randomWeapon;
                }

                if (!availableWeaponTypes.Contains(fav2))
                {
                    Logger.LogInfo(
                        $"Secondary weapon 2 ({fav2}) is unavailable. Resetting to None."
                    );
                    fav2 = PlayerWeaponType.None;
                }

                if (
                    Randomizer.Configuration.weaponExcludePazFromLoadout.Value
                    && fav1 != PlayerWeaponType.RhythmWeapon
                    && fav2 != PlayerWeaponType.RhythmWeapon
                )
                    availableWeaponTypes.Remove(PlayerWeaponType.RhythmWeapon);

                if (
                    Randomizer.Configuration.weaponExcludeTerminusFromLoadout.Value
                    && fav1 != PlayerWeaponType.Falx
                    && fav2 != PlayerWeaponType.Falx
                )
                    availableWeaponTypes.Remove(PlayerWeaponType.Falx);

                if (Randomizer.Configuration.weaponLoadAllAvailableWeapons.Value)
                    weapons = ToIL2CPPArray(availableWeaponTypes);
                else
                    weapons = GetAvailableWeapons(availableWeaponTypes, fav1, fav2);

                Logger.LogInfo($"Overwrote level weapons with {weapons.Count} available types.");
            }

            foreach (PlayerWeaponType weaponType in weapons)
            {
                Logger.LogInfo($"Player loads with weapon {weaponType} in loadout");
            }
            Logger.LogInfo($"Player SetLoadout done, fav1: {fav1}, fav2: {fav2}");
            return true;
        }

        private static Il2CppStructArray<PlayerWeaponType> GetAvailableWeapons(
            List<PlayerWeaponType> availableWeaponTypes,
            PlayerWeaponType fav1,
            PlayerWeaponType fav2
        )
        {
            List<PlayerWeaponType> availableWeaponsList = new List<PlayerWeaponType>() { };
            if (fav1 != PlayerWeaponType.None)
                availableWeaponsList.Add(fav1);

            if (fav2 != PlayerWeaponType.None)
                availableWeaponsList.Add(fav2);

            if (
                !availableWeaponsList.Contains(PlayerWeaponType.RhythmWeapon)
                && availableWeaponTypes.Contains(PlayerWeaponType.RhythmWeapon)
            )
                availableWeaponsList.Add(PlayerWeaponType.RhythmWeapon);

            if (
                !availableWeaponsList.Contains(PlayerWeaponType.Falx)
                && availableWeaponTypes.Contains(PlayerWeaponType.Falx)
            )
                availableWeaponsList.Add(PlayerWeaponType.Falx);

            Il2CppStructArray<PlayerWeaponType> availableWeapons = ToIL2CPPArray(
                availableWeaponsList
            );

            return availableWeapons;
        }

        private static Il2CppStructArray<PlayerWeaponType> ToIL2CPPArray(
            List<PlayerWeaponType> availableWeaponsList
        )
        {
            Il2CppStructArray<PlayerWeaponType> availableWeapons =
                new Il2CppStructArray<PlayerWeaponType>(availableWeaponsList.Count);

            for (int i = 0; i < availableWeaponsList.Count; i++)
            {
                availableWeapons[i] = availableWeaponsList[i];
            }

            return availableWeapons;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.SetLoadout))]
        static void SetLoadoutPostfix(
            ref Player __instance,
            Il2CppStructArray<PlayerWeaponType> weapons,
            PlayerWeaponType fav1,
            PlayerWeaponType fav2
        )
        {
            Logger.LogInfo("Player SetLoadout Postfix called");
            Randomizer.CurrentPrimary = fav1;
            Randomizer.CurrentSecondary = fav2;
        }

        // Used in Leviathan
        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.PickUpWeapon))]
        static bool PickUpWeaponPrefix(
            ref Player __instance,
            PlayerWeaponType weapon,
            ref bool chargeUltimate,
            ref bool isPickup,
            ref bool showHUDMessage,
            ref bool ultimateUnlocked,
            ref bool switchToWeapon
        )
        {
            Logger.LogInfo(
                $"Player PickUpWeapon Prefix for {weapon} called and charges Ultimate: {chargeUltimate}, is Pickup: {isPickup}, show HUD Message: {showHUDMessage}, switch to weapon: {switchToWeapon}"
            );
            if (Randomizer.CurrentGameState != GameStateController.GameStateName.InGame)
                return false;

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.PickUpWeapon))]
        static void PickUpWeaponPostfix(
            ref Player __instance,
            PlayerWeaponType weapon,
            bool chargeUltimate,
            bool isPickup,
            bool showHUDMessage,
            bool ultimateUnlocked,
            bool switchToWeapon
        )
        {
            Logger.LogInfo(
                $"Player PickUpWeapon Postfix for {weapon} called and charges Ultimate: {chargeUltimate}, is Pickup: {isPickup}, show HUD Message: {showHUDMessage}, switch to weapon: {switchToWeapon}"
            );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.KillPlayer))]
        static void KillPlayerPrefix(ref Player __instance, AttackID attackID)
        {
            Randomizer.IsPaused = true;
            Logger.LogDebug($"Player KillPlayer Prefix for {attackID} called");
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.KillPlayer))]
        static void KillPlayerPostfix(ref Player __instance, AttackID attackID)
        {
            Logger.LogInfo($"Player KillPlayer Postfix for {attackID} called");

            if (attackID != AttackID.PlayerShieldUltimateBashAttack)
                Randomizer.Archipelago.SendDeathLink(Randomizer.CurrentLevel, attackID);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.LoadPlayer))]
        static bool LoadPlayerPrefix(
            ref Player __instance,
            Vector3 spawnPosition,
            Quaternion spawnRotation,
            AudioGameplayController audioGameplayController
        )
        {
            Randomizer.IsLoadingSongs = false;
            Logger.LogDebug($"Player LoadPlayer Prefix called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.LoadPlayer))]
        static void LoadPlayerPostfix(
            ref Player __instance,
            Vector3 spawnPosition,
            Quaternion spawnRotation,
            ref AudioGameplayController audioGameplayController
        )
        {
            Logger.LogDebug($"Player LoadPlayer Postfix called");
            Instance = __instance;
            m_AudioGameplayController = audioGameplayController;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.EquipWeapon))]
        static bool EquipWeaponPrefix(ref Player __instance, PlayerWeaponType weaponType)
        {
            Logger.LogDebug($"Player EquipWeapon Prefix for {weaponType} called");
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.EquipWeapon))]
        static void EquipWeaponPostfix(ref Player __instance, PlayerWeaponType weaponType)
        {
            Logger.LogDebug($"Player EquipWeapon Postfix for {weaponType} called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Player.SetOutfitType))]
        static bool SetOutfitTypePrefix(ref Player __instance, ref SkinType outfitType)
        {
            Logger.LogInfo(
                $"Player SetOutfitType Prefix for {outfitType} called, and is randomized: {Randomizer.Configuration.skinsRandomizeOutfits.Value}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Player.SetOutfitType))]
        static void SetOutfitTypePostfix(ref Player __instance, SkinType outfitType)
        {
            Logger.LogInfo($"Player SetOutfitType Postfix for {outfitType} called");
        }
    }

    [HarmonyPatch(typeof(Enemy))]
    public class EnemyPatches
    {
        private static bool weaponTrickeryTrapActive = false;

        public static void ToggleWeaponTrickery(bool turnWeaponTrickeryOn)
        {
            weaponTrickeryTrapActive = turnWeaponTrickeryOn;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(Enemy.KillWithAttack))]
        static bool KillWithAttackPrefix(Enemy __instance, AttackInfo attack)
        {
            Logger.LogInfo(
                $"Enemy KillWithAttack Prefix called for {__instance.Config.ClassType} for {attack.Attack.AttackID}"
            );
            if (IsWeaponTrickeryActive() && !Randomizer.CurrentLevel.StartsWith("CH_Marbas"))
            {
                Logger.LogInfo(
                    $"An enemy has been killed while Weapon Trickery is active, switching weapons"
                );
                WeaponAbilityControllerPatches.Instance.SwitchToNextWeapon();
            }

            if (
                (
                    Randomizer.CurrentGameMode == EGameMode.Stage
                    || Randomizer.CurrentGameMode == EGameMode.Tutorial
                )
                && (
                    attack.Attack.AttackID.ToString().Contains("Player")
                    || attack.Attack.AttackID.ToString().Contains("Vulcan")
                )
            )
                Randomizer.LocationTracker.CheckEnemyKilled(__instance.Config.ClassType);

            if (
                (
                    Randomizer.CurrentGameMode == EGameMode.Stage
                    || Randomizer.CurrentGameMode == EGameMode.Tutorial
                ) && attack.Attack.AttackID.ToString().Contains("Overkill")
            )
                Randomizer.LocationTracker.CheckEnemySlaughtered(__instance.Config.ClassType);
            return true;
        }

        private static bool IsWeaponTrickeryActive()
        {
            Logger.LogDebug(
                $"Enemy is weapon trickery trap active: {weaponTrickeryTrapActive}, config active: {Randomizer.Configuration.gameplayWeaponTrickeryModeActive.Value}"
            );
            return weaponTrickeryTrapActive
                || Randomizer.Configuration.gameplayWeaponTrickeryModeActive.Value;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(Enemy.KillWithAttack))]
        static void KillWithAttackPostfix(Enemy __instance, AttackInfo attack)
        {
            Logger.LogDebug(
                $"Enemy KillWithAttack Postfix called for {__instance.Config.ID} for {attack.Attack.AttackID}"
            );
        }
    }

    [HarmonyPatch(typeof(BossAvatarBehaviourBase))]
    public class BossAvatarBehaviourBasePatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(BossAvatarBehaviourBase.SwitchState))]
        static bool SwitchStatePrefix(BossAvatarBehaviourBase __instance, BossStateType newState)
        {
            Logger.LogDebug(
                $"BossAvatarBehaviourBase SwitchState Prefix called for {__instance.BossAvatar.CurrentType} in new state {newState}"
            );

            if (
                newState == BossStateType.Death
                && ( // Red Judge spawns killable BossAvatars that would trigger each frame after death
                    __instance.BossAvatar.CurrentType.ToString() == Randomizer.CurrentLevel
                    || (
                        Randomizer.CurrentLevel == "Sheol"
                        && __instance.BossAvatar.CurrentType == BossType.Titan
                    )
                )
            )
                Randomizer.LocationTracker.CheckBossKilled(__instance.BossAvatar.CurrentType);

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(BossAvatarBehaviourBase.SwitchState))]
        static void SwitchStatePostfix(BossAvatarBehaviourBase __instance)
        {
            Logger.LogDebug($"BossAvatarBehaviourBase SwitchState Postfix");
        }
    }
}
