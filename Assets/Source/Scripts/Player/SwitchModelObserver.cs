using System;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

public class SwitchModelObserver : MonoBehaviour
{
    private int _currentControllerIndex = 0;
    private List<PlayerModel> _playerModels;
    private PlayerInput _playerInput;
    private CameraController _cameraController;
    private HealthView _healthView;

    public event Action AllModelsInSafe;

    [Inject]
    private void Init(PlayerInput playerInput, CameraController cameraController,
        List<PlayerModel> playerModels, HealthView healthView)
    {
        _playerInput = playerInput;
        _playerModels = playerModels;
        _cameraController = cameraController;
        _healthView = healthView;
    }

    private void OnEnable()
    {
        _playerInput.SwitchButtonIsPressed += SwitchController;

        foreach (var model in _playerModels)
            model.ImInSafeZone += RemoveModel;
    }

    private void OnDisable()
    {
        _playerInput.SwitchButtonIsPressed -= SwitchController;

        foreach (var model in _playerModels)
            model.ImInSafeZone -= RemoveModel;
    }

    private void Start()
    {
        SwitchController();
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