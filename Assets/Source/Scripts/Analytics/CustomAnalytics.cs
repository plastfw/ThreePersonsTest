using Firebase.Analytics;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class CustomAnalytics : IAnalytic
    {
        public void Login()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
            Debug.LogWarning("Login");
        }

        public void CompleteLevel()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
            Debug.LogWarning("LevelEnd");
        }

        public void LoseLevel()
        {
            FirebaseAnalytics.LogEvent("LoseLevel");
            Debug.LogWarning("LoseLevel");
        }
    }
}