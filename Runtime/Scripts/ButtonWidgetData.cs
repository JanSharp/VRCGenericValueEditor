﻿using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ButtonWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "Button";
        public ButtonWidget ActualWidget => (ButtonWidget)widget;

        public ButtonWidgetData WannaBeConstructor(string label)
        {
            LabeledWidgetDataConstructor(label);
            return this;
        }
    }
}
