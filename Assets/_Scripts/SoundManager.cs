using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource collision;
        private void Awake(){
            PlayerHitbox.onCarHit += PlayerHitbox_OnCarHit;
        }

        private void PlayerHitbox_OnCarHit()
        {
            collision.Play();
        }
    }
}
