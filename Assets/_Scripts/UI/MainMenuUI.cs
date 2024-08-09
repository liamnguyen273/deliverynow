using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryNow
{
    public class MainMenuUI : MonoBehaviour
    {
        public enum buttonMenu{
            bonus,
            daily,
            customize,
            challenge,
        }
        public static MainMenuUI Instance {get;private set;}
        [SerializeField] Button playButton;
        [SerializeField] Button bonusButton;
        [SerializeField] Button dailyButton;
        [SerializeField] Button customizeButton;
        [SerializeField] Button challengeButton;
        public event EventHandler<buttonMenu> OnButtonPressed;

        private void Awake(){
            Instance = this;
        }
        private void Start(){
            playButton.onClick.AddListener(()=>
            {
                Loader.Load(Loader.Scene.MainGameScene);
            });
            bonusButton.onClick.AddListener(()=>
            {
                OnButtonPressed?.Invoke(this,buttonMenu.bonus);
            });
            dailyButton.onClick.AddListener(()=>{
                OnButtonPressed?.Invoke(this,buttonMenu.daily);
            });
        }
    }
}
