using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using R3;
using Reflex.Attributes;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Button _next;
    [SerializeField] private Button _menu;

    private LevelManager _levelManager;

    private ReactiveCommand Menu;
    private ReactiveCommand Next;

    [Inject]
    private void Init(LevelManager levelManager)
    {
        _levelManager = levelManager;

        Menu = new ReactiveCommand();
        Next = new ReactiveCommand();

        _levelManager.SubscribeToPlayCommand(Menu, true);
        _levelManager.SubscribeToPlayCommand(Next);
    }

    public void Show()
    {
        _canvas.DOFade(1, .5f).Play();
        _canvas.interactable = true;
    }

    private void Start()
    {
        _next.onClick.AddListener(NextClick);
        _menu.onClick.AddListener(MenuClick);
    }

    private void OnDestroy()
    {
        _next.onClick.RemoveListener(NextClick);
        _menu.onClick.RemoveListener(MenuClick);
    }

    private void MenuClick()
    {
        Menu.Execute(Unit.Default);
    }

    private void NextClick()
    {
        Next.Execute(Unit.Default);
    }
}