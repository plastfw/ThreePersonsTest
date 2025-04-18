using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Factories
{
    public class GameMenuFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;

        public GameMenuFactory(DiContainer container, IAddressableLoader loader, Canvas canvas)
        {
            _container = container;
            _loader = loader;
            _canvas = canvas;
        }

        public async UniTask<GameMenuSystem> Create()
        {
            var presenter = _container.Resolve<GameMenuPresenter>();
            var model = _container.Resolve<GameMenuModel>();
            var view = await _loader.LoadInGameMenu(_canvas.transform);

            model.Construct(view);
            view.Construct(presenter);

            return new GameMenuSystem(model, presenter, view);
        }
    }

    public class GameMenuSystem
    {
        public GameMenuModel Model { get; }
        public GameMenuPresenter Presenter { get; }
        public GameMenuView View { get; }

        public GameMenuSystem(GameMenuModel model, GameMenuPresenter presenter, GameMenuView view)
        {
            Model = model;
            Presenter = presenter;
            View = view;
        }
    }
}