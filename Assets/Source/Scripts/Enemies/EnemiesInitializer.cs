using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Enemies
{
    public class EnemiesInitializer : MonoBehaviour
    {
        [SerializeField] private List<FOVEnemy> _fovEnemies;
        [SerializeField] private List<ShootEnemy> _shootEnemies;
        [SerializeField] private List<TrajectoryEnemy> _trajectoryEnemies;
        [SerializeField] private FOVData _FOVData;
        [SerializeField] private TrajectoryData _trajectoryData;
        [SerializeField] private ShootData _shootData;

        private void Start() => InitializeEnemies();

        private void InitializeEnemies()
        {
            foreach (var enemy in _shootEnemies)
                enemy.Initialize(_shootData.Damage, _shootData.Radius);

            foreach (var enemy in _trajectoryEnemies)
                enemy.Initialize(_trajectoryData.Damage);

            foreach (var enemy in _fovEnemies)
                enemy.Initialize(_FOVData.Damage, _FOVData.Speed);
        }
    }
}