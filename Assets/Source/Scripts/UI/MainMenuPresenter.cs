using System;
using Cysharp.Threading.Tasks;
using R3;

namespace Source.Scripts.UI
{
    public class MainMenuPresenter : IDisposable
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private readonly CompositeDisposable _disposables = new();

        public void Init(MainMenuModel model, MainMenuView view)
        {
            _model = model;
            _view = view;

            _model.IsFirebaseReady.Subscribe(isReady =>
                    _view.ChangeButtonState(isReady))
                .AddTo(_disposables);

            _model.IsIAPReady.Subscribe(isInitialized =>
                    _view.ChangeAdsButtonState(isInitialized))
                .AddTo(_disposables);
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