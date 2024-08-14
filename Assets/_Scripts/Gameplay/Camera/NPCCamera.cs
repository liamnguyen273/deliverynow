using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DeliveryNow
{
    public class NPCCamera : MonoBehaviour
    {
        CinemachineVirtualCamera virtualCamera;
        private void Awake(){
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }
}
