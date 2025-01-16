using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ButtonWidget : LabeledWidget
    {
        public override string WidgetName => "Button";
        public ButtonWidgetData Data => (ButtonWidgetData)BackingWidgetData;

        public Button button;

        public override void UpdateInteractable()
        {
            base.UpdateInteractable();
            button.interactable = Data.Interactable;
        }

        public void OnButtonClick()
        {
            if (Data != null)
                Data.RaiseEvent();
        }
    }
}
