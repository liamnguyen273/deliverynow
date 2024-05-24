using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class Helicopter : MonoBehaviour
{

    enum State
    {
        IDLE,
        PICKUP,
        CARRY,
        DELIVERY,
        DELIVERED
    }
    [SerializeField] AudioSource audioSource;
    [SerializeField] SplineFollower splineFollower;
    [SerializeField] GameObject package;
    State state;
    SplineSample sample = new SplineSample();

    bool IsTouchDown = false;
    float Speed;
    float MaxSpeed = 10;
    Vector3 TouchDownPos;
    float angle;

    void Start()
    {
        splineFollower.onEndReached += OnEndPath;
        SetState(State.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsTouchDown = true;
            TouchDownPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsTouchDown = false;
        }

        if (CanControl() && IsTouchDown)
        {
            // rb.AddForce(Vector3.up * 20, ForceMode.Acceleration);
            Speed += Time.deltaTime;
            Speed = Mathf.Min(Speed, MaxSpeed);
        }
        else
        {
            Speed -= Time.deltaTime * 4;
            Speed = Mathf.Max(Speed, 0);
        }
        splineFollower.followSpeed = Speed;
        

        UpdateSound();

        
    }

    void LatedUpdate()
    {
        switch (state)
        {
            case State.CARRY:
                if (splineFollower.spline != null)
                {
                    splineFollower.spline.Project(sample, transform.position);
                    GameManager.Instance.UpdateProgress((float)sample.percent);
                }
                break;
        }
    }

    void SetState(State state)
    {
        switch (state)
        {
            case State.IDLE:
                Speed = 0;
                break;
            case State.PICKUP:
                audioSource.Play();
                Speed = 0;
                break;
            case State.CARRY:
                ShowTutorial();
                break;
            case State.DELIVERY:
                break;
            case State.DELIVERED:
                audioSource.Stop();
                break;
        }
        this.state = state;
    }

    void UpdateSound()
    {
        float pitch = Mathf.Max(0.6f, (Speed / MaxSpeed));
        audioSource.pitch = pitch;
    }

    bool CanControl()
    {
        return state == State.PICKUP || state == State.CARRY || state == State.DELIVERY;
    }

    public void StartPickup(SplineComputer pickupPath)
    {
        SetPath(pickupPath);
        SetState(State.PICKUP);
    }
    public void StartCarry(SplineComputer carryPath)
    {
        SetPath(carryPath);
        SetState(State.CARRY);
    }
    public void StartDelivery()
    {
        EndDelivery();
    }
    public void EndPickUp()
    {
        DOVirtual.DelayedCall(2, () =>
        {
            GameManager.Instance.EndPickUp();
        });
    }
    public void EndCarry()
    {
        DOVirtual.DelayedCall(2, EndDelivery);
    }
    public void EndDelivery()
    {
        SetState(State.DELIVERED);
        GameManager.Instance.EndDelivery();
    }

    public void SetPath(SplineComputer sp)
    {
        splineFollower.spline = sp;
        splineFollower.SetPercent(0);
    }

    void OnEndPath()
    {
        if (state == State.PICKUP)
        {
            EndPickUp();

        }
        else if (state == State.CARRY)
        {
            EndCarry();
        }
    }

    public void Reset()
    {
        Speed = 0;
        splineFollower.SetPercent(0);
        SetState(State.PICKUP);
    }

    void ShowTutorial()
    {
        //TODO: Show tutorial popup
        //UIPopup.GetPopup(Define.Popup.TUTORIAL_FLY).Show();
    }
}
