using UnityEngine;
using VContainer;
using R3;
using UnityEngine.UIElements;
using Script.Feature.Input;
using System;
public class Inventory : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    private IInventoryRegistry _inventoryRegistry;
    private IDisposable _subscription;
    [Inject] public void Construct (IInventoryRegistry inventoryRegistry, InputProcessor inputProcessor) {
        _inventoryRegistry = inventoryRegistry;
        _subscription = inputProcessor.InventoryEvent.Subscribe(_ => ToggleInventory()) ;
    }
    private void Start() {
        uIDocument.enabled = false;

        // var root = uIDocument.rootVisualElement;
        // var inventory = root.Query<InventoryDisplay>().First();
        // inventory.SetInventoryBinding(_inventoryRegistry);
    }
    private void ToggleInventory() {
        uIDocument.enabled = !uIDocument.enabled;

        if (!uIDocument.enabled) return;

        var root = uIDocument.rootVisualElement;
        var inventory = root.Query<InventoryDisplay>().First();
        inventory.SetInventoryBinding(_inventoryRegistry);                  
    }
    private void OnDisable() {
        _subscription?.Dispose();
    }
}