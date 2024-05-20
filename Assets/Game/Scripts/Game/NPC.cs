using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class NPC : MonoBehaviour
{
    public enum State
    {
        IDLE,
        WALK
    }
    SplineComputer splineComputer;
    SplineFollower splineFollower;
    public Animator animator;
    public State state;
    void Start()
    {
        splineComputer = GetComponent<SplineComputer>();
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.onEndReached += OnEnd;
        SetState(state);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
                
            break;
            case State.WALK:
            break;
        }
    }

    void SetState(State state)
    {
        switch(state)
        {
            case State.IDLE:
                animator.SetTrigger("Idle");
                splineFollower.follow = false;
            break;
            case State.WALK:
                animator.SetTrigger("Walking");
                splineFollower.follow = true;
            break;
        }
        this.state = state;
    }

    void OnEnd()
    {
        splineFollower.SetPercent(0);
    }
}
