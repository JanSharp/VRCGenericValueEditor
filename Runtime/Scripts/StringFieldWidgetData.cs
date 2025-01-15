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
                if (value == null)
                    value = "";
                if (value == this.value)
                    return;
                this.value = value;
                if (ActualWidget != null)
                    ActualWidget.UpdateInputField();
                RaiseEvent();
            }
        }

        public StringFieldWidgetData WannaBeConstructor(string label, string value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
