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
            WALKING,
            WAVING
        }
        [SerializeField] private NPC npc;
        private Animator animator;
        private void Awake(){
            animator = gameObject.GetComponent<Animator>();
        }

        private void NPC_OnPlayerReached()
        {
            SetState(State.WALKING);
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
                case State.WALKING:
                    animator.SetTrigger("Walk");
                    break;
                case State.WAVING:
                    animator.SetTrigger("Wave");
                    break;
            }
        }
        
    }
}
