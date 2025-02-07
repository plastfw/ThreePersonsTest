using System;
using R3;
using System.Collections.Generic;
using ObservableCollections;
using Source.Scripts.Core;

namespace Source.Scripts.Player
{
    public class SwitchModelObserver : IGameListener, IGameStartListener, IGameDisposeListener
    {
        private int _currentControllerIndex;
        private readonly PlayerInput _playerInput;
        private readonly CameraController _cameraController;
        private readonly GameStateManager _gameStateManager;

        public IObservableCollection<PlayerModel> ObservablePlayerModels => _playerModels;

        private readonly ObservableList<PlayerModel> _playerModels = new();

        public ReactiveProperty<PlayerModel> CurrentModel { get; } = new();
        public event Action AllModelsInSafe;

        public SwitchModelObserver(PlayerInput playerInput, CameraController cameraController,
            List<PlayerModel> playerModels, GameStateManager gameStateManager)
        {
            _playerInput = playerInput;
            _cameraController = cameraController;
            _gameStateManager = gameStateManager;
            _gameStateManager.AddListener(this);

            foreach (var model in playerModels)
                _playerModels.Add(model);
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
            CurrentModel.Value.ChangeMoveState(false);
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

            _currentControllerIndex = (_currentControllerIndex + 1) % _playerModels.Count;

            CurrentModel.Value = _playerModels[_currentControllerIndex];

            CurrentModel.Value.ChangeMoveState(true);
            _cameraController.SwitchFollowTarget(CurrentModel.Value.transform);
        }
    }
}