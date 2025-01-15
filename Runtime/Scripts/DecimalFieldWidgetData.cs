using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

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

        // TODO: add range restrictions

        private float floatValue;
        private double doubleValue;
        private decimal decimalValue;

        public float FloatValue
        {
            get
            {
                AssertDecimalType(DecimalWidgetType.Float);
                return floatValue;
            }
            set
            {
                AssertDecimalType(DecimalWidgetType.Float);
                if (value == floatValue)
                    return;
                floatValue = value;
                SetText(value.ToString());
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
                AssertDecimalType(DecimalWidgetType.Double);
                if (value == doubleValue)
                    return;
                doubleValue = value;
                SetText(value.ToString());
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
                AssertDecimalType(DecimalWidgetType.Decimal);
                if (value == decimalValue)
                    return;
                decimalValue = value;
                SetText(value.ToString());
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

        private void SetText(string text)
        {
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(text);
            RaiseEvent();
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

        public DecimalFieldWidgetData WannaBeConstructor(string label, float value)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Float;
            floatValue = value;
            return this;
        }

        public DecimalFieldWidgetData WannaBeConstructor(string label, double value)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Double;
            doubleValue = value;
            return this;
        }

        public DecimalFieldWidgetData WannaBeConstructor(string label, decimal value)
        {
            LabeledWidgetDataConstructor(label);
            decimalType = DecimalWidgetType.Decimal;
            decimalValue = value;
            return this;
        }
    }
}
