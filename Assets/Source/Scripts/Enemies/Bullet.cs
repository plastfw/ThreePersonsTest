using System;
using System.Collections;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Coroutine _returnCoroutine;
        private int _damage;
        private float _speed = 10f;
        private float _lifeTime = 3f;

        public event Action<Bullet> OnHit;

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out PlayerModel playerModel))
            {
                playerModel.TakeDamage(_damage);
                Deactivate();
            }
        }

        public void Shoot(Vector3 position,Transform target,int damage)
        {
            transform.position = position;
            transform.LookAt(target);
            SetDamage(damage);
            
            Vector3 direction = (target.position - transform.position).normalized;
            _rigidbody.linearVelocity = direction * _speed;
            _returnCoroutine = StartCoroutine(ReturnToPoolAfterDelay());
        }

        public void Reset() => StopCoroutine(_returnCoroutine);

        private void SetDamage(int damage) => _damage = damage;

        private IEnumerator ReturnToPoolAfterDelay()
        {
            yield return new WaitForSeconds(_lifeTime);
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
            OnHit?.Invoke(this);
        }
    }
}