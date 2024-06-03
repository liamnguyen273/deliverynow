using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] Slider slider;
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            EnableUI();

            MapLoader.onMapLoadProgressUpdated += UpdateProgress;
            MapLoader.onMapLoaded += DisableUI;
            MapLoader.onMapStartLoading += EnableUI;

        }

        private void OnDestroy()
        {
            MapLoader.onMapLoadProgressUpdated -= UpdateProgress;
            MapLoader.onMapLoaded -= DisableUI;
            MapLoader.onMapStartLoading -= EnableUI;
        }

        private void OnApplicationQuit()
        {
            canvasGroup.alpha = 0f;
        }


        void UpdateProgress(float value)
        {
            slider.value = value;
        }

        void DisableUI()
        {
            canvasGroup.alpha = 0f;
        }

        void EnableUI()
        {
            canvasGroup.alpha = 1f;
            slider.value = 0f;
        }
    }
}
