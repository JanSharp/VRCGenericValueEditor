using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GenericValueEditor : UdonSharpBehaviour
    {
        [HideInInspector] [SerializeField] [SingletonReference] private WannaBeClassesManager wannaBeClasses;
        [SerializeField] private Transform widgetsRoot;
        [SerializeField] private GameObject boxWidgetPrefab;
        [SerializeField] private GameObject buttonWidgetPrefab;
        [SerializeField] private GameObject decimalFieldWidgetPrefab;
        [SerializeField] private GameObject foldOutWidgetPrefab;
        [SerializeField] private GameObject indentWidgetPrefab;
        [SerializeField] private GameObject integerFieldWidgetPrefab;
        [SerializeField] private GameObject labelWidgetPrefab;
        [SerializeField] private GameObject lineWidgetPrefab;
        [SerializeField] private GameObject multilineStringFieldWidgetPrefab;
        [SerializeField] private GameObject sliderFieldWidgetPrefab;
        [SerializeField] private GameObject spaceWidgetPrefab;
        [SerializeField] private GameObject stringFieldWidgetPrefab;
        [SerializeField] private GameObject toggleFieldWidgetPrefab;
        [SerializeField] private GameObject vector2FieldWidgetPrefab;
        [SerializeField] private GameObject vector3FieldWidgetPrefab;
        private DataDictionary widgetPrefabsByName = null;
        private DataDictionary WidgetPrefabsByName
        {
            get
            {
                if (widgetPrefabsByName != null)
                    return widgetPrefabsByName;
                widgetPrefabsByName = new DataDictionary(); // "U# Does not yet support initializer lists".
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
                return widgetPrefabsByName;
            }
        }
        private Widget[] widgets = new Widget[ArrList.MinCapacity];
        private int widgetsCount = 0;

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

        private GameObject GetWidgetPrefab(string widgetName) => (GameObject)WidgetPrefabsByName[widgetName].Reference;

        private Transform currentContainer;
        private Transform[] containerStack = new Transform[ArrList.MinCapacity];
        private WidgetData[][] iteratorWidgetsStack = new WidgetData[ArrList.MinCapacity][];
        private int[] iteratorCountStack = new int[ArrList.MinCapacity];
        private int[] iteratorIndexStack = new int[ArrList.MinCapacity];
        private int iteratorStackDepth = 0;

        private void PushWidgetsToIterate(Transform container, WidgetData[] widgetData, int count)
        {
            currentContainer = container;
            ArrList.Add(ref containerStack, ref iteratorStackDepth, container);
            iteratorStackDepth--;
            ArrList.Add(ref iteratorWidgetsStack, ref iteratorStackDepth, widgetData);
            iteratorStackDepth--;
            ArrList.Add(ref iteratorCountStack, ref iteratorStackDepth, count);
            iteratorStackDepth--;
            ArrList.Add(ref iteratorIndexStack, ref iteratorStackDepth, -1);
        }

        private WidgetData Iterate()
        {
            int index;
            while (true)
            {
                if (iteratorStackDepth == 0)
                    return null;
                int count = iteratorCountStack[iteratorStackDepth - 1];
                index = iteratorIndexStack[iteratorStackDepth - 1];
                index++;
                if (index < count)
                    break;
                iteratorStackDepth--;
                iteratorWidgetsStack[iteratorStackDepth] = null; // Make GC happy.
                containerStack[iteratorStackDepth] = null; // Make GC happy.
                currentContainer = iteratorStackDepth == 0 ? null : containerStack[iteratorStackDepth - 1];
            }
            iteratorIndexStack[iteratorStackDepth - 1] = index;
            return iteratorWidgetsStack[iteratorStackDepth - 1][index];
        }

        public void Draw(WidgetData[] widgetData, int count = -1)
        {
            if (count < 0)
                count = widgetData.Length;
            PushWidgetsToIterate(widgetsRoot, widgetData, count);

            int existingIndex = 0;
            Widget existingWidget = existingIndex < widgetsCount ? widgets[existingIndex++] : null;

            Widget[] newWidgets = new Widget[ArrList.MinCapacity];
            int newWidgetsCount = 0;
            while (true)
            {
                WidgetData currentData = Iterate();
                if (currentData == null)
                    break;

                Widget widget;
                if (existingWidget == null || existingWidget.BackingWidgetData.WidgetName != currentData.WidgetName)
                    widget = Instantiate(GetWidgetPrefab(currentData.WidgetName)).GetComponent<Widget>();
                else
                {
                    widget = existingWidget;
                    existingWidget = existingIndex < widgetsCount ? widgets[existingIndex++] : null;
                }

                Transform t = widget.transform;
                t.SetParent(currentContainer, worldPositionStays: false);
                t.SetAsLastSibling();
                // This ultimately calls IncrementRefsCount, allowing the widgetData array to be StdMoved in.
                widget.BackingWidgetData = currentData;
                currentData.genericValueEditor = this;
                ArrList.Add(ref newWidgets, ref newWidgetsCount, widget);

                if (widget.IsContainer)
                    PushWidgetsToIterate(widget.containerWidgetsRoot, currentData.childWidgets, currentData.childWidgetsCount);
            }

            if (existingWidget != null)
                DestroyUnusedWidgets(existingIndex - 1);

            widgets = newWidgets;
            widgetsCount = newWidgetsCount;
        }

        private void DestroyUnusedWidgets(int startingIndex)
        {
            for (int i = startingIndex; i < widgetsCount; i++)
            {
                Widget widget = widgets[i];
                widget.BackingWidgetData = null; // Free WannaBeClass reference.
                Destroy(widget.gameObject);
            }
        }

        public WidgetData[] StdMoveWidgetData(WidgetData[] widgetData, int count = -1)
        {
            if (count < 0)
                count = widgetData.Length;
            for (int i = 0; i < count; i++)
                widgetData[i].StdMove();
            return widgetData;
        }

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
