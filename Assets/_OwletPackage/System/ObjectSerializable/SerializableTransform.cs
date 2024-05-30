using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet
{

    public class SerializableTransformData
    {
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public SerializableVector3 scale;
    }

    public class SerializableTransform : MonoBehaviour, IObjectSerializable
    {
        
        public GameObject GameObject()
        {
            return gameObject;
        }

        public virtual string GetTag()
        {
            return "transform";
        }

        public virtual void Load(string json)
        {
            var serializedData = JsonConvert.DeserializeObject<SerializableTransformData>(json);
            transform.position = serializedData.position.UnityVector;
            transform.rotation = Quaternion.Euler(serializedData.rotation.UnityVector);
            transform.localScale = serializedData.scale.UnityVector;
        }

        public virtual string Save()
        {
            SerializableTransformData data = new();
            data.position = new(transform.position);
            data.rotation = new(transform.rotation.eulerAngles);
            data.scale = new(transform.localScale);

            return JsonConvert.SerializeObject(data);
        }
    }
}
