using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using Doozy.Engine.UI;

public class Car : MonoBehaviour
{
    enum State
    {
        IDLE,
        TUTORIAL,
        RUN,
        BREAK,
        CRASH,
        FINISH
    }
    enum SubState
    {
        MIDLE,
        LEFT,
        RIGHT
    }
    float AccelerationSpeed = 10, BreakSpeed = 40, MaxSpeed = 20, LaneOffset = 2.1f;
    public TrailRenderer[] trailRenderers;
    float Speed, touchDownTime;
    bool IsTouchDown = false;
    Vector3 TouchDownPos, TouchOffset;
    State state;
    SubState subState;
    [SerializeField] Rigidbody modelRigidbody;
    [SerializeField] Collider modelCollider;
    [SerializeField] SplineFollower splineFollower;
    SplineComputer splineComputer;
    [SerializeField] Transform Main, Model;
    SplineSample splineSample = new SplineSample();
    Sequence sequence;
    [SerializeField] AudioSource audioSourceEngine;
    [SerializeField] AudioClip[] audioClipHorns;
    [SerializeField] AudioClip audioClipSkid, audioClipCrash, audioClipStartEngine, audioClipDoor;
    bool CanControl = false;
    Quest quest;
    void Start()
    {
        splineFollower.onEndReached += OnEndPath;
        SetState(State.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.TUTORIAL:
                if (Input.GetMouseButtonUp(0))
                {
                    SetState(State.RUN);
                }
                break;
            case State.RUN:
                {

                    Speed = Mathf.Lerp(Speed, MaxSpeed, Time.deltaTime * AccelerationSpeed);

                    if (Input.GetMouseButtonDown(0))
                    {
                        IsTouchDown = true;
                        touchDownTime = 0;

                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        IsTouchDown = false;
                        if (subState == SubState.LEFT)
                        {
                            SetSubState(SubState.RIGHT);
                        }
                        else
                        {
                            SetSubState(SubState.LEFT);
                        }
                    }
                    if (IsTouchDown)
                    {
                        touchDownTime += Time.deltaTime;
                        if (touchDownTime >= 0.2f)
                        {
                            SetState(State.BREAK);
                        }
                    }
                }
                break;
            case State.BREAK:
                Speed -= Time.deltaTime * BreakSpeed;
                Speed = Mathf.Max(Speed, 0);
                if (Input.GetMouseButtonUp(0))
                {
                    IsTouchDown = false;
                    SetState(State.RUN);
                }
                break;
            case State.CRASH:
                Speed -= Time.deltaTime * BreakSpeed;
                Speed = Mathf.Max(Speed, 0);
                break;
            case State.FINISH:
                Speed = Mathf.Lerp(Speed, 0, Time.deltaTime * BreakSpeed);
                if (Speed <= 0)
                {
                    SetState(State.IDLE);
                    CanControl = false;
                    Speed = 0;
                }
                break;
        }

        //}
        UpdateSpline();
        UpdateSound();
    }

    bool ShouldBreak()
    {
        int count = 0;
        foreach (var touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                count++;
            }
        }
        return count >= 2;
    }

    void SetState(State state)
    {
        if (this.state != state)
        {
            switch (state)
            {
                case State.TUTORIAL:
                    ShowTutorial();
                    break;
                case State.IDLE:

                    modelRigidbody.isKinematic = true;
                    modelCollider.enabled = false;
                    Model.DOLocalMove(Vector3.zero, 0.5f);
                    Model.DOLocalRotate(Vector3.zero, 0.5f);
                    SetSubState(SubState.RIGHT);
                    Speed = 0;
                    break;
                case State.RUN:
                    modelCollider.enabled = true;
                    SetTrail(false);
                    break;
                case State.BREAK:
                    SetTrail(true);
                    PlayTireSkidSound();
                    break;
                case State.CRASH:
                    StartCoroutine(MissionFail());
                    modelRigidbody.isKinematic = false;
                    modelRigidbody.AddForce(Vector3.up * 3, ForceMode.VelocityChange);
                    modelRigidbody.AddForce(Vector3.forward * Speed * 0.1f, ForceMode.VelocityChange);
                    SetTrail(false);
                    PlayCrashSound();
                    //CanControl = false;
                    Speed = 0;
                    break;
                case State.FINISH:
                    break;
            }
            this.state = state;
        }
    }

    void SetSubState(SubState state)
    {
        if (subState != state)
        {
            switch (state)
            {
                case SubState.RIGHT:
                    sequence = DOTween.Sequence();
                    sequence.Append(Main.DOLocalRotateQuaternion(Quaternion.Euler(0, 5, 0), 0.1f));
                    sequence.Append(Main.DOLocalMoveX(LaneOffset, 0.3f));
                    sequence.Append(Main.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.1f));
                    sequence.Play();
                    break;
                case SubState.LEFT:
                    sequence = DOTween.Sequence();
                    sequence.Append(Main.DOLocalRotateQuaternion(Quaternion.Euler(0, -5, 0), 0.1f));
                    sequence.Append(Main.DOLocalMoveX(-LaneOffset, 0.3f));
                    sequence.Append(Main.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.1f));
                    sequence.Play();
                    break;
                case SubState.MIDLE:
                    Main.DOLocalMoveX(0, 0.3f);
                    break;
            }
            subState = state;
            TouchDownPos = Input.mousePosition;
            PlayTireSkidSound();
        }
    }

    void UpdateSpline()
    {
        if (splineComputer)
        {
            splineFollower.followSpeed = Speed;
            splineComputer.Project(splineSample, transform.position);
            GameManager.Instance.UpdateProgress((float)splineSample.percent);

            if (splineSample.percent >= 1)
            {
                Speed = 0;
            }
        }
    }

    void UpdateSound()
    {
        float pitch = Mathf.Max(0.2f, (Speed / MaxSpeed));
        audioSourceEngine.pitch = pitch;
    }

    public void PlayHorn()
    {
        var audio = audioClipHorns[Random.Range(0, audioClipHorns.Length)];
        audio.PlaySfx();
        Debug.Log("Play horn");
    }

    void PlayTireSkidSound()
    {
        audioClipSkid.PlaySfx();
    }

    void PlayCrashSound()
    {
        audioClipCrash.PlaySfx();
    }

    void SetTrail(bool value)
    {
        foreach (var trail in trailRenderers)
        {
            trail.emitting = value;
        }
    }

    public void OnEndPath()
    {
        EndCarry();
    }

    public void OnCrash(float velocity)
    {
        SetState(State.CRASH);
    }

    public void Reset()
    {
        SetState(State.IDLE);
        splineFollower.SetPercent(0);
        //CanControl = true;
        DG.Tweening.DOVirtual.DelayedCall(1, () =>
        {
            audioClipStartEngine.PlaySfx();
        });
        DG.Tweening.DOVirtual.DelayedCall(2, () =>
        {
            SetState(State.TUTORIAL);
        });
    }

    IEnumerator MissionFail()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.MissionFail();
    }

    public void SetPath(SplineComputer sc)
    {
        splineComputer = sc;
        splineFollower.spline = sc;
        splineFollower.SetPercent(0);
        SetSubState(SubState.RIGHT);
    }

    public void StartCarry(SplineComputer path)
    {
        // CanControl = true;
        SetPath(path);
        audioClipDoor.PlaySfx();
        DG.Tweening.DOVirtual.DelayedCall(1, () =>
        {
            audioClipStartEngine.PlaySfx();
        });
        DG.Tweening.DOVirtual.DelayedCall(2, () =>
        {
            SetState(State.TUTORIAL);
        });
    }
    public void EndCarry()
    {
        SetState(State.FINISH);
        GameManager.Instance.EndCarry();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    void ShowTutorial()
    {
        UIPopup.GetPopup(Define.Popup.TUTORIAL_CAR).Show();
    }
}
