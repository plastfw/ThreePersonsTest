using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameStateManager : ITickable, IInitializable, IDisposable
    {
        private List<IGameUpdateListener> _updateListeners = new();
        private List<IGameDisposeListener> _disposeListeners = new();
        private List<IGamePauseListener> _pauseListeners = new();
        private List<IGameResumeListener> _resumeListeners = new();
        private List<IGameStartListener> _startListeners = new();

        private bool _isPause;

        public void Initialize()
        {
            foreach (var listener in _startListeners)
                listener.OnStart();
        }

        public void Tick()
        {
            CheckPauseClick();
            UpdateInListeners();
        }

        public void Dispose()
        {
            foreach (var listener in _disposeListeners)
                listener.OnDispose();
        }

        public void AddListener(IGameListener listener)
        {
            if (listener is IGameStartListener startListener)
                _startListeners.Add(startListener);

            if (listener is IGameDisposeListener disposeListener)
                _disposeListeners.Add(disposeListener);

            if (listener is IGamePauseListener pauseListener)
                _pauseListeners.Add(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _resumeListeners.Add(resumeListener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Add(updateListener);
        }

        private void UpdateInListeners()
        {
            if (_isPause || _updateListeners.Count == 0) return;

            foreach (var listener in _updateListeners)
                listener.OnUpdate();
        }

        private void SwitchState()
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                foreach (var listener in _pauseListeners)
                    listener.OnPause();
            }
            else
            {
                foreach (var listener in _resumeListeners)
                    listener.OnResume();
            }
        }

        private void CheckPauseClick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SwitchState();
        }
    }
}