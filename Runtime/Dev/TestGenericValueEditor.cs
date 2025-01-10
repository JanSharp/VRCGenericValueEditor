using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TestGenericValueEditor : UdonSharpBehaviour
    {
        public GenericValueEditor valueEditor;

        private void Start()
        {
            valueEditor.Draw(valueEditor.StdMoveWidgetData(new WidgetData[]
            {
                valueEditor.NewBoxScope(),
                valueEditor.NewButton("My Button")
                    .SetListener(this, nameof(OnButtonClicked), nameof(button))
                    .SetCustomData(nameof(fieldName), "My Button"),
                valueEditor.NewLabel("My Label"),
                valueEditor.NewIndentScope(),
                valueEditor.NewLine(),
                valueEditor.NewLabel("My Label"),
                valueEditor.NewLabel("My Label"),
                valueEditor.NewLine(),
                valueEditor.CloseScope(),
                valueEditor.NewLine(),
                valueEditor.NewLabel("My Label"),
                valueEditor.CloseScope(),
                valueEditor.NewSliderField("My Slider", 0.5f)
                    .SetListener(this, nameof(OnSliderFieldValueChanged), nameof(sliderField))
                    .SetCustomData(nameof(fieldName), "My Slider"),
                valueEditor.NewToggleField("My Toggle", true)
                    .SetListener(this, nameof(OnToggleFieldValueChanged), nameof(toggleField))
                    .SetCustomData(nameof(fieldName), "My Toggle"),
            }));
        }

        private string fieldName;

        private ButtonWidgetData button;
        public void OnButtonClicked()
        {
            Debug.Log($"[GenericValueEditor] Clicked {fieldName}.");
        }

        private ToggleFieldWidgetData sliderField;
        public void OnSliderFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {sliderField.Value}.");
        }

        private ToggleFieldWidgetData toggleField;
        public void OnToggleFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {toggleField.Value}.");
        }
    }
}
