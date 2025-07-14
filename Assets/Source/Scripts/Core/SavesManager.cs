using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Remote;
using Source.Scripts.SaveTypes;
using Unity.Services.Authentication;
using UnityEngine;
using Zenject;
using Time = Source.Scripts.SaveTypes.Time;

namespace Source.Scripts.Core
{
    public class SavesManager : IDisposable, IInitializable
    {
        private const string Local = "Local";
        private const string Cloud = "Cloud";

        private ISaveStorage _local;
        private ISaveStorage _cloud;

        private SavesData _localSaves;
        private SavesData _cloudSaves;

        public bool IsReady;

        [Inject]
        public void Construct([Inject(Id = Local)] ISaveStorage local, [Inject(Id = Cloud)] ISaveStorage cloud)
        {
            _local = local;
            _cloud = cloud;
        }

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
            if (await NetworkChecker.HasInternet())
            {
                await _cloud.Save(_localSaves);
                return;
            }

            _localSaves.Time = Time.From(DateTime.Now);
            await _local.Save(_localSaves);
        }

        public bool LoadSettings() => _localSaves.AdsDisabled;

        public void SavePlayerPosition(Vector3 pos)
        {
            _localSaves.Position.Value = pos;
            _localSaves.Position.HasValue = true;
        }

        public void SaveTempPosition(Vector3 pos)
        {
            _localSaves.TempPosition.Value = pos;
            _localSaves.TempPosition.HasValue = true;
        }

        public void SaveAdsState(bool disabled) => _localSaves.AdsDisabled = disabled;

        public SavedVector3 TryGetTempPosition() => _localSaves.TempPosition;

        public SavedVector3 TryGetPosition() => _localSaves.Position;

        public void ResetTempPosition() => _localSaves.TempPosition.HasValue = false;

        public void DeleteAll()
        {
            (_local as LocalSaveStorage)?.Delete();
            _localSaves = new SavesData();
        }

        private async UniTask LoadAll()
        {
            _localSaves = await _local.Load();

            if (!await NetworkChecker.HasInternet())
                return;

            _cloudSaves = await _cloud.Load();
            if (_cloudSaves == null) return;

            if (_cloudSaves.Time.ToDateTime() > _localSaves.Time.ToDateTime())
                _localSaves = _cloudSaves;
        }
    }
}