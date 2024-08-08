using Cinemachine;
using DeliveryNow.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerCamera : MonoBehaviour
    {
        //Suggestion: Use SerializedField for virtualCamera
        CinemachineVirtualCamera virtualCamera;


        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            PlayerController.onPlayerDataLoaded += LockOnPlayer;
            PlayerHitbox.onCarHit += RemoveAim;
        }

        private void OnDestroy()
        {
            PlayerController.onPlayerDataLoaded -= LockOnPlayer;
            PlayerHitbox.onCarHit -= RemoveAim;
        }


        void LockOnPlayer(PlayerController player)
        {
            virtualCamera.Follow = player.body;
            virtualCamera.LookAt = player.body;
        }

        public void RemoveAim()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
        }
    }
}
