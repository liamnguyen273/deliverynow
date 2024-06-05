using DeliveryNow.Gameplay;
using Owlet;
using Owlet.UI;
using Owlet.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class GameManager : Singleton<GameManager>
    {

        PlayerController playerController;
        protected override void Init()
        {
            PlayerController.onPlayerDataLoaded += SetPlayer;
            Application.targetFrameRate = 60;
            base.Init();
        }

        private void Start()
        {
            StartLevel();
        }

        void SetPlayer(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void StartLevel()
        {
            int currentLevel = PlayerDataManager.instance.GetCurrentLevel();
            MapLoader.instance.LoadLevel(currentLevel);
        }

        public void StartNextLevel()
        {
            PlayerDataManager.instance.IncreaseCurrentLevel();
            StartLevel();
            Debug.Log("Start Next Level");

        }

        public void RestartLevel()
        {
            StartLevel();
            Debug.Log("Restart Level");
        }

        public void ContinueLevel()
        {
            playerController.ResetStateToContinue();
        }

        public async void CompleteLevel()
        {
            await PopupManager.instance.OpenUI<Popup>(Keys.Popups.LevelComplete, 2);
        }
    }
}
