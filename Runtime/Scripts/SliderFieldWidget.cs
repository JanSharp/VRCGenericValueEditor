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
            // Unfortunately 'value - (value % 0.001f)' was not precise enough, it had results like 0.8200001
            // (actually 0.820000112056732177734375) even though 0.819999992847442626953125 is the more
            // accurate representation for 0.82 - not only more accurate, there's also
            // 0.82000005245208740234375 which is a step in between those 2 values. So it's actually 2 steps
            // away from the most accurate representation.
            // Tool used: https://www.h-schmidt.net/FloatConverter/IEEE754.html
            Data.Value = float.Parse(slider.value.ToString("0.###"));
        }

        public void OnInputFieldTextChanged()
        {
            if (float.TryParse(inputField.text, out float value))
                Data.Value = value;
        }
    }
}
