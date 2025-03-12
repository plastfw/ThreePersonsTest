using Reflex.Core;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.DI
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(SavesManager));
            builder.AddSingleton(typeof(SceneService));
            builder.AddSingleton(typeof(LevelManager));
            builder.AddSingleton(typeof(CustomAnalytics), typeof(IAnalytic));
        }
    }
}