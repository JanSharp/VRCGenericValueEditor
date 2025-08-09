using UdonSharp;
using UnityEngine.UI;

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

        public override void UpdateInteractable()
        {
            base.UpdateInteractable();
            toggle.interactable = Data.Interactable;
        }

        public void OnValueChanged()
        {
            Data.Value = toggle.isOn;
        }
    }
}
