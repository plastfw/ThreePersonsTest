using ObservableCollections;
using R3;
using Source.Scripts.Core;
using Source.Scripts.Player;

namespace Source.Scripts.UI
{
    public class HUDPresenter : IGameListener, IGameDisposeListener
    {
        private SwitchModelObserver _switchModelObserver;
        private HUDModel _model;
        private HUDView _view;

        private CompositeDisposable _statsDisposables = new();
        private CompositeDisposable _currentModel = new();

        public void Construct(HUDView view, HUDModel model, SwitchModelObserver switchModelObserver)
        {
            _view = view;
            _model = model;
            _switchModelObserver = switchModelObserver;
        }
        
        public void StartInit()
        {
            _switchModelObserver.CurrentModel
                .Subscribe(OnModelChanged)
                .AddTo(_currentModel);

            _switchModelObserver.ObservablePlayerModels
                .ObserveCountChanged()
                .Subscribe(OnCountChange)
                .AddTo(_currentModel);

            OnCountChange(_switchModelObserver.ObservablePlayerModels.Count);
        }

        public void OnDispose()
        {
            _statsDisposables.Dispose();
            _currentModel.Dispose();
        }

        private void OnModelChanged(PlayerModel model)
        {
            _statsDisposables.Dispose();
            _statsDisposables = new CompositeDisposable();

            if (model == null) return;

            model.Health
                .Subscribe(value =>
                {
                    _model.SetHealth(value);
                    _view.SetHealth(value);
                })
                .AddTo(_statsDisposables);

            model.DistanceToExit
                .Subscribe(value =>
                {
                    _view.SetDistance(value);
                    _model.SetDistance(value);
                })
                .AddTo(_statsDisposables);
        }

        private void OnCountChange(int value)
        {
            _model.SetModelCount(value);
            _view.SetCounter(value);
        }
    }
}