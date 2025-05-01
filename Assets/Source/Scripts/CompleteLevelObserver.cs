using System.Collections.Generic;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts
{
    public class CompleteLevelObserver : IGameListener, IGameDisposeListener
    {
        private IAnalytic _analytic;
        private SwitchModelObserver _switchModelObserver;
        private List<PlayerModel> _playerModels;
        private GameMenuModel _menuModel;
        private AdsModel _adsModel;
        private SavesManager _saves;

        public CompleteLevelObserver(SwitchModelObserver switchModelObserver,
            GameStateManager gameStateManager, GameMenuModel gameMenuModel, IAnalytic analytic, SavesManager saves,
            AdsModel adsModel)
        {
            _switchModelObserver = switchModelObserver;
            _menuModel = gameMenuModel;
            _analytic = analytic;
            _saves = saves;
            _adsModel = adsModel;
            gameStateManager.AddListener(this);
        }

        public void Init(List<PlayerModel> models)
        {
            _playerModels = models;

            foreach (var model in _playerModels)
                model.Death += ShowAdsMenu;

            _switchModelObserver.AllModelsInSafe += ShowCompleteMenu;
        }

        private void ShowAdsMenu()
        {
            _adsModel.Show();
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
            _saves.DeleteAll();
        }
    }
}