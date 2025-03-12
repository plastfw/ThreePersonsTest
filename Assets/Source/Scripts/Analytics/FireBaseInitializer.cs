using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class FireBaseInitializer : MonoBehaviour
    {
        public void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    Debug.Log("Firebase Analytics initialized.");

                    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
                }
                else
                    Debug.LogError("Could not resolve Firebase dependencies: " + dependencyStatus);
            });
        }
    }
}