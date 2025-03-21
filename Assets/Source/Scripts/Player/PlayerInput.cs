using System;
using System.Collections.Generic;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

public class PlayerInput : IGameListener, IGameUpdateListener, IGamePauseListener
{
    private readonly IInputService _inputService;

    private readonly List<PlayerModel> _playerModels;

    // private readonly List<MovementController> _movementControllers;
    private readonly GameStateManager _gameStateManager;
    private Vector3 _direction;

    public event Action SwitchButtonIsPressed;

    public PlayerInput(GameStateManager gameStateManager, List<PlayerModel> playerModels,
        IInputService inputService)
    {
        _gameStateManager = gameStateManager;
        _playerModels = playerModels;
        _inputService = inputService;
        _gameStateManager.AddListener(this);
    }

    public void OnPause()
    {
        foreach (var model in _playerModels)
            model.StopMove();
    }

    public void OnUpdate()
    {
        if (_inputService.IsSwitchButtonPressed())
            SwitchButtonIsPressed?.Invoke();

        ReadMoveInput();
    }

    private void ReadMoveInput()
    {
        _direction = new Vector3(
            _inputService.GetHorizontalAxis(),
            0,
            _inputService.GetVerticalAxis()
        );

        if (_direction == Vector3.zero) return;

        foreach (var model in _playerModels)
            model.Move(_direction);
    }
}