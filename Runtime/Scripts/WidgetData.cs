using UdonSharp;
using UnityEngine;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class WidgetData : WannaBeClass
    {
        /// <summary>
        /// <para>Must match with an associated <see cref="Widget.WidgetName"/>.</para>
        /// </summary>
        public abstract string WidgetName { get; }
        /// <summary>
        /// <para>Non <see langword="null"/> whenever this widget data is currently shown in a
        /// <see cref="GenericValueEditor"/>.</para>
        /// </summary>
        [System.NonSerialized] public Widget widget;
        /// <summary>
        /// <para>Non <see langword="null"/> whenever <see cref="widget"/> is non
        /// <see langword="null"/>.</para>
        /// </summary>
        [System.NonSerialized] public GenericValueEditor genericValueEditor;

        [System.NonSerialized] public UdonSharpBehaviour listener;
        [System.NonSerialized] public string listenerEventName;

        [System.NonSerialized] public string customDataFieldName;
        [System.NonSerialized] public object customData;

        /// <summary>
        /// <para>Holds strong references to <see cref="WidgetData"/>, which are
        /// <see cref="WannaBeClass"/>es.</para>
        /// <para>Use <see cref="WannaBeArrList"/> to modify this and <see cref="childWidgetsCount"/>.</para>
        /// </summary>
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
        /// <summary>
        /// <para>When <see langword="false"/> this widget and all its children become invisible. This
        /// ultimately simply changes the <see cref="GameObject.activeSelf"/> state of the root of this
        /// widget. Must do this instead of using <see cref="CanvasGroup.alpha"/> in order for these invisible
        /// widgets not to affect UI layout.</para>
        /// </summary>
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
        /// <summary>
        /// <para>When <see langword="false"/> this widget and all its children cannot be interacted
        /// with. There may exceptions, such as the fold out toggle button for, well, fold out widgets. This
        /// is achieved using <see cref="CanvasGroup"/>s.</para>
        /// </summary>
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

        /// <summary>
        /// <para>Sets <see cref="customDataFieldName"/> and <see cref="customData"/> to the given
        /// values.</para>
        /// <para>A field with the name <see cref="customDataFieldName"/> then gets set to
        /// <see cref="customData"/> right before the <see cref="listenerEventName"/> gets raised on
        /// <see cref="listener"/>.</para>
        /// </summary>
        /// <returns>A reference to <see langword="this"/>.</returns>
        public WidgetData SetCustomData(string customDataFieldName, object customData)
        {
            this.customDataFieldName = customDataFieldName;
            this.customData = customData;
            return this;
        }

        /// <summary>
        /// <para>Unsets <see cref="customDataFieldName"/> and <see cref="customData"/>.</para>
        /// </summary>
        /// <returns>A reference to <see langword="this"/>.</returns>
        public WidgetData UnsetCustomData()
        {
            customDataFieldName = null;
            customData = null;
            return this;
        }

        /// <summary>
        /// <para>Sets <see cref="listener"/> and <see cref="listenerEventName"/> to the given values.</para>
        /// </summary>
        /// <returns>A reference to <see langword="this"/>.</returns>
        public WidgetData SetListener(UdonSharpBehaviour listener, string listenerEventName)
        {
            this.listener = listener;
            this.listenerEventName = listenerEventName;
            return this;
        }

        /// <summary>
        /// <para>Unsets <see cref="listener"/> and <see cref="listenerEventName"/>.</para>
        /// </summary>
        /// <returns>A reference to <see langword="this"/>.</returns>
        public WidgetData UnsetListener()
        {
            listener = null;
            listenerEventName = null;
            return this;
        }

        /// <summary>
        /// <para>Raises an event on <see cref="listener"/> using <see cref="listenerEventName"/> as defined
        /// by <see cref="SetListener(UdonSharpBehaviour, string)"/> 1 frame delayed, deduplicated.</para>
        /// <para>Before the event is raised on the <see cref="listener"/>
        /// <see cref="GenericValueEditor.sendingWidgetData"/> is set to <see langword="this"/> and if
        /// <see cref="customDataFieldName"/> is non <see langword="null"/>, <see cref="customData"/> will be
        /// written to said field on the <see cref="listener"/>.</para>
        /// </summary>
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

        /// <summary>
        /// <para>Appends <paramref name="child"/> to <see cref="childWidgets"/>.</para>
        /// </summary>
        /// <param name="child"></param>
        /// <returns>A reference to <paramref name="child"/>.</returns>
        public WidgetData AddChildDynamic(WidgetData child)
        {
            WannaBeArrList.Add(ref childWidgets, ref childWidgetsCount, child);
            return child;
        }

        /// <summary>
        /// <para>Append all given <paramref name="children"/> to <see cref="childWidgets"/>.</para>
        /// </summary>
        /// <param name="children"></param>
        /// <param name="childCount"></param>
        /// <returns>A reference to <paramref name="children"/>.</returns>
        public WidgetData[] AddChildrenDynamic(WidgetData[] children, int childCount = -1)
        {
            WannaBeArrList.AddRange(ref childWidgets, ref childWidgetsCount, children, childCount);
            return children;
        }

        /// <summary>
        /// <para>Clears <see cref="childWidgets"/> and fills it with the given <paramref name="children"/>
        /// instead.</para>
        /// </summary>
        /// <param name="children"></param>
        /// <param name="childCount"></param>
        /// <returns>A reference to <paramref name="children"/>.</returns>
        public WidgetData[] SetChildren(WidgetData[] children, int childCount = -1)
        {
            ClearChildren();
            return AddChildrenDynamic(children, childCount);
        }

        /// <summary>
        /// <para>Clears <see cref="childWidgets"/>.</para>
        /// </summary>
        public void ClearChildren()
        {
            WannaBeArrList.Clear(ref childWidgets, ref childWidgetsCount);
        }

        public WidgetData SetChildrenChained(WidgetData[] children, int childCount = -1)
        {
            SetChildren(children, childCount);
            return this;
        }

        /// <summary>
        /// <para>Just calls <see cref="WannaBeClass.StdMove"/> but casts the return value to make it easier
        /// to pass the widget to an add function which expects widget data.</para>
        /// </summary>
        /// <returns></returns>
        public WidgetData StdMoveWidget() => (WidgetData)StdMove();
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
