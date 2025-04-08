using System.Drawing;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UIElements;

namespace ADS
{
    [CreateAssetMenu(fileName = "BannerConfig", menuName = "Ads/BannerConfig")]
    public class BannerConfig : ScriptableObject
    {
        [SerializeField] private int _height = 250;
        [SerializeField] private int _width = 250;
        [SerializeField] private AdSize _adSize;
        [SerializeField] private AdPosition _adPosition;

        public AdSize AdSize
        {
            get
            {
                if (_adSize == null)
                {
                    _adSize = new AdSize(_width, _height);
                }
                return _adSize;
            }
        }
        public AdPosition AdPosition => _adPosition;

    }
}
