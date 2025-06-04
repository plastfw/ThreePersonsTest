using Cysharp.Threading.Tasks;
using R3;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.Purchasing;

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

        public MainMenuModel(
            SceneService sceneService,
            IAnalytic analytic,
            SavesManager saves,
            IIAPService iapService,
            MainMenuPresenter menuPresenter)
        {
            _sceneService = sceneService;
            _analytic = analytic;
            _saves = saves;
            _iapService = iapService;
            _menuPresenter = menuPresenter;
        }

        public void Init()
        {
            if (_saves.CurrentSettings.AdsDisabled)
                _menuPresenter.OnAdsButtonClicked();
        }

        public async UniTask StartGameAsync()
        {
            await _sceneService.LoadGameSceneAsync();
            _analytic.Login();
        }

        public async UniTask DisableAds()
        {
            if (_saves.CurrentSettings.AdsDisabled)
                return;

            if (!_iapService.IsInitialized)
            {
                Debug.LogError("IAP not initialized!");
                return;
            }

            IsPurchasing.Value = true;

            try
            {
                var product = _iapService.GetController().products.WithID("test.noads");

                if (product == null || !product.availableToPurchase)
                {
                    Debug.LogError("Product not available");
                    return;
                }

                _iapService.GetController().InitiatePurchase(product);
            }
            finally
            {
                IsPurchasing.Value = false;
            }
        }

        public async UniTask OnAdsPurchaseCompleted()
        {
            _saves.CurrentSettings.AdsDisabled = true;
            _saves.SaveSettings();
            await _menuPresenter.OnAdsButtonClicked();
            _analytic.LogEvent("ads_disabled_purchase");
        }

        public void UpdateFirebaseStatus(bool isReady) => IsFirebaseReady.Value = isReady;
        public void UpdateIAPStatus(bool isReady) => IsIAPReady.Value = isReady;
    }
}