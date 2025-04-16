using Cysharp.Threading.Tasks;
using R3;
using Source.Scripts.Analytics;
using Source.Scripts.Core;

namespace Source.Scripts.UI
{
    public class MainMenuModel
    {
        private readonly SceneService _sceneService;
        private readonly IAnalytic _analytic;
        public readonly ReactiveProperty<bool> IsFirebaseReady = new();

        public MainMenuModel(SceneService sceneService, IAnalytic analytic)
        {
            _sceneService = sceneService;
            _analytic = analytic;
        }

        public async UniTask StartGameAsync()
        {
            await _sceneService.LoadGameSceneAsync();
            _analytic.Login();
        }

        public void UpdateFirebaseStatus(bool isReady) => IsFirebaseReady.Value = isReady;
    }
}