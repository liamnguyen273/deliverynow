using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] Button playButton;
        private void Start(){
            playButton.onClick.AddListener(()=>
            {
                Loader.Load(Loader.Scene.MainGameScene);
            });
        }
    }
}
