using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LabelWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "Label";
        public LabelWidget ActualWidget => (LabelWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.

        public LabelWidgetData WannaBeConstructor(string label)
        {
            LabeledWidgetDataConstructor(label);
            return this;
        }
    }
}
