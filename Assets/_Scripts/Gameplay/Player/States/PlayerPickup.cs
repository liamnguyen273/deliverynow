using System.Collections;
using System.Collections.Generic;
using DeliveryNow.Gameplay;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerPickup : BaseStatePlayerController
    {
        public PlayerPickup(PlayerController playerController) : base(playerController){
        }
        public override IEnumerator OnStateEnter()
        {
            yield return base.OnStateEnter();
        }
        public override IEnumerator OnStateExit()
        {
            return base.OnStateExit();
        }
    }
}
