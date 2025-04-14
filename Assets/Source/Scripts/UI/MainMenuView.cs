using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private MainMenuPresenter _presenter;

        public void Construct(MainMenuPresenter presenter)
        {
            _presenter = presenter;
            _playButton.OnClickAsObservable().Subscribe(_ => _presenter.OnButtonClicked()).AddTo(this);
        }

        public void ChangeButtonState(bool state) => _playButton.interactable = state;
    }
}