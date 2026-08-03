using System.Collections.Concurrent;
using System.Threading.Tasks;
using BepInEx.Logging;

namespace Randomizer
{
    public class Logger
    {
        private static ManualLogSource Log;
        public static bool Testing = false;

        private static readonly ConcurrentQueue<(LogLevel level, string message)> LogQueue = new();
        private static bool isProcessingQueue = false;
        private static readonly object lockObject = new();

        public static void SetLogger(ManualLogSource logger)
        {
            Log = logger;
        }

        public static void LogInfo(string message) => EnqueueLog(LogLevel.Info, message);

        public static void LogWarning(string message) => EnqueueLog(LogLevel.Warning, message);

        public static void LogError(string message) => EnqueueLog(LogLevel.Error, message);

        public static void LogDebug(string message) => EnqueueLog(LogLevel.Debug, message);

        private static void EnqueueLog(LogLevel level, string message)
        {
            if (Log == null)
                return;

            string timestampedMessage = $"[{System.DateTime.Now:HH:mm:ss.fff}] {message}";

            LogQueue.Enqueue((level, timestampedMessage));

            lock (lockObject)
            {
                if (!isProcessingQueue)
                {
                    isProcessingQueue = true;
                    Task.Run(ProcessQueueAsync);
                }
            }
        }

        private static void ProcessQueueAsync()
        {
            while (true)
            {
                while (LogQueue.TryDequeue(out var logEntry))
                {
                    switch (logEntry.level)
                    {
                        case LogLevel.Info:
                            Log.LogInfo(logEntry.message);
                            break;
                        case LogLevel.Warning:
                            Log.LogWarning(logEntry.message);
                            break;
                        case LogLevel.Error:
                            Log.LogError(logEntry.message);
                            break;
                        case LogLevel.Debug:
                            Log.LogDebug(logEntry.message);
                            break;
                    }
                }

                lock (lockObject)
                {
                    if (LogQueue.IsEmpty)
                    {
                        isProcessingQueue = false;
                        break;
                    }
                }
            }
        }
    }
}
