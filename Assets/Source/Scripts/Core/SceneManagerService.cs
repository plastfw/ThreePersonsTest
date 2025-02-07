using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerService
{
  private readonly Dictionary<string, Action> _onSceneLoaded = new();

  public void Print()
  {
    Debug.Log("test work");
  }
  
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
