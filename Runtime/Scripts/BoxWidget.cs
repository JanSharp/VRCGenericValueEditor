using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class BoxWidget : Widget
    {
        public override string WidgetName => "Box";
        public BoxWidgetData Data => (BoxWidgetData)BackingWidgetData;

        public override bool IsContainer => true;

        protected override void InitFromData() { }
    }
}
