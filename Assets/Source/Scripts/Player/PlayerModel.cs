using System;
using DG.Tweening;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Player
{
    [RequireComponent(typeof(MovementController))]
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        private MovementController _movementController;
        private Health _health;
        private ExitZone _exit;
        private SavesManager _saves;
        private string _key;

        public event Action<PlayerModel> ImInSafeZone;
        public event Action DeadEvent;

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
            if (_movementController == null)
                _movementController = transform.GetComponent<MovementController>();

            _key = Convert.ToString(transform.GetSiblingIndex());
        }

        private void Awake() => InitializeStats();

        private void Update()
        {
            if (_exit == null) return;
            DistanceToExit.Value = Vector3.Distance(transform.position, _exit.transform.position);
        }

        private void OnDisable()
        {
            _saves.SaveVector3(_key, transform.position);
        }

        public void ChangeMoveState(bool state)
        {
            _movementController.ChangeActiveState(state);
        }

        public void StayInSafe()
        {
            ImInSafeZone?.Invoke(this);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);

            Health.Value = _health.value;

            if (_health.value <= 0)
                DeadEvent?.Invoke();
        }

        private void InitializeStats()
        {
            transform.position = _saves.LoadVector3(_key, transform.position);
            _movementController.SetSpeed(_playerData.Speed);
            _health = new Health(_playerData.Health);

            _health.SetHealth(_playerData.Health);
            Health.Value = _health.value;
        }
    }
}