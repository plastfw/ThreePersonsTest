using Source.Scripts.SaveTypes;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class SavesManager
    {
        private const string PlayerSaveKey = "PlayerSaveData";

        public void Save(PlayerSaveData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(PlayerSaveKey, json);
            PlayerPrefs.Save();
        }

        public PlayerSaveData Load(PlayerSaveData defaultValue = default)
        {
            if (PlayerPrefs.HasKey(PlayerSaveKey))
            {
                string json = PlayerPrefs.GetString(PlayerSaveKey);
                return JsonUtility.FromJson<PlayerSaveData>(json);
            }

            return defaultValue;
        }

        public void DeleteAll()
        {
            Debug.LogWarning("DeleteAll");
            PlayerPrefs.DeleteAll();
        }
    }
}