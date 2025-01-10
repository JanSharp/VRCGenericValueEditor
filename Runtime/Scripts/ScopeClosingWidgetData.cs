using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ScopeClosingWidgetData : WidgetData
    {
        public override string WidgetName => "ScopeClosing";
    }
}
