using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class LabeledWidget : Widget
    {
        public TextMeshProUGUI label;

        protected override void InitFromData()
        {
            base.InitFromData();
            label.text = ((LabeledWidgetData)BackingWidgetData).Label;
        }
    }
}
