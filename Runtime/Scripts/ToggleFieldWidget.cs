using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleFieldWidget : LabeledWidget
    {
        public override string WidgetName => "ToggleField";
        public ToggleFieldWidgetData Data => (ToggleFieldWidgetData)BackingWidgetData;

        public Toggle toggle;

        protected override void InitFromData()
        {
            base.InitFromData();
            toggle.SetIsOnWithoutNotify(Data.Value);
        }

        public void OnValueChanged()
        {
            Data.Value = toggle.isOn;
        }
    }
}
