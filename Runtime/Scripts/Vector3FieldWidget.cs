using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Vector3FieldWidget : LabeledWidget
    {
        public override string WidgetName => "Vector3Field";
        public Vector3FieldWidgetData Data => (Vector3FieldWidgetData)BackingWidgetData;

        public TMP_InputField xInputField;
        public TMP_InputField yInputField;
        public TMP_InputField zInputField;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateInputFields();
        }

        public void UpdateInputFields()
        {
            Vector3 value = Data.Value;
            xInputField.SetTextWithoutNotify(value.x.ToString());
            yInputField.SetTextWithoutNotify(value.y.ToString());
            zInputField.SetTextWithoutNotify(value.z.ToString());
        }

        private float Parse(string text)
        {
            return float.TryParse(text, out float parsedValue) ? parsedValue : 0f;
        }

        public void OnXChanged()
        {
            Data.X = Parse(xInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }

        public void OnYChanged()
        {
            Data.Y = Parse(yInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }

        public void OnZChanged()
        {
            Data.Z = Parse(zInputField.text);
            UpdateInputFields(); // To handle when the value didn't change, but text does differ.
        }
    }
}
