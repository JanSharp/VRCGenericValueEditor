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
                if (SetValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetValueWithoutNotify(Vector2 value)
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
                Vector2 newValue = this.value;
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
                Vector2 newValue = this.value;
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

        public Vector2FieldWidgetData WannaBeConstructor(string label, Vector2 value)
        {
            LabeledWidgetDataConstructor(label);
            this.value = value;
            return this;
        }
    }
}
