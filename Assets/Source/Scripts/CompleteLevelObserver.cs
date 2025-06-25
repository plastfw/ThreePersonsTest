using System.Collections.Generic;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using Unity.VisualScripting;

namespace Source.Scripts
{
    public class CompleteLevelObserver : IGameListener, IGameDisposeListener
    {
        private IAnalytic _analytic;
        private SwitchModelObserver _switchModelObserver;
        private List<PlayerModel> _playerModels;
        private GameMenuModel _menuModel;
        private AdsPresenter _adsPresenter;
        private SavesManager _saves;

        public CompleteLevelObserver(SwitchModelObserver switchModelObserver,
            GameStateManager gameStateManager, GameMenuModel gameMenuModel, IAnalytic analytic, SavesManager saves)
        {
            _switchModelObserver = switchModelObserver;
            _menuModel = gameMenuModel;
            _analytic = analytic;
            _saves = saves;
            gameStateManager.AddListener(this);
        }

        public void Init(List<PlayerModel> models, AdsPresenter adsPresenter)
        {
            _playerModels = models;
            _adsPresenter = adsPresenter;

            foreach (var model in _playerModels)
                model.Death += TryShowLoseScreen;

            _switchModelObserver.AllModelsInSafe += ShowCompleteMenu;
        }

        private void TryShowLoseScreen()
        {
            if (_saves.LoadSettings())
                _adsPresenter.ModeIsDeath();
            else
                _adsPresenter.Show();
        }

        public void OnDispose()
        {
            foreach (var model in _playerModels)
                model.Death -= ShowCompleteMenu;

            _switchModelObserver.AllModelsInSafe -= ShowCompleteMenu;
        }

        private void ShowCompleteMenu()
        {
            _analytic.CompleteLevel();
            _menuModel.Show();
        }
    }
}