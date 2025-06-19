using System;
using R3;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Player
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private bool _currentActive;
        private Health _health;
        private ExitZone _exit;
        private SavesManager _saves;
        private string _key;

        public event Action<PlayerModel> InSafeZone;
        public event Action Death;

        public ReactiveProperty<float> DistanceToExit { get; private set; } = new();
        public ReactiveProperty<int> Health { get; private set; } = new();

        public void Construct(ExitZone exitZone, SavesManager savesManager)
        {
            _saves = savesManager;
            _exit = exitZone;
            _key = $"model_{Convert.ToString(transform.GetSiblingIndex())}";
        }

        private void Update()
        {
            if (_exit == null) return;
            DistanceToExit.Value = Vector3.Distance(transform.position, _exit.transform.position);
        }

        public void SaveDate()
        {
            _saves.CurrentPlayerPosition.Position = transform.position;
            _saves.SavePlayerPosition();
        }

        public void ChangeMoveState(bool state) => _currentActive = state;

        public void StayInSafe() => InSafeZone?.Invoke(this);

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            Health.Value = _health.value;

            if (Health.Value <= 0)
                Death?.Invoke();
        }

        public void StopMove() => _rigidbody.linearVelocity = Vector3.zero;

        public void Move(Vector3 direction)
        {
            if (!_currentActive) return;
            var cameraRotation = Quaternion.Euler(0, 45, 0);
            var adjustedDirection = cameraRotation * direction.normalized;
            _rigidbody.linearVelocity = adjustedDirection * _speed;
        }

        public void InitializeStats(Transform parent, float speed)
        {
            _saves.LoadPlayerPosition(new PlayerPositionData { Position = parent.position });
            transform.position = _saves.CurrentPlayerPosition.Position;

            _speed = speed;
            _health = new Health(_playerData.Health);
            _health.SetHealth(_playerData.Health);
            Health.Value = _health.value;
        }
    }
}