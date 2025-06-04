using Source.Scripts.UI;
using UnityEngine.Purchasing;

namespace Source.Scripts.Core
{
    public interface IIAPService
    {
        void Initialize(MainMenuModel model);
        bool IsInitialized { get; }
        IStoreController GetController();
    }
}