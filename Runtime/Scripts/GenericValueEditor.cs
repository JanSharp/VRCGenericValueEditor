using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [DefaultExecutionOrder(-10000)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class GenericValueEditor : UdonSharpBehaviour
    {
        [HideInInspector] [SerializeField] [SingletonReference] private WannaBeClassesManager wannaBeClasses;
        public Transform widgetsRoot;
        public GameObject boxWidgetPrefab;
        public GameObject buttonWidgetPrefab;
        public GameObject indentWidgetPrefab;
        public GameObject labelWidgetPrefab;
        public GameObject lineWidgetPrefab;
        public GameObject sliderFieldWidgetPrefab;
        public GameObject textFieldWidgetPrefab;
        public GameObject toggleFieldWidgetPrefab;
        public GameObject vector2FieldWidgetPrefab;
        public GameObject vector3FieldWidgetPrefab;
        private DataDictionary widgetPrefabsByName = new DataDictionary();
        private Widget[] widgets = new Widget[0];

        private void Start()
        {
            widgetPrefabsByName.Add("Box", boxWidgetPrefab);
            widgetPrefabsByName.Add("Button", buttonWidgetPrefab);
            widgetPrefabsByName.Add("Indent", indentWidgetPrefab);
            widgetPrefabsByName.Add("Label", labelWidgetPrefab);
            widgetPrefabsByName.Add("Line", lineWidgetPrefab);
            widgetPrefabsByName.Add("SliderField", sliderFieldWidgetPrefab);
            widgetPrefabsByName.Add("TextField", textFieldWidgetPrefab);
            widgetPrefabsByName.Add("ToggleField", toggleFieldWidgetPrefab);
            widgetPrefabsByName.Add("Vector2Field", vector2FieldWidgetPrefab);
            widgetPrefabsByName.Add("Vector3Field", vector3FieldWidgetPrefab);
        }

        private GameObject GetWidgetPrefab(string widgetName) => (GameObject)widgetPrefabsByName[widgetName].Reference;

        public void Draw(WidgetData[] widgetData, int count = -1)
        {
            if (count < 0)
                count = widgetData.Length;
            Widget[] newWidgets = new Widget[count];
            int existingIndex = 0;
            int existingCount = widgets.Length;
            Widget existingWidget = existingIndex < existingCount ? widgets[existingIndex++] : null;
            for (int i = 0; i < count; i++)
            {
                WidgetData currentData = widgetData[i];
                Widget widget = null;
                if (existingWidget == null)
                {
                    GameObject widgetGo = Instantiate(GetWidgetPrefab(currentData.WidgetName));
                    widgetGo.transform.SetParent(widgetsRoot, worldPositionStays: false);
                    widget = widgetGo.GetComponent<Widget>();
                }
                else if (existingWidget.BackingWidgetData.WidgetName == currentData.WidgetName)
                {
                    widget = existingWidget;
                    widget.transform.SetAsLastSibling();
                    existingWidget = existingIndex < existingCount ? widgets[existingIndex++] : null;
                }
                // This ultimately calls IncrementRefsCount, allowing the widgetData array to be StdMoved in.
                widget.BackingWidgetData = currentData;
                newWidgets[i] = widget;
            }
            if (existingWidget != null)
                while (true)
                {
                    existingWidget.BackingWidgetData = null; // Free backing data wanna be class.
                    Destroy(existingWidget.gameObject);
                    if (existingIndex >= existingCount)
                        break;
                    existingWidget = widgets[existingIndex++];
                }
            widgets = newWidgets;
        }

        public WidgetData[] StdMoveWidgetData(WidgetData[] widgetData, int count = -1)
        {
            if (count < 0)
                count = widgetData.Length;
            for (int i = 0; i < count; i++)
                widgetData[i].StdMove();
            return widgetData;
        }

        public ButtonWidgetData NewButton(string label)
        {
            return wannaBeClasses.New<ButtonWidgetData>(nameof(ButtonWidgetData))
                .WannaBeConstructor(label);
        }

        public SliderFieldWidgetData NewSlider(string label, float value)
        {
            return wannaBeClasses.New<SliderFieldWidgetData>(nameof(SliderFieldWidgetData))
                .WannaBeConstructor(label, value);
        }

        public ToggleFieldWidgetData NewToggle(string label, bool value)
        {
            return wannaBeClasses.New<ToggleFieldWidgetData>(nameof(ToggleFieldWidgetData))
                .WannaBeConstructor(label, value);
        }
    }
}
