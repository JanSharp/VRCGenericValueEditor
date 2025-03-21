using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IntegerFieldWidget : LabeledWidget
    {
        public override string WidgetName => "IntegerField";
        public IntegerFieldWidgetData Data => (IntegerFieldWidgetData)BackingWidgetData;

        public TMP_InputField inputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateInputField();
        }

        public override void UpdateInteractable()
        {
            base.UpdateInteractable();
            inputField.interactable = Data.Interactable;
        }

        public void UpdateInputField(string text = null)
        {
            inputField.SetTextWithoutNotify(text ?? Data.GetValueAsString());
        }

        public void OnTextChanged()
        {
            if (raisingTextChanged)
                return;
            raisingTextChanged = true;
            SendCustomEventDelayedFrames(nameof(OnTextChangedDelayed), 1);
        }

        private bool raisingTextChanged = false;
        public void OnTextChangedDelayed()
        {
            raisingTextChanged = false;
            Data.SetValueFromString(inputField.text);
            UpdateInputField(); // To handle when the value didn't change, but text does differ.
        }
    }
}
