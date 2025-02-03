using System;
using System.Collections.Generic;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerInput : IGameListener, IGameUpdateListener, IGamePauseListener
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        private List<MovementController> _movementControllers;
        private GameStateManager _gameStateManager;
        private Vector3 _direction;

        public event Action SwitchButtonIsPressed;

        public PlayerInput(GameStateManager gameStateManager, List<MovementController> movementControllers)
        {
            _gameStateManager = gameStateManager;
            _movementControllers = movementControllers;
            _gameStateManager.AddListener(this);
        }

        public void OnPause()
        {
            foreach (var controller in _movementControllers)
                controller.StopMove();
        }

        public void OnUpdate()
        {
            ReadMoveInput();
            ReadSwitchInput();
        }

        private void ReadMoveInput()
        {
            _direction = new Vector3(
                Input.GetAxisRaw(HORIZONTAL),
                0,
                Input.GetAxisRaw(VERTICAL)
            );

            if (_direction == Vector3.zero) return;


            foreach (var controller in _movementControllers)
                controller.Move(_direction);
        }

        private void ReadSwitchInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                SwitchButtonIsPressed?.Invoke();
        }
    }
}