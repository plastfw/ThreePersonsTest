using Source.Scripts.Ads;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Source.Scripts.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AdsConfig _adsConfig;

        private const string Local = "Local";
        private const string Cloud = "Cloud";
        private const string Ironsource = "ISConfig";

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
            Container.Bind<ISaveStorage>().WithId(Local).To<LocalSaveStorage>().AsSingle();
            Container.Bind<ISaveStorage>().WithId(Cloud).To<CloudSaveStorage>().AsSingle();


            Container.BindInterfacesAndSelfTo<SavesManager>().AsSingle();
            Container.Bind<AdsConfig>().FromComponentInNewPrefab(_adsConfig).AsSingle();
            Container.Bind<FireBaseInitializer>().FromNewComponentOnNewGameObject().AsSingle();

            // Container.BindInterfacesTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesTo<MockAdsInitializer>().AsSingle();
            Container.BindInterfacesTo<InterstitialAds>().AsSingle();
            Container.BindInterfacesTo<RewardedAds>().AsSingle();

            Container.Bind<IAddressableLoader>().To<AddressableLoader>().AsSingle();

            Container.Bind<SceneService>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();
            Container.Bind<IAnalytic>().To<FirebaseCustomAnalytics>().AsSingle();
        }
    }
}