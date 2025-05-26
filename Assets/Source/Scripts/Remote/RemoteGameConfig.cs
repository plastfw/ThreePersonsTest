using System;

namespace Source.Scripts.Remote
{
    [Serializable]
    public class RemoteGameConfig
    {
        public ShootData shoot_data;
        public TrajectoryData trajectory_data;
        public FOVData fov_data;
        public AdditionalData additional_data;

        [Serializable]
        public class ShootData
        {
            public int damage;
            public float radius;
        }

        [Serializable]
        public class TrajectoryData
        {
            public int damage;
        }

        [Serializable]
        public class FOVData
        {
            public int damage;
            public float speed;
        }

        [Serializable]
        public class AdditionalData
        {
            public float cube_speed;
            public float sphere_speed;
        }
    }
}