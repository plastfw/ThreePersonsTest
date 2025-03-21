using R3;
using Reflex.Attributes;
using Source.Scripts.Analytics;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Source.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private SceneService _sceneService;
        private IAnalytic _analytic;
        private SavesManager _saves;

        [Inject]
        public void Init(SceneService sceneService, IAnalytic analytic, SavesManager savesManager)
        {
            _sceneService = sceneService;
            _analytic = analytic;
            _saves = savesManager;
        }

        private void OnEnable() => _playButton.OnClickAsObservable().Subscribe(_ => OnPlayButtonClick());

        private async void OnPlayButtonClick()
        {
            if (!FireBaseInitializer.IsInitialized)
            {
                Debug.Log("Waiting for Firebase initialization...");
                await WaitForFirebaseInitialization();
            }

            _sceneService.LoadScene(1);
            // _saves.DeleteAll();
            _analytic.Login();
        }

        private async Task WaitForFirebaseInitialization()
        {
            while (!FireBaseInitializer.IsInitialized)
                await Task.Yield();
        }
    }
}