using Cysharp.Threading.Tasks;
using Owlet.UI;
using Owlet.UI.Popups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow.Gameplay
{
    public class PlayerPrepare : BaseStatePlayerController
    {
        public PlayerPrepare(PlayerController playerController) : base(playerController)
        {
        }

        public override IEnumerator OnStateEnter()
        {
            OpenPopup();
            yield return base.OnStateEnter();
        }

        async void OpenPopup()
        {
            Popup popup = await PopupManager.instance.OpenUI<Popup>(Keys.Popups.TapToStart, 0);
            popup.onClosed += StartGame;
        }

        public override IEnumerator OnStateExit()
        {
            PopupManager.instance.CloseUI(Keys.Popups.TapToStart);
            return base.OnStateExit();
        }


        void StartGame()
        {
            stateMachine.SetState(new PlayerMainHandler(playerController));
        }

    }
}
