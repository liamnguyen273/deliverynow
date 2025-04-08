using Unity.Services.LevelPlay;

namespace ADS
{
    public class IronBannerServices : IServiceProvider
    {
        private LevelPlayBannerAd bannerAd;
        private string _id;

        public IronBannerServices(string id)
        {
            //Create banner instance
            bannerAd = new LevelPlayBannerAd(id);
            //Subscribe BannerAd events
            bannerAd.OnAdLoaded += BannerOnAdLoadedEvent;
            bannerAd.OnAdLoadFailed += BannerOnAdLoadFailedEvent;
            bannerAd.OnAdDisplayed += BannerOnAdDisplayedEvent;
            bannerAd.OnAdDisplayFailed += BannerOnAdDisplayFailedEvent;
            bannerAd.OnAdClicked += BannerOnAdClickedEvent;
            bannerAd.OnAdCollapsed += BannerOnAdCollapsedEvent;
            bannerAd.OnAdLeftApplication += BannerOnAdLeftApplicationEvent;
            bannerAd.OnAdExpanded += BannerOnAdExpandedEvent;
            _id = id;
        }

        public ADTYPE GetAdType()
        {
            return ADTYPE.BANNER;
        }

        public void ReloadADS()
        {
            bannerAd.LoadAd();
        }

        public void ShowAds(System.Action onSuccess, System.Action onFailure)
        {
            bannerAd.ShowAd();
        }

        //Implement BannAd Events
        void BannerOnAdLoadedEvent(LevelPlayAdInfo adInfo) { }
        void BannerOnAdLoadFailedEvent(LevelPlayAdError ironSourceError) { }
        void BannerOnAdClickedEvent(LevelPlayAdInfo adInfo) { }
        void BannerOnAdDisplayedEvent(LevelPlayAdInfo adInfo) { }
        void BannerOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError adInfoError) { }
        void BannerOnAdCollapsedEvent(LevelPlayAdInfo adInfo) { }
        void BannerOnAdLeftApplicationEvent(LevelPlayAdInfo adInfo) { }
        void BannerOnAdExpandedEvent(LevelPlayAdInfo adInfo) { }
    }
}
