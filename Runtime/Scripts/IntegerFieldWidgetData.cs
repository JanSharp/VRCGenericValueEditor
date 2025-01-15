using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    internal enum IntegerWidgetType
    {
        // Explicit values to indicate that these relate to indexes into their respective names in an array.
        Invalid = 0,
        Int = 1,
        UInt = 2,
        Long = 3,
        ULong = 4,
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IntegerFieldWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "IntegerField";
        public IntegerFieldWidget ActualWidget => (IntegerFieldWidget)widget;

        private string[] integerTypeNames = new string[]
        {
            "Invalid",
            "Int",
            "UInt",
            "Long",
            "ULong",
        };

        private IntegerWidgetType integerType = IntegerWidgetType.Invalid;

        private int intValue;
        private uint uintValue;
        private long longValue;
        private ulong ulongValue;

        public int IntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Int);
                return intValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Int);
                if (value == intValue)
                    return;
                intValue = value;
                SetText(value.ToString());
            }
        }

        public uint UIntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                return uintValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                if (value == uintValue)
                    return;
                uintValue = value;
                SetText(value.ToString());
            }
        }

        public long LongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Long);
                return longValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Long);
                if (value == longValue)
                    return;
                longValue = value;
                SetText(value.ToString());
            }
        }

        public ulong ULongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                return ulongValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                if (value == ulongValue)
                    return;
                ulongValue = value;
                SetText(value.ToString());
            }
        }

        private void AssertIntegerType(IntegerWidgetType expectedType)
        {
            if (integerType == expectedType)
                return;
            Debug.LogError($"[GenericValueEditor] Attempt to use {integerTypeNames[(int)integerType]} "
                + $"IntegerFieldWidget as {integerTypeNames[(int)expectedType]}. Mismatching data types "
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
            switch (integerType)
            {
                case IntegerWidgetType.Invalid:
                    Debug.LogError("[GenericValueEditor] Attempt to use a IntegerFieldWidget which was never "
                        + $"initialized using one of the WannaBeConstructors.");
                    break;
                case IntegerWidgetType.Int:
                    IntValue = int.TryParse(value, out int parsedIntValue)
                        ? parsedIntValue
                        : 0;
                    break;
                case IntegerWidgetType.UInt:
                    UIntValue = uint.TryParse(value, out uint parsedUintValue)
                        ? parsedUintValue
                        : 0u;
                    break;
                case IntegerWidgetType.Long:
                    LongValue = long.TryParse(value, out long parsedLongValue)
                        ? parsedLongValue
                        : 0L;
                    break;
                case IntegerWidgetType.ULong:
                    ULongValue = ulong.TryParse(value, out ulong parsedULongValue)
                        ? parsedULongValue
                        : 0uL;
                    break;
                default:
                    Debug.LogError($"[GenericValueEditor] Unknown IntegerWidgetType {(int)integerType}, impossible.");
                    break;
            }
        }

        public string GetValueAsString()
        {
            switch (integerType)
            {
                case IntegerWidgetType.Invalid:
                    Debug.LogError("[GenericValueEditor] Attempt to use a IntegerFieldWidget which was never "
                        + $"initialized using one of the WannaBeConstructors.");
                    return null;
                case IntegerWidgetType.Int:
                    return intValue.ToString();
                case IntegerWidgetType.UInt:
                    return uintValue.ToString();
                case IntegerWidgetType.Long:
                    return longValue.ToString();
                case IntegerWidgetType.ULong:
                    return ulongValue.ToString();
                default:
                    Debug.LogError($"[GenericValueEditor] Unknown IntegerWidgetType {(int)integerType}, impossible.");
                    return null;
            }
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, int value)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.Int;
            intValue = value;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, uint value)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.UInt;
            uintValue = value;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, long value)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.Long;
            longValue = value;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, ulong value)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.ULong;
            ulongValue = value;
            return this;
        }
    }
}
