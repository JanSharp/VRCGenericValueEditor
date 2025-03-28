﻿using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class BoxWidgetData : WidgetData
    {
        public override string WidgetName => "Box";
        public BoxWidget ActualWidget => (BoxWidget)widget;
    }
}
