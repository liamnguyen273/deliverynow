using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADS
{
    public static class AdmobHelper
    {
        static AdConfig _adConfig;
        static BannerConfig _bannerConfig;
        public static UntilAdConfig AdConfig
        {
            get
            {
                if (_adConfig == null)
                {
                    _adConfig = Resources.Load<AdConfig>("AdConfig");
                }

                if (_adConfig != null) return _adConfig.adConfig;
                return null;
            }

        }

        public static BannerConfig BannerConfig
        {
            get
            {
                if (_bannerConfig == null)
                {
                    _bannerConfig = Resources.Load<BannerConfig>("BannerConfig");
                }
                if (_bannerConfig != null) return _bannerConfig;
                return null;
            }
        }
    }

}