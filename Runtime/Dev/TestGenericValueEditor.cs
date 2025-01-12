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
                valueEditor.NewMultilineStringField("My Multiline String", "Hello World!\nGoodbye World.")
                    .SetListener(this, nameof(OnMultilineStringFieldValueChanged), nameof(multilineStringField))
                    .SetCustomData(nameof(fieldName), "My Multiline String"),
                valueEditor.NewSliderField("My Slider", 0.5f)
                    .SetListener(this, nameof(OnSliderFieldValueChanged), nameof(sliderField))
                    .SetCustomData(nameof(fieldName), "My Slider"),
                valueEditor.NewStringField("My String", "Greetings")
                    .SetListener(this, nameof(OnStringFieldValueChanged), nameof(stringField))
                    .SetCustomData(nameof(fieldName), "My String"),
                valueEditor.NewToggleField("My Toggle", true)
                    .SetListener(this, nameof(OnToggleFieldValueChanged), nameof(toggleField))
                    .SetCustomData(nameof(fieldName), "My Toggle"),
                valueEditor.NewIntField("My Int", -2000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged), nameof(integerField))
                    .SetCustomData(nameof(fieldName), "My Int"),
                valueEditor.NewUIntField("My UInt", 2000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged), nameof(integerField))
                    .SetCustomData(nameof(fieldName), "My UInt"),
                valueEditor.NewLongField("My Long", -2000000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged), nameof(integerField))
                    .SetCustomData(nameof(fieldName), "My Long"),
                valueEditor.NewULongField("My ULong", 2000000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged), nameof(integerField))
                    .SetCustomData(nameof(fieldName), "My ULong"),
            }));
        }

        private string fieldName;

        private ButtonWidgetData button;
        public void OnButtonClicked()
        {
            Debug.Log($"[GenericValueEditor] Clicked {fieldName}.");
        }

        private IntegerFieldWidgetData integerField;
        public void OnIntegerFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {integerField.GetValueAsString()}.");
        }

        private MultilineStringFieldWidgetData multilineStringField;
        public void OnMultilineStringFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {multilineStringField.Value}.");
        }

        private ToggleFieldWidgetData sliderField;
        public void OnSliderFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {sliderField.Value}.");
        }

        private StringFieldWidgetData stringField;
        public void OnStringFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {stringField.Value}.");
        }

        private ToggleFieldWidgetData toggleField;
        public void OnToggleFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {toggleField.Value}.");
        }
    }
}
