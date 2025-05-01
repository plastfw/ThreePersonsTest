using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Ads
{
    public class RewardedAds : IDisposable
    {
        public void EnableAds()
        {
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        }

        public async UniTask<bool> ShowRewardedAdAsync()
        {
            if (!IronSource.Agent.isRewardedVideoAvailable())
            {
                Debug.Log("Rewarded video not available");
                return false;
            }

            var completionSource = new UniTaskCompletionSource<bool>();

            IronSourceRewardedVideoEvents.onAdClosedEvent += onAdClosed;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += onAdRewarded;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += onAdFailed;

            IronSource.Agent.showRewardedVideo();

            bool result = await completionSource.Task;

            IronSourceRewardedVideoEvents.onAdClosedEvent -= onAdClosed;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= onAdRewarded;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= onAdFailed;

            return result;

            void onAdClosed(IronSourceAdInfo adInfo) => completionSource.TrySetResult(false);

            void onAdRewarded(IronSourcePlacement placement, IronSourceAdInfo adInfo) =>
                completionSource.TrySetResult(true);

            void onAdFailed(IronSourceError error, IronSourceAdInfo adInfo) => completionSource.TrySetResult(false);
        }

        private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdUnavailable()
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
        }

        private void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdShowFailedEvent With Error" + ironSourceError +
                      "And AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement +
                      "And AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement +
                      "And AdInfo " + adInfo);
        }

        public void Dispose()
        {
            IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;
        }
    }
}