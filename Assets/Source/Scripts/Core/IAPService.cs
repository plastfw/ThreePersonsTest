using Cysharp.Threading.Tasks;
using R3;
using Source.Scripts.UI;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Source.Scripts.Core
{
    public class IAPService : IStoreListener, IIAPService, IInitializable
    {
        private IStoreController _storeController;
        private IExtensionProvider _extensions;
        private MainMenuModel _mainMenuModel;
        private readonly Subject<bool> _purchaseCompleted = new();
        private SavesManager _saves;

        public Observable<bool> PurchaseCompleted => _purchaseCompleted;
        public bool IsInitialized => _storeController != null && _extensions != null;

        [Inject]
        public IAPService(SavesManager saves) => _saves = saves;

        public void Initialize()
        {
            UnityServices.InitializeAsync().AsUniTask();
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

        public bool DisableAds()
        {
            var product = _storeController.products.WithID("test.noads");

            if (product == null || !product.availableToPurchase)
            {
                Debug.LogError("Product not available");
                return false;
            }

            _saves.SaveAdsState(true);
            _saves.SaveAll();
            _storeController.InitiatePurchase(product);
            return true;
        }


        //unity сам вызывает этот метод после InitiatePurchase, поэтому ссылок на метод ни у кого нет
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (args.purchasedProduct.definition.id == "test.noads")
                _purchaseCompleted.OnNext(true);

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason reason) =>
            Debug.LogError($"Purchase failed: {product.definition.id}, reason: {reason}");
    }
}