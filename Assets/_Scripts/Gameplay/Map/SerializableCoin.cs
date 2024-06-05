using Owlet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class SerializableCoin : SerializableTransform
    {
        public override string GetTag()
        {
            return "coin";
        }
    }
}
