using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class BaseObstacle : MonoBehaviour
    {
        Rigidbody rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }

        private void OnDisable()
        {
            if(rb != null) rb.velocity = Vector3.zero;
        }
    }
}
