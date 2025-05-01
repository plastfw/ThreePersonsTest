namespace Source.Scripts.UI
{
    public class AdsModel
    {
        private AdsView _view;

        public void Construct(AdsView view) => _view = view;

        public void Show()
        {
            _view.Show();
        }
    }
}