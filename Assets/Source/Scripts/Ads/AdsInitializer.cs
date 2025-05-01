using System;
using com.unity3d.mediation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Source.Scripts.Ads
{
    public class AdsInitializer : IDisposable
    {
        private readonly AdsConfig _config;
        private readonly InterstitialAds _interstitialAds;
        private readonly RewardedAds _rewardedAds;
        public bool isInitialized = false;

        public AdsInitializer(InterstitialAds interstitialAds, RewardedAds rewardedAds, AdsConfig config)
        {
            _interstitialAds = interstitialAds;
            _rewardedAds = rewardedAds;
            _config = config;
        }

        public void InitLevelPlay()
        {
            LevelPlay.Init(_config.GetAppKey(), GatUserId(), adFormats: new[] { LevelPlayAdFormat.REWARDED });
            LevelPlay.OnInitSuccess += OnInitializationComplete;
            LevelPlay.OnInitFailed += OnInitializationFailed;

            // IronSource.Agent.setMetaData("is_test_suite", "enable");
        }

        private void OnInitializationComplete(LevelPlayConfiguration config)
        {
            Debug.Log("Initialization completed successfully with config: " + config);
            isInitialized = true;

            //delete
            // IronSource.Agent.launchTestSuite();

            IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;

            // Enable ads
            _interstitialAds.EnableAds(_config.AndroidInterstitial);
            _rewardedAds.EnableAds();
        }

        private void OnInitializationFailed(LevelPlayInitError error)
        {
            Debug.Log("Initialization failed with error: " + error);
            isInitialized = false;
        }

        private void OnApplicationPause(bool isPaused) => IronSource.Agent.onApplicationPause(isPaused);

        private void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
        {
            Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData);
            Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
        }

        private string GatUserId() => SystemInfo.deviceUniqueIdentifier;

        public void Dispose() => _interstitialAds?.Dispose();
    }
}