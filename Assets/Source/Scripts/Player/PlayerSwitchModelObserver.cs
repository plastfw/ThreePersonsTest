using Reflex.Attributes;
using UnityEngine;

public class PlayerSwitchModelObserver : MonoBehaviour
{
  [SerializeField] private PlayerMovementController[] _playerControllers;

  private int _currentControllerIndex = 0;
  private PlayerInput _playerInput;
  private CameraController _cameraController;

  [Inject]
  private void Init(PlayerInput playerInput,CameraController cameraController)
  {
    _playerInput = playerInput;
    _cameraController = cameraController;
    _playerInput.SwitchButtonIsPressed += SwitchController;
  }

  private void OnValidate()
  {
    _playerControllers = GetComponentsInChildren<PlayerMovementController>();
  }

  private void OnDisable()
  {
    _playerInput.SwitchButtonIsPressed -= SwitchController;
  }

  private void SwitchController()
  {
    _playerControllers[_currentControllerIndex].ChangeActiveState(false);

    if (_currentControllerIndex == _playerControllers.Length - 1)
      _currentControllerIndex = 0;
    else
      _currentControllerIndex++;

    _playerControllers[_currentControllerIndex].ChangeActiveState(true);
    _cameraController.SwitchFollowTarget(_playerControllers[_currentControllerIndex].transform);
  }
}