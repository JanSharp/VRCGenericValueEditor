using TMPro;
using UdonSharp;

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
