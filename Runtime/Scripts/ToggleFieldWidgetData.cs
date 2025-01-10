using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "ToggleField";
        public ToggleFieldWidget ActualWidget => (ToggleFieldWidget)widget;

        private bool value;
        public bool Value
        {
            get => value;
            set
            {
                if (value == this.value)
                    return;
                this.value = value;
                if (ActualWidget != null)
                    ActualWidget.toggle.SetIsOnWithoutNotify(value);
                RaiseEvent();
            }
        }

        public ToggleFieldWidgetData WannaBeConstructor(string label, bool value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
