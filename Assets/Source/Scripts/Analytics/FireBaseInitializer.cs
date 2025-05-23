using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class FireBaseInitializer : MonoBehaviour
    {
        private int _test = 0;
        public bool IsInitialized { get; private set; } = false;

        private async void Start()
        {
            var task = FirebaseApp.CheckAndFixDependenciesAsync();
            await task;

            if (task.Result == DependencyStatus.Available)
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                IsInitialized = true;
                FetchDataAsync();
                Debug.Log("Firebase initialized.");
            }
            else
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);

            // await FetchDataAsync().ContinueWith(FetchComplete);
            // FetchComplete();
        }

        // private UniTask FetchDataAsync()
        // {
        //     return FirebaseRemoteConfig.DefaultInstance
        //         .FetchAsync(TimeSpan.Zero)
        //         .AsUniTask();
        // }

        public Task FetchDataAsync()
        {
            Task fetchTask = FirebaseRemoteConfig.DefaultInstance
                .FetchAsync(TimeSpan.Zero);

            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task task)
        {
            if (!task.IsCompleted)
            {
                Debug.LogError("Retrieval hasn't finished");
                return;
            }

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;

            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError(
                    $"{nameof(FetchComplete)} was unsuccessful \n {nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(task =>
                {
                    Debug.Log("Remote data load and ready to use");
                    var value = (int)FirebaseRemoteConfig.DefaultInstance.GetValue("testValue").LongValue;
                    _test = value;
                    Debug.LogWarning(_test);
                });
        }
    }
}