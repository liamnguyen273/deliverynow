using Owlet.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow.Gameplay
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerStateMachine : MonoBehaviour
    {
        protected State state;
        PlayerController playerController;

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
            SetState(new PlayerMainHandler(playerController));
        }

        public void SetState(State newState)
        {
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
