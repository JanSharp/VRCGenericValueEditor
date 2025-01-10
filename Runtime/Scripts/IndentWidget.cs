using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IndentWidget : Widget
    {
        public override string WidgetName => "Indent";
        public IndentWidgetData Data => (IndentWidgetData)BackingWidgetData;

        public override bool IsContainer => true;

        protected override void InitFromData() { }
    }
}
