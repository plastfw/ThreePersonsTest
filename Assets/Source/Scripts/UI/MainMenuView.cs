using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        [Inject]
        private void Init(MainMenuPresenter presenter)
        {
            _playButton.OnClickAsObservable().Subscribe(_ => presenter.OnButtonClicked()).AddTo(this);
        }

        public void ChangeButtonState(bool state)
        {
            _playButton.interactable = state;
        }
    }
}