using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GroupingWidgetData : WidgetData
    {
        public override string WidgetName => "Grouping";
        public GroupingWidget ActualWidget => (GroupingWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.
    }
}
