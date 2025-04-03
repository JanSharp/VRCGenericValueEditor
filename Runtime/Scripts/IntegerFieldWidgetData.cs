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
        private int minIntValue;
        private int maxIntValue;
        private uint uintValue;
        private uint minUIntValue;
        private uint maxUIntValue;
        private long longValue;
        private long minLongValue;
        private long maxLongValue;
        private ulong ulongValue;
        private ulong minULongValue;
        private ulong maxULongValue;

        public int IntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Int);
                return intValue;
            }
            set
            {
                if (SetIntValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetIntValueWithoutNotify(int value)
        {
            AssertIntegerType(IntegerWidgetType.Int);
            value = System.Math.Min(System.Math.Max(value, minIntValue), maxIntValue);
            if (value == intValue)
                return false;
            intValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public int MinIntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Int);
                return minIntValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Int);
                if (value == minIntValue)
                    return;
                minIntValue = value;
                IntValue = intValue;
            }
        }

        public int MaxIntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Int);
                return maxIntValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Int);
                if (value == maxIntValue)
                    return;
                maxIntValue = value;
                IntValue = intValue;
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
                if (SetUIntValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetUIntValueWithoutNotify(uint value)
        {
            AssertIntegerType(IntegerWidgetType.UInt);
            value = System.Math.Min(System.Math.Max(value, minUIntValue), maxUIntValue);
            if (value == uintValue)
                return false;
            uintValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public uint MinUIntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                return minUIntValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                if (value == minUIntValue)
                    return;
                minUIntValue = value;
                UIntValue = uintValue;
            }
        }

        public uint MaxUIntValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                return maxUIntValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.UInt);
                if (value == maxUIntValue)
                    return;
                maxUIntValue = value;
                UIntValue = uintValue;
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
                if (SetLongValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetLongValueWithoutNotify(long value)
        {
            AssertIntegerType(IntegerWidgetType.Long);
            value = System.Math.Min(System.Math.Max(value, minLongValue), maxLongValue);
            if (value == longValue)
                return false;
            longValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public long MinLongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Long);
                return minLongValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Long);
                if (value == minLongValue)
                    return;
                minLongValue = value;
                LongValue = longValue;
            }
        }

        public long MaxLongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.Long);
                return maxLongValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.Long);
                if (value == maxLongValue)
                    return;
                maxLongValue = value;
                LongValue = longValue;
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
                if (SetULongValueWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetULongValueWithoutNotify(ulong value)
        {
            AssertIntegerType(IntegerWidgetType.ULong);
            value = System.Math.Min(System.Math.Max(value, minULongValue), maxULongValue);
            if (value == ulongValue)
                return false;
            ulongValue = value;
            if (ActualWidget != null)
                ActualWidget.UpdateInputField(value.ToString());
            return true;
        }

        public ulong MinULongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                return minULongValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                if (value == minULongValue)
                    return;
                minULongValue = value;
                ULongValue = ulongValue;
            }
        }

        public ulong MaxULongValue
        {
            get
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                return maxULongValue;
            }
            set
            {
                AssertIntegerType(IntegerWidgetType.ULong);
                if (value == maxULongValue)
                    return;
                maxULongValue = value;
                ULongValue = ulongValue;
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

        public IntegerFieldWidgetData WannaBeConstructor(string label, int value, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.Int;
            intValue = value;
            minIntValue = minValue;
            maxIntValue = maxValue;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, uint value, uint minValue = uint.MinValue, uint maxValue = uint.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.UInt;
            uintValue = value;
            minUIntValue = minValue;
            maxUIntValue = maxValue;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, long value, long minValue = long.MinValue, long maxValue = long.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.Long;
            longValue = value;
            minLongValue = minValue;
            maxLongValue = maxValue;
            return this;
        }

        public IntegerFieldWidgetData WannaBeConstructor(string label, ulong value, ulong minValue = ulong.MinValue, ulong maxValue = ulong.MaxValue)
        {
            LabeledWidgetDataConstructor(label);
            integerType = IntegerWidgetType.ULong;
            ulongValue = value;
            minULongValue = minValue;
            maxULongValue = maxValue;
            return this;
        }
    }
}
