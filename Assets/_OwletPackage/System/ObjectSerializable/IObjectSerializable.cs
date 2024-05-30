using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet
{
    public interface IObjectSerializable
    {
        /// <summary>
        /// Serialize the Object to Json
        /// </summary>
        /// <returns></returns>
        public string Save();

        /// <summary>
        /// Set Json data to Object
        /// </summary>
        /// <param name="json"></param>
        public void Load(string json);

        public GameObject GameObject();

        public string GetTag();
    }
}
