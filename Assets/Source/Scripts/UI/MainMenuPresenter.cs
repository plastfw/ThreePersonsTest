using System;
using Cysharp.Threading.Tasks;
using R3;
using Source.Scripts.Core;

namespace Source.Scripts.UI
{
    public class MainMenuPresenter : IDisposable
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private IIAPService _iap;
        private SavesManager _saves;
        private readonly CompositeDisposable _disposables = new();
        private IAddressableLoader _loader;

        public MainMenuPresenter(IIAPService service, SavesManager saves, IAddressableLoader loader)
        {
            _iap = service;
            _saves = saves;
            _loader = loader;
        }

        public async UniTask Init(MainMenuView view, MainMenuModel model)
        {
            _model = model;
            _view = view;

            _model.AdsDisabled
                .Where(isDisabled => isDisabled) // реагируем только на true
                .Subscribe(_ => OnAdsButtonClicked().Forget())
                .AddTo(_disposables);

            _model.IsFirebaseReady
                .Subscribe(isReady => _view.ChangeButtonState(isReady))
                .AddTo(_disposables);

            _model.IsIAPReady
                .Subscribe(isReady => _view.ChangeAdsButtonState(isReady))
                .AddTo(_disposables);

            _iap.PurchaseCompleted
                .Subscribe(_ => _model.OnAdsPurchaseCompleted().Forget())
                .AddTo(_disposables);

            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _saves.IsReady)
                // UniTask.WaitUntil(() => _loader.IsReady)
            );

            if (_model.Saves.LoadSettings())
            {
                _view.HideAdsButton();
                _model.AdsDisabled.Value = true;
            }
        }

        public void OnStartButtonClicked() => _model.StartGameAsync().Forget();

        public async UniTask OnAdsButtonClicked()
        {
            await _model.DisableAds();
            _view.HideAdsButton();
        }

        public void Dispose() => _disposables.Dispose();
    }
}