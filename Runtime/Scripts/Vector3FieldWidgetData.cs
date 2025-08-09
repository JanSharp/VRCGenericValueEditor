using UdonSharp;
using UnityEngine;

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
                if (SetValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetValueWithoutNotify(Vector3 value)
        {
            if (value == this.value)
                return false;
            this.value = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputFields();
            return true;
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

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetXWithoutNotify(float x)
        {
            Vector3 newValue = value;
            newValue.x = x;
            return SetValueWithoutNotify(newValue);
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

        /// <summary>
        /// </summary>
        /// <param name="y"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetYWithoutNotify(float y)
        {
            Vector3 newValue = value;
            newValue.y = y;
            return SetValueWithoutNotify(newValue);
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

        /// <summary>
        /// </summary>
        /// <param name="z"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetZWithoutNotify(float z)
        {
            Vector3 newValue = value;
            newValue.z = z;
            return SetValueWithoutNotify(newValue);
        }

        public Vector3FieldWidgetData WannaBeConstructor(string label, Vector3 value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
