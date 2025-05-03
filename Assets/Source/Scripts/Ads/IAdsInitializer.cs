namespace Source.Scripts.Ads
{
    public interface IAdsInitializer
    {
        void Init();
        bool IsInitialized { get; }
    }
}