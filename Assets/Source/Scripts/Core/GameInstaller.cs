using Reflex.Core;
using UnityEngine;

public class GameInstaller : MonoBehaviour, IInstaller
{
  [SerializeField] private PlayerInput _playerInput;
  [SerializeField] private CameraController _cameraController;

  public void InstallBindings(ContainerBuilder builder)
  {
    builder.AddSingleton(_playerInput);
    builder.AddSingleton(_cameraController);

    //EXAMPLE
    // builder.AddSingleton(_headPool);
    //
    // builder.AddSingleton(container =>
    //   new StatsController(
    //     container.Resolve<BubblePlayer>(),
    //     container.Resolve<StatsView>()));
  }
}