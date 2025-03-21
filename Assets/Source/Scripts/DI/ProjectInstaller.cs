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
            builder
                .AddSingleton(typeof(SavesManager))
                .AddSingleton(typeof(SceneService))
                .AddSingleton(typeof(LevelManager))
                .AddSingleton(typeof(CustomAnalytics), typeof(IAnalytic));
        }
    }
}