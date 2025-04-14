using System.Collections.Generic;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts
{
    public class CompleteLevelObserver : IGameListener, IGameStartListener, IGameDisposeListener
    {
        private IAnalytic _analytic;
        private SwitchModelObserver _switchModelObserver;
        private List<PlayerModel> _playerModels;
        private GameStateManager _gameStateManager;
        private GameMenuModel _menuModel;

        public CompleteLevelObserver(SwitchModelObserver switchModelObserver, List<PlayerModel> playerModels,
            GameStateManager gameStateManager, GameMenuModel gameMenuModel, IAnalytic analytic)
        {
            _switchModelObserver = switchModelObserver;
            _playerModels = playerModels;
            _gameStateManager = gameStateManager;
            _menuModel = gameMenuModel;
            _analytic = analytic;
            _gameStateManager.AddListener(this);
        }

        public void OnStart()
        {
            foreach (var model in _playerModels)
                model.Death += ShowMenu;

            _switchModelObserver.AllModelsInSafe += ShowMenu;
        }

        public void OnDispose()
        {
            foreach (var model in _playerModels)
                model.Death -= ShowMenu;

            _switchModelObserver.AllModelsInSafe -= ShowMenu;
        }

        private void ShowMenu()
        {
            _analytic.CompleteLevel();
            _menuModel.Show();
        }
    }
}