using Owlet.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        private void OnDestroy()
        {
            PlayerController.onPlayerDataLoaded -= Inititalize;
        }


        void Inititalize(PlayerController playerController)
        {
            this.playerController = playerController;
            SetState(new PlayerPrepare(playerController));
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
