using Source.Scripts.Analytics;
using Source.Scripts.Core;
using Zenject;

namespace Source.Scripts.DI
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SavesManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<SceneService>().AsSingle().NonLazy();
            Container.Bind<LevelManager>().AsSingle().NonLazy();
            Container.Bind<IAnalytic>().To<CustomAnalytics>().AsSingle().NonLazy();
        }
    }
}