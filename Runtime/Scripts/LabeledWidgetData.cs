using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

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

        protected void LabeledWidgetDataConstructor(string label)
        {
            this.label = label;
        }
    }
}
