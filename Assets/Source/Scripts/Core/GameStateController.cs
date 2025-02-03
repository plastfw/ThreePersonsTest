using Reflex.Attributes;
using UnityEngine;
using VInspector;

namespace Source.Scripts.Core
{
    public class GameStateEditor : MonoBehaviour
    {
        [Inject] private GameStateManager _gameStateManager;

        [Button]
        private void SwitchState() => _gameStateManager.SwitchState();
    }
}