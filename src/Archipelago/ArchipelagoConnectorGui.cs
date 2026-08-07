using Outsiders.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OutsidersButton;

namespace Randomizer
{
    public class ArchipelagoConnectorGui : MonoBehaviour
    {
        public static string archipelagoUri;
        public static string archipelagoUsername;
        public static string archipelagoPassword;

        private void Awake()
        {
            archipelagoUri = Randomizer.Configuration.archipelagoUri.Value;
            archipelagoUsername = Randomizer.Configuration.archipelagoUsername.Value;
            archipelagoPassword = Randomizer.Configuration.archipelagoPassword.Value;
        }

        private static string TitleScreenMenu =
            "Main/UIRoot/Overlay/Layer-Default/TitleScreenDisplay(Clone)/MenuParentGroup/MenuVerticalLayout";
        private static string PauseMenu =
            "Main/UIRoot/Overlay/Layer-Default/PauseScreenDisplay(Clone)/MenuDisplay";

        public static bool TryInjectTitle()
        {
            return TryInjectPopup(TitleScreenMenu, "TitleRow(Clone)");
        }

        public static bool TryInjectPause()
        {
            return TryInjectPopup(PauseMenu, "PauseMenuRowTemplate(Clone)");
        }

        private bool IsGameObjectAvailable(string gameObject)
        {
            GameObject layoutObj = GameObject.Find(gameObject);
            bool isAvailable = layoutObj != null;
            return isAvailable;
        }

        private static bool TryInjectPopup(string layoutParent, string childToCopy)
        {
            GameObject layoutObj = GameObject.Find(layoutParent);

            if (layoutObj != null)
            {
                Transform template = layoutObj.transform.Find(childToCopy);
                if (template == null)
                    return false;

                var customRow = Instantiate(template.gameObject, layoutObj.transform);
                customRow.name = "Archipelago_TitleRow";
                customRow.transform.SetSiblingIndex(0);

                var textComponent = customRow.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = "ARCHIPELAGO";
                    textComponent.enableWordWrapping = false;
                }

                OutsidersButton btn = customRow.GetComponentInChildren<OutsidersButton>();
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                    btn.add_onSelectionChanged(
                        new System.Action<SelectionEvent, Il2CppSystem.Object>(OnArchipelagoClick)
                    );
                }

                Logger.LogInfo($"Injected Archipelago option into menu {layoutParent}");
                return true;
            }
            return false;
        }

        public static void OnArchipelagoClick(SelectionEvent sEvent, Il2CppSystem.Object obj)
        {
            if (sEvent == SelectionEvent.Activated)
            {
                ToggleArchipelagoDialog();
            }
        }

        private static TMP_InputField hostInputfield;
        private static TMP_InputField playerInputField;
        private static TMP_InputField passwordInputfield;
        private static TMP_FontAsset font;
        private static Color fontColor;

        private static void ToggleArchipelagoDialog()
        {
            UIMaster ui = UIMaster.sm_instance;
            if (ui != null)
            {
                Logger.LogDebug("UIMaster is not null");
                // ui.ShowUnlockPopup()

                PopupButtonData pbdConnect = ArchipelagoConnectButton();

                string connectionStatus = Randomizer.Archipelago.connected
                    ? "connected"
                    : "disconnected";
                string popupMessage = "You are currently " + connectionStatus;

                PopupViewContract popup = ui.CreateAndShowSystemPopup(
                    "ARCHIPELAGO LOG-IN",
                    popupMessage,
                    pbdConnect,
                    false
                );
                popup.SetIsCancelable?.Invoke(true);

                GameObject layoutObj = GameObject.Find(
                    "Main/UIRoot/Overlay/Layer-System/PopupDisplay(Clone)/BasicPopup/"
                );

                if (layoutObj != null)
                {
                    if (font == null)
                        setFont(layoutObj);

                    GameObject myInputGroup = new GameObject("ArchipelagoInputFields");
                    myInputGroup.transform.SetParent(layoutObj.transform, false);
                    myInputGroup.transform.SetSiblingIndex(6);

                    var layout = myInputGroup.AddComponent<VerticalLayoutGroup>();
                    layout.childAlignment = TextAnchor.UpperCenter;
                    layout.childControlHeight = true;
                    layout.childForceExpandHeight = false;

                    hostInputfield = CreateInputField(
                        myInputGroup.transform,
                        "Host",
                        archipelagoUri
                    );
                    playerInputField = CreateInputField(
                        myInputGroup.transform,
                        "Name",
                        archipelagoUsername
                    );
                    passwordInputfield = CreateInputField(
                        myInputGroup.transform,
                        "Password",
                        archipelagoPassword,
                        true
                    );

                    AddCenteredText("('Esc' to close popup)", layoutObj);
                }
            }
        }

        private static void AddCenteredText(string text, GameObject layoutObj)
        {
            GameObject labelObj = new GameObject("Hint");
            labelObj.transform.SetParent(layoutObj.transform, false);
            var label = labelObj.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 24;
            label.color = fontColor;
            label.font = font;
            label.alignment = TextAlignmentOptions.Center;
            labelObj.transform.SetAsLastSibling();
            var labelLe = labelObj.AddComponent<LayoutElement>();
            labelLe.preferredWidth = 600;
            labelLe.flexibleWidth = 1;
        }

        private static void setFont(GameObject layoutObj)
        {
            Transform scroller = layoutObj.transform.Find("Scroller");
            if (scroller == null)
                return;

            Transform mask = scroller.Find("Mask");
            if (mask == null)
                return;

            Transform label = mask.Find("MessageLabel");
            if (label != null)
            {
                var text = label.GetComponent<TextMeshProUGUI>();
                font = text.font;
                fontColor = text.color;
            }
        }

        private static PopupButtonData ArchipelagoConnectButton()
        {
            PopupButtonData pbdConnect = new PopupButtonData();
            pbdConnect.Text = !Randomizer.Archipelago.connected ? "Connect" : "Disconnect";
            pbdConnect.Callback = !Randomizer.Archipelago.connected
                ? new System.Action(ArchipelagoConnect)
                : new System.Action(ArchipelagoDisconnect);
            return pbdConnect;
        }

        private static TMP_InputField CreateInputField(
            Transform parent,
            string labelText,
            string initialValue,
            bool isPassword = false
        )
        {
            // Row Container
            GameObject row = new GameObject(labelText + "_Row");
            row.transform.SetParent(parent, false);
            var layout = row.AddComponent<HorizontalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            layout.spacing = 10;

            var rowLe = row.AddComponent<LayoutElement>();
            rowLe.preferredWidth = 600;

            // Label
            GameObject labelObj = new GameObject("Label");
            labelObj.transform.SetParent(row.transform, false);
            var label = labelObj.AddComponent<TextMeshProUGUI>();
            label.text = labelText + ":";
            label.fontSize = 24;
            label.color = fontColor;
            label.font = font;
            label.alignment = TextAlignmentOptions.MidlineRight;

            var labelLe = labelObj.AddComponent<LayoutElement>();
            labelLe.preferredWidth = 150;
            labelLe.flexibleWidth = 0;

            // Input Field Background
            GameObject inputObj = new GameObject("Input");
            inputObj.transform.SetParent(row.transform, false);
            var bg = inputObj.AddComponent<Image>();
            bg.color = new Color(0.1f, 0.1f, 0.1f, 0.1f);
            var inputLe = inputObj.AddComponent<LayoutElement>();
            inputLe.preferredWidth = 300;
            inputLe.preferredHeight = 40;

            // Input Component
            var inputField = inputObj.AddComponent<TMP_InputField>();
            inputField.ActivateInputField();
            inputField.caretColor = Color.white;
            inputField.customCaretColor = true;
            inputField.caretBlinkRate = 0.85f;
            inputField.selectionColor = Color.gray;
            inputField.onFocusSelectAll = false;

            if (isPassword)
            {
                inputField.contentType = TMP_InputField.ContentType.Password;
            }

            // Input Text
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(inputObj.transform, false);
            var inputFieldText = textObj.AddComponent<TextMeshProUGUI>();
            inputFieldText.fontSize = 24;
            inputFieldText.color = fontColor;
            inputFieldText.font = font;
            inputFieldText.alignment = TextAlignmentOptions.MidlineLeft;

            // Text Padding and Alignment
            textObj.AddComponent<RectMask2D>();
            var textRect =
                textObj.GetComponent<RectTransform>() ?? textObj.AddComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0, 0);
            textRect.anchorMax = new Vector2(1, 1);
            textRect.offsetMin = new Vector2(10, 5);
            textRect.offsetMax = new Vector2(-10, -5);

            inputField.textComponent = inputFieldText;
            inputField.textViewport = textRect;
            inputField.text = initialValue;
            inputField.CreateCursorVerts();

            // Reenable to show caret and highlighting
            inputField.enabled = false;
            inputField.enabled = true;

            return inputField;
        }

        static void ArchipelagoConnect()
        {
            checkForSettingsUpdate(
                hostInputfield.text,
                playerInputField.text,
                passwordInputfield.text
            );
            Randomizer.Archipelago.TryConnect();
        }

        static void ArchipelagoDisconnect()
        {
            checkForSettingsUpdate(
                hostInputfield.text,
                playerInputField.text,
                passwordInputfield.text
            );
            Randomizer.Archipelago.TryDisconnect();
        }

        static void checkForSettingsUpdate(
            string archipelagoUri,
            string archipelagoUsername,
            string archipelagoPassword
        )
        {
            if (!archipelagoUri.Equals(Randomizer.Configuration.archipelagoUri.Value))
            {
                Randomizer.Configuration.archipelagoUri.Value = archipelagoUri;
            }
            if (!archipelagoUsername.Equals(Randomizer.Configuration.archipelagoUsername.Value))
            {
                Randomizer.Configuration.archipelagoUsername.Value = archipelagoUsername;
            }
            if (!archipelagoPassword.Equals(Randomizer.Configuration.archipelagoPassword.Value))
            {
                Randomizer.Configuration.archipelagoPassword.Value = archipelagoPassword;
            }
        }
    }
}
