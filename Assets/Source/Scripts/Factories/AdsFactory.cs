using Cysharp.Threading.Tasks;
using Source.Scripts.Ads;
using Source.Scripts.Core;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Factories
{
    public class AdsFactory
    {
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;
        private InterstitialAds _interstitialAds;
        private RewardedAds _rewardedAds;
        private LevelManager _levelManager;
        private SwitchModelObserver _switchModelObserver;
        private SavesManager _savesManager;

        public AdsFactory(DiContainer container, IAddressableLoader loader, Canvas canvas,
            InterstitialAds interstitialAds, RewardedAds rewardedAds, LevelManager levelManager,
            SwitchModelObserver switchModelObserver, SavesManager savesManager)
        {
            _container = container;
            _loader = loader;
            _canvas = canvas;
            _interstitialAds = interstitialAds;
            _rewardedAds = rewardedAds;
            _levelManager = levelManager;
            _switchModelObserver = switchModelObserver;
            _savesManager = savesManager;
        }

        public async UniTask Create()
        {
            var presenter = _container.Resolve<AdsPresenter>();
            var model = _container.Resolve<AdsModel>();
            var view = await _loader.LoadAdsMenu(_canvas.transform);

            model.Construct(view);
            view.Construct(presenter);
            presenter.Init(model, view, _interstitialAds, _rewardedAds, _levelManager, _switchModelObserver,
                _savesManager);
        }
    }
}