using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Enemies;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Source.Scripts.Core
{
    public class AddressableLoader : IAddressableLoader
    {
        private const string HUDKEY = "GameplayHUD";
        private const string SPHEREKEY = "PlayerSphere";
        private const string CUBEKEY = "PlayerCube";
        private const string GameMenuKey = "InGameScreenMenu";
        private const string FOVENEMYKEY = "FOVEnemy";
        private const string SHOOTENEMY = "ShootEnemy";
        private const string TRAJECTORYENEMY = "TrakectoryEnemy";
        private const string MainMenuKey = "MainMenuScreen";

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

        public async UniTask<HUDView> LoadHudMenu(Transform parent) => await LoadAssetAsync<HUDView>(HUDKEY, parent);

        public async UniTask<List<PlayerModel>> LoadPlayerModels(Transform parent)
        {
            var cubeTask = await LoadAssetAsync<PlayerModel>(CUBEKEY, parent);
            var sphereTask = await LoadAssetAsync<PlayerModel>(SPHEREKEY, parent);

            return new List<PlayerModel> { cubeTask, sphereTask };
        }

        public async UniTask<GameMenuView> LoadInGameMenu(Transform parent)
        {
            var menuView = await LoadAssetAsync<GameMenuView>(GameMenuKey, parent);
            return menuView;
        }

        public async UniTask<FOVEnemy> LoadFovEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<FOVEnemy>(FOVENEMYKEY, parent);
            return enemy;
        }

        public async UniTask<TrajectoryEnemy> LoadTrajectoryEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<TrajectoryEnemy>(TRAJECTORYENEMY, parent);
            return enemy;
        }

        public async UniTask<ShootEnemy> LoadShootEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<ShootEnemy>(SHOOTENEMY, parent);
            return enemy;
        }

        public async UniTask<MainMenuView> LoadMainMenu()
        {
            var menuView = await LoadAssetAsync<MainMenuView>(MainMenuKey);
            return menuView;
        }

        public void ReleaseAsset()
        {
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}