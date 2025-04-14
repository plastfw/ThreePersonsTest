using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.UI
{
    public class HUDFactory
    {
        private const string HUDAddressableKey = "GameplayHUD";

        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;

        public HUDFactory(DiContainer container, IAddressableLoader loader, Canvas canvas)
        {
            _canvas = canvas;
            _container = container;
            _loader = loader;
        }

        public async UniTask<HUDSystem> Create()
        {
            var presenter = _container.Resolve<HUDPresenter>();
            var model = _container.Resolve<HUDModel>();

            var view = await _loader.LoadAssetAsync<HUDView>(HUDAddressableKey, _canvas.transform);

            model.Construct(view);
            presenter.StartInit();

            return new HUDSystem(model, presenter, view);
        }
    }

    public class HUDSystem
    {
        public HUDModel Model { get; }
        public HUDPresenter Presenter { get; }
        public HUDView View { get; }

        public HUDSystem(HUDModel model, HUDPresenter presenter, HUDView view)
        {
            Model = model;
            Presenter = presenter;
            View = view;
        }
    }
}