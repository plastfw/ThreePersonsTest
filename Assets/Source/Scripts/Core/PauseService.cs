using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Core
{
    public class PauseService
    {
        private Observable<Unit> _pause;
        private readonly ReactiveCommand _pauseButtonClick;

        public Observable<Unit> Pause
        {
            get { return _pause ??= CreatePauseObservable(); }
        }

        public PauseService(Button pauseButton)
        {
            _pauseButtonClick = new ReactiveCommand();
            pauseButton.onClick.AddListener(OnButtonClicked);
        }

        private Observable<Unit> CreatePauseObservable()
        {
            return Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Merge(_pauseButtonClick);
        }

        private void OnButtonClicked() => _pauseButtonClick.Execute(Unit.Default);
    }
}