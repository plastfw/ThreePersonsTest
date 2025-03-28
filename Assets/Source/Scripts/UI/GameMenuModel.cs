using Source.Scripts.Core;

namespace Source.Scripts.UI
{
    public class GameMenuModel
    {
        private GameMenuView _view;

        private LevelManager _levelManager;

        private GameMenuModel(LevelManager levelManager, GameMenuView view)
        {
            _levelManager = levelManager;
            _view = view;
        }

        public void LoadGameScene()
        {
            _levelManager.LoadGameScene();
        }

        public void LoadMenuScene()
        {
            _levelManager.LoadMenuScene();
        }

        public void Show() => _view.Show();
    }
}