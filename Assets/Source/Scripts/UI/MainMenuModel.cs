using Cysharp.Threading.Tasks;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.UI
{
    public class MainMenuModel : IInitializable
    {
        private SceneService _sceneService;
        private IAnalytic _analytic;
        private MainMenuView _view;
        private bool _isLoading = false;

        [Inject]
        public void Init(SceneService sceneService, IAnalytic analytic, MainMenuView view)
        {
            _sceneService = sceneService;
            _analytic = analytic;
            _view = view;
        }

        public void Initialize()
        {
            _view.ChangeButtonState(FireBaseInitializer.IsInitialized);

            if (!FireBaseInitializer.IsInitialized)
                WaitAndEnableButton().Forget();
        }

        public async UniTask StartLoadGame()
        {
            if (_isLoading) return;

            _isLoading = true;
            try
            {
                if (!FireBaseInitializer.IsInitialized)
                {
                    Debug.Log("Waiting for Firebase initialization...");
                    await WaitForFirebaseInitialization();
                }

                _sceneService.LoadScene(1);
                _analytic.Login();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async UniTask WaitForFirebaseInitialization()
        {
            await UniTask.WaitUntil(() => FireBaseInitializer.IsInitialized);
        }

        private async UniTaskVoid WaitAndEnableButton()
        {
            await WaitForFirebaseInitialization();
            _view.ChangeButtonState(true);
        }
    }
}