using UdonSharp;
using UnityEngine;

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

        private float step;
        /// <summary>
        /// <para><c>0</c> means no step, no rounding.</para>
        /// </summary>
        public float Step
        {
            get => step;
            set
            {
                if (value == step)
                    return;
                step = value;
                Value = this.value;
                if (ActualWidget != null)
                    ActualWidget.UpdateSliderStepping();
            }
        }

        private float value;
        public float Value
        {
            get => value;
            set
            {
                if (SetValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetValueWithoutNotify(float value)
        {
            if (step != 0)
                value = Mathf.Round(value / step) * step;
            if (enforceMinMax)
                value = Mathf.Clamp(value, minValue, maxValue);
            if (value == this.value)
                return false;
            this.value = value;
            if (ActualWidget != null)
            {
                ActualWidget.UpdateSlider();
                ActualWidget.UpdateInputField();
            }
            return true;
        }

        /// <summary>
        /// <para><c>0</c> means no step, no rounding.</para>
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public SliderFieldWidgetData SetStep(float step)
        {
            Step = step;
            return this;
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
