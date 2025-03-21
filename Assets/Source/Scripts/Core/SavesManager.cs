using UnityEngine;

public class SavesManager
{
    public void Save<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

        Debug.Log($"Saved {typeof(T)}");
    }

    public T Load<T>(string key, T defaultValue = default) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);

            T data = JsonUtility.FromJson<T>(json);

            return data;
        }

        Debug.LogWarning($"Key {key} not found. Returning default value.");
        return defaultValue;
    }

    public void DeleteAll() => PlayerPrefs.DeleteAll();
}