using System.Collections.Generic;
using Reflex.Core;
using Source.Scripts.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Source.Scripts.Core
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<PlayerModel> _playerModels;
        [SerializeField] private List<MovementController> _movementControllers;
        [FormerlySerializedAs("_playerHealthView")] [SerializeField] private HealthView _healthView;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private GameStateManager _gameStateManager;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private GameMenu _gameMenu;


        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_gameStateManager);
            builder.AddSingleton(_gameMenu);
            builder.AddSingleton(_exit);
            builder.AddSingleton(_playerModels);
            builder.AddSingleton(_movementControllers);
            builder.AddSingleton(_cameraController);
            builder.AddSingleton(_bulletPool);

            builder.AddSingleton(new StandardInputService());
            builder.AddSingleton(new TickableService());
            builder.AddSingleton(new PauseService(_pauseButton));

            builder.AddSingleton(container =>
                new PlayerInput(
                    container.Resolve<GameStateManager>(),
                    container.Resolve<List<MovementController>>(),
                    container.Resolve<StandardInputService>()));

            builder.AddSingleton(container =>
                new SwitchModelObserver(
                    container.Resolve<PlayerInput>(),
                    container.Resolve<CameraController>(),
                    container.Resolve<List<PlayerModel>>(),
                    container.Resolve<GameStateManager>()));

            builder.AddSingleton(container =>
                new CompleteLevelObserver(
                    container.Resolve<SwitchModelObserver>(),
                    container.Resolve<List<PlayerModel>>(),
                    container.Resolve<GameStateManager>(),
                    container.Resolve<GameMenu>()));


            var container = builder.Build();

            container.Resolve<PlayerInput>();
            container.Resolve<CompleteLevelObserver>();
            container.Resolve<SwitchModelObserver>();
            container.Resolve<TickableService>();
            container.Resolve<PauseService>();

            // builder.OnContainerBuilt += container =>
            // {
            //     container.Resolve<GameStateManager>().Initialize();
            //     container.Resolve<PlayerInput>();
            //     container.Resolve<LevelObserver>();
            //     container.Resolve<SwitchModelObserver>();
            // };
        }
    }
}