using R3;

namespace Source.Scripts.Player
{
    public class DistanceView : ViewBase<float>
    {
        protected override void SubscribeToModel(PlayerModel model)
        {
            model.DistanceToExit
                .Subscribe(value => SetField($"Distance: {value:F2}"))
                .AddTo(ModelsDisposable);
        }
    }
}