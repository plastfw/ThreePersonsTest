using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Enemies
{
    public class ShootEnemy : MonoBehaviour, IGameListener, IGameUpdateListener, IEnemy
    {
        [SerializeField] private SphereCollider _sphereCollider;

        private BulletPool _bulletPool;
        private int _damage;
        private Transform _currentTarget;
        private float _coolDown = 2f;

        private float _lastShootTime;

        public void Construct(BulletPool pool = null)
        {
            _bulletPool = pool;
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
            bullet.Shoot(transform.position, _currentTarget, _damage);
            _lastShootTime = Time.time;
        }
    }
}