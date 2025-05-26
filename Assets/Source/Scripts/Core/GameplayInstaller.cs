using Source.Scripts.Enemies;
using Source.Scripts.Factories;
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
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Canvas _sceneCanvas;
        [SerializeField] private Transform _startPlayerPosition;
        [SerializeField] private EnemiesStatsInitializer _enemiesStatsInitializer;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(_sceneCanvas).AsSingle().NonLazy();
            Container.Bind<HUDModel>().AsSingle().NonLazy();
            Container.Bind<HUDPresenter>().AsSingle().NonLazy();
            Container.Bind<HUDFactory>().AsSingle().NonLazy();

            Container.Bind<GameMenuPresenter>().AsSingle().NonLazy();
            Container.Bind<GameMenuModel>().AsSingle().NonLazy();
            Container.Bind<GameMenuFactory>().AsSingle().NonLazy();

            Container.Bind<AdsPresenter>().AsSingle().NonLazy();
            Container.Bind<AdsModel>().AsSingle().NonLazy();
            Container.Bind<AdsFactory>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();
            Container.Bind<Button>().FromInstance(_pauseButton).AsSingle().NonLazy();

            Container.Bind<EnemiesStatsInitializer>().FromInstance(_enemiesStatsInitializer).AsSingle().NonLazy();
            Container.Bind<PlayerModelsFactory>().AsSingle().NonLazy();
            Container.Bind<Transform>().FromInstance(_startPlayerPosition).AsSingle().NonLazy();
            Container.Bind<EnemiesFactory>().AsSingle().NonLazy();

            Container.Bind<ExitZone>().FromInstance(_exit).AsSingle().NonLazy();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
            Container.Bind<BulletPool>().FromInstance(_bulletPool).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<StandardInputService>().AsSingle().NonLazy();
            Container.Bind<PlayerInput>().AsSingle().NonLazy();
            Container.Bind<SwitchModelObserver>().AsSingle().NonLazy();
            Container.Bind<CompleteLevelObserver>().AsSingle().NonLazy();
        }
    }
}