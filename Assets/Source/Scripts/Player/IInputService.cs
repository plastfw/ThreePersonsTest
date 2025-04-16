namespace Source.Scripts.Player
{
    public interface IInputService
    {
        float GetHorizontalAxis();
        float GetVerticalAxis();
        bool IsSwitchButtonPressed();
    }
}