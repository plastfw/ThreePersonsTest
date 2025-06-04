using UnityEngine;

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