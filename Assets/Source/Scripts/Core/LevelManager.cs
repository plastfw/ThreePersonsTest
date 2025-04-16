namespace Source.Scripts.Core
{
    public class LevelManager
    {
        private readonly SceneService _sceneService;

        public LevelManager(SceneService sceneService) => _sceneService = sceneService;

        public void LoadMenuScene() => _sceneService.LoadMenuScene();

        public void LoadGameScene() => _sceneService.LoadGameScene();
    }
}