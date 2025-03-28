using Cysharp.Threading.Tasks;

namespace Source.Scripts.UI
{
    public class MainMenuPresenter
    {
        private MainMenuModel _model;

        public MainMenuPresenter(MainMenuModel model)
        {
            _model = model;
        }

        public void OnButtonClicked()
        {
            _model.StartLoadGame().Forget();
        }
    }
}