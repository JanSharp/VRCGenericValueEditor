using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ButtonWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "Button";
        public ButtonWidget ActualWidget => (ButtonWidget)widget;

        public override bool WannaBeClassSupportsPooling => true;
        // public override void ResetWannaBeClassToDefault() => base.ResetWannaBeClassToDefault(); // Redundant.

        public ButtonWidgetData WannaBeConstructor(string label)
        {
            LabeledWidgetDataConstructor(label);
            return this;
        }
    }
}
