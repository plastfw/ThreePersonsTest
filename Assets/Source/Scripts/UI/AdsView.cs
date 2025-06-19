using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class AdsView : MonoBehaviour
    {
        [SerializeField] private Button _confirmAds;
        [SerializeField] private Button _rejectAds;
        [SerializeField] private CanvasGroup _canvas;

        private AdsPresenter _presenter;

        public void Init(AdsPresenter presenter)
        {
            _presenter = presenter;
            _confirmAds.OnClickAsObservable().Subscribe(_ => _presenter.OnConfirmClicked()).AddTo(this);
            _rejectAds.OnClickAsObservable().Subscribe(_ => _presenter.OnRejectClicked()).AddTo(this);
        }

        public void Show()
        {
            _canvas.DOFade(1, .5f).Play();
            _canvas.blocksRaycasts = true;
            _canvas.interactable = true;
        }
    }
}