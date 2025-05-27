using UnityEngine;

namespace Source.Scripts.Core
{
    public class SavesManager
    {
        private const string SaveKey = "GameSaveData";

        public GameSaveData CurrentSave { get; private set; }

        public SavesManager()
        {
            Load();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(CurrentSave);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public void Load(PlayerPositionData defaultPlayerData = null, SettingsData defaultSettings = null)
        {
            CurrentSave = PlayerPrefs.HasKey(SaveKey)
                ? JsonUtility.FromJson<GameSaveData>(PlayerPrefs.GetString(SaveKey))
                : new GameSaveData();

            if (defaultPlayerData != null)
                CurrentSave.Player = defaultPlayerData;

            if (defaultSettings != null)
                CurrentSave.Settings = defaultSettings;
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            CurrentSave = new GameSaveData();
        }
    }
}