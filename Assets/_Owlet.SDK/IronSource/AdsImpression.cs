
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Owlet.SDK.Ads
{
    public class AdsImpression
    {
        public static Action<IronSourceImpressionData> onImpressionSend;
        public static void Send(IronSourceImpressionData data)
        {
            FirebaseImpression(data);
            onImpressionSend?.Invoke(data);
#if APPSFLYER
            AppsFlyerImpression(data);
#endif
        }

        private static void FirebaseImpression(IronSourceImpressionData data)
        {
            Firebase.Analytics.Parameter[] parameters = {
                new("ad_source", data.adNetwork),
                new("ad_unit_name", data.adUnit),
                new("currency", "USD"),
                new("value", data.revenue.Value),
                new("placement", data.placement),
                new("country_code", data.country),
                new("ad_format", data.instanceName),
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("ad_impression_ironsource", parameters);

        }

#if APPSFLYER
        private static void AppsFlyerImpression(IronSourceImpressionData data)
        {
            Dictionary<string, string> dic = new()
            {
                { AFAdRevenueEvent.COUNTRY, data.country },
                { AFAdRevenueEvent.AD_UNIT, data.adUnit },
                { AFAdRevenueEvent.AD_TYPE, data.instanceName },
                { AFAdRevenueEvent.PLACEMENT, data.placement },
                { AFAdRevenueEvent.ECPM_PAYLOAD, data.encryptedCPM }
            };

            AppsFlyerAdRevenue.logAdRevenue(data.adNetwork, AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeIronSource, data.revenue.Value, "USD", dic);

        }
#endif

    }
}
