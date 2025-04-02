
# Generic Value Editor

Edit values with UI defined more through code rather than unity objects.

# Installing

Head to my [VCC Listing](https://jansharp.github.io/vrc/vcclisting.xhtml) and follow the instructions there.

# Bugs

All input fields part of UI made with the generic value editor do not open the VRC input field + keyboard popup. This is a VRChat bug, **please vote for [the bug report here](https://feedback.vrchat.com/bug-reports/p/instantiated-input-fields-dont-open-the-vrc-keyboard) if you are interested in using this package.**

# Features

- `GenericValueEditor` prefab: A UI pane which can be put into any custom UI
- `GenericValueEditor` script: Resides in the prefab. It's main API function is `Draw`, taking in `WidgetData` and creating `Widget`s inside of the UI pane
- `WidgetData` WannaBeClass instances: Define the look and the user interaction of `Widget`s
- `WidgetManager`: An API to assist with creating `WidgetData` and potentially registering custom `Widget` + `WidgetData` + widget prefab, if desired
- `Widget` instances: The actual UI widgets visible to the user with backing `WidgetData`. These are only part of the API when creating custom widgets, otherwise the `WidgetData` is the only thing scripts interact with

The `WidgetManager` is a singleton script which can be referenced using the `[SingletonReference]` attribute, causing the field to get populated at build time and the required prefab getting instantiated automatically. This is a feature from the JanSharp Common package. For example:

```cs
using JanSharp;
// ...
[HideInInspector] [SerializeField] [SingletonReference] private WidgetManager widgetManager;
```

`WidgetData` derives from `WannaBeClass`. This is a concept from the JanSharp Common package, which is pretty neat except for the fact that these "class instances" require manual memory management, otherwise they stay alive forever even when unused which is a memory leak. Look at the [WannaBeClasses](https://github.com/JanSharp/VRCJanSharpCommon#wannabeclasses) docs for more information.

## Layout

All of the UI layout is handled through unity UI layout elements. The root container of the `GenericValueEditor` is a scroll pane and all `Widget`s inside of it get stretched horizontally to fit into the scroll pane. The height of each `Widget` is controlled by the `Widget` itself, and may be variable. For example labels may span multiple lines, fold out widgets may be expanded or collapsed.

Some built in `Widget`s adjust the layout and display non interactive information:

- Box
- FoldOut
- Grouping
- Indent
- Label
- Line
- Space

The rest of the built in `Widget`s are interactive:

- Button
- DecimalField
- IntegerField
- MultilineStringField
- SliderField
- StringField
- ToggleField
- Vector2Field
- Vector3Field

## Editor Performance

In a commit ([`ff73c8b`](https://github.com/JanSharp/VRCGenericValueEditor/commit/ff73c8ba438114ad6701cf46b54bab6313bd6d7a)) performance was tested. The relevant part here is the huge difference between in Editor and in VRChat performance:

| Context   | Action                       | Time             |
|-----------|------------------------------|------------------|
| In Editor | Creating WidgetData          | 00:00:01.5072221 |
| In VRChat | Creating WidgetData          | 00:00:00.0074896 |
| In Editor | Draw (no Widgets pooled yet) | 00:00:02.9701157 |
| In VRChat | Draw (no Widgets pooled yet) | 00:00:00.0160837 |

Also note that for me personally the in Editor performance varies incredibly over time. Generally if I've had the Editor open for a while performance degrades so much that I've had it run into the 10 second Udon timeout limit in the test scene. My suspicion is that it is specifically the instantiation and with it initialization of UdonBehaviour components along side their Editor helper components, however this is just a guess. What matters is that it's slow, annoyingly so.

For the record I would still consider the 7.5 and 16 ms in VRChat for the test UI at the time to be slow, and I know a large part of this is UdonBehavior instantiation + initialization, which happens for each WidgetData and Widget here. But at least it didn't freeze for whole seconds.
