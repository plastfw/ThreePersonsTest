using UnityEngine;

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
        bullet.Init(this);
        return bullet;
      },
      bullet => { },
      bullet => { bullet.Reset(); },
      _initialPoolSize);
  }

  public Bullet GetBullet() => _bulletPool.Get();

  public void ReturnBullet(Bullet head) => _bulletPool.Return(head);
}