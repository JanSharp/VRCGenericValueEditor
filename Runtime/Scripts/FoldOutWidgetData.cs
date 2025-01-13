using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class FoldOutWidgetData : LabeledWidgetData
    {
        public override string WidgetName => "FoldOut";
        public FoldOutWidget ActualWidget => (FoldOutWidget)widget;

        private bool foldedOut;
        public bool FoldedOut
        {
            get => foldedOut;
            set
            {
                if (value == foldedOut)
                    return;
                foldedOut = value;
                if (ActualWidget != null)
                    ActualWidget.UpdateFoldOut();
                RaiseEvent();
            }
        }

        public FoldOutWidgetData WannaBeConstructor(string label, bool foldedOut)
        {
            LabeledWidgetDataConstructor(label);
            this.foldedOut = foldedOut;
            return this;
        }
    }
}
