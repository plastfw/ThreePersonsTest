using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameStateManager : MonoBehaviour, IDisposable
    {
        private List<IGameListener> _listeners = new();
        private CompositeDisposable _disposable = new();
        private PauseService _pauseService;
        private TickableService _tickableService;
        private bool _isPause;

        [Inject]
        private void Init(PauseService pauseService, TickableService tickableService)
        {
            _pauseService = pauseService;
            _tickableService = tickableService;
        }

        private void Start()
        {
            foreach (var listener in _listeners)
                if (listener is IGameStartListener startListener)
                    startListener.OnStart();

            _pauseService.Pause
                .Subscribe(_ => SwitchState()).AddTo(_disposable);

            _tickableService.Update
                .Subscribe(_ => OnUpdate()).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();

            foreach (var listener in _listeners)
                if (listener is IGameDisposeListener disposeListener)
                    disposeListener.OnDispose();
        }

        public void AddListener(IGameListener listener) => _listeners.Add(listener);

        private void SwitchState()
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

        private void OnUpdate()
        {
            if (_isPause || _listeners.Count == 0) return;

            foreach (var listener in _listeners)
            {
                if (listener is IGameUpdateListener updateListener)
                    updateListener.OnUpdate();
            }
        }
    }
}