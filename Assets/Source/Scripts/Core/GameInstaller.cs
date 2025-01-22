using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<PlayerModel> _playerModels;
        [FormerlySerializedAs("_playerHealthView")] [SerializeField] private HealthView _healthView;
        [FormerlySerializedAs("_playerSwitchModelObserver")] [SerializeField] private SwitchModelObserver _switchModelObserver;
        [SerializeField] private BulletPool _bulletPool;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_playerInput);
            builder.AddSingleton(_playerModels);
            builder.AddSingleton(_cameraController);
            builder.AddSingleton(_healthView);
            builder.AddSingleton(_switchModelObserver);
            builder.AddSingleton(_bulletPool);

            builder.AddSingleton(container =>
                new GameStateManager(
                    container.Resolve<PlayerInput>()));


            // builder.AddSingleton(container =>
            //   new StatsController(
            //     container.Resolve<BubblePlayer>(),
            //     container.Resolve<StatsView>()));
        }
    }
}