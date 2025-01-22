using UnityEngine;

namespace Core
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _initialPoolSize = 5;

        private ObjectPool<Bullet> _bulletPool;

        private void Start()
        {
            _bulletPool = new ObjectPool<Bullet>(
                () =>
                {
                    var bullet = Instantiate(_bulletPrefab);
                    return bullet;
                },
                bullet => { bullet.ReturnMe += ReturnBullet; },
                bullet => { bullet.Reset(); },
                _initialPoolSize);
        }

        public Bullet GetBullet() => _bulletPool.Get();

        public void ReturnBullet(Bullet bullet) => _bulletPool.Return(bullet);
    }
}