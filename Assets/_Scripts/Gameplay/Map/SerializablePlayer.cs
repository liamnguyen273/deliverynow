using DeliveryNow.Gameplay;
using Owlet;
using System;
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

        public override void Load(string json)
        {
            base.Load(json);
            GetComponent<PlayerController>().Initialize();
        }
    }
}
