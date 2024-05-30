using Newtonsoft.Json;
using Owlet;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeliveryNow
{
    public class MapSaver : ObjectSerializer
    {
        [SerializeField] SerializablePlayer player;

        protected override void Serialize()
        {
            List<IObjectSerializable> objects =
                new List<IObjectSerializable>(transform.GetComponentsInChildren<IObjectSerializable>());

            objects.Remove(player);

            MapData mapData = new();

            foreach (var obj in objects)
            {
                SerializeWrapper wrapper = new();
                wrapper.addressableID = GetTrueName(obj.GameObject().name);
                wrapper.tag = obj.GetTag();
                wrapper.data = obj.Save();
                mapData.baseObjectWrappers.Add(wrapper);
            }

            mapData.player = player.Save();

            string finalData = JsonConvert.SerializeObject(mapData, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            SaveJsonToFile(finalData);
            AssetDatabase.Refresh();
        }
    }
}
