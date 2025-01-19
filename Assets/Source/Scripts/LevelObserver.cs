using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObserver : MonoBehaviour
{
  private PlayerSwitchModelObserver _playerSwitchModelObserver;
  private List<PlayerModel> _playerModels;

  [Inject]
  private void Init(PlayerSwitchModelObserver playerSwitchModelObserver, List<PlayerModel> playerModels)
  {
    _playerSwitchModelObserver = playerSwitchModelObserver;
    _playerModels = playerModels;
  }

  private void OnEnable()
  {
    foreach (var model in _playerModels)
      model.DeadEvent += RestartLevel;

    _playerSwitchModelObserver.AllModelsInSafe += RestartLevel;
  }

  private void OnDisable()
  {
    foreach (var model in _playerModels)
      model.DeadEvent += RestartLevel;

    _playerSwitchModelObserver.AllModelsInSafe -= RestartLevel;
  }

  private void RestartLevel()
  {
    SceneManager.LoadScene(0);
  }
}