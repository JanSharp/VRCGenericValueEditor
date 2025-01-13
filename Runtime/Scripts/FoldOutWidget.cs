using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JanSharp
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class FoldOutWidget : LabeledWidget
    {
        public override string WidgetName => "FoldOut";
        public FoldOutWidgetData Data => (FoldOutWidgetData)BackingWidgetData;

        public override bool IsContainer => true;

        public TextMeshProUGUI foldButtonText;
        public GameObject content;

        protected override void InitFromData()
        {
            base.InitFromData();
            UpdateFoldOut();
        }

        public void UpdateFoldOut()
        {
            bool foldedOut = Data.FoldedOut;
            foldButtonText.text = foldedOut ? "v" : ">";
            content.SetActive(foldedOut);
        }

        public void OnFoldButtonClick()
        {
            Data.FoldedOut = !Data.FoldedOut;
        }
    }
}
