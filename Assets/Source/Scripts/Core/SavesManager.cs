using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Remote;
using Source.Scripts.SaveTypes;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;
using Zenject;
using Time = Source.Scripts.SaveTypes.Time;

namespace Source.Scripts.Core
{
    public class SavesManager : IDisposable, IInitializable
    {
        private const string LocalSavesKey = "Local";
        private const string CloudSavesKey = "Cloud";

        private SavesData _localSaves;
        private SavesData _cloudSaves;

        public bool IsReady;

        public void Initialize() => Init().Forget();

        private async UniTaskVoid Init()
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync().AsUniTask();
            await LoadAll();
            IsReady = true;
        }

        public void Dispose() => SaveAll().Forget();

        public async UniTaskVoid SaveAll()
        {
            _localSaves.Time = Time.From(DateTime.Now);
            SaveLocal();

            if (await NetworkChecker.HasInternet())
                await SaveCloud(_localSaves);
        }

        private void SaveLocal()
        {
            var json = JsonUtility.ToJson(_localSaves);
            PlayerPrefs.SetString(LocalSavesKey, json);
            PlayerPrefs.Save();
        }

        private async UniTask SaveCloud(SavesData data)
        {
            try
            {
                var json = JsonUtility.ToJson(data);
                var dict = new Dictionary<string, object> { { CloudSavesKey, json } };
                await CloudSaveService.Instance.Data.Player.SaveAsync(dict);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Cloud save failed: {e}");
            }
        }

        private async UniTask LoadAll()
        {
            _localSaves = LoadLocal();

            if (!await NetworkChecker.HasInternet())
                return;

            var cloud = await LoadCloud();
            if (cloud == null) return;

            _cloudSaves = cloud;

            if (_cloudSaves.Time.ToDateTime() > _localSaves.Time.ToDateTime())
                _localSaves = _cloudSaves;
        }

        private SavesData LoadLocal()
        {
            if (PlayerPrefs.HasKey(LocalSavesKey))
                return JsonUtility.FromJson<SavesData>(PlayerPrefs.GetString(LocalSavesKey));

            var data = new SavesData();
            data.InitDefaults();
            return data;
        }

        private async UniTask<SavesData?> LoadCloud()
        {
            try
            {
                var result =
                    await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { CloudSavesKey });
                if (result.TryGetValue(CloudSavesKey, out var cloudJson))
                    return JsonUtility.FromJson<SavesData>(cloudJson.Value.GetAs<string>());
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Cloud load failed: {e}");
            }

            return null;
        }

        public bool LoadSettings() => _localSaves.AdsDisabled;

        public void SavePlayerPosition(Vector3 position)
        {
            _localSaves.Position.Value = position;
            _localSaves.Position.HasValue = true;
        }

        public void SaveTempPosition(Vector3 position)
        {
            _localSaves.TempPosition.Value = position;
            _localSaves.TempPosition.HasValue = true;
        }

        public void SaveAdsState(bool disabled) => _localSaves.AdsDisabled = disabled;

        public SavedVector3 TryGetTempPosition() => _localSaves.TempPosition;

        public SavedVector3 TryGetPosition() => _localSaves.Position;

        public void ResetTempPosition() => _localSaves.TempPosition.HasValue = false;

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            _localSaves = new SavesData();
        }
    }
}