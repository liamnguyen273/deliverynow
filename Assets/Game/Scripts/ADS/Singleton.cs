using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ADS
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        [SerializeField] private bool _persistance = false;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindAnyObjectByType<T>();
                    return _instance;
                }

                if(_instance == null)
                {
                    GameObject ins = new GameObject();
                    ins.name = typeof(T).Name;
                    _instance = ins.AddComponent<T>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this as T;
                if(_persistance) DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }


            this.Init();
        }

        protected virtual void Init()
        {

        }
    }
}
