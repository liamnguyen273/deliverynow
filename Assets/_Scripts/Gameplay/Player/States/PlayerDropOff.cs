using System.Collections;
using System.Collections.Generic;
using DeliveryNow.Gameplay;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerDropOff : BaseStatePlayerController
    {
        public PlayerDropOff(PlayerController playerController) : base(playerController){   
        }
        public override IEnumerator OnStateEnter()
        {
            Debug.Log("The Player is dropping off NPC");
            yield return base.OnStateEnter();
        }
        public override IEnumerator OnStateExit()
        {
            Debug.Log("The player has dropping off the NPC");
            return base.OnStateExit();
        }
    }
}
