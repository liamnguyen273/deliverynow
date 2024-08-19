using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        private void Awake(){
            MapLoader.onMapLoaded += MapLoader_OnMapLoaded;
            musicAudioSource.Stop();
        }
        private void MapLoader_OnMapLoaded()
        {
            musicAudioSource.Play();
        }
    }
}
