using Owlet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class SerializablePlayer : SerializableTransform
    {
        public override string GetTag()
        {
            return Keys.SerializableObject.Tags.Player;
        }
    }
}
