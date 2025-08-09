using UdonSharp;
using UnityEngine;

namespace JanSharp
{
    internal enum DecimalWidgetType
    {
        // Explicit values to indicate that these relate to indexes into their respective names in an array.
        Invalid = 0,
        Float = 1,
        Double = 2,
        Decimal = 3,
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DecimalFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "DecimalField";
        public DecimalFieldWidget ActualWidget => (DecimalFieldWidget)widget;

        private string[] decimalTypeNames = new string[]
        {
            "Invalid",
            "Float",
            "Double",
            "Decimal",
        };

        private DecimalWidgetType decimalType = DecimalWidgetType.Invalid;

        private float floatValue;
        private float minFloatValue;
        private float maxFloatValue;
        private double doubleValue;
        private double minDoubleValue;
        private double maxDoubleValue;
        private decimal decimalValue;
        private decimal minDecimalValue;
        private decimal maxDecimalValue;

        public float FloatValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Float);
                return floatValue;
            }
            set
            {
                if (SetFloatValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetFloatValueWithoutNotify(float value)
        {
            AssertDecimalType(DecimalWidgetType.Float);
            value = Mathf.Clamp(value, minFloatValue, maxFloatValue);
            if (value == floatValue)
                return false;
            floatValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public float MinFloatValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Float);
                return minFloatValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Float);
                if (value == minFloatValue)
                    return;
                minFloatValue = value;
                FloatValue = floatValue;
            }
        }

        public float MaxFloatValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Float);
                return maxFloatValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Float);
                if (value == maxFloatValue)
                    return;
                maxFloatValue = value;
                FloatValue = floatValue;
            }
        }

        public double DoubleValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Double);
                return doubleValue;
            }
            set
            {
                if (SetDoubleValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetDoubleValueWithoutNotify(double value)
        {
            AssertDecimalType(DecimalWidgetType.Double);
            value = System.Math.Min(System.Math.Max(value, minDoubleValue), maxDoubleValue);
            if (value == doubleValue)
                return false;
            doubleValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public double MinDoubleValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Double);
                return minDoubleValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Double);
                if (value == minDoubleValue)
                    return;
                minDoubleValue = value;
                DoubleValue = doubleValue;
            }
        }

        public double MaxDoubleValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Double);
                return maxDoubleValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Double);
                if (value == maxDoubleValue)
                    return;
                maxDoubleValue = value;
                DoubleValue = doubleValue;
            }
        }

        public decimal DecimalValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Decimal);
                return decimalValue;
            }
            set
            {
                if (SetDecimalValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetDecimalValueWithoutNotify(decimal value)
        {
            AssertDecimalType(DecimalWidgetType.Decimal);
            value = System.Math.Min(System.Math.Max(value, minDecimalValue), maxDecimalValue);
            if (value == decimalValue)
                return false;
            decimalValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public decimal MinDecimalValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Decimal);
                return minDecimalValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Decimal);
                if (value == minDecimalValue)
                    return;
                minDecimalValue = value;
                DecimalValue = decimalValue;
            }
        }

        public decimal MaxDecimalValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Decimal);
                return maxDecimalValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Decimal);
                if (value == maxDecimalValue)
                    return;
                maxDecimalValue = value;
                DecimalValue = decimalValue;
            }
        }

        private void AssertDecimalType(DecimalWidgetType expectedType)
        {
            if (decimalType == expectedType)
                return;
            Debug.LogError($"[GenericValueEditor] Attempt to use {decimalTypeNames[(int)decimalType]} "
                + $"DecimalFieldWidget as {decimalTypeNames[(int)expectedType]}. Mismatching data types "
                + $"on the same widget like this is going to result in unexpected values displayed vs read, "
                + $"since the widget data stores different data types in different variables.");
        }

        public void SetValueFromString(string value)
        {
            switch (decimalType)
            {
                case DecimalWidgetType.Invalid:
                    Debug.LogError("[GenericValueEditor] Attempt to use a DecimalFieldWidget which was never "
                        + $"initialized using one of the WannaBeConstructors.");
                    break;
                case DecimalWidgetType.Float:
                    FloatValue = float.TryParse(value, out float parsedFloatValue)
                        ? parsedFloatValue
                        : 0f;
                    break;
                case DecimalWidgetType.Double:
                    DoubleValue = double.TryParse(value, out double parsedDoubleValue)
                        ? parsedDoubleValue
                        : 0d;
                    break;
                case DecimalWidgetType.Decimal:
                    DecimalValue = decimal.TryParse(value, out decimal parsedDecimalValue)
                        ? parsedDecimalValue
                        : 0m;
                    break;
                default:
                    Debug.LogError($"[GenericValueEditor] Unknown DecimalWidgetType {(int)decimalType}, impossible.");
                    break;
            }
        }

        public string GetValueAsString()
        {
            switch (decimalType)
            {
                case DecimalWidgetType.Invalid:
                    Debug.LogError("[GenericValueEditor] Attempt to use a DecimalFieldWidget which was never "
                        + $"initialized using one of the WannaBeConstructors.");
                    return null;
                case DecimalWidgetType.Float:
                    return floatValue.ToString();
                case DecimalWidgetType.Double:
                    return doubleValue.ToString();
                case DecimalWidgetType.Decimal:
                    return decimalValue.ToString();
                default:
                    Debug.LogError($"[GenericValueEditor] Unknown DecimalWidgetType {(int)decimalType}, impossible.");
                    return null;
            }
        }

        public DecimalFieldWidgetData WannaBeConstructor(string label, float value, float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Float;
            floatValue = value;
            minFloatValue = minValue;
            maxFloatValue = maxValue;
            return this;
        }

        public DecimalFieldWidgetData WannaBeConstructor(string label, double value, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Double;
            doubleValue = value;
            minDoubleValue = minValue;
            maxDoubleValue = maxValue;
            return this;
        }

        public DecimalFieldWidgetData WannaBeConstructor(string label, decimal value, decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Decimal;
            decimalValue = value;
            minDecimalValue = minValue;
            maxDecimalValue = maxValue;
            return this;
        }
    }
}
