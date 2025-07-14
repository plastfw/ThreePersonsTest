using UnityEngine;

namespace Source.Scripts.Ads
{
    public class MockAdsInitializer : IAdsInitializer
    {
        public bool IsInitialized => _isInitialized;

        private bool _isInitialized;

        public void Init()
        {
            Debug.Log("Mock ads initialization completed successfully");
            _isInitialized = true;
        }
    }
}