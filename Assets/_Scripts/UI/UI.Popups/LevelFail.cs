using Owlet.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI.Popups
{
    public class LevelFail : Popup
    {
        [SerializeField] Button btnRetry;
        [SerializeField] Button btnContinue;

        private void Start()
        {
            btnRetry.onClick.AddListener(Retry);
            btnContinue.onClick.AddListener(Continue);
        }

        void Retry()
        {
            GameManager.instance.RestartLevel();
            SelfClosing();
        }


        void Continue()
        {
            GameManager.instance.ContinueLevel();
            SelfClosing();
        }
    }
}
