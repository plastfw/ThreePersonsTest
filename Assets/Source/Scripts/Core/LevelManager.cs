using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class LevelManager : IDisposable

    {
        private readonly SceneManagerService _sceneManagerService;
        private readonly Dictionary<int, string> _levels = new();
        private int _currentLevelIndex = 0;
        private CompositeDisposable _disposable = new();


        public LevelManager(SceneManagerService sceneManagerService)
        {
            _sceneManagerService = sceneManagerService;
            _levels.Add(0, "MainMenu");
            _levels.Add(1, "Level1");

            // _gameMenu.Menu.Subscribe(_ => GoToMenu());
            // _gameMenu.Next.Subscribe(_ => GoToNextLevel());

            Debug.LogWarning("LOAD");
        }

        public void SubscribeToPlayCommand(ReactiveCommand playCommand, bool exit = false)
        {
            Debug.LogWarning("Sub");

            if (exit)
                playCommand.Subscribe(_ => GoToMenu()).AddTo(_disposable);
            else
                playCommand.Subscribe(_ => GoToNextLevel()).AddTo(_disposable);
        }

        private void GoToCurrentLevel()
        {
            if (_levels.TryGetValue(_currentLevelIndex, out var sceneName))
                _sceneManagerService.LoadScene(sceneName);
            else
            {
                _currentLevelIndex = 1;
                GoToCurrentLevel();
            }
        }

        private void GoToNextLevel()
        {
            _currentLevelIndex++;
            GoToCurrentLevel();
        }

        private void GoToMenu()
        {
            _currentLevelIndex = 0;
            GoToCurrentLevel();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}