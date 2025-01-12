using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpaceWidgetData : WidgetData
    {
        public override string WidgetName => "Space";
        public SpaceWidget ActualWidget => (SpaceWidget)widget;
    }
}
