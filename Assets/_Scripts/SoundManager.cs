using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource collision;
        [SerializeField] private AudioSource coinCollect;
        private void Awake(){
            PlayerHitbox.onCarHit += PlayerHitbox_OnCarHit;
            Coin.OnCoinCollected += Coin_OnCoinCollected;
        }

        private void Coin_OnCoinCollected()
        {
            coinCollect.Play();   
        }

        private void PlayerHitbox_OnCarHit()
        {
            collision.Play();
        }
    }
}
