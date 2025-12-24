using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishingGame
{
    public class GameplayUI : BaseUI
    {
        [SerializeField] RectTransform rtFishingReel;
        public float rotateSpeed;
        [SerializeField] Slider sliderReelBar;
        [SerializeField] Image imgFishStatus;
        [SerializeField] TextMeshProUGUI textDistance;

        FishingController fishingController;

        public override void Init(StageManager inStageManager)
        {
            base.Init(inStageManager);
            fishingController = stageManager.fishingController;
        }

        public override void DoUpdate(float dt)
        {
            rotateSpeed = fishingController.currReelRotationSpeed;

            float zRot = rtFishingReel.localEulerAngles.z;

            zRot -= rotateSpeed * dt;
            rtFishingReel.eulerAngles = new Vector3(0, 0, zRot);

            float distance = fishingController.reelDistance;
            textDistance.SetText(distance.ToString("F0"));

            float reelPower = fishingController.currReelPower;
            sliderReelBar.value = reelPower;
        }

        public void ChangeColor(FishStatus fishStatus)
        {
            switch (fishStatus)
            {
                case FishStatus.Red: imgFishStatus.color = Color.red; break;
                case FishStatus.Yellow: imgFishStatus.color = Color.yellow; break;
                case FishStatus.Green: imgFishStatus.color= Color.green; break;
            }

            rotateSpeed = fishingController.currReelRotationSpeed;
        }
    }
}

