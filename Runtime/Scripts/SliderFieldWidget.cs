using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SliderFieldWidget : LabeledWidget
    {
        public override string WidgetName => "SliderField";
        public SliderFieldWidgetData Data => (SliderFieldWidgetData)BackingWidgetData;

        public Slider slider;
        public TMP_InputField inputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            slider.SetValueWithoutNotify(Data.Value);
            UpdateInputField(Data.Value);
        }

        public void UpdateInputField(float value)
        {
            inputField.SetTextWithoutNotify(value.ToString("0.###"));
        }

        public void OnSliderValueChanged()
        {
            Data.Value = slider.value;
        }

        public void OnInputFieldTextChanged()
        {
            if (float.TryParse(inputField.text, out float value))
                Data.Value = value;
        }
    }
}
