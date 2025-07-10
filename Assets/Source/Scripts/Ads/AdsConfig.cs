using UnityEngine;

namespace Source.Scripts.Ads
{
    [CreateAssetMenu(fileName = "IronSourceConfig", menuName = "ScriptableObjects/IronSource Config")]
    public class AdsConfig : ScriptableObject
    {
        private string AndroidAppKey = "85460dcd";
        private string AndroidRewarded = "5cpemg8qrqfqwncx";
        private string AndroidInterstitial = "vcpo4oh8w53qf9ze";

        public string GetAppKey()
        {
#if UNITY_ANDROID
            return AndroidAppKey;
#else
        return "unexpected_platform";
#endif
        }

        public string GetInterstitial() => AndroidInterstitial;
    }
}