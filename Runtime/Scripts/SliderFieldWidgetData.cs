using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SliderFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "SliderField";
        public SliderFieldWidget ActualWidget => (SliderFieldWidget)widget;

        // TODO: Add min and max.

        private float value;
        public float Value
        {
            get => value;
            set
            {
                if (value == this.value)
                    return;
                this.value = value;
                if (ActualWidget != null)
                {
                    ActualWidget.slider.SetValueWithoutNotify(value);
                    ActualWidget.UpdateInputField(value);
                }
                RaiseEvent();
            }
        }

        public SliderFieldWidgetData WannaBeConstructor(string label, float value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
