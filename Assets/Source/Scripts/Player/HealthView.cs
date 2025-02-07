using R3;

namespace Source.Scripts.Player
{
    public class HealthView : ViewBase<int>
    {
        protected override void SubscribeToModel(PlayerModel model)
        {
            model.Health
                .Subscribe(value => SetField($"{value} healths"))
                .AddTo(ModelsDisposable);
        }
    }
}