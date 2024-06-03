using Newtonsoft.Json;
using Owlet;
using Owlet.Systems.Currency;
using Owlet.Systems.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerDataManager : Singleton<PlayerDataManager>, ISaveable
    {
        [SerializeField] PlayerData data;

        protected override void Init()
        {
            SaveManager.saveableObjects.Add(this);
            base.Init();
        }

        public int GetCurrentLevel()
        {
            return data.currentLevel;
        }

        public void IncreaseCurrentLevel()
        {
            data.currentLevel++;
        }


        #region SaveLoad
        public void CreateNewSaveFile()
        {
            data = new();
        }

        public string ID()
        {
            return "player_data";
        }

        public bool Load(string json)
        {
            try
            {
                data = JsonConvert.DeserializeObject<PlayerData>(json);
                if (data == null) return false;
                return true;
            }
            catch
            {
                Helper.Log("Load Player Data failed");
                return false;
            }
        }

        public string Save()
        {
            return JsonConvert.SerializeObject(data);
        }
        #endregion
    }
}
