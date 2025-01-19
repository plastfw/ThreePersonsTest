using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;

public class GameInstaller : MonoBehaviour, IInstaller
{
  [SerializeField] private PlayerInput _playerInput;
  [SerializeField] private CameraController _cameraController;
  [SerializeField] private List<PlayerModel> _playerModels;
  [SerializeField] private PlayerHealthView _playerHealthView;
  [SerializeField] private PlayerSwitchModelObserver _playerSwitchModelObserver;
  [SerializeField] private BulletPool _bulletPool;
  [SerializeField] private GameStateManager _gameStateManager;

  public void InstallBindings(ContainerBuilder builder)
  {
    builder.AddSingleton(_playerModels);
    builder.AddSingleton(_playerInput);
    builder.AddSingleton(_cameraController);
    builder.AddSingleton(_playerHealthView);
    builder.AddSingleton(_playerSwitchModelObserver);
    builder.AddSingleton(_bulletPool);
    builder.AddSingleton(_gameStateManager);
  }
}