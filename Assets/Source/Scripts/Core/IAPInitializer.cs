using UnityEngine;
using UnityEngine.Purchasing;

namespace Source.Scripts.Core
{
    public class IAPInitializer : IStoreListener
    {
        private IStoreController _storeController;
        private IExtensionProvider _extensions;

        public bool IsInitialized => _storeController != null && _extensions != null;
        public IStoreController StoreController => _storeController;

        public void InitializeIAP()
        {
            var module = StandardPurchasingModule.Instance();
            module.useFakeStoreAlways = true;

            var builder = ConfigurationBuilder.Instance(module);
            builder.AddProduct("test.noads", ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensions = extensions;
            Debug.Log("IAP initialized!");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"IAP Init Failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"IAP Init Failed: {error}\nDetails: {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
            => PurchaseProcessingResult.Complete;

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
        }
    }
}