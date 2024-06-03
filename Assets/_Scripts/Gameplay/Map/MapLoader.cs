using Cysharp.Threading.Tasks;
using Lean.Pool;
using Newtonsoft.Json;
using Owlet;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace DeliveryNow
{
    public class MapLoader : Singleton<MapLoader>
    {
        static GameObject baseMapRef;
        static SerializablePlayer playerRef;
        static SerializableSpline pathRef;

        public static Action<float> onMapLoadProgressUpdated;
        public static Action onMapLoaded;
        public static Action onMapStartLoading;



        SerializablePlayer player;
        GameObject baseMap;
        SerializableSpline path;

        public async void LoadLevel(int level)
        {
            onMapStartLoading?.Invoke();
            CleanUpLevel();
            await LoadAssets();
            TextAsset textAsset = await AddressableLoader.Load<TextAsset>($"Level_{level}");
            string data = textAsset.text;

            baseMap = LeanPool.Spawn(baseMapRef, transform);

            var mapData = JsonConvert.DeserializeObject<MapData>(data);
            float totalProgress = mapData.baseObjectWrappers.Count + 3;
            float currentProgress = 0;
            foreach ( var wrapper in mapData.baseObjectWrappers)
            {
                await HandleWrapper(wrapper, baseMap.transform);
                onMapLoadProgressUpdated?.Invoke((++currentProgress) / totalProgress);
            }

            player = LeanPool.Spawn(playerRef, transform);
            path = LeanPool.Spawn(pathRef, transform);
            
            player.GetComponent<SplineAnimate>().Container = path.GetComponent<SplineContainer>();

            path.Load(mapData.path);
            onMapLoadProgressUpdated?.Invoke((++currentProgress) / totalProgress);
            player.Load(mapData.player);
            onMapLoadProgressUpdated?.Invoke((++currentProgress) / totalProgress);
            onMapLoaded?.Invoke();
        }

        public void CleanUpLevel()
        {
            if (transform.childCount == 0) return;
            List<IObjectSerializable> objects = baseMap.GetComponentsInChildren<IObjectSerializable>().ToList();
            for (int i = 0; i < objects.Count; i++)
            {
                LeanPool.Despawn(objects[i].GameObject());
            }

            LeanPool.Despawn(player);
            LeanPool.Despawn(path);
            LeanPool.Despawn(baseMap);
        }

        async UniTask LoadAssets()
        {
            if (baseMapRef == null) baseMapRef = await AddressableLoader.Load<GameObject>(Keys.Addressables.BaseMap);
            if (playerRef == null) playerRef = await AddressableLoader.Load<SerializablePlayer>(Keys.Addressables.Player);
            if (pathRef == null) pathRef = await AddressableLoader.Load<SerializableSpline>(Keys.Addressables.Path);
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
