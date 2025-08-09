using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IndentWidget : Widget
    {
        public override string WidgetName => "Indent";
        public IndentWidgetData Data => (IndentWidgetData)BackingWidgetData;

        public override bool IsContainer => true;
    }
}
