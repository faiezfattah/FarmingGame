using System;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;

public class InventorySystem : IInventorySystem {
    private InventoryRegistry _inventoryRegistry;
    private IDisposable subscription;
    public InventorySystem(InventoryRegistry inventoryRegistry, InputProcessor input) {
        _inventoryRegistry = inventoryRegistry;
        subscription = input.NumberEvent.Subscribe(x => HandleSelect(x));
    }

    public void AddItem(ItemContext item) {
        _inventoryRegistry.registry.Add(item);
    }

    public void RemoveItem(ItemContext item) {
        _inventoryRegistry.registry.Remove(item);
    }
    private void HandleSelect(int num) {
        _inventoryRegistry.activeItem.Value = _inventoryRegistry.registry[num - 1]; 
        foreach (var item in _inventoryRegistry.registry) {
            Debug.Log(item.ItemData.name );
        }
    }
}