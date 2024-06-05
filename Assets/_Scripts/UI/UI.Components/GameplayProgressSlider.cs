using DeliveryNow.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow
{
    public class GameplayProgressSlider : MonoBehaviour
    {
        Slider slider;
        void Start()
        {
            slider = GetComponent<Slider>();
            slider.value = 0f;

            PlayerController.onProgressUpdate += UpdateUI;
        }

        private void OnDestroy()
        {
            PlayerController.onProgressUpdate -= UpdateUI;
        }

        void UpdateUI(float value)
        {
            slider.value = value;
        }
    }
}
