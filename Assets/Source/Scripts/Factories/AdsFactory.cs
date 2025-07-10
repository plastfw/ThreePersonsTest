using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Factories
{
    public class AdsFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;

        public AdsPresenter AdsPresenter { get; private set; }
        public AdsModel AdsModel { get; private set; }

        public AdsFactory(IInstantiator instantiator, IAddressableLoader loader, Canvas canvas)
        {
            _instantiator = instantiator;
            _loader = loader;
            _canvas = canvas;
        }

        public async UniTask Create()
        {
            var presenter = _instantiator.Instantiate<AdsPresenter>();
            var model = _instantiator.Instantiate<AdsModel>();
            var view = await _loader.LoadAdsMenu(_canvas.transform);

            view.Init(presenter);
            presenter.Init(model, view);
        }
    }
}