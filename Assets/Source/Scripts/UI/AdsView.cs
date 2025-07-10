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
        
        public void Init(IAdsViewOutput output)
        {
            _confirmAds.OnClickAsObservable().Subscribe(_ => output.OnConfirmClicked()).AddTo(this);
            _rejectAds.OnClickAsObservable().Subscribe(_ => output.OnRejectClicked()).AddTo(this);
        }

        public void Show()
        {
            _canvas.DOFade(1, .5f).Play();
            _canvas.blocksRaycasts = true;
            _canvas.interactable = true;
        }
    }
}