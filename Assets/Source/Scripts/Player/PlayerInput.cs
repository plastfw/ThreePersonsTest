using System;
using Reflex.Attributes;
using UnityEngine;

public class PlayerInput : MonoBehaviour, Listeners.IGameListener, Listeners.IGamePauseListener,
  Listeners.IGameResumeListener
{
  private GameStateManager _gameStateManager;
  private Vector3 _movement;
  private bool _pause = false;

  public event Action<Vector3> InputIsRead;
  public event Action SwitchButtonIsPressed;

  [Inject]
  private void Init(GameStateManager gameStateManager)
  {
    _gameStateManager = gameStateManager;
    _gameStateManager.AddListener(this);
  }

  private void Update() => ReadInput();

  private void ReadInput()
  {
    if (_pause) return;

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

  public void OnPause() => _pause = true;

  public void OnResume() => _pause = false;
}