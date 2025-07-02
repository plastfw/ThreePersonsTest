using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Source.Scripts.Remote
{
    public static class NetworkChecker
    {
        private const string URL = "https://www.google.com";

        public static async UniTask<bool> HasInternet()
        {
            try
            {
                var request = UnityWebRequest.Head(URL);
                request.timeout = 2;
                await request.SendWebRequest();
                return request.result == UnityWebRequest.Result.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}