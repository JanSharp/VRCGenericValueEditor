using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MultilineStringFieldWidget : StringFieldWidget
    {
        public override string WidgetName => "MultilineStringField";
    }
}
