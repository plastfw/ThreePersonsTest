using Source.Scripts.Factories;
using Source.Scripts.UI;
using Zenject;

namespace Source.Scripts.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartMenuEntryPoint>().AsSingle().NonLazy();
            Container.Bind<MenuSystemFactory>().AsSingle().NonLazy();
            // Container.Bind<MainMenuModel>().AsSingle().NonLazy();
            // Container.Bind<MainMenuPresenter>().AsSingle().NonLazy();
        }
    }
}