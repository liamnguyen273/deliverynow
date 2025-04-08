using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Drawing;
using UnityEngine.UIElements;

namespace ADS
{
    public class BannerServices : IServiceProvider
    {
        private string _id;

        private AdPosition _adPosition;
        BannerView _bannerView;

        public BannerServices(string id, AdPosition adPosition)
        {
            _id = id;
            _adPosition = adPosition;
        }

        public ADTYPE GetAdType()
        {
            return ADTYPE.BANNER;
        }

        public void ReloadADS()
        {
            // create an instance of a banner view first.
            if (_bannerView == null)
            {
                CreateBannerView();
            }

            // create our request used to load the ad.
            var adRequest = new GoogleMobileAds.Api.AdRequest();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }

        public void ShowAds(System.Action onSuccess, System.Action onFailure)
        {
            this.ReloadADS();
        }

        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyAd();
            }

            // Create a 320x50 banner at top of the screen
            _bannerView = new BannerView(_id, AdSize.Banner, this._adPosition);
            ListenToAdEvents();
        }

        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        private void ListenToAdEvents()
        {
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
                ReloadADS();
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
                ReloadADS();
            };
        }


    }

}