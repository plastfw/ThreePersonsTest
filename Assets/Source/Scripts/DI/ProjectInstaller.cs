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
            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavesManager>().FromNew().AsSingle();
            Container.Bind<AdsConfig>().FromScriptableObjectResource("IronSourceConfig").AsSingle();
            Container.Bind<FireBaseInitializer>().FromNewComponentOnNewGameObject().AsSingle();

            Container.BindInterfacesTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesTo<InterstitialAds>().AsSingle();
            Container.BindInterfacesTo<RewardedAds>().AsSingle();

            Container.Bind<IAddressableLoader>().To<AddressableLoader>().AsSingle();

            Container.Bind<SceneService>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();
            Container.Bind<IAnalytic>().To<FirebaseCustomAnalytics>().AsSingle();
        }
    }
}