using UnityEngine;

namespace Source.Scripts.Core
{
    public class SavesManager
    {
        private const string PlayerPositionKey = "PlayerPositionData";
        private const string SettingsKey = "SettingsData";

        public PlayerPositionData CurrentPlayerPosition { get; private set; }
        public SettingsData CurrentSettings { get; private set; }

        public SavesManager()
        {
            LoadAll();
        }

        public void SaveAll()
        {
            SavePlayerPosition();
            SaveSettings();
        }

        public void SavePlayerPosition()
        {
            string json = JsonUtility.ToJson(CurrentPlayerPosition);
            PlayerPrefs.SetString(PlayerPositionKey, json);
            PlayerPrefs.Save();
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(CurrentSettings);
            PlayerPrefs.SetString(SettingsKey, json);
            PlayerPrefs.Save();
        }

        public void LoadAll(PlayerPositionData defaultPlayerData = null, SettingsData defaultSettings = null)
        {
            LoadPlayerPosition(defaultPlayerData);
            LoadSettings(defaultSettings);
        }

        public void LoadPlayerPosition(PlayerPositionData defaultPlayerData = null)
        {
            CurrentPlayerPosition = PlayerPrefs.HasKey(PlayerPositionKey)
                ? JsonUtility.FromJson<PlayerPositionData>(PlayerPrefs.GetString(PlayerPositionKey))
                : defaultPlayerData ?? new PlayerPositionData();
        }

        public void LoadSettings(SettingsData defaultSettings = null)
        {
            CurrentSettings = PlayerPrefs.HasKey(SettingsKey)
                ? JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString(SettingsKey))
                : defaultSettings ?? new SettingsData();
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            CurrentPlayerPosition = new PlayerPositionData();
            CurrentSettings = new SettingsData();
        }
    }
}

