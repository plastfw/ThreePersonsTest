using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Core
{
    public class SceneService
    {
        public void LoadMenuScene() => SceneManager.LoadScene(0);

        public void LoadGameScene() => SceneManager.LoadScene(1);

        public async UniTask LoadGameSceneAsync(bool allowSceneActivation = true)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(1);
            asyncOperation.allowSceneActivation = allowSceneActivation;

            await asyncOperation.ToUniTask();
        }
    }
}