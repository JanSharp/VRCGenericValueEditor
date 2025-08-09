using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    [SingletonScript("0c974bef0ebc228e7ae9a3818dcca853")] // Runtime/Prefabs/WidgetManager.prefab
    public class WidgetManager : UdonSharpBehaviour
    {
        [HideInInspector][SerializeField][SingletonReference] private WannaBeClassesManager wannaBeClasses;
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

        bool initialized = false;
        private void Initialize()
        {
            if (initialized)
                return;
#if GENERIC_VALUE_EDITOR_DEBUG
            Debug.Log($"[GenericValueEditorDebug] WidgetManager  Initialize");
#endif
            initialized = true;

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
        /// <para>Register a custom <see cref="Widget"/> with an associated <paramref name="widgetPrefab"/>
        /// and underlying <see cref="WidgetData"/>.</para>
        /// <para>The <see cref="Widget.WidgetName"/> and <see cref="WidgetData.WidgetName"/> must match
        /// <paramref name="widgetName"/>.</para>
        /// <para><paramref name="widgetPrefab"/>Must have a <see cref="Widget"/> component (the custom
        /// deriving class) on the root game object.</para>
        /// <para>For best consistency also add 2 extension methods.<br/>
        /// One to <see cref="WidgetManager"/> which creates a <c>New</c> instance of the custom
        /// <see cref="WidgetData"/> and potentially calls an appropriate <c>WannaBeConstructor</c>.<br/>
        /// And an extension method to <see cref="GenericValueEditor"/> which casts the
        /// <see cref="GenericValueEditor.sendingWidgetData"/> to the custom <see cref="WidgetData"/>
        /// type.</para>
        /// </summary>
        /// <param name="widgetName">Must be unique.</param>
        /// <param name="widgetPrefab"></param>
        public void RegisterCustomWidget(string widgetName, GameObject widgetPrefab)
        {
#if GENERIC_VALUE_EDITOR_DEBUG
            Debug.Log($"[GenericValueEditorDebug] WidgetManager  RegisterCustomWidget - widgetName: {widgetName}");
#endif
            Initialize();
            if (widgetPrefabsByName.ContainsKey(widgetName))
            {
                Debug.LogError($"[GenericValueEditor] A widget with the name '{widgetName}' already exists, "
                    + "cannot register another with the same name.");
                return;
            }
            widgetPrefabsByName.Add(widgetName, widgetPrefab);
            DataList widgetPool = new DataList();
            ArrList.Add(ref allWidgetPools, ref allWidgetPoolsCount, widgetPool);
            widgetPools.Add(widgetName, widgetPool);
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
#if GENERIC_VALUE_EDITOR_DEBUG
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            GameObject go = Instantiate(GetWidgetPrefab(widgetName));
#if GENERIC_VALUE_EDITOR_DEBUG
            Debug.Log($"[GenericValueEditorDebug] [sw] WidgetManager  GetWidgetInstance - instantiateMs: {sw.Elapsed.Milliseconds}");
#endif
            return go.GetComponent<Widget>();
        }

        /// <summary>
        /// <para>Used by <see cref="GenericValueEditor"/> to return widget instances back to the pool.</para>
        /// </summary>
        /// <param name="widgets"></param>
        /// <param name="widgetsCount"></param>
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

        /// <summary>
        /// <para>Calls <see cref="WannaBeClass.StdMove"/> on each <see cref="WidgetData"/> in
        /// <paramref name="widgetData"/>, optionally limited by <paramref name="count"/>. All of the widgets
        /// must not be <see langword="null"/>.</para>
        /// </summary>
        /// <param name="widgetData"></param>
        /// <param name="count"><c>-1</c> means "use <paramref name="widgetData"/> length".</param>
        /// <returns>Simply a reference to <paramref name="widgetData"/>.</returns>
        public WidgetData[] StdMoveWidgets(WidgetData[] widgetData, int count = -1)
        {
#if GENERIC_VALUE_EDITOR_DEBUG
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

        public DecimalFieldWidgetData NewFloatField(string label, float value, float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
        }

        public DecimalFieldWidgetData NewDoubleField(string label, double value, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
        }

        public DecimalFieldWidgetData NewDecimalField(string label, decimal value, decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue)
        {
            return wannaBeClasses.New<DecimalFieldWidgetData>(nameof(DecimalFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
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

        public IntegerFieldWidgetData NewIntField(string label, int value, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
        }

        public IntegerFieldWidgetData NewUIntField(string label, uint value, uint minValue = uint.MinValue, uint maxValue = uint.MaxValue)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
        }

        public IntegerFieldWidgetData NewLongField(string label, long value, long minValue = long.MinValue, long maxValue = long.MaxValue)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
        }

        public IntegerFieldWidgetData NewULongField(string label, ulong value, ulong minValue = ulong.MinValue, ulong maxValue = ulong.MaxValue)
        {
            return wannaBeClasses.New<IntegerFieldWidgetData>(nameof(IntegerFieldWidgetData))
                .WannaBeConstructor(label, value, minValue, maxValue);
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
