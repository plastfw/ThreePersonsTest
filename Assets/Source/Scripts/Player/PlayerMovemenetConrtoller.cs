using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private bool _currentActive;

    public void Move(Vector3 direction)
    {
        if (_currentActive == false) return;

        var cameraRotation = Quaternion.Euler(0, 45, 0);
        var adjustedDirection = cameraRotation * direction.normalized;
        _rigidbody.linearVelocity = adjustedDirection * _speed;
    }

    public void ChangeActiveState(bool state) => _currentActive = state;

    public void StopMove() => _rigidbody.linearVelocity = Vector3.zero;

    public void SetSpeed(float speed) => _speed = speed;
}