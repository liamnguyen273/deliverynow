using Owlet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DeliveryNow
{
    public class MapData
    {
        public List<SerializeWrapper> baseObjectWrappers;
        public string player;
        public string path;
        public string npc;
        public string npcStartPath;
        public string npcEndPath;

        public MapData()
        {
            baseObjectWrappers = new();
        }
    }
}
