using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private PauseReader _reader;
    private List<IGameListener> _listeners = new();
    private bool _isPause;

    [Inject]
    private void Init(PauseReader pauseReader)
    {
        _reader = pauseReader;
    }

    private void Start()
    {
        _reader.SwitchGameStateButtonIsPressed += SwitchState;

        foreach (var listener in _listeners)
            if (listener is IGameStartListener startListener)
                startListener.OnStart();
    }

    public void Dispose()
    {
        _reader.SwitchGameStateButtonIsPressed -= SwitchState;

        foreach (var listener in _listeners)
            if (listener is IGameDisposeListener disposeListener)
                disposeListener.OnDispose();
    }

    public void AddListener(IGameListener listener) => _listeners.Add(listener);

    public void SwitchState()
    {
        _isPause = !_isPause;

        foreach (var listener in _listeners)
        {
            if (_isPause)
            {
                if (listener is IGamePauseListener pauseListener)
                    pauseListener.OnPause();
            }
            else
            {
                if (listener is IGameResumeListener pauseListener)
                    pauseListener.OnResume();
            }
        }
    }

    private void Update()
    {
        if (_isPause || _listeners.Count == 0) return;

        foreach (var listener in _listeners)
        {
            if (listener is IGameUpdateListener updateListener)
                updateListener.OnUpdate();
        }
    }
}