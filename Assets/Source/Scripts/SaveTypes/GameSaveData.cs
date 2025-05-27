using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public PlayerPositionData Player;
    public SettingsData Settings;

    public GameSaveData()
    {
        Player = new PlayerPositionData();
        Settings = new SettingsData();
    }
}

[System.Serializable]
public class PlayerPositionData
{
    public Vector3 Position;

    public PlayerPositionData()
    {
        Position = Vector3.zero;
    }
}

[System.Serializable]
public class SettingsData
{
    public bool AdsDisabled;

    public SettingsData()
    {
        AdsDisabled = false;
    }
}