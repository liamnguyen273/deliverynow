using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasFindUICamera : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        }

        [Button]
        void Find()
        {
            GetComponent<Canvas>().worldCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        }
    }
}
