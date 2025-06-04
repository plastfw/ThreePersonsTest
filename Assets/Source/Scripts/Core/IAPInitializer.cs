using Cysharp.Threading.Tasks;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Source.Scripts.Core
{
    public class IAPInitializer : IStoreListener, IIAPService
    {
        private IStoreController _storeController;
        private IExtensionProvider _extensions;
        private MainMenuModel _mainMenuModel;

        public bool IsInitialized
        {
            get { return _storeController != null && _extensions != null; }
        }

        public void Initialize(MainMenuModel model)
        {
            _mainMenuModel = model;
            var module = StandardPurchasingModule.Instance();
            module.useFakeStoreAlways = true;

            var builder = ConfigurationBuilder.Instance(module);
            builder.AddProduct("test.noads", ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public IStoreController GetController()
        {
            return _storeController;
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
        {
            if (args.purchasedProduct.definition.id == "test.noads")
            {
                _mainMenuModel?.OnAdsPurchaseCompleted();
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
        {
            Debug.LogError($"Purchase failed: {product.definition.id}, reason: {reason}");
        }
    }
}