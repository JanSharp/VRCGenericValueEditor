using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StringFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "StringField";
        public StringFieldWidget ActualWidget => (StringFieldWidget)widget;

        private string value = "";
        public string Value
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
        public bool SetValueWithoutNotify(string value)
        {
            if (value == null)
                value = "";
            if (value == this.value)
                return false;
            this.value = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField();
            return true;
        }

        public StringFieldWidgetData WannaBeConstructor(string label, string value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
