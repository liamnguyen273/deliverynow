using Cinemachine;
using DeliveryNow.Gameplay;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace DeliveryNow
{
    public class PlayerCameraTesting : MonoBehaviour
    {
        [Button]
        void FindPlayer()
        {
            GameObject player = GameObject.Find("Player");
            if (player == null) return;
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController == null) return;
            var camera = GetComponent<CinemachineVirtualCamera>();
            camera.LookAt = playerController.body;
            camera.Follow = playerController.body;
        }
    }
}
