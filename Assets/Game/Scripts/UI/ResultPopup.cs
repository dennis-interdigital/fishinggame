using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class ResultPopup : BaseUI
    {
        [SerializeField] TextMeshProUGUI textResult;
        [SerializeField] Button buttonTryAgain;

        public override void Init(StageManager inStageManager)
        {
            base.Init(inStageManager);
            buttonTryAgain.onClick.AddListener(OnClickTryAgain);
        }

        public override IEnumerator Showing(params object[] payload)
        {
            bool isWin = (bool)payload[0];

            string resultString = isWin ? "Win" : "Lose";
            textResult.SetText(resultString);

            yield return null;
        }

        void OnClickTryAgain()
        {
            uiManager.HidePopup(PopupState.Result);
            stageManager.fishingController.StartFishing();
        }
    }
}
