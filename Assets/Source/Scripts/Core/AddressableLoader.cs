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
        private const string Hudkey = "GameplayHUD";
        private const string Spherekey = "PlayerSphere";
        private const string Cubekey = "PlayerCube";
        private const string GameMenuKey = "InGameScreenMenu";
        private const string Fovenemykey = "FOVEnemy";
        private const string Shootenemy = "ShootEnemy";
        private const string Trajectoryenemy = "TrakectoryEnemy";
        private const string MainMenuKey = "MainMenuScreen";
        private const string AdsKey = "AdsScreen";

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

        public async UniTask<HUDView> LoadHudMenu(Transform parent) => await LoadAssetAsync<HUDView>(Hudkey, parent);

        public async UniTask<List<PlayerModel>> LoadPlayerModels(Transform parent)
        {
            var cubeTask = await LoadAssetAsync<PlayerModel>(Cubekey, parent);
            var sphereTask = await LoadAssetAsync<PlayerModel>(Spherekey, parent);

            return new List<PlayerModel> { cubeTask, sphereTask };
        }

        public async UniTask<GameMenuView> LoadInGameMenu(Transform parent)
        {
            var menuView = await LoadAssetAsync<GameMenuView>(GameMenuKey, parent);
            return menuView;
        }

        public async UniTask<FOVEnemy> LoadFovEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<FOVEnemy>(Fovenemykey, parent);
            return enemy;
        }

        public async UniTask<TrajectoryEnemy> LoadTrajectoryEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<TrajectoryEnemy>(Trajectoryenemy, parent);
            return enemy;
        }

        public async UniTask<ShootEnemy> LoadShootEnemy(Transform parent)
        {
            var enemy = await LoadAssetAsync<ShootEnemy>(Shootenemy, parent);
            return enemy;
        }

        public async UniTask<MainMenuView> LoadMainMenu()
        {
            var menuView = await LoadAssetAsync<MainMenuView>(MainMenuKey);
            return menuView;
        }

        public async UniTask<AdsView> LoadAdsMenu(Transform parent)
        {
            var menuView = await LoadAssetAsync<AdsView>(AdsKey, parent);
            return menuView;
        }

        public void ReleaseAsset()
        {
            if (_handle.IsValid())
                Addressables.Release(_handle);
        }
    }
}