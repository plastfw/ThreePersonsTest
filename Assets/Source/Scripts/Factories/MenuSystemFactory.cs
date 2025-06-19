using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Factories
{
    public class MenuSystemFactory
    {
        private readonly IAddressableLoader _loader;
        private readonly IInstantiator _instantiator;

        public MenuSystemFactory(IAddressableLoader loader, IInstantiator instantiator)
        {
            _loader = loader;
            _instantiator = instantiator;
        }

        public async UniTask<MainMenuModel> Create()
        {
            var model = _instantiator.Instantiate<MainMenuModel>();
            var presenter = _instantiator.Instantiate<MainMenuPresenter>();
            var view = await _loader.LoadMainMenu();

            await model.Init();
            await presenter.Init(view, model);
            view.Init(presenter);
            return model;
        }
    }
}