using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class LabeledWidgetData : WidgetData
    {
        protected string label;
        public string Label
        {
            get => label;
            set
            {
                if (value == null)
                    value = "";
                if (value == label)
                    return;
                label = value;
                if (widget != null)
                    ((LabeledWidget)widget).label.text = value;
            }
        }

        // public override bool WannaBeClassSupportsPooling => true; // Up to the deriving class.
        public override void ResetWannaBeClassToDefault()
        {
            base.ResetWannaBeClassToDefault();
            label = default;
        }

        protected void LabeledWidgetDataConstructor(string label)
        {
            this.label = label;
        }
    }
}
