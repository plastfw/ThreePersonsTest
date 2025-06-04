using System;
using R3;

namespace Source.Scripts.UI
{
    public class AdsPresenter : IDisposable
    {
        private AdsModel _model;
        private AdsView _view;


        private readonly CompositeDisposable _disposable = new();

        public void Init(AdsModel model, AdsView adsView)
        {
            _view = adsView;
            _model = model;
        }

        public void OnConfirmClicked() => _model.ConfirmClickedEvent();

        public void OnRejectClicked() => _model.RejectClicked();

        public void Dispose() => _disposable.Dispose();

        public void Show()
        {
            _view.Show();
        }
    }
}