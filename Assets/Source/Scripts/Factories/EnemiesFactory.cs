using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.Enemies;

namespace Source.Scripts.Factories
{
    public class EnemiesFactory
    {
        private const string FOVENEMYKEY = "FOVEnemy";
        private const string SHOOTENEMY = "ShootEnemy";
        private const string TRAJECTORYENEMY = "TrakectoryEnemy";

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
            var fovEnemy = await _loader.LoadAssetAsync<FOVEnemy>(FOVENEMYKEY, _enemiesStatsInitializer.transform);
            var trajectoryEnemy =
                await _loader.LoadAssetAsync<TrajectoryEnemy>(TRAJECTORYENEMY, _enemiesStatsInitializer.transform);
            var shootEnemy = await _loader.LoadAssetAsync<ShootEnemy>(SHOOTENEMY, _enemiesStatsInitializer.transform);

            fovEnemy.Construct(_gameStateManager);
            trajectoryEnemy.Construct(_gameStateManager);
            shootEnemy.Construct(_gameStateManager, _bulletPool);

            _enemiesStatsInitializer.AddEnemies(fovEnemy, trajectoryEnemy, shootEnemy);
        }
    }
}