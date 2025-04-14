using Cysharp.Threading.Tasks;

namespace Source.Scripts.UI
{
    public class MainMenuPresenter
    {
        private MainMenuModel _model;

        public void Init(MainMenuModel model) => _model = model;

        public void OnButtonClicked() => _model.StartGameAsync().Forget();
    }
}