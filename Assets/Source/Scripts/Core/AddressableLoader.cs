using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Source.Scripts.Core
{
    public class AddressableLoader : IAddressableLoader
    {
        private AsyncOperationHandle<GameObject> _handle;

        public async UniTask<T> LoadAssetAsync<T>(string key, Transform parent = null) where T : Component
        {
            _handle = Addressables.LoadAssetAsync<GameObject>(key);
            await _handle.ToUniTask();

            if (_handle.Status == AsyncOperationStatus.Succeeded)
            {
                Transform parentTransform = parent != null ? parent.transform : null;
                var obj = Object.Instantiate(_handle.Result, parentTransform);

                if (obj.TryGetComponent<T>(out var component))
                    return component;

                Debug.LogError($"Component of type {typeof(T)} not found on loaded asset");
                return null;
            }

            Debug.LogError($"Failed to load asset with key: {key}");
            return null;
        }

        public void ReleaseAsset()
        {
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}