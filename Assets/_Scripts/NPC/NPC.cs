using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DeliveryNow;
using DeliveryNow.Gameplay;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 endPoint;
    private float speed = 1f;
    private void Awake(){
        MapLoader.onMapLoaded += MapLoader_onMapLoaded;
    }

    private void MapLoader_onMapLoaded()
    {
        endPoint = player.transform.position;
        Debug.Log(player.transform.position);
    }

    private void Update(){
        if(endPoint == null){

        }else{
            transform.position = Vector3.MoveTowards(transform.position,endPoint, speed * Time.deltaTime);
        }
        
    }
}
