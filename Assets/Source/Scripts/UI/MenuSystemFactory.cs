using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.UI
{
    public class MenuSystemFactory
    {
        private const string MenuAddressableKey = "MainMenuScreen";

        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;

        public MenuSystemFactory(DiContainer container, IAddressableLoader loader)
        {
            _container = container;
            _loader = loader;
        }

        public async UniTask<MenuSystem> Create()
        {
            var presenter = _container.Resolve<MainMenuPresenter>();
            var model = _container.Resolve<MainMenuModel>();
            var view = await _loader.LoadAssetAsync<MainMenuView>(MenuAddressableKey);

            view.Construct(presenter);
            presenter.Init(model);

            return new MenuSystem(model, presenter, view);
        }
    }

    public class MenuSystem
    {
        public MainMenuModel Model { get; }
        public MainMenuPresenter Presenter { get; }
        public MainMenuView View { get; }

        public MenuSystem(MainMenuModel model, MainMenuPresenter presenter, MainMenuView view)
        {
            Model = model;
            Presenter = presenter;
            View = view;
        }
    }
}