using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpaceWidget : Widget
    {
        public override string WidgetName => "Space";
        public SpaceWidgetData Data => (SpaceWidgetData)BackingWidgetData;
    }
}
