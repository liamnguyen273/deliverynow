using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet.StateMachines
{
    public class StateMachine : Singleton<StateMachine>
    {
        protected State state;


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
