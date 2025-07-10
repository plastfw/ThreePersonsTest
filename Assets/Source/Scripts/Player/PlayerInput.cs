using System;
using System.Collections.Generic;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerInput : IGameListener, IGameUpdateListener, IGamePauseListener
    {
        private readonly IInputService _inputService;

        private readonly List<PlayerModel> _playerModels = new();

        private Vector3 _direction;

        public event Action SwitchButtonIsPressed;

        public PlayerInput(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Construct(List<PlayerModel> models)
        {
            foreach (var model in models)
                _playerModels.Add(model);
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
}