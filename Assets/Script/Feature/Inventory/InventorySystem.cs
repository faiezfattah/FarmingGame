using System;
using System.Linq;
using MessagePipe;
using ObservableCollections;
using R3;
using Script.Core.Model.Item;
using Script.Core.Model.Item.Seeds;
using Script.Feature.Input;
using UnityEngine;

public class InventorySystem : IInventorySystem, IDisposable {
    private InventoryRegistry _inventoryRegistry;
    private IDisposable subscription;
    private ISubscriber<SeedUsed> _seedUsedEvent;
    private IDisposable hotbarSyncSubscription;
    private R3.DisposableBag _bag = new();
    public InventorySystem(InventoryRegistry inventoryRegistry, InputProcessor input, ISubscriber<SeedUsed> seedUsedEvent) {
        _inventoryRegistry = inventoryRegistry;
        subscription = input.NumberEvent.Subscribe(x => HandleSelect(x)).AddTo(ref _bag);
        seedUsedEvent.Subscribe(x => RemoveItem(x.context)).AddTo(ref _bag);
    }
    public void AddItem(ItemContext item, int amount = 1) {
        var pack =_inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value += amount;
        } else {
            _inventoryRegistry.registry.Add(new(item, amount));
        }
        Debug.Log("added: " + item.BaseData.name + " : " + amount);
    }
    public void RemoveItem(ItemContext item, int amount = 1) {
        var pack =_inventoryRegistry.registry
                          .Where(x => x.ItemContext.BaseData.name == item.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value -= amount;
            if (pack.Count.Value == 0) {
                _inventoryRegistry.registry.Remove(pack);
            }
        } else {
            Debug.LogWarning("Attempting to reduce null pack");
        }
    }
    private void HandleSelect(int num) {
        if (_inventoryRegistry.activeItem.Value == null) {
            var item = _inventoryRegistry.registry[num - 1];
            _inventoryRegistry.activeItem.Value = item.ItemContext;
            hotbarSyncSubscription = item.Count
                                         .Where(x => x <= 0)
                                         .Subscribe(_ => _inventoryRegistry.activeItem.Value = null);
        } else {
            _inventoryRegistry.activeItem.Value = null;
            hotbarSyncSubscription?.Dispose();
        }
    }
    public void Dispose() {
        _bag.Dispose();
    }
}