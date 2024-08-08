using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] Button resumeButton;
        //[SerializeField] Button retryButton;
        [SerializeField] Button mainMenuButton;

        private void Start(){
            resumeButton.onClick.AddListener(()=>
            {
                GameManager.instance.UnpauseGame(Hide);
            });

            /*retryButton.onClick.AddListener(()=>
            {
                GameManager.instance.RestartLevel();
                Hide();
            });*/

            mainMenuButton.onClick.AddListener(()=>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            GameManager.instance.OnGamePaused += GameManager_OnGamePaused;
            Hide();
        }

        private void GameManager_OnGamePaused(object sender, EventArgs e)
        {
            Show();
        }

        private void Show(){
            gameObject.SetActive(true);
        }

        private void Hide(){
            gameObject.SetActive(false);
        }
    }
}
