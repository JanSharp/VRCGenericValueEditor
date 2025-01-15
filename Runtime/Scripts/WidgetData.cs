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

        [System.NonSerialized] public UdonSharpBehaviour listener;
        [System.NonSerialized] public string listenerEventName;
        [System.NonSerialized] public string listenerWidgetDataFieldName;

        [System.NonSerialized] public string customDataFieldName;
        [System.NonSerialized] public object customData;

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

        public WidgetData SetListener(UdonSharpBehaviour listener, string listenerEventName, string listenerWidgetDataFieldName)
        {
            this.listener = listener;
            this.listenerEventName = listenerEventName;
            this.listenerWidgetDataFieldName = listenerWidgetDataFieldName;
            return this;
        }

        public WidgetData UnsetListener()
        {
            listener = null;
            listenerEventName = null;
            listenerWidgetDataFieldName = null;
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
            listener.SetProgramVariable(listenerWidgetDataFieldName, this);
            if (customDataFieldName != null)
                listener.SetProgramVariable(customDataFieldName, customData);
            listener.SendCustomEvent(listenerEventName);
        }
    }
}
