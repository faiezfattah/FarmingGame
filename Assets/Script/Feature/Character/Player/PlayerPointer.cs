using R3;
using Script.Core.Interface;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.Player {
public class PlayerPointer : MonoBehaviour {
    [SerializeField] private GameObject indicator;
    private ISelectable _currentSelection; 

    private DisposableBag _bag;
    [Inject] public void Construct(InputProcessor inputProcessor) {
        inputProcessor.InteractEvent.Subscribe(_ => Interact()).AddTo(ref _bag);
    }
    private void Interact() {
        if (_currentSelection == null) return;
        Debug.Log("interacted with soil");
        _currentSelection.Select();
    }
    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent<ISelectable>(out var selectable);
        if (selectable == null) return;
        
        if (_currentSelection == null) {
            _currentSelection = selectable;
            indicator.SetActive(true);
            indicator.transform.position = selectable.GetPointerPosition();
        } else {
            _currentSelection = selectable;
            indicator.transform.position = selectable.GetPointerPosition();
        }
    }
    private void OnTriggerExit(Collider other){
        other.TryGetComponent<ISelectable>(out var selectable);
        if (selectable == null) return;
        _currentSelection = null;
        indicator.SetActive(false);
    }
}
}