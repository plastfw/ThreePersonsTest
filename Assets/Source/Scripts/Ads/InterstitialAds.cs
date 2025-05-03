using System;
using com.unity3d.mediation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Ads
{
    public class InterstitialAds : IInterstitialAds
    {
        private LevelPlayInterstitialAd interstitialAd;
        private Action onAdCompleteCallback;

        public void EnableAds(string interstitialAdUnitId)
        {
            interstitialAd = new LevelPlayInterstitialAd(interstitialAdUnitId);

            interstitialAd.OnAdLoaded += InterstitialOnAdLoadedEvent;
            interstitialAd.OnAdLoadFailed += InterstitialOnAdLoadFailedEvent;
            interstitialAd.OnAdDisplayed += InterstitialOnAdDisplayedEvent;
            interstitialAd.OnAdDisplayFailed += InterstitialOnAdDisplayFailedEvent;
            interstitialAd.OnAdClicked += InterstitialOnAdClickedEvent;
            interstitialAd.OnAdClosed += InterstitialOnAdClosedEvent;
            interstitialAd.OnAdInfoChanged += InterstitialOnAdInfoChangedEvent;

            LoadInterstitial();
        }

        private void LoadInterstitial() => interstitialAd.LoadAd();

        public async UniTask<bool> ShowInterstitialAsync()
        {
            if (!interstitialAd.IsAdReady())
            {
                Debug.Log("Interstitial ad not ready");
                return false;
            }

            var completionSource = new UniTaskCompletionSource<bool>();

            void OnAdCompleted(bool success)
            {
                completionSource.TrySetResult(success);
                // Отписываемся от событий
                interstitialAd.OnAdDisplayed -= OnDisplayed;
                interstitialAd.OnAdClosed -= OnClosed;
                interstitialAd.OnAdDisplayFailed -= OnDisplayFailed;
            }

            void OnDisplayed(LevelPlayAdInfo _) => OnAdCompleted(true);
            void OnClosed(LevelPlayAdInfo _) => OnAdCompleted(true);
            void OnDisplayFailed(LevelPlayAdDisplayInfoError _) => OnAdCompleted(false);

            // Подписываемся на события
            interstitialAd.OnAdDisplayed += OnDisplayed;
            interstitialAd.OnAdClosed += OnClosed;
            interstitialAd.OnAdDisplayFailed += OnDisplayFailed;

            interstitialAd.ShowAd();

            bool result = await completionSource.Task;
            LoadInterstitial(); // Загружаем следующий баннер
            return result;
        }


        private void InterstitialOnAdLoadedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("unity-script: I got InterstitialOnAdLoadedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdLoadFailedEvent(LevelPlayAdError error)
        {
            Debug.Log("unity-script: I got InterstitialOnAdLoadFailedEvent With Error " + error);
            onAdCompleteCallback?.Invoke(); // Execute the callback on ad completion
        }

        private void InterstitialOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("unity-script: I got InterstitialOnAdDisplayedEvent With AdInfo " + adInfo);
            onAdCompleteCallback?.Invoke(); // Execute the callback on ad completion
            LoadInterstitial(); // Load the next ad
        }

        private void InterstitialOnAdDisplayFailedEvent(LevelPlayAdDisplayInfoError infoError)
        {
            Debug.Log("unity-script: I got InterstitialOnAdDisplayFailedEvent With InfoError " + infoError);
            onAdCompleteCallback?.Invoke(); // Execute the callback on ad failed
        }

        private void InterstitialOnAdClickedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdClosedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
        }

        private void InterstitialOnAdInfoChangedEvent(LevelPlayAdInfo adInfo)
        {
            Debug.Log("unity-script: I got InterstitialOnAdInfoChangedEvent With AdInfo " + adInfo);
        }

        public void Dispose() => interstitialAd?.Dispose();
    }
}