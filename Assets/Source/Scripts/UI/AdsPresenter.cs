namespace Source.Scripts.UI
{
    public class AdsPresenter : IAdsViewOutput
    {
        private AdsModel _model;
        private AdsView _view;

        public void Init(AdsModel model, AdsView adsView)
        {
            _view = adsView;
            _model = model;
        }

        public void OnConfirmClicked() => _model.ConfirmClickedEvent();

        public void OnRejectClicked() => _model.RejectClicked();

        public void Show() => _view.Show();
    }
}