using UdonSharp;
using UnityEngine;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class Widget : UdonSharpBehaviour
    {
        /// <summary>
        /// <para>Must match with an associated <see cref="WidgetData.WidgetName"/>.</para>
        /// </summary>
        public abstract string WidgetName { get; }
        /// <summary>
        /// <para>Whether this type of widget can contain child widgets.</para>
        /// </summary>
        public virtual bool IsContainer => false;
        /// <summary>
        /// <para>Must be set in the inspector if <see cref="IsContainer"/> is <see langword="true"/>.</para>
        /// </summary>
        public Transform containerWidgetsRoot;
        public CanvasGroup InteractableCanvasGroup;

        private WidgetData backingWidgetData;
        public WidgetData BackingWidgetData
        {
            get => backingWidgetData;
            set
            {
                if (value == backingWidgetData)
                    return;
                if (backingWidgetData != null)
                    backingWidgetData.DecrementRefsCount();
                backingWidgetData = value;
                if (value != null)
                {
                    value.IncrementRefsCount();
                    value.widget = this;
                    InitFromData();
                }
            }
        }

        protected virtual void InitFromData()
        {
            this.gameObject.SetActive(backingWidgetData.IsVisible);
            UpdateInteractable();
        }

        public virtual void UpdateInteractable()
        {
            if (InteractableCanvasGroup != null)
                InteractableCanvasGroup.interactable = backingWidgetData.Interactable;
        }
    }
}
