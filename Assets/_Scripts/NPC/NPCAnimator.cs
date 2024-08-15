using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class NPCAnimator : MonoBehaviour
    {
        private enum State{
            IDLE,
            WALK,
            WAVE
        }
        private Animator animator;
        private void Awake(){
            animator = GetComponent<Animator>();
            SetState(State.WALK);
        }

        private void NPC_OnPlayerReached()
        {
            SetState(State.WALK);
        }

        private void MapLoader_OnMapLoaded()
        {
            SetState(State.IDLE);
        }


        private void SetState(State state){
            switch(state){
                case State.IDLE:
                    animator.SetTrigger("Idle");
                    break;
                case State.WALK:
                    animator.SetTrigger("Walk");
                    break;
                case State.WAVE:
                    animator.SetTrigger("Wave");
                    break;
            }
        }
        
    }
}
