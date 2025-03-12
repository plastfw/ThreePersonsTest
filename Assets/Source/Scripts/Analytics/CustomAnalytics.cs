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
    }
}