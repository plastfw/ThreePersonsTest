using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class FireBaseInitializer : MonoBehaviour
    {
        public bool IsInitialized { get; private set; } = false;

        private async void Start()
        {
            var task = FirebaseApp.CheckAndFixDependenciesAsync();
            await task;

            if (task.Result == DependencyStatus.Available)
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                IsInitialized = true;
                Debug.Log("Firebase Analytics initialized.");
            }
            else
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
        }
    }
}