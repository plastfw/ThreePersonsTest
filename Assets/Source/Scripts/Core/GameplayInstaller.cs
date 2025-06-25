using Source.Scripts.Enemies;
using Source.Scripts.Factories;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private BulletsContainer _bulletsContainer;
        [SerializeField] private ExitZone _exit;
        [SerializeField] private Canvas _sceneCanvas;
        [SerializeField] private Transform _startPlayerPosition;
        [SerializeField] private EnemiesStatsInitializer _enemiesStatsInitializer;
        [SerializeField] private Bullet _bulletPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(_sceneCanvas).AsSingle();
            Container.Bind<HUDModel>().AsSingle();
            Container.Bind<HUDPresenter>().AsSingle();
            Container.Bind<HUDFactory>().AsSingle();

            Container.Bind<GameMenuPresenter>().AsSingle();
            Container.Bind<GameMenuModel>().AsSingle();
            Container.Bind<GameMenuFactory>().AsSingle();

            Container.Bind<AdsFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle();

            Container.Bind<EnemiesStatsInitializer>().FromInstance(_enemiesStatsInitializer).AsSingle();
            Container.Bind<PlayerModelsFactory>().AsSingle();
            Container.Bind<Transform>().FromInstance(_startPlayerPosition).AsSingle();
            Container.Bind<EnemiesFactory>().AsSingle();

            Container.Bind<ExitZone>().FromInstance(_exit).AsSingle();
            Container.Bind<CameraController>().FromInstance(_cameraController).AsSingle();
            Container.Bind<BulletPool>().AsSingle();
            Container.Bind<Bullet>().FromInstance(_bulletPrefab);
            Container.Bind<BulletsContainer>().FromInstance(_bulletsContainer).AsSingle();
            Container.Bind<IInputService>().To<StandardInputService>().AsSingle();
            Container.Bind<PlayerInput>().AsSingle();
            Container.Bind<SwitchModelObserver>().AsSingle();
            Container.Bind<CompleteLevelObserver>().AsSingle();
        }
    }
}