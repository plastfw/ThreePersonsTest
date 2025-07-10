using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.Enemies;

namespace Source.Scripts.Factories
{
    public class EnemiesFactory
    {
        private readonly IAddressableLoader _loader;
        private readonly EnemiesStatsInitializer _enemiesStatsInitializer;
        private readonly GameStateManager _gameStateManager;
        private readonly BulletPool _bulletPool;

        public EnemiesFactory(IAddressableLoader loader, EnemiesStatsInitializer enemiesStatsInitializer,
            GameStateManager gameStateManager, BulletPool bulletPool)
        {
            _loader = loader;
            _enemiesStatsInitializer = enemiesStatsInitializer;
            _gameStateManager = gameStateManager;
            _bulletPool = bulletPool;
        }

        public async UniTask Create()
        {
            var fovEnemy = await _loader.LoadFovEnemy(_enemiesStatsInitializer.transform);
            var trajectoryEnemy =
                await _loader.LoadTrajectoryEnemy(_enemiesStatsInitializer.transform);
            var shootEnemy = await _loader.LoadShootEnemy(_enemiesStatsInitializer.transform);

            fovEnemy.Construct();
            trajectoryEnemy.Construct();
            shootEnemy.Construct(_bulletPool);
            
            _gameStateManager.AddListener(fovEnemy);
            _gameStateManager.AddListener(trajectoryEnemy);
            _gameStateManager.AddListener(shootEnemy);

            _enemiesStatsInitializer.AddEnemies(fovEnemy, trajectoryEnemy, shootEnemy);
        }
    }
}