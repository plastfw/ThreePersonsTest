using R3;
using Reflex.Attributes;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private ReactiveCommand _playCommand;
    private LevelManager _levelManager;

    [Inject]
    public void Init(LevelManager levelManager)
    {
        _playCommand = new ReactiveCommand();
        _levelManager = levelManager;
        _levelManager.SubscribeToPlayCommand(_playCommand);
    }

    private void OnEnable() => _playButton.onClick.AddListener(PlayButtonEvent);

    private void OnDisable() => _playButton.onClick.RemoveListener(PlayButtonEvent);

    private void PlayButtonEvent()
    {
        print("Click");
        _playCommand.Execute(Unit.Default);
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }
}