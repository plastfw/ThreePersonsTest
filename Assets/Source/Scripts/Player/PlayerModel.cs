using System;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private MovementController _movementController;
    private Health _health;

    public event Action<PlayerModel> ImInSafeZone;
    public event Action<int> DamageIsTake;
    public event Action DeadEvent;

    private void OnValidate()
    {
        if (_movementController == null)
            _movementController = transform.GetComponent<MovementController>();
    }

    private void Start() => InitializeStats();

    public int GetHealth() => _health.HealthValue;

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
        DamageIsTake?.Invoke(_health.HealthValue);

        if (_health.HealthValue <= 0)
            DeadEvent?.Invoke();
    }
    
    private void InitializeStats()
    {
        _movementController.SetSpeed(_playerData.Speed);
        _health = new Health(_playerData.Health);

        _health.SetHealth(_playerData.Health);
    }
}