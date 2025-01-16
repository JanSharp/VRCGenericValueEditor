using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class Widget : UdonSharpBehaviour
    {
        public abstract string WidgetName { get; }
        public virtual bool IsContainer => false;
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
