using System;
using System.Collections.Generic;
using R3;

namespace Source.Scripts.Core
{
    public class LevelManager : IDisposable
    {
        private readonly SceneService _sceneService;
        private CompositeDisposable _disposable = new();

        public LevelManager(SceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public void SubscribeToPlayCommand(ReactiveCommand playCommand, bool exit = false)
        {
            if (exit)
                playCommand.Subscribe(_ => _sceneService.LoadScene(0)).AddTo(_disposable);
            else
                playCommand.Subscribe(_ => _sceneService.LoadScene(1)).AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}