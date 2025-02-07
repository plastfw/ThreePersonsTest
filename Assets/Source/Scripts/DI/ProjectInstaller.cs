using Reflex.Core;
using Source.Scripts.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(typeof(SceneManagerService));
        builder.AddSingleton(typeof(LevelManager));
    }
}