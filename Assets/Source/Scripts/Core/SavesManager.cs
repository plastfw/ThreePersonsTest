using UnityEngine;

public class SavesManager
{
    public void SaveVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "_x", value.x);
        PlayerPrefs.SetFloat(key + "_y", value.y);
        PlayerPrefs.SetFloat(key + "_z", value.z);
        PlayerPrefs.Save(); 
    }

    public Vector3 LoadVector3(string key, Vector3 defaultValue)
    {
        float x = PlayerPrefs.GetFloat(key + "_x", defaultValue.x);
        float y = PlayerPrefs.GetFloat(key + "_y", defaultValue.y);
        float z = PlayerPrefs.GetFloat(key + "_z", defaultValue.z);
        return new Vector3(x, y, z);
    }

    public void DeleteAll() => PlayerPrefs.DeleteAll();
}