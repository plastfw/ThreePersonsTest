using ObservableCollections;
using R3;
using Source.Scripts.Core;
using Source.Scripts.Player;

public class HUDPresenter : IGameListener, IGameStartListener, IGameDisposeListener
{
    private GameStateManager _gameStateManager;
    private SwitchModelObserver _switchModelObserver;
    private HUDModel _model;

    private CompositeDisposable _statsDisposables = new();
    private CompositeDisposable _currentModel = new();


    public HUDPresenter(HUDModel model, SwitchModelObserver switchModelObserver, GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _switchModelObserver = switchModelObserver;
        _model = model;
        _gameStateManager.AddListener(this);
    }

    public void OnStart()
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
            .Subscribe(value => _model.SetHealth(value))
            .AddTo(_statsDisposables);

        model.DistanceToExit
            .Subscribe(value => _model.SetDistance(value))
            .AddTo(_statsDisposables);
    }

    private void OnCountChange(int value)
    {
        _model.SetModelCount(value);
    }
}