using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DeliveryNow
{
    public class LevelText : MonoBehaviour
    {
        TextMeshProUGUI text;
        
        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.SetText($"Level {PlayerDataManager.instance.GetCurrentLevel()}");

            PlayerDataManager.onCurrentLevelChange += UpdateUI;
        }

        private void OnDestroy()
        {
            PlayerDataManager.onCurrentLevelChange -= UpdateUI;
        }


        void UpdateUI(int level)
        {
            text.SetText($"Level {level}");
        }
    }
}
