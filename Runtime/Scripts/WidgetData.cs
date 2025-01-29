using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class WidgetData : WannaBeClass
    {
        public abstract string WidgetName { get; }
        [System.NonSerialized] public Widget widget;
        [System.NonSerialized] public GenericValueEditor genericValueEditor;

        [System.NonSerialized] public UdonSharpBehaviour listener;
        [System.NonSerialized] public string listenerEventName;

        [System.NonSerialized] public string customDataFieldName;
        [System.NonSerialized] public object customData;

        [System.NonSerialized] public WidgetData[] childWidgets = new WidgetData[WannaBeArrList.MinCapacity];
        [System.NonSerialized] public int childWidgetsCount = 0;

        /// <summary>
        /// <para>When <see langword="true"/> events are raised 1 frame delayed, and if there were multiple
        /// changes within 1 frame, only 1 event is raised and intermediate values will not get their
        /// events.</para>
        /// </summary>
        [System.NonSerialized] public bool preventRecursion = false;
        private bool waitingForRaiseEvent = false;

        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (value == isVisible)
                    return;
                isVisible = value;
                if (widget != null)
                    widget.gameObject.SetActive(value);
            }
        }

        private bool interactable = true;
        public bool Interactable
        {
            get => interactable;
            set
            {
                if (value == interactable)
                    return;
                interactable = value;
                if (widget != null)
                    widget.UpdateInteractable();
            }
        }

        public override void WannaBeDestructor()
        {
            ClearChildren();
        }

        /// <summary>
        /// <para>Just sets <see cref="IsVisible"/>, enables function call chaining.</para>
        /// </summary>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public WidgetData SetIsVisible(bool isVisible)
        {
            IsVisible = isVisible;
            return this;
        }

        /// <summary>
        /// <para>Just sets <see cref="Interactable"/>, enables function call chaining.</para>
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        public WidgetData SetInteractable(bool interactable)
        {
            Interactable = interactable;
            return this;
        }

        public WidgetData SetCustomData(string customDataFieldName, object customData)
        {
            this.customDataFieldName = customDataFieldName;
            this.customData = customData;
            return this;
        }

        public WidgetData UnsetCustomData()
        {
            customDataFieldName = null;
            customData = null;
            return this;
        }

        public WidgetData SetListener(UdonSharpBehaviour listener, string listenerEventName)
        {
            this.listener = listener;
            this.listenerEventName = listenerEventName;
            return this;
        }

        public WidgetData UnsetListener()
        {
            listener = null;
            listenerEventName = null;
            return this;
        }

        public void RaiseEvent()
        {
            if (!preventRecursion)
            {
                RaiseEventInternal();
                return;
            }
            if (waitingForRaiseEvent)
                return;
            waitingForRaiseEvent = true;
            SendCustomEventDelayedFrames(nameof(RaiseEventInternal), 1);
        }

        public void RaiseEventInternal()
        {
            waitingForRaiseEvent = false;
            if (listener == null)
                return;
            if (genericValueEditor != null)
                genericValueEditor.sendingWidgetData = this;
            if (customDataFieldName != null)
                listener.SetProgramVariable(customDataFieldName, customData);
            listener.SendCustomEvent(listenerEventName);
        }

        public WidgetData AddChildDynamic(WidgetData child)
        {
            WannaBeArrList.Add(ref childWidgets, ref childWidgetsCount, child);
            return child;
        }

        public WidgetData[] AddChildrenDynamic(WidgetData[] children, int childCount = -1)
        {
            WannaBeArrList.AddRange(ref childWidgets, ref childWidgetsCount, children, childCount);
            return children;
        }

        public WidgetData[] SetChildren(WidgetData[] children, int childCount = -1)
        {
            ClearChildren();
            return AddChildrenDynamic(children, childCount);
        }

        public void ClearChildren()
        {
            WannaBeArrList.Clear(ref childWidgets, ref childWidgetsCount);
        }

        public WidgetData SetChildrenChained(WidgetData[] children, int childCount = -1)
        {
            SetChildren(children, childCount);
            return this;
        }
    }

    public static class WidgetDataExtensions
    {
        public static T AddChild<T>(this WidgetData widgetData, T child)
            where T : WidgetData
        {
            return (T)widgetData.AddChildDynamic(child);
        }

        public static T[] AddChildren<T>(this WidgetData widgetData, T[] children, int childCount = -1)
            where T : WidgetData
        {
            return (T[])widgetData.AddChildrenDynamic(children, childCount);
        }
    }
}
