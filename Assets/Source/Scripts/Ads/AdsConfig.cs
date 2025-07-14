using UnityEngine;

namespace Source.Scripts.Ads
{
    public class AdsConfig : MonoBehaviour
    {
        [SerializeField] private string AndroidAppKey = "85460dcd";
        [SerializeField] private string AndroidRewarded = "5cpemg8qrqfqwncx";
        [SerializeField] private string AndroidInterstitial = "vcpo4oh8w53qf9ze";
        [SerializeField] private string Unexpected = "unexpected_platform";

        public string GetAppKey()
        {
#if UNITY_ANDROID
            return AndroidAppKey;
#else
            return Unexpected;
#endif
        }

        public string GetInterstitial() => AndroidInterstitial;
    }
}