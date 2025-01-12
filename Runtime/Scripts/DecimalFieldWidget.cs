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
            inputField.SetTextWithoutNotify(Data.GetValueAsString());
        }

        public void OnTextChanged()
        {
            Data.SetValueFromString(inputField.text);
        }
    }
}
