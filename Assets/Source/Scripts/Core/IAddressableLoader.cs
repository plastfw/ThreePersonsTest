using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Enemies;
using Source.Scripts.Player;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Core
{
    public interface IAddressableLoader
    {
        UniTask<T> InstantiateAssetAsync<T>(string key, Transform parent = null) where T : UnityEngine.Component;

        UniTask<HUDView> LoadHudMenu(Transform parent);
        UniTask<List<PlayerModel>> LoadPlayerModels(Transform parent);
        UniTask<GameMenuView> LoadInGameMenu(Transform parent);
        UniTask<FOVEnemy> LoadFovEnemy(Transform parent);
        UniTask<TrajectoryEnemy> LoadTrajectoryEnemy(Transform parent);
        UniTask<ShootEnemy> LoadShootEnemy(Transform parent);
        UniTask<MainMenuView> LoadMainMenu();
        UniTask<AdsView> LoadAdsMenu(Transform parent);

        void ReleaseAsset();
    }
}