using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;

public class Obstacle : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("........");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ContactPoint contact = collision.contacts[0];
            GameManager.Instance.Car.OnCrash(collision.relativeVelocity.magnitude);
        }
    }
}
