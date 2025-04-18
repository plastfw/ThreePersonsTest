using Cysharp.Threading.Tasks;
using Source.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.UI
{
    public class HUDFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;

        public HUDFactory(DiContainer container, IAddressableLoader loader, Canvas canvas)
        {
            _canvas = canvas;
            _container = container;
            _loader = loader;
        }

        public async UniTask Create()
        {
            var presenter = _container.Resolve<HUDPresenter>();
            var model = _container.Resolve<HUDModel>();
            var view = await _loader.LoadHudMenu(_canvas.transform);

            model.Construct(view);
            presenter.StartInit();
        }
    }
}