using R3;

namespace Source.Scripts.Core
{
    public interface IIAPService
    {
        void Initialize();
        bool DisableAds();
        bool IsInitialized { get; }
        Observable<bool> PurchaseCompleted { get; }
    }
}