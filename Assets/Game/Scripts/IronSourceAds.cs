using System;
using System.Collections.Generic;
using UnityEngine;
using Define;
using com.adjust.sdk;
using Firebase.Analytics;

public class IronSourceAds : Singleton<IronSourceAds>
{
    Action<AdsResult> Callback;

    AdsRewardType mRewardType;
    string location;
    AdsRewardType type;

    public bool RewardVideoAvailable
    {
        get
        {
#if UNITY_EDITOR
            return true;
#else
            return IronSource.Agent.isRewardedVideoAvailable(); 
#endif
        }
    }

    void Start()
    {
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;

        IronSource.Agent.validateIntegration();

        FirebaseMgr.Instance.LogEvent("FullAds_Load");
        IronSource.Agent.loadInterstitial();
    }

    string RewardTypeToString()
    {
        return Enum.GetName(typeof(AdsRewardType), mRewardType).ToLower(); ;
    }

    public void ShowInterstitial(string location, Action<AdsResult> callback)
    {
#if UNITY_EDITOR
        callback.Invoke(AdsResult.Watched);
#else
        Callback = callback;
        this.location = location;
        bool isReady = IronSource.Agent.isInterstitialReady();

        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location }
        };
        
        FirebaseMgr.Instance.LogEvent("FullAds_Show", events);

        if (isReady)
        {
            IronSource.Agent.showInterstitial();
            FirebaseMgr.Instance.LogEvent("FullAds_Show_Ready", events);
        }
        else
        {
            FirebaseMgr.Instance.LogEvent("FullAds_Show_NotReady", events);
        }
#endif
    }

    public void ShowRewardVideo(AdsRewardType type, string location, Action<AdsResult> callback)
    {
#if UNITY_EDITOR
        callback.Invoke(AdsResult.Watched);
#else
        this.type = type;
        this.location = location;
        bool isReady = IronSource.Agent.isRewardedVideoAvailable();
        Callback = callback;

        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "type", RewardTypeToString() },
            { "location", location }
        };
        
        FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show", events);

        if (isReady)
        {
            IronSource.Agent.showRewardedVideo();
            FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show_Ready", events);
        }
        else
        {
            Callback.Invoke(AdsResult.Fail);
            FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show_NotReady", events);
        }
#endif
    }

    void InterstitialAdOpenedEvent()
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location }
        };
        FirebaseMgr.Instance.LogEvent("FullAds_Show_Start", events);
    }

    void InterstitialAdClosedEvent()
    {
        FirebaseMgr.Instance.LogEvent("FullAds_Load");
        IronSource.Agent.loadInterstitial();

        Callback.Invoke(AdsResult.Watched);
    }

    void InterstitialAdClickedEvent()
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location }
        };
        FirebaseMgr.Instance.LogEvent("FullAds_Click", events);
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location },
            { "error", error.getDescription() }
        };
        FirebaseMgr.Instance.LogEvent("FullAds_Load_Failed", events);
    }

    void InterstitialAdShowSucceededEvent()
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location }
        };
        FirebaseMgr.Instance.LogEvent("FullAds_Show_Finished", events);
    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "location", location },
            { "error", error.getDescription() }
        };
        FirebaseMgr.Instance.LogEvent("FullAds_Show_Failed", events);

        Callback.Invoke(AdsResult.Fail);
    }

    void RewardedVideoAdOpenedEvent()
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "type", RewardTypeToString() },
            { "location", location }
        };
        FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show_Start", events);
    }

    void RewardedVideoAdClosedEvent()
    {
        Callback.Invoke(AdsResult.Closed);
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "type", RewardTypeToString() },
            { "location", location }
        };
        FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show_Finished", events);

        Callback.Invoke(AdsResult.Watched);
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "type", RewardTypeToString() },
            { "location", location },
            { "error", error.getDescription() }
        };
        FirebaseMgr.Instance.LogEvent("Rewarded_vid_Show_Failed", events);

        Callback.Invoke(AdsResult.Fail);
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {
        Dictionary<string, object> events = new Dictionary<string, object>() {
            { "type", RewardTypeToString() },
            { "location", location },
        };
        FirebaseMgr.Instance.LogEvent("Rewarded_vid_Click", events);
    }

    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("ImpressionSuccessEvent impressionData = " + impressionData);
        if (impressionData != null)
        {
            Parameter[] AdParameters = {
                new Firebase.Analytics.Parameter("adUnit", impressionData.adUnit),
                new Firebase.Analytics.Parameter("adNetwork", impressionData.adNetwork),
                new Firebase.Analytics.Parameter("instanceName", impressionData.instanceName),
                new Firebase.Analytics.Parameter("instanceId", impressionData.instanceId),
                new Firebase.Analytics.Parameter("country", impressionData.country),
                new Firebase.Analytics.Parameter("placement", impressionData.placement),
                new Firebase.Analytics.Parameter("revenue", (double)impressionData.revenue),
                new Firebase.Analytics.Parameter("precision", impressionData.precision),
                new Firebase.Analytics.Parameter("ab", impressionData.ab),
                new Firebase.Analytics.Parameter("segmentName", impressionData.segmentName),
                new Firebase.Analytics.Parameter("lifetimeRevenue", (double)impressionData.lifetimeRevenue),
            };
            FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
