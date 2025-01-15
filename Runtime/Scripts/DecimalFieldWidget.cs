using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DecimalFieldWidget : LabeledWidget
    {
        public override string WidgetName => "DecimalField";
        public DecimalFieldWidgetData Data => (DecimalFieldWidgetData)BackingWidgetData;

        public TMP_InputField inputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateInputField();
        }

        public void UpdateInputField(string text = null)
        {
            inputField.SetTextWithoutNotify(text != null ? text : Data.GetValueAsString());
        }

        public void OnTextChanged()
        {
            Data.SetValueFromString(inputField.text);
            UpdateInputField(); // To handle when the value didn't change, but text does differ.
        }
    }
}
