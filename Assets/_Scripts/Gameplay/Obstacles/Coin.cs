using Lean.Pool;
using Owlet.Systems.Currency;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Keys.Tags.Player))
            {
                CurrencyManager.instance.GainResource(Keys.Currency.Coin,1 ,"pick_up");
                LeanPool.Despawn(gameObject);
            }
        }
    }
}
