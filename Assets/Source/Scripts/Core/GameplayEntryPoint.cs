using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Enemies;
using Source.Scripts.Factories;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly HUDFactory _hudFactory;
        private readonly GameMenuFactory _menuFactory;
        private readonly PlayerModelsFactory _playerModelsFactory;
        private readonly AdsFactory _adsFactory;
        private readonly SwitchModelObserver _observer;
        private readonly ExitZone _exit;
        private readonly SavesManager _saves;
        private readonly PlayerInput _playerInput;
        private EnemiesFactory _enemiesFactory;
        private GameStateManager _gameStateManager;
        private EnemiesStatsInitializer _enemiesStatsInitializer;
        private Transform _startPosition;
        private SavesManager _saveManager;
        private CompleteLevelObserver _completeLevelObserver;
        private AdsInitializer _adsInitializer;

        private List<PlayerModel> _playerModels;

        public GameplayEntryPoint(HUDFactory factory, GameMenuFactory menuFactory,
            PlayerModelsFactory playerModelsFactory, SwitchModelObserver observer, ExitZone exitZone,
            SavesManager saves, PlayerInput playerInput, EnemiesFactory enemiesFactory,
            EnemiesStatsInitializer enemiesStatsInitializer, Transform startPosition, SavesManager saveManager,
            AdsFactory adsFactory, CompleteLevelObserver completeLevelObserver, AdsInitializer adsInitializer)
        {
            _hudFactory = factory;
            _menuFactory = menuFactory;
            _playerModelsFactory = playerModelsFactory;
            _observer = observer;
            _exit = exitZone;
            _saves = saves;
            _playerInput = playerInput;
            _enemiesFactory = enemiesFactory;
            _startPosition = startPosition;
            _saveManager = saveManager;
            _adsFactory = adsFactory;
            _completeLevelObserver = completeLevelObserver;
            _adsInitializer = adsInitializer;
            _enemiesStatsInitializer = enemiesStatsInitializer;
        }

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            await _hudFactory.Create();
            await _menuFactory.Create();
            await _adsFactory.Create();

            _playerModels = await _playerModelsFactory.Create();

            foreach (var model in _playerModels)
            {
                model.Construct(_exit, _saves);
                model.InitializeStats(_startPosition);
            }

            _observer.Construct(_playerModels);
            _playerInput.Construct(_playerModels);
            await _enemiesFactory.Create();
            _enemiesStatsInitializer.InitializeEnemies();
            _completeLevelObserver.Init(_playerModels);
            _adsInitializer.Init();
        }
    }
}