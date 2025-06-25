using System;
using UnityEngine;

namespace Source.Scripts.SaveTypes
{
    [Serializable]
    public class SavesData
    {
        public SavedVector3 Position;
        public SavedVector3 TempPosition;
        public bool AdsDisabled;

        public void InitDefaults()
        {
            AdsDisabled = false;
        }
    }

    [Serializable]
    public struct SavedVector3
    {
        public Vector3 Value;
        public bool HasValue;
    }
}