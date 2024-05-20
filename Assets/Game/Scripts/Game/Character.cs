using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;

public class Character : MonoBehaviour
{
    enum State
    {
        INIT,
        PICKUP,
        DELIVERY,
        DRIVE,
        DELIVERED
    }
    [SerializeField] Animator animator;
    [SerializeField] SplineFollower splineFollower;
    State state;
    float Speed = 2;
    Quest quest;
    void Start()
    {
        splineFollower.onEndReached +=  OnMoveDone;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.PICKUP:

            break;
            case State.DELIVERY:

            break;
            case State.DRIVE:

            break;
            case State.DELIVERED:
            break;
        }
    }

    void SetState(State state)
    {
        switch(state)
        {
            case State.INIT:
                splineFollower.SetPercent(0);
                splineFollower.followSpeed = 0;
            break;
            case State.PICKUP:
                gameObject.SetActive(true);
                animator.Play("Walking");
                splineFollower.followSpeed = Speed;
            break;
            case State.DELIVERY:
                gameObject.SetActive(true); 
                animator.Play("Walking");
                splineFollower.followSpeed = Speed;
            break;
            case State.DRIVE:
                gameObject.SetActive(false);
            break;
            case State.DELIVERED:
                animator.Play("Idle");
            break;
        }

        this.state = state;
    }

    public void StartPickup(SplineComputer pickupPath)
    {
        SetPath(pickupPath);
        SetState(State.PICKUP);
    }
    public void StartCarry(SplineComputer carryPath)
    {
        SetState(State.DRIVE);
    }
    public void StartDelivery(SplineComputer deliveryPath)
    {
        SetPath(deliveryPath);
        SetState(State.DELIVERY);
    }

    public void EndPickUp()
    {
        SetState(State.DRIVE);
        GameManager.Instance.EndPickUp();
    }

    public void EndDelivery()
    {
        SetState(State.DELIVERED);
        GameManager.Instance.EndDelivery();
    }

    void OnMoveDone()
    {
        if(state == State.PICKUP)
        {
            EndPickUp();
        }
        else if(state == State.DELIVERY)
        {
            EndDelivery();
        }
    }

    void SetPath(SplineComputer splineComputer)
    {
        splineFollower.spline = splineComputer;
        SetState(State.INIT);
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
