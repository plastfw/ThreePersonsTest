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
        private const string HudKey = "GameplayHUD";
        private const string SphereKey = "PlayerSphere";
        private const string CubeKey = "PlayerCube";
        private const string GameMenuKey = "InGameScreenMenu";
        private const string FovEnemyKey = "FOVEnemy";
        private const string ShootEnemyKey = "ShootEnemy";
        private const string TrajectoryEnemyKey = "TrakectoryEnemy";
        private const string MainMenuKey = "MainMenuScreen";
        private const string AdsKey = "AdsScreen";

        private AsyncOperationHandle<GameObject> _handle;

        public async UniTask<T> InstantiateAssetAsync<T>(string key, Transform parent = null) where T : Component
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

        public async UniTask<HUDView> LoadHudMenu(Transform parent) =>
            await InstantiateAssetAsync<HUDView>(HudKey, parent);

        public async UniTask<List<PlayerModel>> LoadPlayerModels(Transform parent)
        {
            var cubeTask = await InstantiateAssetAsync<PlayerModel>(CubeKey, parent);
            var sphereTask = await InstantiateAssetAsync<PlayerModel>(SphereKey, parent);

            return new List<PlayerModel> { cubeTask, sphereTask };
        }

        public async UniTask<GameMenuView> LoadInGameMenu(Transform parent)
        {
            var menuView = await InstantiateAssetAsync<GameMenuView>(GameMenuKey, parent);
            return menuView;
        }

        public async UniTask<FOVEnemy> LoadFovEnemy(Transform parent)
        {
            var enemy = await InstantiateAssetAsync<FOVEnemy>(FovEnemyKey, parent);
            return enemy;
        }

        public async UniTask<TrajectoryEnemy> LoadTrajectoryEnemy(Transform parent)
        {
            var enemy = await InstantiateAssetAsync<TrajectoryEnemy>(TrajectoryEnemyKey, parent);
            return enemy;
        }

        public async UniTask<ShootEnemy> LoadShootEnemy(Transform parent)
        {
            var enemy = await InstantiateAssetAsync<ShootEnemy>(ShootEnemyKey, parent);
            return enemy;
        }

        public async UniTask<MainMenuView> LoadMainMenu()
        {
            var menuView = await InstantiateAssetAsync<MainMenuView>(MainMenuKey);
            return menuView;
        }

        public async UniTask<AdsView> LoadAdsMenu(Transform parent)
        {
            var menuView = await InstantiateAssetAsync<AdsView>(AdsKey, parent);
            return menuView;
        }

        public void ReleaseAsset()
        {
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}