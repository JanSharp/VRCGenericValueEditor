using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpaceWidgetData : WidgetData
    {
        public override string WidgetName => "Space";
        public SpaceWidget ActualWidget => (SpaceWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.
    }
}
