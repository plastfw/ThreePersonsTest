using Source.Scripts.Factories;
using Zenject;

namespace Source.Scripts.Core
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StartMenuEntryPoint>().AsSingle();
            Container.Bind<MenuSystemFactory>().AsSingle();
        }
    }
}