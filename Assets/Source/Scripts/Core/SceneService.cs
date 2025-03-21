using UnityEngine.SceneManagement;

namespace Source.Scripts.Core
{
    public class SceneService
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}