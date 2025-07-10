using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using Source.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Source.Scripts.UI
{
    public class HUDFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;
        private GameStateManager _gameStateManager;
        private SwitchModelObserver _switchModelObserver;

        public HUDFactory(DiContainer container, IAddressableLoader loader, Canvas canvas,
            GameStateManager gameStateManager, SwitchModelObserver switchModelObserver)
        {
            _canvas = canvas;
            _gameStateManager = gameStateManager;
            _switchModelObserver = switchModelObserver;
            _container = container;
            _loader = loader;
        }

        public async UniTask Create()
        {
            var presenter = _container.Resolve<HUDPresenter>();
            var model = _container.Resolve<HUDModel>();
            var view = await _loader.LoadHudMenu(_canvas.transform);

            model.Construct(view);
            presenter.Construct(view, model,_switchModelObserver);
            presenter.StartInit();
            _gameStateManager.AddListener(presenter);
        }
    }
}