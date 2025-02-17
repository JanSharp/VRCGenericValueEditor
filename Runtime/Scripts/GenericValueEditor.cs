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
        [HideInInspector] [SerializeField] [SingletonReference] private WidgetManager widgetManager;
        public Transform widgetsRoot;
        private Widget[] widgets = new Widget[ArrList.MinCapacity];
        private int widgetsCount = 0;

        [System.NonSerialized] public WidgetData sendingWidgetData;
        public BoxWidgetData GetSendingBox() => (BoxWidgetData)sendingWidgetData;
        public ButtonWidgetData GetSendingButton() => (ButtonWidgetData)sendingWidgetData;
        public DecimalFieldWidgetData GetSendingDecimalField() => (DecimalFieldWidgetData)sendingWidgetData;
        public FoldOutWidgetData GetSendingFoldOut() => (FoldOutWidgetData)sendingWidgetData;
        public GroupingWidgetData GetSendingGrouping() => (GroupingWidgetData)sendingWidgetData;
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

        private Transform currentContainer;
        private Transform[] containerStack = new Transform[ArrList.MinCapacity];
        private WidgetData[][] iteratorWidgetsStack = new WidgetData[ArrList.MinCapacity][];
        private int[] iteratorCountStack = new int[ArrList.MinCapacity];
        private int[] iteratorIndexStack = new int[ArrList.MinCapacity];
        private int iteratorStackDepth = 0;

        private void PushWidgetsToIterate(Transform container, WidgetData[] widgetData, int count)
        {
            #if GenericValueEditorDebug
            Debug.Log($"[GenericValueEditorDebug] GenericValueEditor  PushWidgetsToIterate");
            #endif
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
            #if GenericValueEditorDebug
            Debug.Log($"[GenericValueEditorDebug] [sw] GenericValueEditor  Draw");
            #endif
            widgetManager.MoveObjectsToPool(widgets, widgetsCount);
            ArrList.Clear(ref widgets, ref widgetsCount);

            if (count < 0)
                count = widgetData.Length;
            PushWidgetsToIterate(widgetsRoot, widgetData, count);

            while (true)
            {
                WidgetData currentData = Iterate();
                if (currentData == null)
                    break;
                Widget widget = widgetManager.GetWidgetInstance(currentData.WidgetName);
                Transform t = widget.transform;
                t.SetParent(currentContainer, worldPositionStays: false);
                t.SetAsLastSibling();
                // This ultimately calls IncrementRefsCount, allowing the widgetData array to be StdMoved in.
                widget.BackingWidgetData = currentData;
                currentData.genericValueEditor = this;
                ArrList.Add(ref widgets, ref widgetsCount, widget);

                if (widget.IsContainer)
                    PushWidgetsToIterate(widget.containerWidgetsRoot, currentData.childWidgets, currentData.childWidgetsCount);
            }
        }
    }
}
