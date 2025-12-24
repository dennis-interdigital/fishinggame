using FishingGame;
using System.Collections;
using UnityEngine;

public enum FishStatus
{
    Red,
    Yellow,
    Green,
    COUNT
}

public class FishingController : MonoBehaviour
{
    [SerializeField] float rotationSpeedReelRed;
    [SerializeField] float rotationSpeedReelYellow;
    [SerializeField] float rotationSpeedReelGreen;
    [SerializeField] float rotationSpeedRelease;

    [Space(10f)]
    [SerializeField] float reelSpeedRateRed;
    [SerializeField] float reelSpeedRateYellow;
    [SerializeField] float reelSpeedRateGreen;
    [SerializeField] float releaseSpeedRate;

    [Space(10f)]
    [SerializeField] float reelPowerRateRed;
    [SerializeField] float reelPowerRateYellow;
    [SerializeField] float reelPowerRateGreen;
    [SerializeField] float reelPowerRateRelease;

    [Space(10f)]
    [SerializeField] float reelTimeRed;
    [SerializeField] float reelTimeYellow;
    [SerializeField] float reelTimeGreen;

    StageManager stageManager;

    [Header("Runtime")]
    public bool isFishing;
    public bool isReeling;
    public FishStatus fishStatus;
    public float reelDistance;
    public float currReelSpeedRate;

    public float currReelPower;
    public float currReelPowerRate;

    public float tReelTime;
    public float currReelTime;

    public float currReelRotationSpeed;


    public void Init(StageManager inStageManager)
    {
        stageManager = inStageManager;

        fishStatus = FishStatus.COUNT;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (isFishing && fishStatus != FishStatus.COUNT)
        {
            //Input
            if (Input.GetMouseButtonDown(0))
            {
                currReelSpeedRate = GetReelSpeedRate();
                currReelPowerRate = GetReelPowerRate();
                currReelRotationSpeed = GetReelRotationSpeed();
            }

            if (Input.GetMouseButtonUp(0))
            {
                currReelPowerRate = reelPowerRateRelease;
                currReelSpeedRate = releaseSpeedRate;
                currReelRotationSpeed = rotationSpeedRelease;
            }

            reelDistance -= currReelSpeedRate * dt;
            tReelTime += dt;

            if (tReelTime >= currReelTime)
            {
                tReelTime = 0;
                SwitchFishStatus();
            }

            currReelPower += currReelPowerRate * dt;
            currReelPower = Mathf.Clamp01(currReelPower);

            bool isFishCaught = reelDistance <= 0f;
            bool isFishReleased = reelDistance >= 200f;
            bool isPowerMax = currReelPower >= 1f;

            if (isFishCaught || isFishReleased || isPowerMax)
            {
                currReelRotationSpeed = 0;

                isFishing = false;
                fishStatus = FishStatus.COUNT;

                bool win = isFishCaught;
                stageManager.uiManager.ShowPopup(PopupState.Result, win);
            }
        }
    }

    public void StartFishing()
    {
        tReelTime = 0;
        currReelPower = 0;
        currReelPowerRate = 0;
        currReelRotationSpeed = 0;
        currReelSpeedRate = 0;

        StartCoroutine(WaitFishBait());
    }

    IEnumerator WaitFishBait()
    {
        isFishing = true;

        yield return new WaitForSeconds(3f);

        fishStatus = FishStatus.Red;
        SwitchFishStatus();
        reelDistance = 150f;

        currReelPower = 0.55f;
        currReelPowerRate = reelPowerRateRelease;
        currReelSpeedRate = releaseSpeedRate;
        currReelRotationSpeed = rotationSpeedRelease;
    }

    void SwitchFishStatus()
    {
        if (fishStatus == FishStatus.Green)
        {
            fishStatus = FishStatus.Red;
        }
        else if (fishStatus == FishStatus.Red)
        {
            fishStatus = FishStatus.Yellow;
        }
        else if (fishStatus == FishStatus.Yellow)
        {
            fishStatus = FishStatus.Green;
        }

        currReelTime = GetReelTime();

        if (Input.GetMouseButton(0))
        {
            currReelSpeedRate = GetReelSpeedRate();
            currReelPowerRate = GetReelPowerRate();
            currReelRotationSpeed = GetReelRotationSpeed();
        }
        else
        {
            currReelPowerRate = reelPowerRateRelease;
            currReelSpeedRate = releaseSpeedRate;
            currReelRotationSpeed = rotationSpeedRelease;
        }

        GameplayUI gameplayUI = stageManager.uiManager.currentActiveUI as GameplayUI;
        gameplayUI.ChangeColor(fishStatus);
    }

    float GetReelPowerRate()
    {
        float result = 0;
        switch (fishStatus)
        {
            case FishStatus.Red: result = reelPowerRateRed; break;
            case FishStatus.Yellow: result = reelPowerRateYellow; break;
            case FishStatus.Green: result = reelPowerRateGreen; break;
        }
        return result;
    }

    float GetReelSpeedRate()
    {
        float result = 0;
        switch (fishStatus)
        {
            case FishStatus.Red: result = reelSpeedRateRed; break;
            case FishStatus.Yellow: result = reelSpeedRateYellow; break;
            case FishStatus.Green: result = reelSpeedRateGreen; break;
        }
        return result;
    }

    float GetReelRotationSpeed()
    {
        float result = 0;
        switch (fishStatus)
        {
            case FishStatus.Red: result = rotationSpeedReelRed; break;
            case FishStatus.Yellow: result = rotationSpeedReelYellow; break;
            case FishStatus.Green: result = rotationSpeedReelGreen; break;
        }
        return result;
    }

    float GetReelTime()
    {
        float result = 0;
        switch (fishStatus)
        {
            case FishStatus.Red: result = reelTimeRed; break;
            case FishStatus.Yellow: result = reelTimeYellow; break;
            case FishStatus.Green: result = reelTimeGreen; break;
        }
        return result;
    }
}
