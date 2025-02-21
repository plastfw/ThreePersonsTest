using Reflex.Attributes;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private LevelManager _levelManager;

        [Inject]
        public void Init(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void OnEnable() => _playButton.onClick.AddListener(PlayButtonEvent);

        private void OnDisable() => _playButton.onClick.RemoveListener(PlayButtonEvent);

        private void PlayButtonEvent() => _levelManager.GoToNextLevel();

        public void Show()
        {
        }
    }
}