﻿using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class GameStateManager : ITickable, IInitializable, IDisposable
    {
        private List<IGameListener> _listeners = new();
        private CompositeDisposable _disposable = new();
        private bool _isPause;

        [Inject]
        private void Init(Button pauseButton)
        {
            pauseButton.OnClickAsObservable().Subscribe(_ => SwitchState()).AddTo(_disposable);
        }

        public void Initialize()
        {
            foreach (var listener in _listeners)
                if (listener is IGameStartListener startListener)
                    startListener.OnStart();
        }

        public void Tick()
        {
            CheckPauseClick();
            UpdateInListeners();
        }

        private void CheckPauseClick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SwitchState();
        }

        private void UpdateInListeners()
        {
            if (_isPause || _listeners.Count == 0) return;

            foreach (var listener in _listeners)
            {
                if (listener is IGameUpdateListener updateListener)
                    updateListener.OnUpdate();
            }
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
    }
}