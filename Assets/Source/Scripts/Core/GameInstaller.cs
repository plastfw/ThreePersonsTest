using Reflex.Core;
using UnityEngine;

public class GameInstaller : MonoBehaviour, IInstaller
{
  public void InstallBindings(ContainerBuilder builder)
  {
    //EXAMPLE
    // builder.AddSingleton(_headPool);
    //
    // builder.AddSingleton(container =>
    //   new StatsController(
    //     container.Resolve<BubblePlayer>(),
    //     container.Resolve<StatsView>()));
  }
}