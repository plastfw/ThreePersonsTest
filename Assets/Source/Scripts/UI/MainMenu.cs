using Reflex.Attributes;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private LevelManager _levelManager;
        private IAnalytic _analytic;
        private SavesManager _saves;

        [Inject]
        public void Init(LevelManager levelManager, IAnalytic analytic, SavesManager savesManager)
        {
            _levelManager = levelManager;
            _analytic = analytic;
            _saves = savesManager;
        }

        private void OnEnable() => _playButton.onClick.AddListener(PlayButtonEvent);

        private void OnDisable() => _playButton.onClick.RemoveListener(PlayButtonEvent);

        private void PlayButtonEvent()
        {
            _saves.DeleteAll();
            _levelManager.GoToNextLevel();
            _analytic.Login();
        }
    }
}