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
        private readonly SavesManager _saves;
        private MainMenuPresenter _menuPresenter;

        public readonly ReactiveProperty<bool> IsFirebaseReady = new();

        public MainMenuModel(SceneService sceneService, IAnalytic analytic, SavesManager saves,
            MainMenuPresenter menuPresenter)
        {
            _sceneService = sceneService;
            _analytic = analytic;
            _saves = saves;
            _menuPresenter = menuPresenter;
        }

        public void Init()
        {
            if (_saves.CurrentSave.Settings.AdsDisabled)
                _menuPresenter.OnAdsButtonClicked();
        }

        public async UniTask StartGameAsync()
        {
            await _sceneService.LoadGameSceneAsync();
            _analytic.Login();
        }

        public void DisableAds()
        {
            _saves.CurrentSave.Settings.AdsDisabled = true;
            _saves.Save();
        }

        public void UpdateFirebaseStatus(bool isReady) => IsFirebaseReady.Value = isReady;
    }
}