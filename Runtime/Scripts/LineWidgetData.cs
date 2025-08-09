using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LineWidgetData : WidgetData
    {
        public override string WidgetName => "Line";
        public LineWidget ActualWidget => (LineWidget)widget;
    }
}
