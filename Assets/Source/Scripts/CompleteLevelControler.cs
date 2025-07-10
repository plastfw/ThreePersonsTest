using System.Collections.Generic;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;

namespace Source.Scripts
{
    public class CompleteLevelControler : IGameListener, IGameDisposeListener
    {
        private IAnalytic _analytic;
        private SwitchModelObserver _switchModelObserver;
        private List<PlayerModel> _playerModels;
        private GameMenuModel _menuModel;
        private AdsModel _adsModel;
        private AdsPresenter _adsPresenter;
        private SavesManager _saves;

        public CompleteLevelControler(SwitchModelObserver switchModelObserver, GameMenuModel gameMenuModel,
            IAnalytic analytic, SavesManager saves)
        {
            _switchModelObserver = switchModelObserver;
            _menuModel = gameMenuModel;
            _analytic = analytic;
            _saves = saves;
        }

        public void Init(List<PlayerModel> models, AdsModel adsModel, AdsPresenter adsPresenter)
        {
            _playerModels = models;
            _adsPresenter = adsPresenter;
            _adsModel = adsModel;

            foreach (var model in _playerModels)
                model.Death += TryShowLoseScreen;

            _switchModelObserver.AllModelsInSafe += ShowCompleteMenu;
        }

        private void TryShowLoseScreen()
        {
            if (_saves.LoadSettings())
                _adsModel.RejectClicked();
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