using Cysharp.Threading.Tasks;

namespace Source.Scripts.Ads
{
    public interface IInterstitialAds
    {
        void EnableAds(string args);
        UniTask<bool> ShowInterstitialAsync();
        void Dispose();
    }
}