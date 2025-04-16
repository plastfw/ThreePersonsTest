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
        }

        public void OnButtonClicked() => _model.StartGameAsync().Forget();

        public void Dispose() => _disposables.Dispose();
    }
}