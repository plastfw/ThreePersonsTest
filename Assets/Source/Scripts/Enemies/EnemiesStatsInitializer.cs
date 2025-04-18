using System.Collections.Generic;
using Source.Scripts.Enemies.Data;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemiesStatsInitializer : MonoBehaviour
    {
        private readonly List<FOVEnemy> _fovEnemies = new();
        private readonly List<ShootEnemy> _shootEnemies = new();
        private readonly List<TrajectoryEnemy> _trajectoryEnemies = new();

        [SerializeField] private FOVData _FOVData;
        [SerializeField] private TrajectoryData _trajectoryData;
        [SerializeField] private ShootData _shootData;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private Transform _fovPosition;
        [SerializeField] private Transform _trajectoryPosition;

        public void InitializeEnemies()
        {
            foreach (var enemy in _shootEnemies)
                enemy.Initialize(_shootData.Damage, _shootData.Radius);

            foreach (var enemy in _trajectoryEnemies)
                enemy.Initialize(_trajectoryData.Damage);

            foreach (var enemy in _fovEnemies)
                enemy.Initialize(_FOVData.Damage, _FOVData.Speed);
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