using Cysharp.Threading.Tasks;
using R3;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class MainMenuModel
    {
        private readonly SceneService _sceneService;
        private readonly IAnalytic _analytic;
        private readonly SavesManager _saves;
        private readonly IIAPService _iapService;
        private MainMenuPresenter _menuPresenter;

        public readonly ReactiveProperty<bool> IsFirebaseReady = new();
        public readonly ReactiveProperty<bool> IsIAPReady = new();
        public readonly ReactiveProperty<bool> IsPurchasing = new(false);
        public readonly ReactiveProperty<bool> AdsDisabled = new(false);

        public SavesManager Saves => _saves;

        public MainMenuModel(
            SceneService sceneService,
            IAnalytic analytic,
            SavesManager saves,
            IIAPService iapService)
        {
            _sceneService = sceneService;
            _analytic = analytic;
            _saves = saves;
            _iapService = iapService;
        }

        public async UniTask Init() => AdsDisabled.Value = _saves.LoadSettings();

        public async UniTask StartGameAsync()
        {
            await _sceneService.LoadGameSceneAsync();
            _analytic.Login();
        }

        public async UniTask DisableAds()
        {
            if (_saves.LoadSettings())
                return;

            if (!_iapService.IsInitialized)
            {
                Debug.LogError("IAP not initialized!");
                return;
            }

            IsPurchasing.Value = _iapService.DisableAds();
        }

        public async UniTask OnAdsPurchaseCompleted()
        {
            _saves.SaveAdsState(true);
            AdsDisabled.OnNext(true);
            _analytic.LogEvent("ads_disabled_purchase");
        }

        public void UpdateFirebaseStatus(bool isReady) => IsFirebaseReady.Value = isReady;
        public void UpdateIAPStatus(bool isReady) => IsIAPReady.Value = isReady;
    }
}