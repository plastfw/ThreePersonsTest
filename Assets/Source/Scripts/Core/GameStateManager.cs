using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class GameStateManager : MonoBehaviour
{
  private List<Listeners.IGameListener> _listeners = new();

  public void AddListener(Listeners.IGameListener listener) => _listeners.Add(listener);

  [Button]
  private void OnPause()
  {
    foreach (var gameListener in _listeners)
    {
      if (gameListener is Listeners.IGamePauseListener pauseListener)
        pauseListener.OnPause();
    }
  }


  [Button]
  private void OnResume()
  {
    foreach (var gameListener in _listeners)
    {
      if (gameListener is Listeners.IGameResumeListener resumeListener)
        resumeListener.OnResume();
    }
  }
}