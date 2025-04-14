using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private Button _next;
        [SerializeField] private Button _menu;

        private GameMenuPresenter _presenter;

        public void Construct(GameMenuPresenter gameMenuPresenter)
        {
            _presenter = gameMenuPresenter;
        }

        private void OnEnable()
        {
            _next.OnClickAsObservable().Subscribe(_ => _presenter.NextButtonClicked()).AddTo(this);
            _menu.OnClickAsObservable().Subscribe(_ => _presenter.MenuMuttonClicked()).AddTo(this);
        }

        public void Show()
        {
            _canvas.DOFade(1, .5f).Play();
            _canvas.interactable = true;
        }
    }
}