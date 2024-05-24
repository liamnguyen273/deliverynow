using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet.SDK.Ads
{
    public class BannerHandler : Singleton<BannerHandler>
    {
        bool mrec = false;
        protected override void Init()
        {
            AdsManager.onAdsInitialize += LoadBanner;
            //Add AdInfo Banner Events
            IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
            IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
            IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
            IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
            IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
            IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

            AdsManager.onAdsRemove += RemoveBanner;
            //NoAdsButton.onNoAdsPurchased += DestroyBanner;
            //AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        }

        public void LoadBanner()
        {
#if NO_ADS
            return;
#endif
            Debug.Log(">>>Banner: try load");
            if (AdsManager.removedAds)
            {
                return;
            }
            var size = mrec? IronSourceBannerSize.RECTANGLE : IronSourceBannerSize.SMART;
            size.SetAdaptive(true);
            IronSource.Agent.loadBanner(size, IronSourceBannerPosition.BOTTOM);
        }
        public void SetMrec(bool mrec)
        {
            if(this.mrec != mrec)
            {
                this.mrec = mrec;
                DestroyBanner();
                LoadBanner();
            }
        }
        public void DestroyBanner()
        {
            IronSource.Agent.destroyBanner();
            Debug.Log(">>>Banner: Destroyed");
        }
        
        void RemoveBanner(bool remove)
        {
            if(remove)
            {
                DestroyBanner();
            }
        }


        /************* Banner AdInfo Delegates *************/
        //Invoked once the banner has loaded
        void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log(">>>Banner: Loaded");
        }
        //Invoked when the banner loading process has failed.
        void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
        {
            Debug.Log(">>>Banner load fail: " + ironSourceError.getErrorCode() + "__" + ironSourceError.getDescription());
            if (AdsManager.removedAds)
            {
                return;
            }
            LoadBanner();
        }
        // Invoked when end user clicks on the banner ad
        void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
        }
        //Notifies the presentation of a full screen content following user click
        void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log(">>>Banner: Present");

        }
        //Notifies the presented screen has been dismissed
        void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log(">>>Banner: Dismiss");

        }
        //Invoked when the user leaves the app
        void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log(">>>Banner: Left App");

        }

    }
}
