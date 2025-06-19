using System;
using Unity.Services.LevelPlay;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Source.Scripts.Ads
{
    public class AdsInitializer : IDisposable, IAdsInitializer
    {
        private readonly AdsConfig _config;
        private readonly IInterstitialAds _interstitialAds;
        private readonly IRewardedAds _rewardedAds;
        public bool IsInitialized { get; private set; } = false;

        public AdsInitializer(IInterstitialAds interstitialAds, IRewardedAds rewardedAds, AdsConfig config)
        {
            _interstitialAds = interstitialAds;
            _rewardedAds = rewardedAds;
            _config = config;
        }

        //В доке и гпт говорит что ревады только в легаси пакете есть.
        //Поэтому сделал так
        public void Init()
        {
            LevelPlay.Init(
                _config.GetAppKey(),
                GatUserId(),
                adFormats: new[] { com.unity3d.mediation.LevelPlayAdFormat.REWARDED }
            );

            LevelPlay.OnInitSuccess += OnInitializationComplete;
            LevelPlay.OnInitFailed += OnInitializationFailed;

            // IronSource.Agent.setMetaData("is_test_suite", "enable");
        }


        private void OnInitializationComplete(LevelPlayConfiguration config)
        {
            Debug.Log("Initialization completed successfully with config: " + config);
            IsInitialized = true;

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
            IsInitialized = false;
        }

        private void OnApplicationPause(bool isPaused) => IronSource.Agent.onApplicationPause(isPaused);

        private void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
        {
            Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData);
            Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
        }

        private string GatUserId() => SystemInfo.deviceUniqueIdentifier;

        public void Dispose()
        {
            _rewardedAds?.Dispose();
            _interstitialAds?.Dispose();
        }
    }
}