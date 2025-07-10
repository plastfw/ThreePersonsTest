using System;
using UnityEngine;

namespace Source.Scripts.SaveTypes
{
    [Serializable]
    public class SavesData
    {
        public SavedVector3 Position;
        public SavedVector3 TempPosition;
        public bool AdsDisabled = false;
        public Time Time;
    }

    [Serializable]
    public struct SavedVector3
    {
        public Vector3 Value;
        public bool HasValue;
    }

    [Serializable]
    public struct Time
    {
        private string _time;

        public static Time From(DateTime dateTime) =>
            new Time { _time = dateTime.ToString("o") };

        public DateTime ToDateTime() =>
            DateTime.TryParse(_time, out var result) ? result : default;

        public override string ToString() => _time;
    }
}