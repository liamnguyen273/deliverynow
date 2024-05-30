using Cysharp.Threading.Tasks;
using Lean.Pool;
using Newtonsoft.Json;
using Owlet;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] TextAsset data;

        private void Start()
        {
            Load();
        }

        [Button]
        async void Load()
        {
            string data = this.data.text;

            GameObject baseMapRef = await AddressableLoader.Load<GameObject>(Keys.Prefabs.BaseMap);
            GameObject baseMap = LeanPool.Spawn(baseMapRef, transform);

            var mapData = JsonConvert.DeserializeObject<MapData>(data);
            foreach ( var wrapper in mapData.baseObjectWrappers)
            {
                await HandleWrapper(wrapper, baseMap.transform);
            }
        }


        async UniTask HandleWrapper(SerializeWrapper wrapper, Transform baseMap)
        {
            if (wrapper.tag == Keys.SerializableObject.Tags.Transform)
            {
                SerializableTransform serializeTransform = await AddressableLoader.Load<SerializableTransform>(wrapper.addressableID);
                SerializableTransform instance = LeanPool.Spawn(serializeTransform, baseMap.transform);
                instance.Load(wrapper.data);
            }
        }
    }
}
