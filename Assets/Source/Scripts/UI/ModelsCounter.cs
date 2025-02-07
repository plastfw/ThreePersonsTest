using System.Linq;
using R3;
using ObservableCollections;
using Reflex.Attributes;
using Source.Scripts.Player;
using TMPro;
using UnityEngine;

public class ModelsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;
    private SwitchModelObserver _switchModelObserver;
    private CompositeDisposable _disposable = new();

    [Inject]
    private void Init(SwitchModelObserver switchModelObserver)
    {
        _switchModelObserver = switchModelObserver;


        _switchModelObserver.ObservablePlayerModels
            .ObserveCountChanged()
            .Subscribe(ChangeValue)
            .AddTo(_disposable);

        ChangeValue(_switchModelObserver.ObservablePlayerModels.Count);
    }

    private void ChangeValue(int value) => _counter.text = $"{value} models left";

    private void OnDestroy() => _disposable.Dispose();
}