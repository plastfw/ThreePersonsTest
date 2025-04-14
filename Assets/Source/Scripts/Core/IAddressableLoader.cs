using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Scripts.Core
{
    public interface IAddressableLoader
    {
        UniTask<T> LoadAssetAsync<T>(string key, Transform parent = null) where T : UnityEngine.Component;

        void ReleaseAsset();
    }
}