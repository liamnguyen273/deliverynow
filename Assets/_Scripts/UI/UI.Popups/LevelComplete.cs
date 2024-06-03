using Owlet.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI.Popups
{
    public class LevelComplete : Popup
    {
        [SerializeField] Button btnContinue;
        [SerializeField] Button btnRetry;


        private void Start()
        {
            btnContinue.onClick.AddListener(Continue);
            btnRetry.onClick.AddListener(Retry);
        }


        void Continue()
        {
            GameManager.instance.StartNextLevel();
            SelfClosing();
        }

        void Retry()
        {
            GameManager.instance.RestartLevel();
            SelfClosing();
        }
    }
}
