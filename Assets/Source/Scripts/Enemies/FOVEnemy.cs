using UnityEngine;

public class FOVEnemy : MonoBehaviour
{
  [SerializeField] private ViewArea _viewArea;
  [SerializeField] private Rigidbody _rigidbody;

  private Transform _target;
  private bool _haveTarget;
  private float _speed = 5f;
  private int _damage;

  private bool _isPause = false;

  public void Init(int damage, float speed)
  {
    _damage = damage;
    _speed = speed;
  }

  private void OnEnable() => _viewArea.SeePlayer += MoveToPlayer;

  private void OnDisable() => _viewArea.SeePlayer -= MoveToPlayer;

  private void OnCollisionEnter(Collision collision)
  {
    if (_isPause) return;

    if (collision.transform.TryGetComponent(out PlayerModel playerModel))
    {
      playerModel.TakeDamage(_damage);
      gameObject.SetActive(false);
    }
  }

  private void Update()
  {
    if (_isPause) return;


    if (_haveTarget == false) return;

    Vector3 direction = (_target.position - transform.position).normalized;
    _rigidbody.linearVelocity = direction * _speed;
  }

  private void MoveToPlayer(Transform player)
  {
    _haveTarget = true;
    _target = player;
  }

  public void Pause()
  {
    _isPause = true;
    _rigidbody.linearVelocity = Vector3.zero;
  }

  public void Resume()
  {
    _isPause = false;
  }
}