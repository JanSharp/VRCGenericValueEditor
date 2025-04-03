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
                if (SetFoldedOutWithoutNotify(value))
                    RaiseEvent();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see langword="true"/> if the value changed.</returns>
        public bool SetFoldedOutWithoutNotify(bool value)
        {
            if (value == foldedOut)
                return false;
            foldedOut = value;
            if (ActualWidget != null)
                ActualWidget.UpdateFoldOut();
            return true;
        }

        public FoldOutWidgetData WannaBeConstructor(string label, bool foldedOut)
        {
            LabeledWidgetDataConstructor(label);
            this.foldedOut = foldedOut;
            return this;
        }
    }
}
