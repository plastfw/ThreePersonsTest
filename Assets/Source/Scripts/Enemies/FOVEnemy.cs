using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Enemies
{
    public class FOVEnemy : MonoBehaviour, IGameListener, IGameUpdateListener, IGamePauseListener, IEnemy
    {
        [SerializeField] private ViewArea _viewArea;
        [SerializeField] private Rigidbody _rigidbody;

        private GameStateManager _gameStateManager;
        private Transform _target;
        private bool _haveTarget;
        private float _speed = 5f;
        private int _damage;

        public void Construct(GameStateManager manager, BulletPool pool = null)
        {
            _gameStateManager = manager;
            _gameStateManager.AddListener(this);
        }

        private void Start()
        {
            _viewArea.SeePlayer += MoveToPlayer;
        }

        public void Initialize(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
        }

        private void OnDestroy() => _viewArea.SeePlayer -= MoveToPlayer;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerModel playerModel))
            {
                playerModel.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }

        public void OnUpdate()
        {
            if (_haveTarget == false) return;

            Vector3 direction = (_target.position - transform.position).normalized;
            _rigidbody.linearVelocity = direction * _speed;
        }

        public void OnPause() => _rigidbody.linearVelocity = Vector3.zero;

        private void MoveToPlayer(Transform player)
        {
            _haveTarget = true;
            _target = player;
        }
    }
}