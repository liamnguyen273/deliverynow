using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADS
{
    public enum AdType
    {
        GAME_START,
        GAME_LEVEL_START,
        GAME_LEVEL_FINISHED,
        GAME_LEVEL_FAILED,
        GAME_LIFE_LOST,
        GAME_PAUSE,
        GAME_RESUME,
        GAME_OVER,
        GAME_CHECKPOINT,
        GAME_TUTORIAL,
        GAME_IAP,
        GAME_ACHIEVEMENT_UNLOCKED,
        GAME_COUNTDOWN,
        GAME_DAILY_REWARD
    }

    public class AdsManager : Singleton<AdsManager>
    {
        public Action OnSuccessAds;
        public Action OnFailedAds;

        public void ShowAd()
        {
            SoundManager.Instance.Mute();
#if UNITY_EDITOR || !UNITY_WEBGL
            Debug.Log("Show Mid Roll Ads");
#else
            WeeGooAdManager.Instance.GetAd();
#endif
            SoundManager.Instance.UnMute();
        }

        public void ShowRewardedAd(Action onRewSuccess, Action onRewFailed = null)
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            Debug.Log("Show Rewarded Ads");
            onRewSuccess?.Invoke();
            return;
#endif
            SoundManager.Instance.Mute();
            OnSuccessAds = onRewSuccess;
            OnFailedAds = onRewFailed;
            WeeGooAdManager.Instance.ShowRewardAd();
        }

        public void OnSuccess()
        {
            OnSuccessAds?.Invoke();
            OnSuccessAds = null;
            OnFailedAds = null;
            SoundManager.Instance.UnMute();

            Debug.Log("Ad Success");
        }

        public void OnFailed()
        {
            OnFailedAds?.Invoke();
            OnSuccessAds = null;
            OnFailedAds = null;
            Debug.Log("Ad Failed");
            SoundManager.Instance.UnMute();
        }


        public void AdEvents(AdType adType)
        {
            SoundManager.Instance.Mute();
#if UNITY_EDITOR || !UNITY_WEBGL
            Debug.Log("Show Mid Roll Ads: " + adType.ToString());
#else
            switch (adType)
            {
                case AdType.GAME_START:
                    WeeGooAdManager.Instance.GAME_START();
                    break;
                case AdType.GAME_LEVEL_START:
                    WeeGooAdManager.Instance.GAME_LEVEL_START();
                    break;
                case AdType.GAME_LEVEL_FINISHED:
                    WeeGooAdManager.Instance.GAME_LEVEL_FINISHED();
                    break;
                case AdType.GAME_LEVEL_FAILED:
                    WeeGooAdManager.Instance.GAME_LEVEL_FAILED();
                    break;
                case AdType.GAME_LIFE_LOST:
                    WeeGooAdManager.Instance.GAME_LIFE_LOST();
                    break;
                case AdType.GAME_PAUSE:
                    WeeGooAdManager.Instance.GAME_PAUSE();
                    break;
                case AdType.GAME_RESUME:
                    WeeGooAdManager.Instance.GAME_RESUME();
                    break;
                case AdType.GAME_OVER:
                    WeeGooAdManager.Instance.GAME_OVER();
                    break;
                case AdType.GAME_CHECKPOINT:
                    WeeGooAdManager.Instance.GAME_CHECKPOINT();
                    break;
                case AdType.GAME_TUTORIAL:
                    WeeGooAdManager.Instance.GAME_TUTORIAL();
                    break;
                case AdType.GAME_IAP:
                    WeeGooAdManager.Instance.GAME_IAP();
                    break;
                case AdType.GAME_ACHIEVEMENT_UNLOCKED:
                    WeeGooAdManager.Instance.GAME_ACHIEVEMENT_UNLOCKED();
                    break;
                case AdType.GAME_COUNTDOWN:
                    WeeGooAdManager.Instance.GAME_COUNTDOWN();
                    break;
                case AdType.GAME_DAILY_REWARD:
                    WeeGooAdManager.Instance.GAME_DAILY_REWARD();
                    break;
                default:
                    break;
            }
#endif
            SoundManager.Instance.Mute();

        }
    }
}
