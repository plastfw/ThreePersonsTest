using Reflex.Attributes;
using UnityEngine;
using VInspector;

namespace Core
{
    public class GameStateEditor : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;


        [Button]
        private void Pause() => _gameStateManager.OnPause();

        [Button]
        private void Resume() => _gameStateManager.OnResume();
    }
}