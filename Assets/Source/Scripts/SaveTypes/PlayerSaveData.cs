using UnityEngine;

namespace Source.Scripts.SaveTypes
{
    [System.Serializable]
    public class PlayerSaveData
    {
        public Vector3 Position;

        public PlayerSaveData()
        {
            Position = Vector3.zero;
        }

        public PlayerSaveData(Vector3 position)
        {
            Position = position;
        }
    }
}