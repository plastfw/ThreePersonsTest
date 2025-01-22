using System;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IGameListener, IGamePauseListener
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    [SerializeField] private List<MovementController> _movementControllers;

    private GameStateManager _gameStateManager;
    private Vector3 _direction;
    private bool _pause = false;

    public event Action SwitchButtonIsPressed;
    public event Action SwitchGameStateButtonIsPressed;

    [Inject]
    private void Init(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _gameStateManager.AddListener(this);
    }

    public void Update() => ReadInput();


    public void OnPause() => _pause = true;

    public void OnResume() => _pause = false;

    private void ReadInput()
    {
        ReadPauseButton();

        if (_pause) return;

        ReadMoveInput();
        ReadSwitchInput();
    }

    private void ReadPauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchGameStateButtonIsPressed?.Invoke();
    }

    private void ReadMoveInput()
    {
        _direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical")
        );

        if (_direction == Vector3.zero) return;


        foreach (var controller in _movementControllers)
            controller.Move(_direction);
    }

    private void ReadSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchButtonIsPressed?.Invoke();
    }
}