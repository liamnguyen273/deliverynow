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

        public MapData()
        {
            baseObjectWrappers = new();
        }
    }
}
