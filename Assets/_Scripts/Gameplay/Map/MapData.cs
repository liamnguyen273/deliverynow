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

        public MapData()
        {
            baseObjectWrappers = new();
        }
    }
}
