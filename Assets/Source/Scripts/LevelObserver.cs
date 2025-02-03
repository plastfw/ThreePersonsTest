using System.Collections.Generic;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts
{
    public class LevelObserver : IGameListener, IGameStartListener, IGameDisposeListener
    {
        private SwitchModelObserver _switchModelObserver;
        private List<PlayerModel> _playerModels;
        private GameStateManager _gameStateManager;

        public LevelObserver(SwitchModelObserver switchModelObserver, List<PlayerModel> playerModels,
            GameStateManager gameStateManager)
        {
            _switchModelObserver = switchModelObserver;
            _playerModels = playerModels;
            _gameStateManager = gameStateManager;
            _gameStateManager.AddListener(this);
        }

        public void OnStart()
        {
            foreach (var model in _playerModels)
                model.DeadEvent += RestartLevel;

            _switchModelObserver.AllModelsInSafe += RestartLevel;
        }

        public void OnDispose()
        {
            Debug.LogWarning("OnDispose");

            foreach (var model in _playerModels)
                model.DeadEvent -= RestartLevel;

            _switchModelObserver.AllModelsInSafe -= RestartLevel;
        }

        private void RestartLevel() => SceneManager.LoadScene(0);
    }
}