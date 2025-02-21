using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private int _initialPoolSize = 5;

        private ObjectPool<Bullet> _bulletPool;

        public void Start()
        {
            _bulletPool = new ObjectPool<Bullet>(
                () =>
                {
                    var bullet = Instantiate(_bullet);
                    return bullet;
                },
                bullet => { bullet.IsCollision += ReturnBullet; },
                bullet =>
                {
                    bullet.IsCollision -= ReturnBullet;
                    bullet.Reset();
                },
                _initialPoolSize);
        }

        public Bullet GetBullet() => _bulletPool.Get();

        public void ReturnBullet(Bullet bullet) => _bulletPool.Return(bullet);
    }
}