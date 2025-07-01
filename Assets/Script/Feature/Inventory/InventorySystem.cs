using System;
using System.Linq;
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
        var pack = _inventoryRegistry.inventory
                          .Where(x => x.ItemContext.BaseData.name == item.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value += amount;
        }
        else {
            _inventoryRegistry.inventory.Add(new(item.CreateBaseContext(), amount));
        }
        Debug.Log("added: " + item.name + " : " + amount);
    }
    public void AddItem(PackedItemContext packedItem, int amount = 1) {
        var pack = _inventoryRegistry.inventory
                          .Where(x => x.ItemContext.BaseData.name == packedItem.ItemContext.BaseData.name)
                          .FirstOrDefault();
        if (pack != null) {
            pack.Count.Value += amount;
        }
        else {
            _inventoryRegistry.inventory.Add(packedItem);
        }
        Debug.Log("added: " + packedItem.ItemContext.BaseData.name + " : " + amount);
    }

    public void RemoveItem(ItemData item, int amount = 1) {
        Debug.Log("removing item: " + item.name + " : " + amount);

        var pack = _inventoryRegistry.inventory
            .FirstOrDefault(x => x.ItemContext.BaseData.name == item.name);

        if (pack == null) {
            Debug.LogWarning("Attempting to remove item that does not exist in inventory: " + item.name);
            return;
        }

        pack.Count.Value -= amount;

        if (pack.Count.Value <= 0) {
            _inventoryRegistry.inventory.Remove(pack);
        }
    }
    public void RemoveItem(PackedItemContext packedItem, int amount = 1) {
        if (packedItem != null) {
            packedItem.Count.Value -= amount;
            if (packedItem.Count.Value == 0) {
                _inventoryRegistry.inventory.Remove(packedItem);
            }
        }
        else {
            Debug.LogWarning("Attempting to reduce null pack");
        }
    }
    private void HandleSelect(int num) {
        var index = num - 1;
        if (index >= _inventoryRegistry.inventory.Count)
            return;

        var selectedPack = _inventoryRegistry.inventory[index];

        // if no item is currently active, activate and subscribe
        if (_inventoryRegistry.activeItem.Value == null) {
            _inventoryRegistry.activeItem.Value = selectedPack.ItemContext;

            hotbarSyncSubscription?.Dispose();

            hotbarSyncSubscription = selectedPack.Count
                .Where(count => count <= 0)
                .Subscribe(_ => _inventoryRegistry.activeItem.Value = null);
            return;
        }

        // if there is an active item but different selected item
        // deactive and activate the new one
        if (_inventoryRegistry.activeItem.Value != null &&
            _inventoryRegistry.activeItem.Value != selectedPack.ItemContext) {
                
            _inventoryRegistry.activeItem.Value = selectedPack.ItemContext;

            hotbarSyncSubscription?.Dispose();

            hotbarSyncSubscription = selectedPack.Count
                .Where(count => count <= 0)
                .Subscribe(_ => _inventoryRegistry.activeItem.Value = null);

            return;
        }

        // if an item is already active deactivate it

        _inventoryRegistry.activeItem.Value = null;
        hotbarSyncSubscription?.Dispose();
    }
    public void Dispose() {
        _bag.Dispose();
    }
}