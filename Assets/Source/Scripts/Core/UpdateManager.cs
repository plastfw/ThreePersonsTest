using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Core
{
    public class UpdateManager : MonoBehaviour, IGameListener, IGamePauseListener, IGameResumeListener
    {
        [Inject] private GameStateManager _gameStateManager;

        private List<IGameUpdateListener> _listeners = new();
        private bool _pause = false;

        private void Start() => _gameStateManager.AddListener(this);

        public void AddListener(IGameUpdateListener updateListener) => _listeners.Add(updateListener);

        public void OnPause() => _pause = true;
        public void OnResume() => _pause = false;

        private void Update()
        {
            if (_pause)
                return;

            foreach (var listener in _listeners)
                listener.OnUpdate();
        }
    }
}