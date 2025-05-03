using System;
using R3;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class AdsPresenter : IDisposable
    {
        private AdsModel _model;
        private AdsView _view;
        private IInterstitialAds _interstitialAds;
        private IRewardedAds _rewardedAds;
        private LevelManager _levelManager;
        private SwitchModelObserver _switchModelObserver;
        private SavesManager _savesManager;
        private IAnalytic _analytic;

        private readonly CompositeDisposable _disposable = new();

        public void Init(AdsModel model, AdsView view, IInterstitialAds interstitialAds, IRewardedAds rewardedAds,
            LevelManager levelManager, SwitchModelObserver observer, SavesManager savesManager, IAnalytic analytic)
        {
            _rewardedAds = rewardedAds;
            _interstitialAds = interstitialAds;
            _model = model;
            _view = view;
            _levelManager = levelManager;
            _switchModelObserver = observer;
            _savesManager = savesManager;
            _analytic = analytic;
        }

        public async void OnConfirmClicked()
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

        public async void OnRejectClicked()
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

        private void RejectEvent()
        {
            _analytic.LoseLevel();
            _levelManager.LoadMenuScene();
            _savesManager.DeleteAll();
        }

        private void ConfirmEvent()
        {
            _switchModelObserver.SaveData();
            _levelManager.LoadGameScene();
        }

        public void Dispose() => _disposable.Dispose();
    }
}