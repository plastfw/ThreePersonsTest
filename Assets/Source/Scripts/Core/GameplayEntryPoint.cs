using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
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
        private CompleteLevelControler _completeLevelControler;
        private IAdsInitializer _adsInitializer;
        private FireBaseInitializer _fireBaseInitializer;
        private List<PlayerModel> _playerModels;
        private BulletPool _bulletPool;

        public GameplayEntryPoint(HUDFactory factory, GameMenuFactory menuFactory,
            PlayerModelsFactory playerModelsFactory, SwitchModelObserver observer, ExitZone exitZone,
            SavesManager saves, PlayerInput playerInput, EnemiesFactory enemiesFactory,
            EnemiesStatsInitializer enemiesStatsInitializer, Transform startPosition, SavesManager saveManager,
            AdsFactory adsFactory, CompleteLevelControler completeLevelControler, IAdsInitializer adsInitializer,
            FireBaseInitializer fireBaseInitializer, BulletPool bulletPool, GameStateManager gameStateManager)
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
            _completeLevelControler = completeLevelControler;
            _adsInitializer = adsInitializer;
            _fireBaseInitializer = fireBaseInitializer;
            _bulletPool = bulletPool;
            _enemiesStatsInitializer = enemiesStatsInitializer;
            _gameStateManager = gameStateManager;
        }

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            await _hudFactory.Create();
            await _menuFactory.Create();
            await _adsFactory.Create();

            _bulletPool.Init();

            _playerModels = await _playerModelsFactory.Create();

            _playerModels[0].Construct(_exit, _saves);
            _playerModels[0]
                .InitializeStats(_startPosition, _fireBaseInitializer.GetConfig().additional_data.cube_speed);

            _playerModels[1].Construct(_exit, _saves);
            _playerModels[1].InitializeStats(_startPosition,
                _fireBaseInitializer.GetConfig().additional_data.sphere_speed);

            _observer.Construct(_playerModels);
            _playerInput.Construct(_playerModels);
            await _enemiesFactory.Create();
            _enemiesStatsInitializer.InitializeEnemies(_fireBaseInitializer.GetConfig());
            _completeLevelControler.Init(_playerModels, _adsFactory.AdsModel, _adsFactory.AdsPresenter);
            _gameStateManager.AddListener(_completeLevelControler);
            _gameStateManager.AddListener(_playerInput);
            _gameStateManager.AddListener(_observer);
            _adsInitializer.Init();
        }
    }
}