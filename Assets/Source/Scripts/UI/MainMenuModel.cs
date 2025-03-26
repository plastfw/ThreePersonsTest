using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Source.Scripts.UI
{
    public class MainMenuModel
    {
        private SceneService _sceneService;
        private IAnalytic _analytic;

        [Inject]
        public void Init(SceneService sceneService, IAnalytic analytic)
        {
            _sceneService = sceneService;
            _analytic = analytic;
        }

        public async UniTask StartLoadGame()
        {
            if (!FireBaseInitializer.IsInitialized)
            {
                Debug.Log("Waiting for Firebase initialization...");
                await WaitForFirebaseInitialization();
            }

            _sceneService.LoadScene(1);
            _analytic.Login();
        }

        private async UniTask WaitForFirebaseInitialization()
        {
            await UniTask.WaitUntil(() => FireBaseInitializer.IsInitialized);
        }
    }
}