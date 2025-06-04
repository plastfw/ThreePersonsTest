using System;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class AdsModel
    {
        private AdsView _view;
        private SwitchModelObserver _switchModelObserver;
        private LevelManager _levelManager;
        private IRewardedAds _rewardedAds;
        private SavesManager _savesManager;
        private IAnalytic _analytic;
        private IInterstitialAds _interstitialAds;

        public void Construct(AdsView view, SwitchModelObserver switchModel, LevelManager levelManager,
            IRewardedAds rewardedAds, SavesManager saves, IAnalytic analytic, IInterstitialAds interstitial)
        {
            _view = view;
            _switchModelObserver = switchModel;
            _levelManager = levelManager;
            _rewardedAds = rewardedAds;
            _savesManager = saves;
            _analytic = analytic;
            _interstitialAds = interstitial;
        }

        public void Show()
        {
        }

        public async void ConfirmClickedEvent()
        {
#if !UNITY_EDITOR
            try
            {
                bool adCompleted = await _rewardedAds.ShowRewardedAdAsync();
                ConfirmEvent();
                Debug.Log($"Rewarded ad completed: {adCompleted}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error showing rewarded ad: {e}");
            }
#else
            ConfirmEvent();
#endif
        }

        public async void RejectClicked()
        {
#if !UNITY_EDITOR
            try
            {
                bool adShown = await _interstitialAds.ShowInterstitialAsync();
                RejectEvent();
                Debug.Log($"Interstitial ad shown: {adShown}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error showing interstitial ad: {e}");
            }
#else
            RejectEvent();
#endif
        }

        private void ConfirmEvent()
        {
            _switchModelObserver.SaveData();
            _levelManager.LoadGameScene();
        }

        private void RejectEvent()
        {
            _analytic.LoseLevel();
            _levelManager.LoadMenuScene();
            _savesManager.DeleteAll();
        }
    }
}