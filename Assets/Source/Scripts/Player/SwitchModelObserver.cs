using System;
using System.Collections.Generic;

public class SwitchModelObserver : IGameListener, IGameStartListener, IGameDisposeListener
{
    private int _currentControllerIndex = 0;
    private List<PlayerModel> _playerModels;
    private PlayerInput _playerInput;
    private CameraController _cameraController;
    private HealthView _healthView;
    private GameStateManager _gameStateManager;

    public event Action AllModelsInSafe;

    public SwitchModelObserver(PlayerInput playerInput, CameraController cameraController,
        List<PlayerModel> playerModels, HealthView healthView, GameStateManager gameStateManager)
    {
        _playerInput = playerInput;
        _cameraController = cameraController;
        _playerModels = playerModels;
        _healthView = healthView;
        _gameStateManager = gameStateManager;
        _gameStateManager.AddListener(this);
    }

    public void OnStart()
    {
        _playerInput.SwitchButtonIsPressed += SwitchController;

        foreach (var model in _playerModels)
            model.ImInSafeZone += RemoveModel;

        SwitchController();
    }

    public void OnDispose()
    {
        _playerInput.SwitchButtonIsPressed -= SwitchController;

        foreach (var model in _playerModels)
            model.ImInSafeZone -= RemoveModel;
    }

    private void RemoveModel(PlayerModel model)
    {
        _playerModels[_currentControllerIndex].ChangeMoveState(false);
        _playerModels[_currentControllerIndex].DamageIsTake -= _healthView.SetTextField;
        _playerModels.Remove(model);
        _currentControllerIndex = 0;
        SwitchController();
    }

    private void SwitchController()
    {
        if (_playerModels.Count == 0)
        {
            AllModelsInSafe?.Invoke();
            return;
        }

        _playerModels[_currentControllerIndex].ChangeMoveState(false);

        if (_currentControllerIndex == _playerModels.Count - 1)
            _currentControllerIndex = 0;
        else
            _currentControllerIndex++;

        _playerModels[_currentControllerIndex].ChangeMoveState(true);
        _cameraController.SwitchFollowTarget(_playerModels[_currentControllerIndex].transform);
        _healthView.SetTextField(_playerModels[_currentControllerIndex].GetHealth());

        _playerModels[_currentControllerIndex].DamageIsTake += _healthView.SetTextField;
    }
}