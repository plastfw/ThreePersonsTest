using System;
using Reflex.Attributes;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerModel : MonoBehaviour
{
  [SerializeField] private PlayerData _playerData;

  private PlayerInput _playerInput;
  private PlayerMovementController _playerMovementController;
  private PlayerHealth _playerHealth;

  public event Action<PlayerModel> ImInSafeZone;
  public event Action<int> DamageIsTake;
  public event Action DeadEvent;

  [Inject]
  private void Init(PlayerInput playerInput)
  {
    _playerInput = playerInput;
    _playerInput.InputIsRead += Move;
  }

  private void OnValidate()
  {
    if (_playerMovementController == null)
      _playerMovementController = transform.GetComponent<PlayerMovementController>();
  }

  private void OnDisable() =>
    _playerInput.InputIsRead -= Move;

  private void Start()
  {
    InitializeStats();
  }

  private void Move(Vector3 direction) => _playerMovementController.Move(direction);

  private void InitializeStats()
  {
    _playerMovementController.SetSpeed(_playerData.Speed);
    _playerHealth = new PlayerHealth(_playerData.Health);

    _playerHealth.SetHealth(_playerData.Health);
  }

  public int GetHealth() => _playerHealth.Health;

  public void ChangeMoveState(bool state)
  {
    _playerMovementController.ChangeActiveState(state);
  }

  public void StayInSafe()
  {
    ImInSafeZone?.Invoke(this);
  }

  public void TakeDamage(int damage)
  {
    _playerHealth.TakeDamage(damage);
    DamageIsTake?.Invoke(_playerHealth.Health);

    if (_playerHealth.Health <= 0)
      DeadEvent?.Invoke();
  }
}