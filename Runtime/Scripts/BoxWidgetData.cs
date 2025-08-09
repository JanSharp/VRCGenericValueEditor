using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class BoxWidgetData : WidgetData
    {
        public override string WidgetName => "Box";
        public BoxWidget ActualWidget => (BoxWidget)widget;
    }
}
