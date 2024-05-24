using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Owlet;
namespace Owlet.SDK.Ads
{
    public class AdsManager : Singleton<AdsManager>
    {
        public static bool success = false;
#if UNITY_ANDROID
#if DEV
        string appKey = "85460dcd";
#else
        string appKey = "1cafcd615";
#endif

#elif UNITY_IPHONE
#if DEV
        string appKey = "85460dcd";
#else
        string appKey = "1cafd4925";
#endif

#else
        string appKey = "unexpected_platform";
#endif

        float interCooldown;
        float INTER_COOLDOWN = 45.0f;

        public static Action onInterClosed;
        public static Action onAdsRewarded;
        public static Action onAdsInitialize;

        public static Action<string> onInterShow;
        public static Action<string> onRewardedShow;

        public static Action<bool> onAdsRemove;
        public static bool removedAds;

        public static bool isShowingAds;
        int LEVEL_TO_SHOW_INTER = 8;

        protected override void Init()
        {
            //FalconRemoteConfigHandler.onDataFetched += FetchRemoteConfigData;
            RemoveAds(PlayerPrefs.GetInt("no_ads", 0) == 1);
        }

        private void Start()
        {
            if (!init) return;
        }

        private void OnEnable()
        {
            if (!init) return;

            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitialized;

            //Add AdInfo Interstitial Events
            IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        }

        public void InitIronSource(bool consented)
        {
            IronSource.Agent.setMetaData("is_child_directed", "false");
            IronSource.Agent.setConsent(consented);
            IronSource.Agent.shouldTrackNetworkState(true);
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(appKey);
            PlayerPrefs.SetInt("user_consent", consented == true ? 1 : 0);
            Debug.Log(">>>User consention: " + consented);
            success = true;
        }

        public void RemoveAds(bool remove)
        {
            removedAds = remove;
            onAdsRemove?.Invoke(remove);
            Debug.Log("$$$ " + remove);
            PlayerPrefs.SetInt("no_ads", remove ? 1 : 0);
        }
        private void Update()
        {
            interCooldown -= Time.deltaTime;
        }

        void SdkInitialized()
        {
            Debug.Log(">>> SDK initialized!!");
            LoadInterstitial();
            IronSourceEvents.onImpressionDataReadyEvent += AdsImpression.Send;
            interCooldown = 45;
            onAdsInitialize?.Invoke();
        }

        void FetchRemoteConfigData()
        {
            LEVEL_TO_SHOW_INTER = 6;
            ResetInterCooldown();
        }

        void ResetInterCooldown()
        {
              interCooldown = 45;
        }

        void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }

        #region interstitial

        public void LoadInterstitial()
        {
            IronSource.Agent.loadInterstitial();
        }
        public bool ShowInterstitial(string placement, Action onClosedCallBack = null)
        {
#if NO_ADS
            onClosedCallBack?.Invoke();
            return true;
#endif
            if (removedAds)
            {
                onClosedCallBack?.Invoke();
                return true;
            }
            //NOTE: Add more condition if needed here
            if (interCooldown >= 0)
            {
                onClosedCallBack?.Invoke();
                return true;
            }
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial(placement);
                onInterClosed = null;
                onInterClosed += () => onClosedCallBack?.Invoke();
                ResetInterCooldown();

                //Log
                /*AppsFlyerEventLogger.Log("af_inters_show");*/

                onInterShow?.Invoke(placement);
                isShowingAds = true;
                return true;
            }
            else
            {
                Debug.Log("interstitial not ready!!");
                onClosedCallBack?.Invoke();
                return false;
            }
        }


        /************* Interstitial AdInfo Delegates *************/
        // Invoked when the interstitial ad was loaded succesfully.
        void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the initialization process has failed.
        void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
        {
            LoadInterstitial();
            isShowingAds = false;

        }
        // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
        void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            //AppsFlyerEventLogger.Log("af_inters_displayed");

        }
        // Invoked when end user clicked on the interstitial ad
        void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the ad failed to show.
        void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
            LoadInterstitial();
            isShowingAds = false;

        }
        // Invoked when the interstitial ad closed and the user went back to the application screen.
        void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            LoadInterstitial();
            onInterClosed?.Invoke();
            onInterClosed = null;
            isShowingAds = false;
        }
        // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
        // This callback is not supported by all networks, and we recommend using it only if  
        // it's supported by all networks you included in your build. 
        void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
        {
        }

#endregion

        #region rewarded

        public void LoadRewarded()
        {
            IronSource.Agent.loadRewardedVideo();
        }
        public bool ShowRewarded(string placement, Action onClosedCallBack = null)
        {
#if UNITY_EDITOR || NO_ADS
            onClosedCallBack?.Invoke();
            return true;
#endif
            if (removedAds)
            {
                onClosedCallBack?.Invoke();
                return true;
            }

            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
                onAdsRewarded = null;
                onAdsRewarded += () => onClosedCallBack?.Invoke();

                //Log Event
                /*AppsFlyerEventLogger.Log("af_rewarded_show");*/
                onRewardedShow?.Invoke(placement);
                isShowingAds = true;
                return true;
            }
            else
            {
                Debug.Log("rewarded not ready!!");
                isShowingAds = false;

                return false;
            }
        }


        /************* RewardedVideo AdInfo Delegates *************/
        // Indicates that there�s an available ad.
        // The adInfo object includes information about the ad that was loaded successfully
        // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
        }
        // Indicates that no ads are available to be displayed
        // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        void RewardedVideoOnAdUnavailable()
        {

        }
        // The Rewarded Video ad view has opened. Your activity will loose focus.
        void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            //AppsFlyerEventLogger.Log("af_rewarded_displayed");
            isShowingAds = false;

        }
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            isShowingAds = false;

        }
        // The user completed to watch the video, and should be rewarded.
        // The placement parameter will include the reward data.
        // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            onAdsRewarded?.Invoke();
            onAdsRewarded = null;
            isShowingAds = false;
        }
        // The rewarded video ad was failed to show.
        void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the video ad was clicked.
        // This callback is not supported by all networks, and we recommend using it only if
        // it�s supported by all networks you included in your build.
        void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
        }

#endregion
    }
}
