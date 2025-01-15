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

        private float minValue;
        public float MinValue
        {
            get => minValue;
            set
            {
                if (value == minValue)
                    return;
                minValue = value;
                if (ActualWidget != null)
                    ActualWidget.slider.minValue = value;
            }
        }

        private float maxValue;
        public float MaxValue
        {
            get => maxValue;
            set
            {
                if (value == maxValue)
                    return;
                maxValue = value;
                if (ActualWidget != null)
                    ActualWidget.slider.maxValue = value;
            }
        }

        private bool enforceMinMax;
        public bool EnforceMinMax
        {
            get => enforceMinMax;
            set
            {
                if (value == enforceMinMax)
                    return;
                enforceMinMax = value;
                if (value)
                    Value = Mathf.Clamp(Value, minValue, maxValue);
            }
        }

        private float value;
        public float Value
        {
            get => value;
            set
            {
                if (enforceMinMax)
                    value = Mathf.Clamp(value, minValue, maxValue);
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

        public SliderFieldWidgetData WannaBeConstructor(string label, float value, float minValue, float maxValue, bool enforceMinMax = true)
        {
            LabeledWidgetDataConstructor(label);
            this.value = enforceMinMax ? Mathf.Clamp(value, minValue, maxValue) : value;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.enforceMinMax = enforceMinMax;
            return this;
        }
    }
}
