using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Factories;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class StartMenuEntryPoint : IInitializable
    {
        private readonly MenuSystemFactory _menuFactory;
        private readonly FireBaseInitializer _firebaseInitializer;
        private MainMenuModel _model;
        private AdsInitializer _adsInitializer;

        public StartMenuEntryPoint(MenuSystemFactory menuFactory, FireBaseInitializer firebaseInitializer,
            MainMenuModel model, AdsInitializer adsInitializer)
        {
            _menuFactory = menuFactory;
            _firebaseInitializer = firebaseInitializer;
            _model = model;
            _adsInitializer = adsInitializer;
        }

        public async void Initialize()
        {
            await _menuFactory.Create();

#if !UNITY_EDITOR
            _adsInitializer.InitLevelPlay();

            if (!_adsInitializer.isInitialized)
                await UniTask.WaitUntil(() => _adsInitializer.isInitialized);
#endif

            _model.UpdateFirebaseStatus(_firebaseInitializer.IsInitialized);

            if (!_firebaseInitializer.IsInitialized)
            {
                await UniTask.WaitUntil(() => _firebaseInitializer.IsInitialized);
                _model.UpdateFirebaseStatus(true);
            }
        }
    }
}