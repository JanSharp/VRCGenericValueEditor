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
        private DataDictionary widgetPools = new DataDictionary();
        private DataList[] allWidgetPools = new DataList[ArrList.MinCapacity];
        private int allWidgetPoolsCount = 0;
        private int pooledWidgetsCount = 0;
        private bool cleanupLoopIsRunning = false;
        private int nextCleanupLoopPoolIndex = 0;

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

            allWidgetPoolsCount = widgetPrefabsByName.Count;
            ArrList.EnsureCapacity(ref allWidgetPools, allWidgetPoolsCount);
            for (int i = 0; i < allWidgetPoolsCount; i++)
                allWidgetPools[i] = new DataList();

            int j = 0;
            widgetPools.Add("Box", allWidgetPools[j++]);
            widgetPools.Add("Button", allWidgetPools[j++]);
            widgetPools.Add("DecimalField", allWidgetPools[j++]);
            widgetPools.Add("FoldOut", allWidgetPools[j++]);
            widgetPools.Add("Grouping", allWidgetPools[j++]);
            widgetPools.Add("Indent", allWidgetPools[j++]);
            widgetPools.Add("IntegerField", allWidgetPools[j++]);
            widgetPools.Add("Label", allWidgetPools[j++]);
            widgetPools.Add("Line", allWidgetPools[j++]);
            widgetPools.Add("MultilineStringField", allWidgetPools[j++]);
            widgetPools.Add("SliderField", allWidgetPools[j++]);
            widgetPools.Add("Space", allWidgetPools[j++]);
            widgetPools.Add("StringField", allWidgetPools[j++]);
            widgetPools.Add("ToggleField", allWidgetPools[j++]);
            widgetPools.Add("Vector2Field", allWidgetPools[j++]);
            widgetPools.Add("Vector3Field", allWidgetPools[j++]);
        }

        /// <summary>
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns>May return a disabled object.</returns>
        public Widget GetWidgetInstance(string widgetName)
        {
            Initialize();
            DataList pool = widgetPools[widgetName].DataList;
            int count = pool.Count;
            if (count != 0)
            {
                pooledWidgetsCount--;
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
            // Does not need to call Initialize because GetWidgetInstance does and it's the only way to get widget instances.
            pooledWidgetsCount += widgetsCount;
            Transform managerTransform = this.transform;
            for (int i = 0; i < widgetsCount; i++)
            {
                Widget widget = widgets[i];
                widget.BackingWidgetData = null;
                widget.gameObject.SetActive(false);
                widget.transform.SetParent(managerTransform, worldPositionStays: false);
                string widgetName = widget.WidgetName;
                widgetPools[widgetName].DataList.Add(widget);
            }
        }

        private void StartCleanupLoop()
        {
            if (cleanupLoopIsRunning)
                return;
            cleanupLoopIsRunning = true;
            SendCustomEventDelayedSeconds(nameof(InternalCleanupLoop), 240f);
        }

        public void InternalCleanupLoop()
        {
            nextCleanupLoopPoolIndex = nextCleanupLoopPoolIndex % allWidgetPoolsCount;
            DataList pool = allWidgetPools[nextCleanupLoopPoolIndex];
            int countToDelete = 1 + pooledWidgetsCount / 10;
            int count = pool.Count;
            int stopIndex = System.Math.Max(0, count - countToDelete);
            for (int i = count - 1; i >= stopIndex; i--)
                Destroy(((Widget)pool[i].Reference).gameObject);
            nextCleanupLoopPoolIndex++;
            SendCustomEventDelayedSeconds(nameof(InternalCleanupLoop), 1f);
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
