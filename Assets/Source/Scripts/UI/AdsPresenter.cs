using System;
using R3;
using Source.Scripts.Ads;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class AdsPresenter : IDisposable
    {
        private AdsModel _model;
        private AdsView _view;
        private InterstitialAds _interstitialAds;
        private RewardedAds _rewardedAds;
        private LevelManager _levelManager;
        private SwitchModelObserver _switchModelObserver;
        private SavesManager _savesManager;

        private readonly CompositeDisposable _disposable = new();

        public void Init(AdsModel model, AdsView view, InterstitialAds interstitialAds, RewardedAds rewardedAds,
            LevelManager levelManager, SwitchModelObserver observer, SavesManager savesManager)
        {
            _rewardedAds = rewardedAds;
            _interstitialAds = interstitialAds;
            _model = model;
            _view = view;
            _levelManager = levelManager;
            _switchModelObserver = observer;
            _savesManager = savesManager;
        }

        public async void OnConfirmClicked()
        {
#if !UNITY_EDITOR
            try
            {
                bool adCompleted = await _rewardedAds.ShowRewardedAdAsync();
                //Здесь логика после просмотра рекламы
                _view.Hide();
                _switchModelObserver.SaveData();
                _levelManager.LoadGameScene();
                Debug.Log($"Rewarded ad completed: {adCompleted}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error showing rewarded ad: {e}");
            }
#else
            _view.Hide();
            _switchModelObserver.SaveData();
            _levelManager.LoadGameScene();
            Debug.LogWarning("Продолжить игру с того же места");
#endif
        }

        public async void OnRejectClicked()
        {
#if !UNITY_EDITOR
            try
            {
                bool adShown = await _interstitialAds.ShowInterstitialAsync();
                // Общая логика после показа
                _view.Hide();
                _levelManager.LoadMenuScene();
                _savesManager.DeleteAll();
                Debug.Log($"Interstitial ad shown: {adShown}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error showing interstitial ad: {e}");
            }
#else
            _view.Hide();
            _levelManager.LoadMenuScene();
            _savesManager.DeleteAll();
            Debug.LogWarning("Выйти в меню");
#endif
        }

        public void Dispose() => _disposable.Dispose();
    }
}