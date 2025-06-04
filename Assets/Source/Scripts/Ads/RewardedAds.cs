using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Ads
{
    public class RewardedAds : IRewardedAds
    {
        private UniTaskCompletionSource<bool> _completionSource;

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

            _completionSource = new UniTaskCompletionSource<bool>();
            IronSource.Agent.showRewardedVideo();
            bool result = await _completionSource.Task;
            _completionSource = null;
            return result;
        }

        private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
        }

        private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
            _completionSource?.TrySetResult(false);
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
            _completionSource?.TrySetResult(false);
        }

        private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
        {
            Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement +
                      "And AdInfo " + adInfo);
            _completionSource?.TrySetResult(true);
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
            
            _completionSource?.TrySetResult(false);
            _completionSource = null;
        }
    }
}