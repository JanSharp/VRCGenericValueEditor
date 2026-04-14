using UdonSharp;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "ToggleField";
        public ToggleFieldWidget ActualWidget => (ToggleFieldWidget)widget;

        public virtual bool IsLeftToggle => false;

        private bool value;
        public bool Value
        {
            get => value;
            set
            {
                if (SetValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetValueWithoutNotify(bool value)
        {
            if (value == this.value)
                return false;
            this.value = value;
            if (ActualWidget != null)
                ActualWidget.toggle.SetIsOnWithoutNotify(value);
            return true;
        }

        public override bool WannaBeClassSupportsPooling => true;
        public override void ResetWannaBeClassToDefault()
        {
            base.ResetWannaBeClassToDefault();
            value = default;
        }

        public ToggleFieldWidgetData WannaBeConstructor(string label, bool value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
