using DG.Tweening;
using R3;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.UI
{
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

        private void Start()
        {
            _next.OnClickAsObservable().Subscribe(_ => NextClick()).AddTo(this);
            _menu.OnClickAsObservable().Subscribe(_ => MenuClick()).AddTo(this);
        }


        public void Show()
        {
            _canvas.DOFade(1, .5f).Play();
            _canvas.interactable = true;
        }

        private void MenuClick() => Menu.Execute(Unit.Default);

        private void NextClick() => Next.Execute(Unit.Default);
    }
}