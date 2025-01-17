using Reflex.Attributes;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
  [SerializeField] private Rigidbody _rigidbody;
  [SerializeField] private float _speed;

  private bool _currentActive;
  private PlayerInput _playerInput;

  [Inject]
  private void Init(PlayerInput playerInput)
  {
    _playerInput = playerInput;
    _playerInput.InputIsRead += Move;
  }

  private void OnDisable() =>
    _playerInput.InputIsRead -= Move;

  public void Move(Vector3 direction)
  {
    if (_currentActive == false) return;

    var cameraRotation = Quaternion.Euler(0, 45, 0);
    var adjustedDirection = cameraRotation * direction.normalized;
    _rigidbody.linearVelocity = adjustedDirection * _speed;
  }

  public void ChangeActiveState(bool state) => _currentActive = state;

  public void SetSpeed(float speed) => _speed = speed;
}