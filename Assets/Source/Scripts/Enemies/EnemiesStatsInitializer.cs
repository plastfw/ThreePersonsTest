using System.Collections.Generic;
using Source.Scripts.Remote;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemiesStatsInitializer : MonoBehaviour
    {
        private readonly List<FOVEnemy> _fovEnemies = new();
        private readonly List<ShootEnemy> _shootEnemies = new();
        private readonly List<TrajectoryEnemy> _trajectoryEnemies = new();

        [SerializeField] private Transform _shootPosition;
        [SerializeField] private Transform _fovPosition;
        [SerializeField] private Transform _trajectoryPosition;

        public void InitializeEnemies(RemoteGameConfig config)
        {
            foreach (var enemy in _shootEnemies)
                enemy.Initialize(config.shoot_data.damage, config.shoot_data.radius);

            foreach (var enemy in _trajectoryEnemies)
                enemy.Initialize(config.trajectory_data.damage);

            foreach (var enemy in _fovEnemies)
                enemy.Initialize(config.fov_data.damage, config.fov_data.speed);
        }

        public void AddEnemies(FOVEnemy fovEnemy, TrajectoryEnemy trajectoryEnemy, ShootEnemy shootEnemy)
        {
            _fovEnemies.Add(fovEnemy);
            _trajectoryEnemies.Add(trajectoryEnemy);
            _shootEnemies.Add(shootEnemy);

            fovEnemy.transform.position = _fovPosition.position;
            shootEnemy.transform.position = _shootPosition.position;
            trajectoryEnemy.transform.position = _trajectoryPosition.position;
        }
    }
}