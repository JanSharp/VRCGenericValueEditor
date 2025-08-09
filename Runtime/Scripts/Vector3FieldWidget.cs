using TMPro;
using UdonSharp;
using UnityEngine;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector3FieldWidget : LabeledWidget
    {
        public override string WidgetName => "Vector3Field";
        public Vector3FieldWidgetData Data => (Vector3FieldWidgetData)BackingWidgetData;

        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;

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
            zInputField.interactable = interactable;
        }

        public void UpdateInputFields()
        {
            Vector3 value = Data.Value;
            // No format string to let people type in whatever they want.
            xInputField.SetTextWithoutNotify(value.x.ToString());
            yInputField.SetTextWithoutNotify(value.y.ToString());
            zInputField.SetTextWithoutNotify(value.z.ToString());
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

        public void OnZChanged()
        {
            if (raisingZChanged)
                return;
            raisingZChanged = true;
            SendCustomEventDelayedFrames(nameof(OnZChangedDelayed), 1);
        }

        private bool raisingZChanged = false;
        public void OnZChangedDelayed()
        {
            raisingZChanged = false;
            Data.Z = Parse(zInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }
    }
}
