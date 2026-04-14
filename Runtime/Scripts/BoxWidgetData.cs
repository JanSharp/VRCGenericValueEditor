using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class BoxWidgetData : WidgetData
    {
        public override string WidgetName => "Box";
        public BoxWidget ActualWidget => (BoxWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.
    }
}
