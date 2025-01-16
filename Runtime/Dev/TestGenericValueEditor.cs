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
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            BoxWidgetData toggleAbleBox = valueEditor.NewBoxScope();
            toggleAbleBox.IsVisible = false;

            WidgetData[] interactableWidgets = new WidgetData[]
            {
                valueEditor.NewButton("Button"),
                valueEditor.NewDecimalField("Decimal Field", 0m),
                valueEditor.NewFoldOutScope("Fold Out", false),
                valueEditor.NewLine(),
                valueEditor.CloseScope(),
                valueEditor.NewIntField("Integer Field", 0),
                valueEditor.NewMultilineStringField("Multiline String Field", ""),
                valueEditor.NewSliderField("Slider field", 0f, 0f, 1f),
                valueEditor.NewStringField("String Field", ""),
                valueEditor.NewToggleField("Toggle Field", false),
                valueEditor.NewVector2Field("Vector2 Field", Vector2.zero),
                valueEditor.NewVector3Field("Vector3 Field", Vector3.zero),
            };

            WidgetData[] widgets = new WidgetData[]
            {
                valueEditor.NewFoldOutScope("My Boxes and Stuff", false),
                valueEditor.NewBoxScope(),
                valueEditor.NewButton("My Button")
                    .SetListener(this, nameof(OnButtonClicked))
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
                valueEditor.CloseScope(),

                valueEditor.NewFoldOutScope("My Strings", false),
                valueEditor.NewMultilineStringField("My Multiline String", "Hello World!\nGoodbye World.")
                    .SetListener(this, nameof(OnStringFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Multiline String"),
                valueEditor.NewStringField("My String", "Greetings")
                    .SetListener(this, nameof(OnStringFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My String"),
                valueEditor.CloseScope(),

                valueEditor.NewSliderField("My Slider", 0.5f, 0f, 2f)
                    .SetListener(this, nameof(OnSliderFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Slider"),
                valueEditor.NewSliderField("My Unrestricted Slider", 0.5f, -1f, 1f, enforceMinMax: false)
                    .SetListener(this, nameof(OnSliderFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Unrestricted Slider"),
                valueEditor.NewToggleField("My Toggle", true)
                    .SetListener(this, nameof(OnToggleFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Toggle"),

                valueEditor.NewFoldOutScope("My Integers", false),
                valueEditor.NewIntField("My Int", -2000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Int"),
                valueEditor.NewUIntField("My UInt", 2000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My UInt"),
                valueEditor.NewSpace(),
                valueEditor.NewLongField("My Long", -2000000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Long"),
                valueEditor.NewULongField("My ULong", 2000000)
                    .SetListener(this, nameof(OnIntegerFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My ULong"),
                valueEditor.CloseScope(),

                valueEditor.NewFoldOutScope("My Decimals", false),
                valueEditor.NewFloatField("My Float", 0.5f)
                    .SetListener(this, nameof(OnDecimalFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Float"),
                valueEditor.NewDoubleField("My Double", 1234.56789)
                    .SetListener(this, nameof(OnDecimalFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Double"),
                valueEditor.NewDecimalField("My Decimal", 123456789.123456789m)
                    .SetListener(this, nameof(OnDecimalFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Decimal"),
                valueEditor.CloseScope(),

                valueEditor.NewFoldOutScope("My Vectors", false),
                valueEditor.NewVector2Field("My Vector2", new Vector2(100, 200))
                    .SetListener(this, nameof(OnVector2FieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Vector2"),
                valueEditor.NewVector3Field("My Vector3", new Vector3(100, 200, 300))
                    .SetListener(this, nameof(OnVector3FieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Vector3"),
                valueEditor.CloseScope(),

                valueEditor.NewToggleField("Show More", toggleAbleBox.IsVisible)
                    .SetListener(this, nameof(OnWidgetToggleValueChanged))
                    .SetCustomData(nameof(widgetToToggle), toggleAbleBox),
                valueEditor.NewToggleField("Interactable", toggleAbleBox.Interactable)
                    .SetListener(this, nameof(OnWidgetInteractableToggleValueChanged))
                    .SetCustomData(nameof(allInteractableWidgets), interactableWidgets),
                toggleAbleBox,
                // If there's ever proof that something has a bad API, it's if you're encouraged to do dumb
                // stuff like this. I do blame Udon partially, but I can improve this API, surely.
                interactableWidgets[0],
                interactableWidgets[1],
                interactableWidgets[2],
                interactableWidgets[3],
                interactableWidgets[4],
                interactableWidgets[5],
                interactableWidgets[6],
                interactableWidgets[7],
                interactableWidgets[8],
                interactableWidgets[9],
                interactableWidgets[10],
                interactableWidgets[11],
                valueEditor.NewLabel("Hello World!"),
                valueEditor.NewLine(),
                valueEditor.NewLabel("There's so much content in this box."),
                valueEditor.CloseScope(),
            };
            Debug.Log($"[GenericValueEditor] Creating widget data took {sw.Elapsed}.");
            valueEditor.Draw(valueEditor.StdMoveWidgetData(widgets));
            Debug.Log($"[GenericValueEditor] Draw took {sw.Elapsed}.");
        }

        private string fieldName;

        public void OnButtonClicked()
        {
            Debug.Log($"[GenericValueEditor] Clicked {fieldName}.");
        }

        public void OnDecimalFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingDecimalField().GetValueAsString()}.");
        }

        public void OnIntegerFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingIntegerField().GetValueAsString()}.");
        }

        public void OnSliderFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingSliderField().Value}.");
        }

        public void OnStringFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingStringField().Value}.");
        }

        public void OnToggleFieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingToggleField().Value}.");
        }

        public void OnVector2FieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingVector2Field().Value}.");
        }

        public void OnVector3FieldValueChanged()
        {
            Debug.Log($"[GenericValueEditor] Value for {fieldName} changed to {valueEditor.GetSendingVector3Field().Value}.");
        }

        private WidgetData widgetToToggle;
        public void OnWidgetToggleValueChanged()
        {
            widgetToToggle.IsVisible = valueEditor.GetSendingToggleField().Value;
        }

        private WidgetData[] allInteractableWidgets;
        public void OnWidgetInteractableToggleValueChanged()
        {
            foreach (WidgetData widget in allInteractableWidgets)
                widget.Interactable = valueEditor.GetSendingToggleField().Value;
        }
    }
}
