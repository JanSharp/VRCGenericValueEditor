using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MultilineStringFieldWidget : StringFieldWidget
    {
        public override string WidgetName => "MultilineStringField";
    }
}
