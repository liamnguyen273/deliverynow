using Owlet.StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeliveryNow;

namespace DeliveryNow.Gameplay
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public State state { get; private set; }
        PlayerController playerController;

        private void Awake()
        {
            PlayerController.onPlayerDataLoaded += Inititalize;
            NPC.OnPlayerReached += NPC_onPlayerReached;
            PlayerController.onFinishLineReached += PlayerController_OnFinishLineReached;
        }

        private void PlayerController_OnFinishLineReached()
        {
            SetState(new PlayerDropOff(playerController));
        }

        private void NPC_onPlayerReached()
        {
            if(!GameManager.IsGameComplete){
                SetState(new PlayerPrepare(playerController));
            }else{
                GameManager.instance.CompleteLevel();
            }
        }

        private void OnDestroy()
        {
            PlayerController.onPlayerDataLoaded -= Inititalize;
        }


        void Inititalize(PlayerController playerController)
        {
            this.playerController = playerController;
            switch(GameManager.Restart){
                case true:
                    SetState(new PlayerPrepare(playerController));
                    break;
                case false:
                    SetState(new PlayerPickup(playerController));
                    break;
            }
        }

        public void SetState(State newState)
        {
            if (!gameObject.activeInHierarchy) return;
            Debug.Log(newState);
            if (state != null) StartCoroutine(state.OnStateExit());
            if (newState == null) return;
            state = newState;
            StartCoroutine(state.OnStateEnter());
        }

        private void Update()
        {
            if (state == null) return;
            state.Update();
        }
    }
}
