using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  private Vector3 _movement;

  public event Action<Vector3> InputIsRead;
  public event Action SwitchButtonIsPressed;

  private void Update() => ReadInput();

  private void ReadInput()
  {
    ReadMoveInput();
    ReadSwitchInput();
  }

  private void ReadMoveInput()
  {
    _movement = new Vector3(
      Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0,
      0,
      Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0
    );

    InputIsRead?.Invoke(_movement);
  }

  private void ReadSwitchInput()
  {
    if (Input.GetKeyDown(KeyCode.Tab))
      SwitchButtonIsPressed?.Invoke();
  }
}