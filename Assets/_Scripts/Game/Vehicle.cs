using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;

public class Vehicle : MonoBehaviour
{
    enum State
    {
        IDLE,
        MOVE,
        CRASH
    }
    Rigidbody rb;
    State state;
    [SerializeField] State InitState;
    float crashVelocity;
    Vector3 StartPosition, crashPoint;
    Quaternion startRotation;
    SplineComputer splineComputer;
    SplineFollower splineFollower;
    void Start()
    {
        GameManager.Instance.AddVehicle(this);

        rb = GetComponent<Rigidbody>();
        splineComputer = GetComponent<SplineComputer>();
        splineFollower = GetComponent<SplineFollower>();
        
        StartPosition = transform.position;
        startRotation = transform.rotation;
        SetState(InitState);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.IDLE:
            
            break;
            case State.MOVE:
            break;
            case State.CRASH:
            break;
        }
    }

    void SetState(State state)
    {
        switch(state)
        {
            case State.IDLE:
                if(splineFollower)
                {
                    splineFollower.follow = false;
                }
                else
                {
                    transform.position = StartPosition;
                    transform. rotation = startRotation;
                }
            break;
            case State.MOVE:
                if(splineFollower)
                {
                    splineFollower.follow = true;
                }
            break;
            case State.CRASH:
                if(splineFollower)
                {
                    splineFollower.follow = false;
                }
                rb.AddForceAtPosition(Vector3.up * 3, crashPoint, ForceMode.VelocityChange);
            break;
        }
    }

    public void Reset()
    {
        SetState(InitState);
    }

    public void OnCrash(float velocity, Vector3 point)
    {
        crashVelocity = velocity;
        crashPoint = point;
        SetState(State.CRASH);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ContactPoint contact = collision.contacts[0];
            OnCrash(collision.relativeVelocity.magnitude, contact.point);

            GameManager.Instance.Car.OnCrash(collision.relativeVelocity.magnitude);
        }
    }
}
