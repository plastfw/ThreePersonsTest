using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Zenject;

namespace Source.Scripts.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AdsConfig>().FromScriptableObjectResource("IronSourceConfig").AsSingle();
            Container.Bind<IAdsInitializer>().To<AdsInitializer>().AsSingle().NonLazy();
            Container.Bind<IInterstitialAds>().To<InterstitialAds>().AsSingle().NonLazy();
            Container.Bind<IRewardedAds>().To<RewardedAds>().AsSingle().NonLazy();

            Container.Bind<IAddressableLoader>().To<AddressableLoader>().AsSingle().NonLazy();

            Container.Bind<SavesManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<SceneService>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();
            Container.Bind<IAnalytic>().To<CustomAnalytics>().AsSingle().NonLazy();
        }
    }
}