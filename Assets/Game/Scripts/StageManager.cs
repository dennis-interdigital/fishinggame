using UnityEngine;

namespace FishingGame 
{
    public class StageManager : MonoBehaviour
    {
        public FishingController fishingController;
        
        public UIManager uiManager;

        bool gameReady = false;

        void Start()
        {
            fishingController.Init(this);
            uiManager.Init(this);

            gameReady = true;
            uiManager.ShowUI(UIState.Gameplay);

            fishingController.StartFishing();
        }

        void FixedUpdate()
        {
            if (gameReady)
            {
                float dt = Time.deltaTime;

                //DoUpdate here
                uiManager.DoUpdate(dt);
            }
        }
    }
}
