using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI
{
    public class GameplayOverlay : MonoBehaviour
    {
        [SerializeField] Button btnSettings;
        [SerializeField] Button btnRetry;

        private void Start()
        {
            btnSettings.onClick.AddListener(OpenSetting);
            btnRetry.onClick.AddListener(Retry);
        }


        private void OpenSetting()
        {
            GameManager.instance.PauseGame();
        }

        void Retry()
        {
            GameManager.instance.RestartLevel();
        }

    }
}
