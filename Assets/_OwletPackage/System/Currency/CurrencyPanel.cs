using DG.Tweening;
using Owlet.Systems.Currency;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Owlet.UI
{
    public class CurrencyPanel : MonoBehaviour
    {
        [SerializeField] CurrencyType currencyType;
        [SerializeField] TextMeshProUGUI txtAmount;
        [SerializeField] Image icon;

        const float ANIMATION_TIME = 1f;

        private void Start()
        {
            CurrencyManager.onResourceGained += UpdateUI;
            CurrencyManager.onResourceUsed += UpdateUI;

            txtAmount.text = CurrencyManager.instance.GetResource(currencyType.currencyName).ToString();
            icon.sprite = currencyType.icon;
        }


        private void OnDestroy()
        {
            CurrencyManager.onResourceGained -= UpdateUI;
            CurrencyManager.onResourceUsed -= UpdateUI;
        }

        private void UpdateUI(CurrencyType type, int amount, string source)
        {
            if (type != currencyType) return;
            DOTween.Kill(gameObject);
            int currentAmount = int.Parse(txtAmount.text);
            DOVirtual.Float(currentAmount, amount, ANIMATION_TIME, (float value) =>
            {
                txtAmount.text = value.ToString();
            }).SetTarget(this);

        }
    }
}
