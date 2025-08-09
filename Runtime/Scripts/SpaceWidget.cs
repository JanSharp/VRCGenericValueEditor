using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SpaceWidget : Widget
    {
        public override string WidgetName => "Space";
        public SpaceWidgetData Data => (SpaceWidgetData)BackingWidgetData;
    }
}
