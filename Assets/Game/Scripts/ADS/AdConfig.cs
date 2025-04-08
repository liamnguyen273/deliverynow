using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADS
{
    [CreateAssetMenu(fileName = "AdConfig", menuName = "Ads/AdConfig")]
    public class AdConfig : ScriptableObject
    {
        [SerializeField] private UntilAdConfig _android;
        [SerializeField] private UntilAdConfig _ios;

        public UntilAdConfig adConfig
#if UNITY_ANDROID
            => _android;
#else
        => _ios;
#endif
    }

    [System.Serializable]
    public class UntilAdConfig
    {
        [SerializeField] private string _banner;
        [SerializeField] private string _intersitial;
        [SerializeField] private string _reward;

        public string Banner => _banner;
        public string Intersitial => _intersitial;
        public string Reward => _reward;

    }

}