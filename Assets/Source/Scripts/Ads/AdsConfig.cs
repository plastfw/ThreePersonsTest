﻿using UnityEngine;

namespace Source.Scripts.Ads
{
    [CreateAssetMenu(fileName = "IronSourceConfig", menuName = "ScriptableObjects/IronSource Config")]
    public class AdsConfig : ScriptableObject
    {
        public string AndroidAppKey = "85460dcd";
        public string AndroidRewarded = "5cpemg8qrqfqwncx";
        public string AndroidInterstitial = "vcpo4oh8w53qf9ze";

        public string GetAppKey()
        {
#if UNITY_ANDROID
            return AndroidAppKey;
#elif UNITY_IPHONE
        return IOSAppKey;
#else
        return "unexpected_platform";
#endif
        }

        public string GetInterstitialAdUnitId() => AndroidInterstitial;

        public string GetRewardedAdUnitId() => AndroidRewarded;
    }
}