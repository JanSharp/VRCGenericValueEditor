using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StringFieldWidget : LabeledWidget
    {
        public override string WidgetName => "StringField";
        public StringFieldWidgetData Data => (StringFieldWidgetData)BackingWidgetData;

        public TMP_InputField inputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            inputField.SetTextWithoutNotify(Data.Value);
        }

        public void OnTextChanged()
        {
            Data.Value = inputField.text;
        }
    }
}
