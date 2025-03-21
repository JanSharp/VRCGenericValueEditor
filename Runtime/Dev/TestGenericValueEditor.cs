using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TestGenericValueEditor : UdonSharpBehaviour
    {
        [HideInInspector] [SerializeField] [SingletonReference] private WidgetManager widgetManager;
        public GenericValueEditor valueEditor;

        private void Start()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            BoxWidgetData toggleAbleBox = (BoxWidgetData)widgetManager.NewBoxScope().SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
            {
                widgetManager.NewButton("Button"),
                widgetManager.NewDecimalField("Decimal Field", 0m),
                widgetManager.NewFoldOutScope("Fold Out", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewLine(),
                })),
                widgetManager.NewIntField("Integer Field", 0),
                widgetManager.NewMultilineStringField("Multiline String Field", ""),
                widgetManager.NewSliderField("Slider field", 0f, 0f, 1f),
                widgetManager.NewStringField("String Field", ""),
                widgetManager.NewToggleField("Toggle Field", false),
                widgetManager.NewVector2Field("Vector2 Field", Vector2.zero),
                widgetManager.NewVector3Field("Vector3 Field", Vector3.zero),
                widgetManager.NewLabel("Hello World!"),
                widgetManager.NewLine(),
                widgetManager.NewLabel("There's so much content in this box."),
            }));
            toggleAbleBox.IsVisible = false;

            WidgetData[] widgets = new WidgetData[]
            {
                widgetManager.NewFoldOutScope("My Boxes and Stuff", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewBoxScope().SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                    {
                        widgetManager.NewButton("My Button")
                            .SetListener(this, nameof(OnButtonClicked))
                            .SetCustomData(nameof(fieldName), "My Button"),
                        widgetManager.NewLabel("My Label"),
                        widgetManager.NewIndentScope().SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                        {
                            widgetManager.NewGrouping().SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                            {
                                widgetManager.NewLine(),
                                widgetManager.NewLabel("My Label"),
                                widgetManager.NewLabel("My Label"),
                                widgetManager.NewLine(),
                            })),
                        })),
                        widgetManager.NewLine(),
                        widgetManager.NewLabel("My Label"),
                    })),
                })),

                widgetManager.NewFoldOutScope("My Strings", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewMultilineStringField("My Multiline String", "Hello World!\nGoodbye World.")
                        .SetListener(this, nameof(OnStringFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Multiline String"),
                    widgetManager.NewStringField("My String", "Greetings")
                        .SetListener(this, nameof(OnStringFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My String"),
                })),

                widgetManager.NewSliderField("My Slider", 0.5f, 0f, 2f)
                    .SetListener(this, nameof(OnSliderFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Slider"),
                widgetManager.NewSliderField("My Unrestricted Slider", 0.5f, -1f, 1f, enforceMinMax: false)
                    .SetListener(this, nameof(OnSliderFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Unrestricted Slider"),
                widgetManager.NewSliderField("My Stepped Slider", 0.5f, -5f, 10f).SetStep(0.5f)
                    .SetListener(this, nameof(OnSliderFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Stepped Slider"),
                widgetManager.NewToggleField("My Toggle", true)
                    .SetListener(this, nameof(OnToggleFieldValueChanged))
                    .SetCustomData(nameof(fieldName), "My Toggle"),

                widgetManager.NewFoldOutScope("My Integers", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewIntField("My Int", -2000)
                        .SetListener(this, nameof(OnIntegerFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Int"),
                    widgetManager.NewUIntField("My UInt", 2000u, 1000u, 10000u)
                        .SetListener(this, nameof(OnIntegerFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My UInt"),
                    widgetManager.NewSpace(),
                    widgetManager.NewLongField("My Long", -2000000)
                        .SetListener(this, nameof(OnIntegerFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Long"),
                    widgetManager.NewULongField("My ULong", 2000000)
                        .SetListener(this, nameof(OnIntegerFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My ULong"),
                })),

                widgetManager.NewFoldOutScope("My Decimals", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewFloatField("My Float", 0.5f, -20f, 20f)
                        .SetListener(this, nameof(OnDecimalFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Float"),
                    widgetManager.NewDoubleField("My Double", 1234.56789)
                        .SetListener(this, nameof(OnDecimalFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Double"),
                    widgetManager.NewDecimalField("My Decimal", 123456789.123456789m)
                        .SetListener(this, nameof(OnDecimalFieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Decimal"),
                })),

                widgetManager.NewFoldOutScope("My Vectors", false).SetChildrenChained(widgetManager.StdMoveWidgets(new WidgetData[]
                {
                    widgetManager.NewVector2Field("My Vector2", new Vector2(100, 200))
                        .SetListener(this, nameof(OnVector2FieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Vector2"),
                    widgetManager.NewVector3Field("My Vector3", new Vector3(100, 200, 300))
                        .SetListener(this, nameof(OnVector3FieldValueChanged))
                        .SetCustomData(nameof(fieldName), "My Vector3"),
                })),

                widgetManager.NewToggleField("Show More", toggleAbleBox.IsVisible)
                    .SetListener(this, nameof(OnWidgetToggleValueChanged))
                    .SetCustomData(nameof(widgetToToggle), toggleAbleBox),
                widgetManager.NewToggleField("Interactable", toggleAbleBox.Interactable)
                    .SetListener(this, nameof(OnWidgetInteractableToggleValueChanged))
                    .SetCustomData(nameof(widgetToToggle), toggleAbleBox),
                toggleAbleBox,
            };
            Debug.Log($"[GenericValueEditor] Creating widget data took {sw.Elapsed.TotalMilliseconds}ms.");
            valueEditor.Draw(widgetManager.StdMoveWidgets(widgets));
            Debug.Log($"[GenericValueEditor] Draw took {sw.Elapsed.TotalMilliseconds}ms.");
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

        public void OnWidgetInteractableToggleValueChanged()
        {
            // Test individual disables, doing the whole box would just test the canvas groups working.
            for (int i = 0; i < widgetToToggle.childWidgetsCount; i++)
                widgetToToggle.childWidgets[i].Interactable = valueEditor.GetSendingToggleField().Value;
        }
    }
}
