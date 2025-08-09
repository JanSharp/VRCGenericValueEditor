using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MultilineStringFieldWidgetData : StringFieldWidgetData
    {
        public override string WidgetName => "MultilineStringField";
    }
}
