using System.Collections.Generic;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private List<PlayerModel> _playerModels;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Canvas _sceneCanvas;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(_sceneCanvas).AsSingle().NonLazy();
            Container.Bind<HUDModel>().AsSingle().NonLazy();
            Container.Bind<HUDPresenter>().AsSingle().NonLazy();
            Container.Bind<HUDFactory>().AsSingle().NonLazy();

            Container.Bind<GameMenuPresenter>().AsSingle().NonLazy();
            Container.Bind<GameMenuModel>().AsSingle().NonLazy();
            Container.Bind<GameMenuFactory>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();
            Container.Bind<Button>().FromInstance(_pauseButton).AsSingle().NonLazy();

            Container.Bind<ExitZone>().FromInstance(_exit).AsSingle().NonLazy();
            Container.Bind<List<PlayerModel>>().FromInstance(_playerModels).AsSingle().NonLazy();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
            Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<StandardInputService>().AsSingle().NonLazy();
            Container.Bind<PlayerInput>().AsSingle().NonLazy();
            Container.Bind<SwitchModelObserver>().AsSingle().NonLazy();
            Container.Bind<CompleteLevelObserver>().AsSingle().NonLazy();
        }
    }
}