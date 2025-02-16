using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    [SingletonScript("0c974bef0ebc228e7ae9a3818dcca853")]
    public class WidgetManager : UdonSharpBehaviour
    {
        [HideInInspector] [SerializeField] [SingletonReference] private WannaBeClassesManager wannaBeClasses;
        [SerializeField] private GameObject boxWidgetPrefab;
        [SerializeField] private GameObject buttonWidgetPrefab;
        [SerializeField] private GameObject decimalFieldWidgetPrefab;
        [SerializeField] private GameObject foldOutWidgetPrefab;
        [SerializeField] private GameObject groupingWidgetPrefab;
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
        /// <summary>
        /// <para><see cref="string"/> => <see cref="GameObject"/></para>
        /// </summary>
        private DataDictionary widgetPrefabsByName = new DataDictionary();
        /// <summary>
        /// <para><see cref="string"/> => <see cref="DataList"/> of <see cref="Widget"/></para>
        /// </summary>
        private DataDictionary widgetsPools = new DataDictionary();

        private GameObject GetWidgetPrefab(string widgetName) => (GameObject)widgetPrefabsByName[widgetName].Reference;

        bool initialize = false;
        private void Initialize()
        {
            if (initialize)
                return;
            #if GenericValueEditorDebug
            Debug.Log($"[GenericValueEditorDebug] GenericValueEditorManager  Initialize");
            #endif
            initialize = true;

            widgetPrefabsByName.Add("Box", boxWidgetPrefab);
            widgetPrefabsByName.Add("Button", buttonWidgetPrefab);
            widgetPrefabsByName.Add("DecimalField", decimalFieldWidgetPrefab);
            widgetPrefabsByName.Add("FoldOut", foldOutWidgetPrefab);
            widgetPrefabsByName.Add("Grouping", groupingWidgetPrefab);
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

            widgetsPools.Add("Box", new DataList());
            widgetsPools.Add("Button", new DataList());
            widgetsPools.Add("DecimalField", new DataList());
            widgetsPools.Add("FoldOut", new DataList());
            widgetsPools.Add("Grouping", new DataList());
            widgetsPools.Add("Indent", new DataList());
            widgetsPools.Add("IntegerField", new DataList());
            widgetsPools.Add("Label", new DataList());
            widgetsPools.Add("Line", new DataList());
            widgetsPools.Add("MultilineStringField", new DataList());
            widgetsPools.Add("SliderField", new DataList());
            widgetsPools.Add("Space", new DataList());
            widgetsPools.Add("StringField", new DataList());
            widgetsPools.Add("ToggleField", new DataList());
            widgetsPools.Add("Vector2Field", new DataList());
            widgetsPools.Add("Vector3Field", new DataList());
        }

        /// <summary>
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns>May return a disabled object.</returns>
        public Widget GetWidgetInstance(string widgetName)
        {
            Initialize();
            DataList pool = widgetsPools[widgetName].DataList;
            int count = pool.Count;
            if (count != 0)
            {
                int index = count - 1;
                Widget widget = (Widget)pool[index].Reference;
                pool.RemoveAt(index);
                return widget;
            }
            #if GenericValueEditorDebug
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            #endif
            GameObject go = Instantiate(GetWidgetPrefab(widgetName));
            #if GenericValueEditorDebug
            Debug.Log($"[GenericValueEditorDebug] [sw] GenericValueEditorManager  GetWidgetInstance - instantiateMs: {sw.Elapsed.Milliseconds}");
            #endif
            return go.GetComponent<Widget>();
        }

        public void MoveObjectsToPool(Widget[] widgets, int widgetsCount)
        {
            for (int i = 0; i < widgetsCount; i++)
            {
                Widget widget = widgets[i];
                widget.gameObject.SetActive(false);
                string widgetName = widget.WidgetName;
                widgetsPools[widgetName].DataList.Add(widget);
            }
        }

        public WidgetData[] StdMoveWidgetData(WidgetData[] widgetData, int count = -1)
        {
            #if GenericValueEditorDebug
            Debug.Log($"[GenericValueEditorDebug] GenericValueEditor  StdMoveWidgetData");
            #endif
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

        public GroupingWidgetData NewGrouping()
        {
            return wannaBeClasses.New<GroupingWidgetData>(nameof(GroupingWidgetData));
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
