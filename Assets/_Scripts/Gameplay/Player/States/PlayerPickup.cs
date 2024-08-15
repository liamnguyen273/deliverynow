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
            Debug.Log("The Player is picking up NPC");
            yield return base.OnStateEnter();
        }
        public override IEnumerator OnStateExit()
        {
            Debug.Log("The player has picked up the NPC");
            return base.OnStateExit();
        }
    }
}
