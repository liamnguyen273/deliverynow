using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Owlet.StateMachines
{
    public class State
    {
        public State()
        {

        }


        public virtual IEnumerator OnStateEnter()
        {
            yield break;
        }
        public virtual IEnumerator OnStateExit()
        {
            yield break;
        }

        /// <summary>
        /// The state handler need to call this function on its Update function
        /// </summary>
        public virtual void Update()
        {

        }

    }

}