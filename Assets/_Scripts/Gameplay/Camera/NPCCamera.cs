using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using DeliveryNow.Gameplay;
using UnityEngine;

namespace DeliveryNow
{
    public class NPCCamera : MonoBehaviour
    {
        CinemachineVirtualCamera virtualCamera;
        private void Awake(){
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            NPC.OnNPCDataLoaded += NPC_OnNPCDataLoaded;
            NPC.OnPlayerReached += NPC_OnPlayerReached;
            PlayerController.onFinishLineReached += PlayerController_OnFinishLineReached;
        }

        private void PlayerController_OnFinishLineReached()
        {
            virtualCamera.gameObject.SetActive(true);
        }

        private void NPC_OnPlayerReached()
        {
            virtualCamera.gameObject.SetActive(false);
        }

        private void NPC_OnNPCDataLoaded()
        {
            virtualCamera.Follow = NPC.Instance.gameObject.GetComponent<Transform>();
            virtualCamera.LookAt = NPC.Instance.gameObject.GetComponent<Transform>();
        }
    }
}
