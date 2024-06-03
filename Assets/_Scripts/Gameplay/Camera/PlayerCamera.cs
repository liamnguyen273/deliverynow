using Cinemachine;
using DeliveryNow.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerCamera : MonoBehaviour
    {
        CinemachineVirtualCamera virtualCamera;


        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            PlayerController.onPlayerDataLoaded += LockOnPlayer;
        }

        private void OnDestroy()
        {
            PlayerController.onPlayerDataLoaded -= LockOnPlayer;
        }


        void LockOnPlayer(PlayerController player)
        {
            virtualCamera.Follow = player.body;
            virtualCamera.LookAt = player.body;
        }
    }
}
