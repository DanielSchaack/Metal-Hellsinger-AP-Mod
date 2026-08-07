using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Randomizer
{
    [HarmonyPatch(typeof(AudioEventSystem))]
    public class AudioEventSystemPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(AudioEventSystem.Update))]
        static bool UpdatePrefix(ref AudioEventSystem __instance)
        {
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AudioEventSystem.Update))]
        static void UpdatePostfix(AudioEventSystem __instance) { }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AudioEventSystem.TriggerVO))]
        static bool TriggerVOPrefix(
            ref AudioEventSystem __instance,
            AudioEventSystem.VOEventRuntimeData data
        )
        {
            Logger.LogInfo(
                $"AudioEventSystem TriggerVO Prefix called for event {data.VOAndSubtitleData.SoundEventName} triggering in {data.TimeToTrigger}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AudioEventSystem.TriggerVO))]
        static void TriggerVOPostfix(AudioEventSystem __instance)
        {
            Logger.LogInfo($"AudioEventSystem TriggerVO Postfix called");
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AudioEventSystem.HandleAudioEventEmitterGameObjectEvent))]
        static bool HandleAudioEventEmitterGameObjectEventPrefix(
            AudioEventEmitter emitter,
            AudioEventEmitter.AudioEventEmitterPlayEvent playEvent
        )
        {
            // if (emitter != null)
            //     Logger.LogInfo(
            //         $"AudioEventSystem HandleAudioEventEmitterGameObjectEvent Prefix called for event {emitter.Event} triggering due to {playEvent}"
            //     );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AudioEventSystem.HandleAudioEventEmitterGameObjectEvent))]
        static void HandleAudioEventEmitterGameObjectEventPostfix(AudioEventSystem __instance)
        {
            // Logger.LogInfo(
            //     $"AudioEventSystem HandleAudioEventEmitterGameObjectEvent Postfix called"
            // );
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AudioEventSystem.PlayAdvancedVOEvent))]
        static bool PlayAdvancedVOEventPrefix(
            ref AudioEventSystem __instance,
            AudioEventSystem.AdvancedVOEventData eventData
        )
        {
            Logger.LogInfo(
                $"AudioEventSystem PlayAdvancedVOEvent Prefix called for event {eventData.VOEventData.name}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(AudioEventSystem.PlayAdvancedVOEvent))]
        static void PlayAdvancedVOEventPostfix(AudioEventSystem __instance)
        {
            Logger.LogInfo($"AudioEventSystem PlayAdvancedVOEvent Postfix called");
        }
    }

    [HarmonyPatch(typeof(SoundEmitterSystem))]
    public class SoundEmitterSystemPatches
    {
        public static SoundEmitterSystem Instance;
        private static readonly System.Collections.Concurrent.ConcurrentQueue<VoRequest> VoQueue = new();
        private class VoRequest
        {
            public VOAndSubtitleIDTuple Data;
            public string Message;

            public VoRequest(VOAndSubtitleIDTuple data, string message)
            {
                Data = data;
                Message = message;
            }
        }

        private static readonly System.Collections.Generic.List<string> ComplementingVoDataId =
        [
            "EndlessArenaCompleteVO",
        ];

        private static readonly System.Collections.Generic.List<string> FailureVoData =
        [
            "DeathCutsceneRandomVoData",
            "EndlessDeathVOData",
            "EndlessDeathAtBossVOData",
            "TormentDeathCutsceneVOData",
        ];

        public static void PlayComplement()
        {
            Logger.LogInfo($"Added a Complement quip to the queue");
            var data1 = GetRandomVoTuple(ComplementingVoDataId);
            var data2 = GetChallengeVoTuple("ChallengeVoData", "Gold");
            var data = new System.Random().Next(2) > 0 ? data1 : data2;
            VoQueue.Enqueue(new VoRequest(data, "Complement"));
        }


        public static void PlayEncouragement()
        {
            Logger.LogInfo($"Added a Encouragement quip to the queue");
            var data1 = GetChallengeVoTuple("ChallengeVoData", "No");
            var data2 = GetChallengeVoTuple("ChallengeVoData", "Bronze");
            var data3 = GetChallengeVoTuple("ChallengeVoData", "Silver");
            var data = new System.Random().Next(3) switch
            {
                0 => data1,
                1 => data2,
                2 => data3,
                _ => data1,
            };

            VoQueue.Enqueue(new VoRequest(data, "Encouragement"));
        }

        public static void PlayFailure()
        {
            Logger.LogInfo($"Added a Failure quip to the queue");
            var data1 = GetRandomVoTuple(FailureVoData);
            var data2 = GetChallengeVoTuple("ChallengeVoData", "Death");
            var data = new System.Random().Next(2) > 0 ? data1 : data2;
            VoQueue.Enqueue(new VoRequest(data, "Failure"));
        }

        private static float ActiveTime = 0f;
        private static float CheckInterval = 3f;

        public static void PollQueue()
        {
            ActiveTime += UnityEngine.Time.unscaledDeltaTime;
            if(ActiveTime < CheckInterval || !Randomizer.AreItemsDispensible())
                return;

            if(VoQueue.TryPeek(out var request))
            {
                ActiveTime = 0f;
                PlayVoiceline(request.Data, request.Message);
                VoQueue.TryDequeue(out _);
            }
        }

        private static SoundEmitter GetEmitter(VOAndSubtitleIDTuple data)
        {
            SoundEmitter emitter = Randomizer.RegisterTypeAndCreateObjectWithCollider<SoundEmitter>(
                "EncouragingVO"
            );
            emitter.PlayOnlyOncePerSession = false;
            emitter.TriggerEntered = null;
            emitter.VORandomizer = new Il2CppReferenceArray<VORandomizer>(0);
            emitter.Data = data;
            return emitter;
        }

        private static VOAndSubtitleIDTuple GetRandomVoTuple(
            System.Collections.Generic.List<string> voDataIds
        )
        {
            var dataId = voDataIds[new System.Random().Next(voDataIds.Count)];
            var data = RandomVoDataCache.Get(dataId).randomVOList;
            return data[new System.Random().Next(data.Count)];
        }

        private static VOAndSubtitleIDTuple GetChallengeVoTuple(string voDataId, string rank)
        {
            System.Collections.Generic.List<Il2CppReferenceArray<VOAndSubtitleIDTuple>> dataLists =
                new System.Collections.Generic.List<Il2CppReferenceArray<VOAndSubtitleIDTuple>>();
            var data = ChallengeVoDataCache.Get(voDataId);
            switch (rank)
            {
                case "Gold":
                    dataLists.Add(data.goldRank);
                    break;
                case "Silver":
                    dataLists.Add(data.silverRank);
                    break;
                case "Bronze":
                    dataLists.Add(data.bronzeRank);
                    break;
                case "No":
                    dataLists.Add(data.noRank);
                    break;
                case "Death":
                    dataLists.Add(data.deathQuips);
                    break;
                default:
                    dataLists.Add(data.bronzeRank);
                    dataLists.Add(data.deathQuips);
                    dataLists.Add(data.silverRank);
                    dataLists.Add(data.goldRank);
                    dataLists.Add(data.noRank);
                    break;
            }
            var quips = dataLists[new System.Random().Next(dataLists.Count)];
            return quips[new System.Random().Next(quips.Count)];
        }

        private static void PlayVoiceline(VOAndSubtitleIDTuple data, string message)
        {
            IngameMessagesPatches.DisplayItemActivated(message);
            SoundEmitter emitter = GetEmitter(data);
            Instance.OnSoundEmitterTriggerEntered(emitter);
            UnityEngine.Object.Destroy(emitter.gameObject);
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(SoundEmitterSystem.OnSoundEmitterTriggerEntered))]
        static bool OnSoundEmitterTriggerEnteredPrefix(
            ref SoundEmitterSystem __instance,
            SoundEmitter emitter
        )
        {
            Logger.LogInfo(
                $"SoundEmitterSystem OnSoundEmitterTriggerEntered Prefix called for emitter {emitter.Data.SoundEventName}"
            );
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoundEmitterSystem.OnSoundEmitterTriggerEntered))]
        static void OnSoundEmitterTriggerEnteredPostfix(SoundEmitterSystem __instance) { }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoundEmitterSystem.Register))]
        static void RegisterPostfix(ref SoundEmitterSystem __instance)
        {
            Logger.LogInfo($"SoundEmitterSystem Register Postfix called");
            if (Instance == null)
                Instance = __instance;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(SoundEmitterSystem.TearDown))]
        static void TearDownPostfix(ref SoundEmitterSystem __instance)
        {
            Logger.LogInfo($"SoundEmitterSystem TearDown Postfix called");
            Instance = null;
        }
    }
}
