using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

public class EnemiesController : MonoBehaviour, Listeners.IGameListener, Listeners.IGamePauseListener,
  Listeners.IGameResumeListener
{
  [SerializeField] private List<FOVEnemy> _fovEnemies;
  [SerializeField] private List<ShootEnemy> _shootEnemies;
  [SerializeField] private List<TrakectoryEnemy> _trakectoryEnemies;
  [SerializeField] private FOVData _FOVData;
  [SerializeField] private TrajectoryData _trajectoryData;
  [SerializeField] private ShootData _shootData;

  private GameStateManager _gameStateManager;

  [Inject]
  private void Init(GameStateManager gameStateManager)
  {
    _gameStateManager = gameStateManager;
    _gameStateManager.AddListener(this);
  }

  private void Start()
  {
    InitializeEnemies();
  }

  private void InitializeEnemies()
  {
    foreach (var enemy in _shootEnemies)
      enemy.Init(_shootData.Damage, _shootData.Radius);

    foreach (var enemy in _trakectoryEnemies)
      enemy.Init(_trajectoryData.Damage);

    foreach (var enemy in _fovEnemies)
      enemy.Init(_FOVData.Damage, _FOVData.Speed);
  }

  public void OnPause()
  {
    foreach (var enemy in _shootEnemies)
      enemy.Pause();
    foreach (var enemy in _trakectoryEnemies)
      enemy.Pause();
    foreach (var enemy in _fovEnemies)
      enemy.Pause();
  }

  public void OnResume()
  {
    foreach (var enemy in _shootEnemies)
      enemy.Resume();
    foreach (var enemy in _trakectoryEnemies)
      enemy.Resume();
    foreach (var enemy in _fovEnemies)
      enemy.Resume();
  }
}