using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ButtonWidget : LabeledWidget
    {
        public override string WidgetName => "Button";
        public ButtonWidgetData Data => (ButtonWidgetData)BackingWidgetData;

        public void OnButtonClick()
        {
            if (Data != null)
                Data.RaiseEvent();
        }
    }
}
