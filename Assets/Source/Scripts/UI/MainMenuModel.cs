using Cysharp.Threading.Tasks;
using Source.Scripts.Analytics;
using Source.Scripts.Core;

namespace Source.Scripts.UI
{
    public class MainMenuModel
    {
        private readonly SceneService _sceneService;
        private readonly IAnalytic _analytic;

        public MainMenuModel(SceneService sceneService, IAnalytic analytic)
        {
            _sceneService = sceneService;
            _analytic = analytic;
        }

        public async UniTask StartGameAsync()
        {
            await _sceneService.LoadSceneAsync(1);
            _analytic.Login();
        }
    }
}