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

        protected override void OnTap()
        {
            playerController.ChangeLane();
            playerController.ResetSpeed();
        }

        protected override void OnTouch()
        {
            playerController.StopMoving();
            base.OnTouch();
        }

        protected override void OnRelease()
        {
            playerController.StartMoving();
            base.OnRelease();
        }
    }
}
