using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LineWidgetData : WidgetData
    {
        public override string WidgetName => "Line";
        public LineWidget ActualWidget => (LineWidget)widget;
    }
}
