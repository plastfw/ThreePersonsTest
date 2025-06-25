using Source.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class BulletPool
    {
        private Bullet _bullet;
        private readonly int _initialPoolSize = 5;
        private IInstantiator _instantiator;
        private readonly BulletsContainer _container;

        private ObjectPool<Bullet> _bulletPool;

        public BulletPool(Bullet bullet, IInstantiator instantiator, BulletsContainer container)
        {
            _bullet = bullet;
            _instantiator = instantiator;
            _container = container;
        }

        public void Init()
        {
            _bulletPool = new ObjectPool<Bullet>(
                () =>
                {
                    var bullet = _instantiator.InstantiatePrefabForComponent<Bullet>(_bullet, _container.transform);
                    return bullet;
                },
                bullet => { bullet.OnHit += ReturnBullet; },
                bullet =>
                {
                    bullet.OnHit -= ReturnBullet;
                    bullet.Reset();
                },
                _initialPoolSize);
        }

        public Bullet GetBullet() => _bulletPool.Get();

        public void ReturnBullet(Bullet bullet) => _bulletPool.Return(bullet);
    }
}