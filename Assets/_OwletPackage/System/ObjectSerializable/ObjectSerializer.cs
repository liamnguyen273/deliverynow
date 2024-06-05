using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Owlet
{
    public class ObjectSerializer : MonoBehaviour
    {

        [Button]
        protected virtual void Serialize()
        {
            List<IObjectSerializable> objects =
                new List<IObjectSerializable>(transform.GetComponentsInChildren<IObjectSerializable>());

            List<SerializeWrapper> wrappers = new List<SerializeWrapper>();

            foreach (var obj in objects)
            {
                SerializeWrapper wrapper = new();
                wrapper.addressableID = GetTrueName(obj.GameObject().name);
                wrapper.tag = obj.GetTag();
                wrapper.data = obj.Save();
                wrappers.Add(wrapper);
            }

            string finalData = JsonConvert.SerializeObject(wrappers);
            SaveJsonToFile(finalData);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }

        protected void SaveJsonToFile(string jsonData)
        {
            string directoryPath = Path.Combine(Application.dataPath, "_Addressables/Levels");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{GetTrueName(gameObject.name)}.json");

            File.WriteAllText(filePath, jsonData);

            Debug.Log($"Data saved to {filePath}");
        }


        protected string GetTrueName(string input)
        {
            int index = input.IndexOf(" (");
            if (index != -1)
            {
                return input.Substring(0, index);
            }
            return input;

        }
    }
}
