using System;
using Source.Scripts.SaveTypes;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Core
{
    public class SavesManager : IInitializable, IDisposable
    {
        private const string SavesKey = "SettingsData";
        private SavesData _currentSaves;

        public void Initialize() => LoadAll();
        public void Dispose() => SaveAll();

        public void SaveAll()
        {
            var json = JsonUtility.ToJson(_currentSaves);
            PlayerPrefs.SetString(SavesKey, json);
            PlayerPrefs.Save();
        }

        private void LoadAll()
        {
            if (PlayerPrefs.HasKey(SavesKey))
                _currentSaves = JsonUtility.FromJson<SavesData>(PlayerPrefs.GetString(SavesKey));
            else
            {
                _currentSaves = new SavesData();
                _currentSaves.InitDefaults();
            }
        }

        public bool LoadSettings()
        {
            return _currentSaves.AdsDisabled;
        }

        public void SavePlayerPosition(Vector3 position)
        {
            _currentSaves.Position.Value = position;
            _currentSaves.Position.HasValue = true;
        }

        public void SaveTempPosition(Vector3 position)
        {
            _currentSaves.TempPosition.Value = position;
            _currentSaves.TempPosition.HasValue = true;
        }

        public void SaveAdsState(bool disabled) => _currentSaves.AdsDisabled = disabled;

        public SavedVector3 TryGetTempPosition()
        {
            return _currentSaves.TempPosition;
        }

        public SavedVector3 TryGetPosition()
        {
            return _currentSaves.Position;
        }

        public void ResetTempPosition()
        {
            _currentSaves.TempPosition.HasValue = false;
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            _currentSaves = new SavesData();
        }
    }
}