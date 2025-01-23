using System;
using UnityEngine;

public class PauseReader : MonoBehaviour
{
    public event Action SwitchGameStateButtonIsPressed;

    private void Update() => ReadPauseButton();

    private void ReadPauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchGameStateButtonIsPressed?.Invoke();
    }
}