using Core;
using Enemies;
using Reflex.Attributes;
using UnityEngine;

namespace Enemies
{
    public class ShootEnemy : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;

        [Inject] private BulletPool _bulletPool;

        private int _damage;
        private Transform _currentTarget;
        private float _coolDown = 2f;
        private float _lastShootTime;
        private bool _isPause = false;

        public void Init(int damage, float radius)
        {
            _damage = damage;
            _sphereCollider.radius = radius;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_isPause) return;

            if (collider.TryGetComponent(out PlayerModel playerModel))
                _currentTarget = playerModel.transform;
        }

        private void OnTriggerExit(Collider collider)
        {
            if (_isPause) return;

            if (collider.TryGetComponent(out PlayerModel playerModel))
                _currentTarget = null;
        }

        private void Update()
        {
            if (_isPause) return;

            if (_currentTarget == null || Time.time - _lastShootTime < _coolDown) return;

            ShootAtTarget();
        }

        public void Pause() => _isPause = true;

        public void Resume() => _isPause = false;

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