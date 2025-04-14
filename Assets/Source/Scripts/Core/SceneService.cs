using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Core
{
    public class SceneService
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);

        public async UniTask LoadSceneAsync(int index, bool allowSceneActivation = true)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(index);
            asyncOperation.allowSceneActivation = allowSceneActivation;

            await asyncOperation.ToUniTask();
        }
    }
}