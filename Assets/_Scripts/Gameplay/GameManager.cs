using DeliveryNow.Gameplay;
using Owlet;
using Owlet.Systems.SaveLoad;
using Owlet.UI;
using Owlet.UI.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class GameManager : Singleton<GameManager>
    {
        public event EventHandler OnGamePaused;
        private bool _isGamePaused = false;
        public bool IsGamePaused {
            get{return _isGamePaused;}
            set{_isGamePaused = this;}
        }
        PlayerController playerController;
        protected override void Init()
        {
            PlayerController.onPlayerDataLoaded += SetPlayer;
            Application.targetFrameRate = 60;
            SaveManager.onDataLoaded += StartLevel;

            base.Init();
        }

        private void OnDestroy()
        {
            SaveManager.onDataLoaded -= StartLevel;
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
        // Issue: The game doesn't restart the player's achieved coin during the fail level.
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

        public void PauseGame(){
            OnGamePaused?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 0f;
        }

        public void UnpauseGame(Action hidePauseUI){
            Time.timeScale = 1f;
            hidePauseUI();
        }
    }
}
