using Cysharp.Threading.Tasks;
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

        public StartMenuEntryPoint(MenuSystemFactory menuFactory, FireBaseInitializer firebaseInitializer,
            MainMenuModel model)
        {
            _menuFactory = menuFactory;
            _firebaseInitializer = firebaseInitializer;
            _model = model;
        }

        public async void Initialize()
        {
            await _menuFactory.Create();
            _model.UpdateFirebaseStatus(_firebaseInitializer.IsInitialized);

            if (!_firebaseInitializer.IsInitialized)
            {
                await UniTask.WaitUntil(() => _firebaseInitializer.IsInitialized);
                _model.UpdateFirebaseStatus(true);
            }
        }
    }
}