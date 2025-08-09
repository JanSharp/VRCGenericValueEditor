using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GroupingWidget : Widget
    {
        public override string WidgetName => "Grouping";
        public GroupingWidgetData Data => (GroupingWidgetData)BackingWidgetData;

        public override bool IsContainer => true;
    }
}
