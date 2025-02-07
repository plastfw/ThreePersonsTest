using R3;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Player
{
    public abstract class ViewBase<T> : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;

        private SwitchModelObserver _switchModelObserver;
        private readonly CompositeDisposable _disposable = new();
        protected CompositeDisposable ModelsDisposable = new();

        [Inject]
        private void Init(SwitchModelObserver switchModelObserver) => _switchModelObserver = switchModelObserver;

        private void Start()
        {
            _switchModelObserver.CurrentModel
                .Subscribe(OnModelChanged)
                .AddTo(_disposable);
        }

        private void OnDestroy() => _disposable.Dispose();

        private void OnModelChanged(PlayerModel model)
        {
            ModelsDisposable.Dispose();
            ModelsDisposable = new CompositeDisposable();

            if (model == null) return;

            SubscribeToModel(model);
        }

        protected abstract void SubscribeToModel(PlayerModel model);

        protected void SetField(string text) => _textField.text = text;
    }
}