using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LeftToggleFieldWidgetData : ToggleFieldWidgetData
    {
        public override string WidgetName => "LeftToggleField";
        public override bool IsLeftToggle => true;
    }
}
