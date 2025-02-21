using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Core
{
  public class SceneService
  {
    private readonly Dictionary<string, Action> _onSceneLoaded = new();
  
    public void LoadScene(string sceneName, Action onLoaded = null)
    {
      if (onLoaded != null)
        _onSceneLoaded[sceneName] = onLoaded;
    
      SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(string sceneName)
    {
      if (_onSceneLoaded.TryGetValue(sceneName, out var callback))
      {
        callback?.Invoke();
        _onSceneLoaded.Remove(sceneName);
      }
    }
  }
}
