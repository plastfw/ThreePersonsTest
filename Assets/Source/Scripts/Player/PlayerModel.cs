using System;
using R3;
using Source.Scripts.Core;
using Source.Scripts.SaveTypes;
using UnityEngine;
using Zenject;

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

        [Inject]
        private void Init(ExitZone exitZone, SavesManager savesManager)
        {
            _saves = savesManager;
            _exit = exitZone;
        }

        private void OnValidate()
        {
            _key = $"model_{Convert.ToString(transform.GetSiblingIndex())}";
        }

        private void Awake() => InitializeStats();

        private void Update()
        {
            if (_exit == null) return;
            DistanceToExit.Value = Vector3.Distance(transform.position, _exit.transform.position);
        }

        private void OnDisable()
        {
            var saveData = new PlayerSaveData(transform.position);
            _saves.Save(_key, saveData);
        }

        public void ChangeMoveState(bool state) => _currentActive = state;

        public void StayInSafe() => InSafeZone?.Invoke(this);

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
            Health.Value = _health.value;
            if (_health.value <= 0)
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

        private void InitializeStats()
        {
            var loadedData = _saves.Load(_key, new PlayerSaveData(transform.position));
            transform.position = loadedData.Position;

            _speed = _playerData.Speed;
            _health = new Health(_playerData.Health);
            _health.SetHealth(_playerData.Health);
            Health.Value = _health.value;
        }
    }
}