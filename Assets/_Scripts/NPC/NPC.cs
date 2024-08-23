using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Triggers;
using DeliveryNow;
using UnityEditor;
using UnityEngine;
using DeliveryNow.Gameplay;
using UnityEngine.Splines;
using Lean.Pool;

public class NPC : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    public static NPC Instance{get;set;}
    public static Action OnPlayerReached;
    public static Action OnNPCDataLoaded;
    public static Action OnNPCReachedEndPath;
    private readonly float speed = 1f;
    private void Awake(){
        splineAnimate.Updated += splineAnimate_Update;
        PlayerController.onFinishLineReached += PlayerController_OnFinishLineReached;
    }

    private void OnDestroy()
    {
    splineAnimate.Updated -= splineAnimate_Update;
    PlayerController.onFinishLineReached -= PlayerController_OnFinishLineReached;
    }

    private void PlayerController_OnFinishLineReached()
    {
        gameObject.GetComponent<SplineAnimate>().Container = MapLoader.NPCEndPath.GetComponent<SplineContainer>();
        gameObject.SetActive(true);
        splineAnimate.Restart(true);
        splineAnimate.Play();
    }

    private void splineAnimate_Update(Vector3 vector, Quaternion quaternion)
    {
        if(splineAnimate.NormalizedTime == 1f){
            gameObject.SetActive(false);
            if(!GameManager.IsGameComplete){
                OnPlayerReached?.Invoke();
            }else{
                OnNPCReachedEndPath?.Invoke();
            }
        }
    }
    public void Initialize(){
        Instance = this;
        OnNPCDataLoaded?.Invoke();
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.MaxSpeed = speed;
        splineAnimate.Loop = SplineAnimate.LoopMode.Once;
        splineAnimate.Restart(true);
        splineAnimate.Play();
    }
}