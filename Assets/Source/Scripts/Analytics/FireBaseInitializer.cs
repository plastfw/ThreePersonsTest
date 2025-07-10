using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using Source.Scripts.Remote;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class FireBaseInitializer : MonoBehaviour
    {
        private const string ConfigKey = "game_config_json";
        private bool _firebaseInitialized = false;
        private RemoteGameConfig _config;

        public bool IsInitialized { get; private set; }

        private async void Start()
        {
            await InitializeFirebase();
            if (_firebaseInitialized)
                await InitializeRemoteConfig();
            else
                NotifyDefaultConfig();
        }

        private async UniTask InitializeFirebase()
        {
            try
            {
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _firebaseInitialized = true;
                    IsInitialized = true;
                    Debug.Log("Firebase initialized successfully");
                }
                else
                {
                    _firebaseInitialized = false;
                    Debug.LogError($"Could not resolve Firebase dependencies: {dependencyStatus}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Firebase initialization failed: {e}");
                _firebaseInitialized = false;
            }
        }

        private async UniTask InitializeRemoteConfig()
        {
            try
            {
                var defaults = new Dictionary<string, object> { [ConfigKey] = GetDefaultConfigJson() };
                await FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).AsUniTask();

                await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).AsUniTask();
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync().AsUniTask();

                await ProcessRemoteConfig();
            }
            catch (Exception e)
            {
                Debug.LogError($"Remote config error: {e}");
                NotifyDefaultConfig();
            }
        }

        private async UniTask ProcessRemoteConfig()
        {
            try
            {
                string json = FirebaseRemoteConfig.DefaultInstance.GetValue(ConfigKey).StringValue;
                _config = JsonUtility.FromJson<RemoteGameConfig>(json);
                Debug.Log("Remote config loaded successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"Config parsing error: {e}");
                NotifyDefaultConfig();
            }
        }

        private void NotifyDefaultConfig()
        {
            _config = JsonUtility.FromJson<RemoteGameConfig>(GetDefaultConfigJson());
            Debug.LogWarning("Using default config");
        }

        private string GetDefaultConfigJson()
        {
            return JsonUtility.ToJson(new RemoteGameConfig
            {
                shoot_data = new RemoteGameConfig.ShootData { damage = 10, radius = 1.5f },
                trajectory_data = new RemoteGameConfig.TrajectoryData { damage = 5 },
                fov_data = new RemoteGameConfig.FOVData { damage = 8, speed = 3.2f },
                additional_data = new RemoteGameConfig.AdditionalData { cube_speed = 3.0f, sphere_speed = 1.8f }
            });
        }

        public RemoteGameConfig GetConfig() => _config;
    }
}