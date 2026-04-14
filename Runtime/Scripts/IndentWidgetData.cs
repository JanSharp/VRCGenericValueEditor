using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IndentWidgetData : WidgetData
    {
        public override string WidgetName => "Indent";
        public IndentWidget ActualWidget => (IndentWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.
    }
}
