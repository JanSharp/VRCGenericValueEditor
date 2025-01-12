﻿using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector2FieldWidget : LabeledWidget
    {
        public override string WidgetName => "Vector2Field";
        public Vector2FieldWidgetData Data => (Vector2FieldWidgetData)BackingWidgetData;

        public TMP_InputField xInputField;
        public TMP_InputField yInputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateInputFields();
        }

        public void UpdateInputFields()
        {
            Vector2 value = Data.Value;
            xInputField.SetTextWithoutNotify(value.x.ToString());
            yInputField.SetTextWithoutNotify(value.y.ToString());
        }

        private float Parse(string text)
        {
            return float.TryParse(text, out float parsedValue) ? parsedValue : 0f;
        }

        public void OnXChanged()
        {
            Data.X = Parse(xInputField.text);
        }

        public void OnYChanged()
        {
            Data.Y = Parse(yInputField.text);
        }
    }
}
