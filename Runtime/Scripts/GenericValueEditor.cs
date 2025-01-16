using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [DefaultExecutionOrder(-10000)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GenericValueEditor : UdonSharpBehaviour
    {
        [HideInInspector] [SerializeField] [SingletonReference] private WannaBeClassesManager wannaBeClasses;
        public Transform widgetsRoot;
        public GameObject boxWidgetPrefab;
        public GameObject buttonWidgetPrefab;
        public GameObject decimalFieldWidgetPrefab;
        public GameObject foldOutWidgetPrefab;
        public GameObject indentWidgetPrefab;
        public GameObject integerFieldWidgetPrefab;
        public GameObject labelWidgetPrefab;
        public GameObject lineWidgetPrefab;
        public GameObject multilineStringFieldWidgetPrefab;
        public GameObject sliderFieldWidgetPrefab;
        public GameObject spaceWidgetPrefab;
        public GameObject stringFieldWidgetPrefab;
        public GameObject toggleFieldWidgetPrefab;
        public GameObject vector2FieldWidgetPrefab;
        public GameObject vector3FieldWidgetPrefab;
        private DataDictionary widgetPrefabsByName = new DataDictionary();
        private Widget[] widgets = new Widget[0];

        public WidgetData sendingWidgetData;
        public BoxWidgetData GetSendingBox() => (BoxWidgetData)sendingWidgetData;
        public ButtonWidgetData GetSendingButton() => (ButtonWidgetData)sendingWidgetData;
        public DecimalFieldWidgetData GetSendingDecimalField() => (DecimalFieldWidgetData)sendingWidgetData;
        public FoldOutWidgetData GetSendingFoldOut() => (FoldOutWidgetData)sendingWidgetData;
        public IndentWidgetData GetSendingIndent() => (IndentWidgetData)sendingWidgetData;
        public IntegerFieldWidgetData GetSendingIntegerField() => (IntegerFieldWidgetData)sendingWidgetData;
        public LabelWidgetData GetSendingLabel() => (LabelWidgetData)sendingWidgetData;
        public LineWidgetData GetSendingLine() => (LineWidgetData)sendingWidgetData;
        public MultilineStringFieldWidgetData GetSendingMultilineStringField() => (MultilineStringFieldWidgetData)sendingWidgetData;
        public SliderFieldWidgetData GetSendingSliderField() => (SliderFieldWidgetData)sendingWidgetData;
        public SpaceWidgetData GetSendingSpace() => (SpaceWidgetData)sendingWidgetData;
        public StringFieldWidgetData GetSendingStringField() => (StringFieldWidgetData)sendingWidgetData;
        public ToggleFieldWidgetData GetSendingToggleField() => (ToggleFieldWidgetData)sendingWidgetData;
        public Vector2FieldWidgetData GetSendingVector2Field() => (Vector2FieldWidgetData)sendingWidgetData;
        public Vector3FieldWidgetData GetSendingVector3Field() => (Vector3FieldWidgetData)sendingWidgetData;

        private ScopeClosingWidgetData scopeClosingWidgetData;

        private void Start()
        {
            widgetPrefabsByName.Add("Box", boxWidgetPrefab);
            widgetPrefabsByName.Add("Button", buttonWidgetPrefab);
            widgetPrefabsByName.Add("DecimalField", decimalFieldWidgetPrefab);
            widgetPrefabsByName.Add("FoldOut", foldOutWidgetPrefab);
            widgetPrefabsByName.Add("Indent", indentWidgetPrefab);
            widgetPrefabsByName.Add("IntegerField", integerFieldWidgetPrefab);
            widgetPrefabsByName.Add("Label", labelWidgetPrefab);
            widgetPrefabsByName.Add("Line", lineWidgetPrefab);
            widgetPrefabsByName.Add("MultilineStringField", multilineStringFieldWidgetPrefab);
            widgetPrefabsByName.Add("SliderField", sliderFieldWidgetPrefab);
            widgetPrefabsByName.Add("Space", spaceWidgetPrefab);
            widgetPrefabsByName.Add("StringField", stringFieldWidgetPrefab);
            widgetPrefabsByName.Add("ToggleField", toggleFieldWidgetPrefab);
            widgetPrefabsByName.Add("Vector2Field", vector2FieldWidgetPrefab);
            widgetPrefabsByName.Add("Vector3Field", vector3FieldWidgetPrefab);
            scopeClosingWidgetData = wannaBeClasses.New<ScopeClosingWidgetData>(nameof(ScopeClosingWidgetData));
        }

        private GameObject GetWidgetPrefab(string widgetName) => (GameObject)widgetPrefabsByName[widgetName].Reference;

        public void Draw(WidgetData[] widgetData, int count = -1)
        {
            // This function is way too big, way too mountainous, and thanks to he beauty of Udon I can't do
            // anything about it without sacrificing performance.

            if (count < 0)
                count = widgetData.Length;

            Transform[] widgetContainerStack = new Transform[ArrList.MinCapacity];
            int widgetContainerStackCount = 0;
            Transform currentWidgetContainer = widgetsRoot;

            Widget[] newWidgets = new Widget[count];
            int newIndex = 0;
            int existingIndex = 0;
            int existingCount = widgets.Length;
            Widget existingWidget = existingIndex < existingCount ? widgets[existingIndex++] : null;
            for (int i = 0; i < count; i++)
            {
                WidgetData currentData = widgetData[i];
                if (currentData == scopeClosingWidgetData)
                {
                    if (widgetContainerStackCount == 0)
                    {
                        Debug.LogError($"[GenericValueEditor] Attempt to close more scoped widget containers than there are open, index: {i}.");
                        return;
                    }
                    currentWidgetContainer = ArrList.RemoveAt(ref widgetContainerStack, ref widgetContainerStackCount, widgetContainerStackCount - 1);
                    newWidgets[i] = null;
                    continue;
                }

                Widget widget;
                if (existingWidget == null || existingWidget.BackingWidgetData.WidgetName != currentData.WidgetName)
                    widget = Instantiate(GetWidgetPrefab(currentData.WidgetName)).GetComponent<Widget>();
                else
                {
                    widget = existingWidget;
                    do
                    {
                        if (existingIndex >= existingCount)
                        {
                            existingWidget = null;
                            break;
                        }
                        existingWidget = widgets[existingIndex++];
                    }
                    while (existingWidget == null);
                }

                Transform t = widget.transform;
                t.SetParent(currentWidgetContainer, worldPositionStays: false);
                t.SetAsLastSibling();
                // This ultimately calls IncrementRefsCount, allowing the widgetData array to be StdMoved in.
                widget.BackingWidgetData = currentData;
                currentData.genericValueEditor = this;
                newWidgets[newIndex++] = widget;

                if (widget.IsContainer)
                {
                    ArrList.Add(ref widgetContainerStack, ref widgetContainerStackCount, currentWidgetContainer);
                    currentWidgetContainer = widget.containerWidgetsRoot;
                }
            }

            if (widgetContainerStackCount != 0)
                Debug.LogError($"[GenericValueEditor] Did not close all scoped widget containers.");

            if (existingWidget != null)
                while (true)
                {
                    existingWidget.BackingWidgetData = null; // Free backing data wanna be class.
                    Destroy(existingWidget.gameObject);
                    if (existingIndex >= existingCount)
                        break;
                    existingWidget = widgets[existingIndex++];
                }

            widgets = newWidgets;
        }

        public WidgetData[] StdMoveWidgetData(WidgetData[] widgetData, int count = -1)
        {
            if (count < 0)
                count = widgetData.Length;
            for (int i = 0; i < count; i++)
                widgetData[i].StdMove();
            return widgetData;
        }

        public ScopeClosingWidgetData CloseScope() => scopeClosingWidgetData;

        public BoxWidgetData NewBoxScope()
        {
            return wannaBeClasses.New<BoxWidgetData>(nameof(BoxWidgetData));
        }

        public ButtonWidgetData NewButton(string label)
        {
            return wannaBeClasses.New<ButtonWidgetData>(nameof(ButtonWidgetData))
                .WannaBeConstructor(label);
        }

        public DecimalFieldWidgetData NewFloatField(string label, float value)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public DecimalFieldWidgetData NewDoubleField(string label, double value)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public DecimalFieldWidgetData NewDecimalField(string label, decimal value)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public FoldOutWidgetData NewFoldOutScope(string label, bool foldedOut)
        {
            return wannaBeClasses.New<FoldOutWidgetData>(nameof(FoldOutWidgetData))
                .WannaBeConstructor(label, foldedOut);
        }

        public IndentWidgetData NewIndentScope()
        {
            return wannaBeClasses.New<IndentWidgetData>(nameof(IndentWidgetData));
        }

        public IntegerFieldWidgetData NewIntField(string label, int value)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public IntegerFieldWidgetData NewUIntField(string label, uint value)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public IntegerFieldWidgetData NewLongField(string label, long value)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public IntegerFieldWidgetData NewULongField(string label, ulong value)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public LabelWidgetData NewLabel(string label)
        {
            return wannaBeClasses.New<LabelWidgetData>(nameof(LabelWidgetData))
                .WannaBeConstructor(label);
        }

        public LineWidgetData NewLine()
        {
            return wannaBeClasses.New<LineWidgetData>(nameof(LineWidgetData));
        }

        public MultilineStringFieldWidgetData NewMultilineStringField(string label, string value)
        {
            MultilineStringFieldWidgetData result = wannaBeClasses.New<MultilineStringFieldWidgetData>(nameof(MultilineStringFieldWidgetData));
            result.WannaBeConstructor(label, value);
            return result;
        }

        public SliderFieldWidgetData NewSliderField(string label, float value, float minValue, float maxValue, bool enforceMinMax = true)
        {
            return wannaBeClasses.New<SliderFieldWidgetData>(nameof(SliderFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue, enforceMinMax);
        }

        public SpaceWidgetData NewSpace()
        {
            return wannaBeClasses.New<SpaceWidgetData>(nameof(SpaceWidgetData));
        }

        public StringFieldWidgetData NewStringField(string label, string value)
        {
            return wannaBeClasses.New<StringFieldWidgetData>(nameof(StringFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public ToggleFieldWidgetData NewToggleField(string label, bool value)
        {
            return wannaBeClasses.New<ToggleFieldWidgetData>(nameof(ToggleFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public Vector2FieldWidgetData NewVector2Field(string label, Vector2 value)
        {
            return wannaBeClasses.New<Vector2FieldWidgetData>(nameof(Vector2FieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public Vector3FieldWidgetData NewVector3Field(string label, Vector3 value)
        {
            return wannaBeClasses.New<Vector3FieldWidgetData>(nameof(Vector3FieldWidgetData))
                .WannaBeConstructor(label, value);
        }
    }
}
