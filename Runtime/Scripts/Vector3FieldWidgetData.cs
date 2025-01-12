using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector3FieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "Vector3Field";
        public Vector3FieldWidget ActualWidget => (Vector3FieldWidget)widget;

        private Vector3 value;
        public Vector3 Value
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
                Vector3 newValue = this.value;
                newValue.x = value;
                Value = newValue;
            }
        }

        public float Y
        {
            get => value.y;
            set
            {
                Vector3 newValue = this.value;
                newValue.y = value;
                Value = newValue;
            }
        }

        public float Z
        {
            get => value.z;
            set
            {
                Vector3 newValue = this.value;
                newValue.z = value;
                Value = newValue;
            }
        }

        public Vector3FieldWidgetData WannaBeConstructor(string label, Vector3 value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
