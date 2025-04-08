using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace ADS
{
    public class InterstitialServices : IServiceProvider
    {

        private string _id;

        private InterstitialAd _interstitialAd;
        public InterstitialServices(string id)
        {
            _id = id;
        }

        public ADTYPE GetAdType()
        {
            return ADTYPE.INTERSTITIAL;
        }

        public void ReloadADS()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad. " + this._id);

            // create our request used to load the ad.
            var adRequest = new GoogleMobileAds.Api.AdRequest();

            // send the request to load the ad.
            InterstitialAd.Load(_id, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                    RegisterReloadHandler(ad);
                });
        }

        public void ShowAds(System.Action onSuccess, System.Action onFailure)
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
                onSuccess?.Invoke();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
                onFailure?.Invoke();
            }
        }

        private void RegisterReloadHandler(InterstitialAd interstitialAd)
        {
            // Raised when the ad closed full screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial Ad full screen content closed.");

                // Reload the ad so that we can show another as soon as possible.
                this.ReloadADS();
            };
            // Raised when the ad failed to open full screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);

                // Reload the ad so that we can show another as soon as possible.
                this.ReloadADS();
            };
        }
    }

}