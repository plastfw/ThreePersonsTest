using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelObserver : MonoBehaviour
{
    private SwitchModelObserver _switchModelObserver;
    private List<PlayerModel> _playerModels;

    [Inject]
    private void Init(SwitchModelObserver switchModelObserver, List<PlayerModel> playerModels)
    {
        _switchModelObserver = switchModelObserver;
        _playerModels = playerModels;
    }

    private void Start()
    {
        foreach (var model in _playerModels)
            model.DeadEvent += RestartLevel;

        _switchModelObserver.AllModelsInSafe += RestartLevel;
    }

    private void OnDestroy()
    {
        foreach (var model in _playerModels)
            model.DeadEvent += RestartLevel;

        _switchModelObserver.AllModelsInSafe -= RestartLevel;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}