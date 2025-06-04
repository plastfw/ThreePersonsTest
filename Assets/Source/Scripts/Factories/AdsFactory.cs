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
        private readonly DiContainer _container;
        private readonly IAddressableLoader _loader;
        private readonly Canvas _canvas;
        private IInterstitialAds _interstitialAds;
        private IRewardedAds _rewardedAds;
        private LevelManager _levelManager;
        private SwitchModelObserver _switchModelObserver;
        private SavesManager _savesManager;
        private IAnalytic _analytic;

        public AdsFactory(DiContainer container, IAddressableLoader loader, Canvas canvas,
            IInterstitialAds interstitialAds, IRewardedAds rewardedAds, LevelManager levelManager,
            SwitchModelObserver switchModelObserver, SavesManager savesManager, IAnalytic analytic)
        {
            _container = container;
            _loader = loader;
            _canvas = canvas;
            _interstitialAds = interstitialAds;
            _rewardedAds = rewardedAds;
            _levelManager = levelManager;
            _switchModelObserver = switchModelObserver;
            _savesManager = savesManager;
            _analytic = analytic;
        }

        public async UniTask Create()
        {
            var presenter = _container.Resolve<AdsPresenter>();
            var model = _container.Resolve<AdsModel>();
            var view = await _loader.LoadAdsMenu(_canvas.transform);

            model.Construct(view, _switchModelObserver, _levelManager, _rewardedAds, _savesManager, _analytic,
                _interstitialAds);
            view.Construct(presenter);
            presenter.Init(model, view);
        }
    }
}