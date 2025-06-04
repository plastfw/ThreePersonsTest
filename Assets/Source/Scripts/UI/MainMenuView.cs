using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _buyAdsButton;

        private MainMenuPresenter _presenter;

        public void Construct(MainMenuPresenter presenter)
        {
            _presenter = presenter;
            _playButton.OnClickAsObservable().Subscribe(_ => _presenter.OnStartButtonClicked()).AddTo(this);
            _buyAdsButton.OnClickAsObservable().Subscribe(_ => _presenter.OnAdsButtonClicked()).AddTo(this);
        }

        public void ChangeButtonState(bool state) => _playButton.interactable = state;

        public void HideAdsButton() => _buyAdsButton.gameObject.SetActive(false);

        public void ChangeAdsButtonState(bool state)
        {
            _buyAdsButton.interactable = state;
        }
    }
}