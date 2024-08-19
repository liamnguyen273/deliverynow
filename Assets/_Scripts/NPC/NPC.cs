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
    public static event EventHandler OnStateChanged;
    private readonly float speed = 1f;
    private void Awake(){
        splineAnimate.Updated += splineAnimate_Update;
        PlayerController.onFinishLineReached += PlayerController_onFinishLineReached;
    }

    private void PlayerController_onFinishLineReached()
    {
        gameObject.GetComponent<SplineAnimate>().Container = MapLoader.npcEndPath.GetComponent<SplineContainer>();
        LeanPool.Spawn(gameObject);
        splineAnimate.Play();
    }

    private void splineAnimate_Update(Vector3 vector, Quaternion quaternion)
    {
        if(splineAnimate.NormalizedTime == 1f){
            gameObject.SetActive(false);
            OnPlayerReached?.Invoke();
        }
    }

    public void Initialize(){
        Instance = this;
        OnNPCDataLoaded?.Invoke();
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.MaxSpeed = speed;
        splineAnimate.Loop = SplineAnimate.LoopMode.Once;
        splineAnimate.PlayOnAwake = true;
        
    }

    
}