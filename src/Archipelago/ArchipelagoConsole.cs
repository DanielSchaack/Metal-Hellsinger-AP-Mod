using System.Collections.Generic;
using System.Linq;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using System.Text;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using BepInEx;
using UnityEngine;

namespace Randomizer
{
    // shamelessly stolen from oc2-modding https://github.com/toasterparty/oc2-modding/blob/main/OC2Modding/GameLog.cs
    public class ArchipelagoConsole : MonoBehaviour
    {
        public static ArchipelagoConsole Instance { get; private set; }
        public bool Hidden = true;

        public List<string> logLines = new();
        private List<float> logTimes = new();
        private Vector2 scrollView;
        private Rect window;
        private Rect scroll;
        private Rect text;
        private Rect hideShowButton;

        private GUIStyle textStyle = new();
        private GUIStyle buttonTextStyle = null;
        private int buttonFontSize = -1;
        private string scrollText = "";
        private float lastUpdateTime = Time.time;
        private const int MaxLogLines = 80;

        private const string defaultText = "Console initialized. Waiting for logs...";
        private string CommandText = "!help";
        private Rect CommandTextRect;
        private Rect SendCommandButton;

        private float LOG_WIDTH_RATIO = 0.46f;
        private float LOG_SAFE_MARGIN_X_RATIO = 0.02f;

        public void Awake()
        {
            Instance = this;

            UpdateLayout();
            LogMessage($"Mod v{PluginInfo.VERSION} started");
        }

        internal void LogApMessage(LogMessage message)
        {
            var stringBuilder = new StringBuilder();

            bool containsPlayer = false;
            var colorizedParts = message.Parts.Select(part =>
            {
                if (part.IsBackgroundColor)
                    return part.Text;
                var c = part.Color;
                if(part.Type == Archipelago.MultiClient.Net.MessageLog.Parts.MessagePartType.Player)
                    containsPlayer = true;
                var hex = $"{c.R:X2}{c.G:X2}{c.B:X2}";
                if (hex == "008000")
                    hex = "44C444";
                return $"<color=#{hex}>{part.Text}</color>";
            });

            string coloredMessage = string.Join("", colorizedParts);

            if (
                containsPlayer
                && Randomizer.Configuration.archipelagoConsoleFilterToPlayer.Value
                && !coloredMessage.Contains(Randomizer.Configuration.archipelagoUsername.Value)
            )
                return;

            LogMessage(coloredMessage);
        }

        public void LogMessage(string message)
        {
            if (message.IsNullOrWhiteSpace())
                return;

            if (logLines.Count == MaxLogLines)
            {
                logLines.RemoveAt(0);
                if (logTimes.Count > 0)
                    logTimes.RemoveAt(0);
            }

            string timestampedMessage = $"[{System.DateTime.Now:HH:mm:ss.fff}] {message}";
            logLines.Add(timestampedMessage);
            logTimes.Add(Time.time);
            Logger.LogInfo(message);

            lastUpdateTime = Time.time;
            UpdateLogText();

            if (!Hidden)
            {
                scrollView.y = 99999f;
            }
        }

        private void UpdateButtonTextStyle()
        {
            int desiredFontSize = Mathf.Max(12, (int)((float)Screen.height * 0.015f));
            if (buttonTextStyle != null && desiredFontSize == buttonFontSize)
            {
                return;
            }

            buttonFontSize = desiredFontSize;
            buttonTextStyle = new GUIStyle(GUI.skin.button);
            buttonTextStyle.fontSize = buttonFontSize;
            buttonTextStyle.alignment = TextAnchor.MiddleCenter;
        }

        public int GetSharedOverlayWidth()
        {
            int safeMarginX = Mathf.RoundToInt((float)Screen.width * LOG_SAFE_MARGIN_X_RATIO);
            int maxWidth = Screen.width - (safeMarginX * 2);
            int preferredWidth = Mathf.RoundToInt((float)Screen.width * LOG_WIDTH_RATIO);
            return Mathf.Clamp(preferredWidth, 760, maxWidth);
        }

        public void OnGUI()
        {
            if (!Randomizer.Configuration.archipelagoConsoleEnabled.Value)
                return;

            UpdateButtonTextStyle();

            if (Hidden)
            {
                UpdateLogText();
                UpdateLayout();
            }

            bool shouldShowBox = Hidden ? !string.IsNullOrEmpty(scrollText) : (logLines.Count > 0);

            if (shouldShowBox)
            {
                scrollView = GUI.BeginScrollView(window, scrollView, scroll);
                GUI.Box(text, "");
                GUI.Box(text, scrollText, textStyle);
                GUI.EndScrollView();
            }

            if (Randomizer.IsPaused)
            {
                if (GUI.Button(hideShowButton, Hidden ? "Show" : "Hide", buttonTextStyle))
                {
                    Hidden = !Hidden;
                    UpdateLogText();
                    UpdateLayout();
                }
            }

            if (Hidden)
                return;

            CommandText = GUI.TextField(CommandTextRect, CommandText);
            if (
                !CommandText.IsNullOrWhiteSpace()
                && GUI.Button(SendCommandButton, "Send", buttonTextStyle)
            )
            {
                Randomizer.Archipelago.SendArchipelagoMessage(CommandText);
                CommandText = "";
            }
        }

        public void UpdateLayout()
        {
            int width = GetSharedOverlayWidth();
            int marginX = (int)(Screen.width * 0.01f);
            int marginY = (int)(Screen.height * 0.01f);

            int buttonWidth = (int)((float)Screen.width * 0.035f);
            int buttonHeight = (int)((float)Screen.height * 0.03f);
            int buttonSpacing = (int)(buttonWidth / 3f);

            int cmdHeight = (int)(Screen.height * 0.022f);
            int elementSpacing = (int)(Screen.height * 0.005f);

            int windowX = Screen.width - width - marginX;
            int bottomBaseline = Screen.height - marginY;

            int fontSize = Mathf.Max(12, (int)((float)Screen.height * 0.015f));
            int xPadding = (int)(Screen.width * 0.006f);
            int yPadding = (int)(Screen.height * 0.004f);

            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = Color.white;
            textStyle.wordWrap = !Hidden;
            textStyle.padding = new RectOffset(xPadding, xPadding, yPadding, yPadding);

            int buttonY = bottomBaseline - buttonHeight;
            hideShowButton = new Rect(windowX - buttonWidth - buttonSpacing, buttonY, buttonWidth, buttonHeight);

            if (Hidden)
            {
                textStyle.alignment = TextAnchor.UpperLeft;

                int activeLineCount = string.IsNullOrEmpty(scrollText) ? 1 : scrollText.Split('\n').Length;
                int lineHeight = Mathf.RoundToInt(fontSize * 1.25f);
                int height = (activeLineCount * lineHeight) + (yPadding * 2);

                int scrollDepth = height;
                int windowY = bottomBaseline - height;

                window = new Rect(windowX, windowY, width, height);
                scroll = new Rect(0, 0, width, scrollDepth);
                scrollView = new Vector2(0.0f, scrollDepth);
                text = new Rect(0, 0, width, scrollDepth);
            }
            else
            {
                textStyle.alignment = TextAnchor.LowerLeft;

                int logHeight = (int)((float)Screen.height * 0.3f);
                int scrollDepth = logHeight * 10;

                // Stack upwards from bottomBaseline: SendButton -> CommandText -> Log Window
                int sendBtnY = bottomBaseline - cmdHeight;
                int cmdTextY = sendBtnY - elementSpacing - cmdHeight;
                int logWindowY = cmdTextY - elementSpacing - logHeight;

                window = new Rect(windowX, logWindowY, width, logHeight);
                scroll = new Rect(0, 0, width * 0.9f, scrollDepth);
                scrollView = new Vector2(0.0f, scrollDepth);
                text = new Rect(0, 0, width, scrollDepth);

                CommandTextRect = new Rect(windowX, cmdTextY, width, cmdHeight);

                int sendBtnWidth = (int)(Screen.width * 0.035f);
                SendCommandButton = new Rect(windowX, sendBtnY, sendBtnWidth, cmdHeight);
            }
        }

        private void UpdateLogText()
        {
            scrollText = "";

            if (Hidden)
            {
                float currentTime = Time.time;
                List<string> activeToasts = new List<string>();

                for (int i = logLines.Count - 1; i >= 0; i--)
                {
                    if (i < logTimes.Count && (currentTime - logTimes[i] < Randomizer.Configuration.archipelagoConsoleMessageDuration.Value))
                    {
                        activeToasts.Insert(0, logLines[i]);
                        if (activeToasts.Count >= Randomizer.Configuration.archipelagoConsoleMessageCount.Value)
                            break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (activeToasts.Count > 0)
                {
                    scrollText = string.Join("\n", activeToasts);
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < logLines.Count; i++)
                {
                    sb.Append("> ");
                    sb.Append(logLines[i]);
                    if (i < logLines.Count - 1)
                    {
                        sb.Append("\n");
                    }
                }
                scrollText = sb.ToString();
            }
        }

        internal void LogDeathlink(DeathLink deathLinkObject)
        {
            string DeathLinkMessage =
                deathLinkObject.Cause == null
                    ? $"\"<b><color=#FF0000>{deathLinkObject.Source}</color></b> died and took you with them.\""
                    : $"\"{deathLinkObject.Cause}\"";
            LogMessage(DeathLinkMessage);
        }
    }
}
