using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector2FieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "Vector2Field";
        public Vector2FieldWidget ActualWidget => (Vector2FieldWidget)widget;

        private Vector2 value;
        public Vector2 Value
        {
            get => value;
            set
            {
                if (value == this.value)
                    return;
                this.value = value;
                if (ActualWidget != null)
                    ActualWidget.UpdateInputFields();
                RaiseEvent();
            }
        }

        public float X
        {
            get => value.x;
            set
            {
                Vector2 newValue = this.value;
                newValue.x = value;
                Value = newValue;
            }
        }

        public float Y
        {
            get => value.y;
            set
            {
                Vector2 newValue = this.value;
                newValue.y = value;
                Value = newValue;
            }
        }

        public Vector2FieldWidgetData WannaBeConstructor(string label, Vector2 value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
