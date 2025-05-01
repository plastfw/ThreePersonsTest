using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Factories
{
    public class MenuSystemFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;

        public MenuSystemFactory(DiContainer container, IAddressableLoader loader)
        {
            _container = container;
            _loader = loader;
        }

        public async UniTask Create()
        {
            var presenter = _container.Resolve<MainMenuPresenter>();
            var model = _container.Resolve<MainMenuModel>();
            var view = await _loader.LoadMainMenu();

            view.Construct(presenter);
            presenter.Init(model, view);
        }
    }
}