using System;
using System.Collections.Generic;

public class GameStateManager : IDisposable
{
    private PlayerInput _playerInput;
    private List<IGameListener> _listeners = new();
    private bool _isPause;

    public GameStateManager(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _playerInput.SwitchGameStateButtonIsPressed += SwitchState;
    }

    public void AddListener(IGameListener listener) => _listeners.Add(listener);

    public void Dispose() => _playerInput.SwitchGameStateButtonIsPressed -= SwitchState;

    private void SwitchState()
    {
        _isPause = !_isPause;

        if (_isPause)
            OnPause();
        else
            OnResume();
    }

    public void OnPause()
    {
        foreach (var gameListener in _listeners)
        {
            if (gameListener is IGamePauseListener pauseListener)
                pauseListener.OnPause();
        }
    }

    public void OnResume()
    {
        foreach (var gameListener in _listeners)
        {
            if (gameListener is IGameResumeListener resumeListener)
                resumeListener.OnResume();
        }
    }
}