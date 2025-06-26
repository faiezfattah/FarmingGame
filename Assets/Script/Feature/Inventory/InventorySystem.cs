using System;
using System.Linq;
using MessagePipe;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;

public class InventorySystem : IInventorySystem, IDisposable {
    private InventoryRegistry _inventoryRegistry;
    private IDisposable hotbarSyncSubscription;
    private R3.DisposableBag _bag = new();
    public InventorySystem(InventoryRegistry inventoryRegistry, InputProcessor input) {
        _inventoryRegistry = inventoryRegistry;
        input.NumberEvent.Subscribe(x => HandleSelect(x)).AddTo(ref _bag);
        SeedContext.Event.OnUsed.Subscribe(x => RemoveItem(x.BaseData)).AddTo(ref _bag);
    }
    public void AddItem(ItemData item, int amount = 1) {
        var pack = _inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value += amount;
        }
        else {
            _inventoryRegistry.registry.Add(new(item.CreateBaseContext(), amount));
        }
        Debug.Log("added: " + item.name + " : " + amount);
    }
    public void AddItem(PackedItemContext packedItem, int amount = 1) {
        var pack = _inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == packedItem.ItemContext.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value += amount;
        }
        else {
            _inventoryRegistry.registry.Add(packedItem);
        }
        Debug.Log("added: " + packedItem.ItemContext.BaseData.name + " : " + amount);
    }

    public void RemoveItem(ItemData item, int amount = 1) {
        var pack = _inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.name)
                          .First();
        if (pack != null) {
            pack.Count.Value -= amount;
            if (pack.Count.Value == 0) {
                _inventoryRegistry.registry.Remove(pack);
            }
        }
        else {
            Debug.LogWarning("Attempting to reduce null pack");
        }
    }
    public void RemoveItem(PackedItemContext packedItem, int amount = 1) {
        if (packedItem != null) {
            packedItem.Count.Value -= amount;
            if (packedItem.Count.Value == 0) {
                _inventoryRegistry.registry.Remove(packedItem);
            }
        }
        else {
            Debug.LogWarning("Attempting to reduce null pack");
        }
    }
    private void HandleSelect(int num) {
        if (_inventoryRegistry.registry.Count < num - 1) return; // check for empty slots

        if (_inventoryRegistry.activeItem.Value == null) {
            var item = _inventoryRegistry.registry[num - 1];
            _inventoryRegistry.activeItem.Value = item.ItemContext;
            hotbarSyncSubscription = item.Count
                                         .Where(x => x <= 0)
                                         .Subscribe(_ => _inventoryRegistry.activeItem.Value = null);
        }
        else {
            _inventoryRegistry.activeItem.Value = null;
            hotbarSyncSubscription?.Dispose();
        }
    }
    public void Dispose() {
        _bag.Dispose();
    }
}