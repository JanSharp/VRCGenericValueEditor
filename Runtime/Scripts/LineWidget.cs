using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LineWidget : Widget
    {
        public override string WidgetName => "Line";
        public LineWidgetData Data => (LineWidgetData)BackingWidgetData;
    }
}
