using UnityEngine;

public class SavesManager
{
    public void Save<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public T Load<T>(string key, T defaultValue = default) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);

            T data = JsonUtility.FromJson<T>(json);

            return data;
        }

        return defaultValue;
    }

    public void DeleteAll() => PlayerPrefs.DeleteAll();
}