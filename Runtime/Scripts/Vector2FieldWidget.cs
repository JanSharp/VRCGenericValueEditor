using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector2FieldWidget : LabeledWidget
    {
        public override string WidgetName => "Vector2Field";
        public Vector2FieldWidgetData Data => (Vector2FieldWidgetData)BackingWidgetData;

        public TMP_InputField xInputField;
        public TMP_InputField yInputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateInputFields();
        }

        public override void UpdateInteractable()
        {
            base.UpdateInteractable();
            bool interactable = Data.Interactable;
            xInputField.interactable = interactable;
            yInputField.interactable = interactable;
        }

        public void UpdateInputFields()
        {
            Vector2 value = Data.Value;
            // No format string to let people type in whatever they want.
            xInputField.SetTextWithoutNotify(value.x.ToString());
            yInputField.SetTextWithoutNotify(value.y.ToString());
        }

        private float Parse(string text)
        {
            return float.TryParse(text, out float parsedValue) ? parsedValue : 0f;
        }

        public void OnXChanged()
        {
            if (raisingXChanged)
                return;
            raisingXChanged = true;
            SendCustomEventDelayedFrames(nameof(OnXChangedDelayed), 1);
        }

        private bool raisingXChanged = false;
        public void OnXChangedDelayed()
        {
            raisingXChanged = false;
            Data.X = Parse(xInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }

        public void OnYChanged()
        {
            if (raisingYChanged)
                return;
            raisingYChanged = true;
            SendCustomEventDelayedFrames(nameof(OnYChangedDelayed), 1);
        }

        private bool raisingYChanged = false;
        public void OnYChangedDelayed()
        {
            raisingYChanged = false;
            Data.Y = Parse(yInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }
    }
}
