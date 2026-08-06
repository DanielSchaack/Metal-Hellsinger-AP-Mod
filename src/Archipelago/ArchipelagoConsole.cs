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
        private const float HideTimeout = 15f;

        private const string defaultText = "Console initialized. Waiting for logs...";
        private string CommandText = "!help";
        private Rect CommandTextRect;
        private Rect SendCommandButton;

        private float LOG_WIDTH_RATIO = 0.44f;
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

            var colorizedParts = message.Parts.Select(part =>
            {
                if (part.IsBackgroundColor)
                    return part.Text;
                var c = part.Color;
                var hex = $"{c.R:X2}{c.G:X2}{c.B:X2}";
                if (hex == "008000")
                    hex = "44C444";
                return $"<color=#{hex}>{part.Text}</color>";
            });

            LogMessage(string.Join("", colorizedParts));
        }

        public void LogMessage(string message)
        {
            if (message.IsNullOrWhiteSpace())
                return;

            if (logLines.Count == MaxLogLines)
            {
                logLines.RemoveAt(0);
            }

            logLines.Add(message);
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

            // Show recent entries as single line
            if (logLines.Count > 0 && (!Hidden || Time.time - lastUpdateTime < HideTimeout))
            {
                scrollView = GUI.BeginScrollView(window, scrollView, scroll);
                GUI.Box(text, "");
                GUI.Box(text, scrollText, textStyle);
                GUI.EndScrollView();
            }

            if (GUI.Button(hideShowButton, Hidden ? "Show" : "Hide", buttonTextStyle))
            {
                Hidden = Hidden ? false : true;
                UpdateLogText();
                UpdateLayout();
            }

            // draw client/server commands entry
            // if (Hidden || Archipelago.instance == null || !Archipelago.instance.connected)
            // if (Hidden || Archipelago.instance == null)
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
            int height;
            int scrollDepth;

            if (Hidden)
            {
                height = (int)((float)Screen.height * 0.045f);
                scrollDepth = height;
            }
            else
            {
                height = (int)((float)Screen.height * 0.3f);
                scrollDepth = height * 10;
            }
            window = new Rect((Screen.width / 2) - (width / 2), 0, width, height);
            scroll = new Rect(0, 0, width * 0.9f, scrollDepth);
            scrollView = new Vector2(0.0f, scrollDepth);
            text = new Rect(0, 0, width, scrollDepth);

            textStyle.alignment = TextAnchor.LowerLeft;
            textStyle.fontSize = (int)(Screen.height * 0.0195f);
            textStyle.normal.textColor = Color.white;
            textStyle.wordWrap = !Hidden;

            var xPadding = (int)(Screen.width * 0.01f);
            var yPadding = (int)(Screen.height * 0.01f);

            textStyle.padding = new RectOffset(xPadding, xPadding, yPadding, yPadding);

            int buttonWidth = (int)((float)Screen.width * 0.035f);
            int buttonHeight = (int)((float)Screen.height * 0.03f);

            hideShowButton = new Rect(
                (Screen.width / 2) + (width / 2) + (buttonWidth / 3),
                Screen.height * 0.004f,
                buttonWidth,
                buttonHeight
            );

            // draw server command text field and button
            width = (int)(Screen.width * 0.4f);
            var xPos = (int)(Screen.width / 2.0f - width / 2.0f);
            var yPos = (int)(Screen.height * 0.307f);
            height = (int)(Screen.height * 0.022f);

            CommandTextRect = new Rect(xPos, yPos, width, height);

            width = (int)(Screen.width * 0.035f);
            yPos += (int)(Screen.height * 0.03f);
            SendCommandButton = new Rect(xPos, yPos, width, height);
        }

        private void UpdateLogText()
        {
            scrollText = "";

            if (Hidden)
            {
                if (logLines.Count > 0)
                {
                    scrollText = logLines[logLines.Count - 1];
                }
            }
            else
            {
                for (var i = 0; i < logLines.Count; i++)
                {
                    scrollText += "> ";
                    scrollText += logLines.ElementAt(i);
                    if (i < logLines.Count - 1)
                    {
                        scrollText += "\n\n";
                    }
                }
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
