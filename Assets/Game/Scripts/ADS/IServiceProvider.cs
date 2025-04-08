using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ADS
{
    public interface IServiceProvider
    {
        public void ReloadADS();
        public void ShowAds(Action onSuccess, Action onFailure);
        public ADTYPE GetAdType();
    }
}
