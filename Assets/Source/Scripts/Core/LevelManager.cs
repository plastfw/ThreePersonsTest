namespace Source.Scripts.Core
{
    public class LevelManager
    {
        private readonly SceneService _sceneService;

        public LevelManager(SceneService sceneService) => _sceneService = sceneService;

        public void LoadMenuScene() => _sceneService.LoadScene(0);

        public void LoadGameScene() => _sceneService.LoadScene(1);
    }
}