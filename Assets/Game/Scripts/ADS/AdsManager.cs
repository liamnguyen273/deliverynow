using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADS
{
    public class AdRequest
    {
        public readonly ADTYPE AdType;
        public readonly Action onSuccess;
        public readonly Action onFailure;

        public AdRequest(ADTYPE adType, Action onSuccess = null, Action onFailure = null)
        {
            this.AdType = adType;
            this.onSuccess = onSuccess;
            this.onFailure = onFailure;
        }

    }

    public class AdsManager : Singleton<AdsManager>
    {
        public static bool isFreeAds;
        protected override void Init()
        {
            base.Init();
            GameMonetize.OnResumeGame += OnResumeGame;
            GameMonetize.OnPauseGame += OnPauseGame;
        }

        private void OnDisable()
        {
            GameMonetize.OnResumeGame -= OnResumeGame;
            GameMonetize.OnPauseGame -= OnPauseGame;
        }

        public void RequestAd(AdRequest adRequest)
        {
            ShowAd();
            adRequest.onSuccess?.Invoke();
        }

        public void OnResumeGame()
        {
            Time.timeScale = 1;
            SoundManager.Instance.UnMute();

        }

        public void OnPauseGame()
        {
            SoundManager.Instance.Mute();
            Time.timeScale = 0;

        }

        public void ShowAd()
        {
            GameMonetize.Instance.ShowAd();
        }
    }
}
