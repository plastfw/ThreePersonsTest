using System;
using System.Collections.Generic;
using R3;

namespace Source.Scripts.Core
{
    public class LevelManager : IDisposable
    {
        private readonly SceneService _sceneService;
        private readonly Dictionary<int, string> _levels = new();
        private int _currentLevelIndex = 0;
        private CompositeDisposable _disposable = new();

        public LevelManager(SceneService sceneService)
        {
            _sceneService = sceneService;
            _levels.Add(0, "MainMenu");
            _levels.Add(1, "Level1");
        }

        public void SubscribeToPlayCommand(ReactiveCommand playCommand, bool exit = false)
        {
            if (exit)
                playCommand.Subscribe(_ => GoToMenu()).AddTo(_disposable);
            else
                playCommand.Subscribe(_ => GoToNextLevel()).AddTo(_disposable);
        }

        private void GoToCurrentLevel()
        {
            if (_levels.TryGetValue(_currentLevelIndex, out var sceneName))
                _sceneService.LoadScene(sceneName);
            else
            {
                _currentLevelIndex = 1;
                GoToCurrentLevel();
            }
        }

        public void GoToNextLevel()
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