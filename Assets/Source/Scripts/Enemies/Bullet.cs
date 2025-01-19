using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
  [SerializeField] private Rigidbody _rigidbody;

  private BulletPool _bulletPool;
  private Coroutine _returnCoroutine;
  private int _damage;
  private float _speed = 10f;
  private float _lifeTime = 3f;

  public void Init(BulletPool bulletPool) => _bulletPool = bulletPool;

  public void Shoot(Vector3 targetPosition)
  {
    Vector3 direction = (targetPosition - transform.position).normalized;
    _rigidbody.linearVelocity = direction * _speed;
    _returnCoroutine = StartCoroutine(ReturnToPoolAfterDelay());
  }

  private IEnumerator ReturnToPoolAfterDelay()
  {
    yield return new WaitForSeconds(_lifeTime);
    _bulletPool.ReturnBullet(this);
    gameObject.SetActive(false);
  }

  public void Reset()
  {
    StopCoroutine(_returnCoroutine);
  }

  public void OnCollisionEnter(Collision collision)
  {
    if (collision.transform.TryGetComponent(out PlayerModel playerModel))
    {
      playerModel.TakeDamage(_damage);
      _bulletPool.ReturnBullet(this);
      gameObject.SetActive(false);
    }
  }

  public void SetDamage(int damage) => _damage = damage;
}