using System;
using R3;
using Script.Core.Interface;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.Player {
public class PlayerPointer : MonoBehaviour {
    [SerializeField] private GameObject indicator;
    private IActionable _currentSelection; 
    private DisposableBag _bag;
    private InventoryRegistry _inventoryRegistry;
    [Inject] public void Construct(InputProcessor inputProcessor, InventoryRegistry inventoryRegistry) {
        inputProcessor.InteractEvent.Subscribe(_ => Interact()).AddTo(ref _bag);
        _inventoryRegistry = inventoryRegistry;
    }
    private void Interact() {
        if (_currentSelection == null) return;
        if (_inventoryRegistry.activeTool.Value == null) return;
        
        if (_inventoryRegistry.activeItem.Value != null 
            && _inventoryRegistry.activeItem.Value is IUseable item) {
        _currentSelection.Action(item);
        } 
        else {
            _currentSelection.Action(_inventoryRegistry.activeTool.Value);
        } 

    }
    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent<IActionable>(out var actionable);
        if (actionable == null) return;
        
        if (_currentSelection == null) {
            _currentSelection = actionable;
            indicator.SetActive(true);
            indicator.transform.position = actionable.GetPointerPosition();
        } 
        else {
            _currentSelection = actionable;
            indicator.transform.position = actionable.GetPointerPosition();
        }
    }
    private void OnTriggerExit(Collider other){
        other.TryGetComponent<IActionable>(out var selectable);
        if (selectable == null) return;
        _currentSelection = null;
        indicator.SetActive(false);
    }
}
}