using Source.Scripts.Analytics;
using Source.Scripts.Factories;
using Source.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private FireBaseInitializer _firebaseInitializer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartMenuEntryPoint>().AsSingle().NonLazy();
            Container.Bind<MenuSystemFactory>().AsSingle().NonLazy();
            Container.Bind<MainMenuModel>().AsSingle().NonLazy();
            Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
            Container.Bind<FireBaseInitializer>().FromInstance(_firebaseInitializer).AsSingle().NonLazy();
        }
    }
}