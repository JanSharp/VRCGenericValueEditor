using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GroupingWidgetData : WidgetData
    {
        public override string WidgetName => "Grouping";
        public GroupingWidget ActualWidget => (GroupingWidget)widget;
    }
}
