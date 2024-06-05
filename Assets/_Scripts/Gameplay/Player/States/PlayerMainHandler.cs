using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow.Gameplay
{
    public class PlayerMainHandler : BaseStatePlayerController
    {
        public PlayerMainHandler(PlayerController playerController) : base(playerController)
        {
        }

        public override IEnumerator OnStateEnter()
        {
            playerController.StartCar();
            return base.OnStateEnter();
        }

        public override IEnumerator OnStateExit()
        {
            return base.OnStateExit();
        }

        protected override void OnTap()
        {
            playerController.ChangeLane();
            playerController.ResetSpeed();
        }

        protected override void OnTouch()
        {
            playerController.Brake();
            base.OnTouch();
        }

        protected override void OnRelease()
        {
            playerController.StartEngine();
            base.OnRelease();
        }
    }
}
