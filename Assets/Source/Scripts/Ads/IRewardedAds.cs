using Cysharp.Threading.Tasks;

namespace Source.Scripts.Ads
{
    public interface IRewardedAds
    {
        void EnableAds();
        UniTask<bool> ShowRewardedAdAsync();
    }
}