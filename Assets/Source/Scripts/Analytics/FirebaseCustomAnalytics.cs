using Firebase.Analytics;
using UnityEngine;

namespace Source.Scripts.Analytics
{
    public class FirebaseCustomAnalytics : IAnalytic
    {
        public void Login()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
            Debug.Log("Login");
        }

        public void CompleteLevel()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
            Debug.Log("LevelEnd");
        }

        public void LoseLevel()
        {
            FirebaseAnalytics.LogEvent("LoseLevel");
            Debug.Log("LoseLevel");
        }

        public void LogEvent(string str)
        {
            FirebaseAnalytics.LogEvent(str);
            Debug.Log(str);
        }
    }
}