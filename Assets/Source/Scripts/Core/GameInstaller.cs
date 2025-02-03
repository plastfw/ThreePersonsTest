using System.Collections.Generic;
using Reflex.Core;
using Source.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Core
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<PlayerModel> _playerModels;
        [SerializeField] private List<MovementController> _movementControllers;
        [FormerlySerializedAs("_playerHealthView")] [SerializeField] private HealthView _healthView;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private PauseReader _pauseReader;
        [SerializeField] private GameStateManager _gameStateManager;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_gameStateManager);
            builder.AddSingleton(_pauseReader);
            builder.AddSingleton(_healthView);
            builder.AddSingleton(_playerModels);
            builder.AddSingleton(_movementControllers);
            builder.AddSingleton(_cameraController);
            builder.AddSingleton(_bulletPool);

            builder.AddSingleton(container =>
                new PlayerInput(
                    container.Resolve<GameStateManager>(),
                    container.Resolve<List<MovementController>>()));

            builder.AddSingleton(container =>
                new SwitchModelObserver(
                    container.Resolve<PlayerInput>(),
                    container.Resolve<CameraController>(),
                    container.Resolve<List<PlayerModel>>(),
                    container.Resolve<HealthView>(),
                    container.Resolve<GameStateManager>()));

            builder.AddSingleton(container =>
                new LevelObserver(
                    container.Resolve<SwitchModelObserver>(),
                    container.Resolve<List<PlayerModel>>(),
                    container.Resolve<GameStateManager>()));

            builder.Build().Resolve<PlayerInput>();
            builder.Build().Resolve<LevelObserver>();
            builder.Build().Resolve<SwitchModelObserver>();

            // builder.AddSingleton(container =>
            //   new StatsController(
            //     container.Resolve<BubblePlayer>(),
            //     container.Resolve<StatsView>()));
        }
    }
}