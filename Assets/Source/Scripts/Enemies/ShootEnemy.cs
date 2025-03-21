using Reflex.Attributes;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class ShootEnemy : MonoBehaviour, IGameListener, IGameUpdateListener
    {
        [SerializeField] private SphereCollider _sphereCollider;

        private BulletPool _bulletPool;
        private GameStateManager _gameStateManager;
        private int _damage;
        private Transform _currentTarget;
        private float _coolDown = 2f;
        private float _lastShootTime;

        [Inject]
        private void Init(BulletPool bulletPool, GameStateManager gameStateManager)
        {
            _bulletPool = bulletPool;
            _gameStateManager = gameStateManager;
            _gameStateManager.AddListener(this);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerModel playerModel))
                _currentTarget = playerModel.transform;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent(out PlayerModel playerModel))
                _currentTarget = null;
        }

        public void Initialize(int damage, float radius)
        {
            _damage = damage;
            _sphereCollider.radius = radius;
        }

        public void OnUpdate()
        {
            if (_currentTarget == null || Time.time - _lastShootTime < _coolDown) return;

            ShootAtTarget();
        }

        private void ShootAtTarget()
        {
            Bullet bullet = _bulletPool.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.LookAt(_currentTarget);
            bullet.SetDamage(_damage);
            bullet.Shoot(_currentTarget.position);
            _lastShootTime = Time.time;
        }
    }
}