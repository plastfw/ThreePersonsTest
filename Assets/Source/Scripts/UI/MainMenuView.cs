using R3;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private CompositeDisposable _disposable = new();

        [Inject]
        private void Init(MainMenuPresenter presenter)
        {
            _playButton.OnClickAsObservable().Subscribe(_ => presenter.OnButtonClicked()).AddTo(_disposable);
        }

        private void OnDestroy() => _disposable.Dispose();
    }
}