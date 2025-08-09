using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SliderFieldWidget : LabeledWidget
    {
        public override string WidgetName => "SliderField";
        public SliderFieldWidgetData Data => (SliderFieldWidgetData)BackingWidgetData;

        public Slider slider;
        public TMP_InputField inputField;
        private bool hasStep;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateSliderStepping();
            UpdateInputField();
        }

        public override void UpdateInteractable()
        {
            base.UpdateInteractable();
            bool interactable = Data.Interactable;
            slider.interactable = interactable;
            inputField.interactable = interactable;
        }

        public void UpdateSliderStepping()
        {
            float step = Data.Step;
            hasStep = step != 0f;
            slider.wholeNumbers = hasStep;
            float minValue = Data.MinValue;
            float maxValue = Data.MaxValue;
            if (hasStep)
            {
                minValue = Mathf.Round(minValue / step);
                maxValue = Mathf.Round(maxValue / step);
            }
            UpdateSlider(); // To prevent the next 2 property setters from raising a value change event.
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            UpdateSlider(); // To fix the value after the previous 2 setters.
            // Currently do not know why the value changes to a seemingly random new value when changing min/max.
        }

        public void UpdateSlider()
        {
            slider.SetValueWithoutNotify(hasStep ? Data.Value / Data.Step : Data.Value);
        }

        // private string GetFormat()
        // {
        //     if (!hasStep)
        //         return "0.###";
        //     float step = Data.Step;
        //     if (step < 0.01f)
        //         return "0.###";
        //     if (step < 0.1f)
        //         return "0.##";
        //     if (step < 1f)
        //         return "0.#";
        //     return "0";
        // }

        public void UpdateInputField()
        {
            inputField.SetTextWithoutNotify(Data.Value.ToString("0.###"));
        }

        public void OnSliderValueChanged()
        {
            // Unfortunately 'value - (value % 0.001f)' was not precise enough, it had results like 0.8200001
            // (actually 0.820000112056732177734375) even though 0.819999992847442626953125 is the more
            // accurate representation for 0.82 - not only more accurate, there's also
            // 0.82000005245208740234375 which is a step in between those 2 values. So it's actually 2 steps
            // away from the most accurate representation.
            // Tool used: https://www.h-schmidt.net/FloatConverter/IEEE754.html
            float value = float.Parse(slider.value.ToString("0.###"));
            Data.Value = hasStep ? value * Data.Step : value;
        }

        public void OnInputFieldTextChanged()
        {
            if (raisingTextChanged)
                return;
            raisingTextChanged = true;
            SendCustomEventDelayedFrames(nameof(OnInputFieldTextChangedDelayed), 1);
        }

        private bool raisingTextChanged = false;
        public void OnInputFieldTextChangedDelayed()
        {
            raisingTextChanged = false;
            Data.Value = float.TryParse(inputField.text, out float value) ? value : 0f;
            UpdateInputField(); // To handle when the value didn't change, but text does differ.
        }
    }
}
