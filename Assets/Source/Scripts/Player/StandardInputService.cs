using UnityEngine;
using Action = System.Action;

public class StandardInputService : IInputService
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    
    public float GetHorizontalAxis() => Input.GetAxisRaw(HORIZONTAL);
    public float GetVerticalAxis() => Input.GetAxisRaw(VERTICAL);

    public bool IsSwitchButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}