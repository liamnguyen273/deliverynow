using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerHitbox : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hit");
        }
    }
}
