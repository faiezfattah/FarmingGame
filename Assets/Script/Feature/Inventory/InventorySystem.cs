using System;
using System.Linq;
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
        var pack =_inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value++;
        } else {
            _inventoryRegistry.registry.Add(new(item));
        }
    }

    public void RemoveItem(ItemContext item) {
        var pack =_inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value--;
            if (pack.Count.Value == 0) {
                _inventoryRegistry.registry.Remove(pack);
            }
        } else {
            Debug.LogWarning("Attempting to reduce empty pack");
        }
    }
    private void HandleSelect(int num) {
        _inventoryRegistry.activeItem.Value = _inventoryRegistry.registry[num - 1].ItemContext;
    }
}