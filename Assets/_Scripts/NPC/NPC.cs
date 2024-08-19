using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Triggers;
using DeliveryNow;
using UnityEditor;
using UnityEngine;
using DeliveryNow.Gameplay;
using UnityEngine.Splines;

public class NPC : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;
    public static NPC Instance{get;set;}
    public static Action OnPlayerReached;
    public static Action OnNPCDataLoaded;
    public static event EventHandler OnStateChanged;
    private Vector3 playerPosition;
    private readonly float speed = 1f;
    public void Initialize(){
        Instance = this;
        OnNPCDataLoaded?.Invoke();
        splineAnimate.Restart(true);
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("NPC collided with " + collision.gameObject.name);
            Destroy(gameObject);
            OnPlayerReached?.Invoke();
        }
    }
    public void SetPlayer(Vector3 playerPosition){
        this.playerPosition = playerPosition;
    }
    
}