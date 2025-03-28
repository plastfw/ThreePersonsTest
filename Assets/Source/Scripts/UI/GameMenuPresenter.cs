namespace Source.Scripts.UI
{
    public class GameMenuPresenter
    {
        private GameMenuModel _menuModel;

        public GameMenuPresenter(GameMenuModel model)
        {
            _menuModel = model;
        }

        public void NextButtonClicked() => _menuModel.LoadGameScene();

        public void MenuMuttonClicked() => _menuModel.LoadMenuScene();
    }
}