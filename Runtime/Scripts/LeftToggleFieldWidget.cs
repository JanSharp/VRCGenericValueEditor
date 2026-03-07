using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LeftToggleFieldWidget : ToggleFieldWidget
    {
        public override string WidgetName => "LeftToggleField";
        public override bool IsLeftToggle => true;
    }
}
