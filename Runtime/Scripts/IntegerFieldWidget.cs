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
            inputField.SetTextWithoutNotify(Data.GetValueAsString());
        }

        public void OnTextChanged()
        {
            Data.SetValueFromString(inputField.text);
        }
    }
}
