using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
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

        private List<IServiceProvider> _serviceProviders;

        protected override void Init()
        {
            base.Init();
            _serviceProviders = GetDependency();
        }

        private void Start()
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
                foreach (var serviceProvider in _serviceProviders)
                {
                    serviceProvider.ReloadADS();
                }
            });

        }

        private List<IServiceProvider> GetDependency()
        {
            List<IServiceProvider> service = new List<IServiceProvider>();
            var banner = new BannerServices(AdmobHelper.AdConfig.Banner, AdmobHelper.BannerConfig.AdPosition);
            var inter = new InterstitialServices(AdmobHelper.AdConfig.Intersitial);
            var reward = new RewardServices(AdmobHelper.AdConfig.Reward);
            Debug.Log("Init insterId: " + AdmobHelper.AdConfig.Intersitial);
            Debug.Log("Init rewardId: " + AdmobHelper.AdConfig.Reward);
            Debug.Log("Init bannerId: " + AdmobHelper.AdConfig.Banner);

            service.Add(inter);
            service.Add(reward);
            service.Add(banner);
            return service;
        }

        public void RequestAd(AdRequest adRequest)
        {
            //Handler Free Ads

            IServiceProvider service = this._serviceProviders.Find(x => x.GetAdType() == adRequest.AdType);
            if (service == null)
            {
                adRequest.onFailure?.Invoke();
                return;
            }

            service.ShowAds(adRequest.onSuccess, adRequest.onFailure);
        }
    }
}
