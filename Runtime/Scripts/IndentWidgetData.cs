using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IndentWidgetData : WidgetData
    {
        public override string WidgetName => "Indent";
        public IndentWidget ActualWidget => (IndentWidget)widget;
    }
}
