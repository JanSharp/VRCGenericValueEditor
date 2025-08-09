using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LabelWidget : LabeledWidget
    {
        public override string WidgetName => "Label";
        public LabelWidgetData Data => (LabelWidgetData)BackingWidgetData;
    }
}
