using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Triggers;
using DeliveryNow;
using UnityEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    private bool _reachedPlayer = false;
    public static NPC Instance{get;set;}
    public static Action OnPlayerReached;
    private Player player;
    private Vector3 endPoint;
    private readonly float speed = 1f;
    private void Awake(){
        MapLoader.onMapLoaded += MapLoader_onMapLoaded;
        Instance = this;
    }

    private void MapLoader_onMapLoaded(){
        endPoint = player.transform.position;
        Debug.Log(player.transform.position);
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("NPC collided with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
    private void Update(){
        if(endPoint != null){
            transform.position = Vector3.MoveTowards(transform.position,endPoint, speed * Time.deltaTime);
        }else{
            Debug.Log("Player has not been Initialized");
        }
    }
    public void SetPlayer(Player player){
        this.player = player;
    }
    
}