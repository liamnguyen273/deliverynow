using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Triggers;
using DeliveryNow;
using UnityEditor;
using UnityEngine;
using DeliveryNow.Gameplay;

public class NPC : MonoBehaviour
{
    public static NPC Instance{get;set;}
    public static Action OnPlayerReached;
    public static Action OnNPCDataLoaded;
    private Vector3 playerPosition;
    private readonly float speed = 1f;
    public void Initialize(){
        Instance = this;
        OnNPCDataLoaded?.Invoke();
    }
    private void Awake(){
    }
    //Alt Metdod: Lift the NPC from the ground to avoid falling to the ground when collide with the Player
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("NPC collided with " + collision.gameObject.name);
            Destroy(gameObject);
            OnPlayerReached?.Invoke();
        }
    }
    private void Update(){
        if(playerPosition != null){
            transform.position = Vector3.MoveTowards(transform.position,playerPosition, speed * Time.deltaTime);
        }else{
            Debug.Log("Player has not been Initialized");
        }
    }
    public void SetPlayer(Vector3 playerPosition){
        this.playerPosition = playerPosition;
    }
    
}