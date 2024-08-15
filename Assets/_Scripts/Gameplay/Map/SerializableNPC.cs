using System.Collections;
using System.Collections.Generic;
using Owlet;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace DeliveryNow
{
    public class SerializableNPC : SerializableTransform
    {
        public override string GetTag()
        {
            return Keys.SerializableObject.Tags.NPC;
        }

        public override void Load(string json)
        {
            base.Load(json);
            GetComponent<NPC>().Initialize();
        }
    }
}
