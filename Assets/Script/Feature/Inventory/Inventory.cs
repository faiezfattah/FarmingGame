using UnityEngine;
using VContainer;
using R3;
using R3.Collections;
using ObservableCollections;
using System;
using Script.Core.Model.Item;
using System.Collections.Generic;
public class Inventory : MonoBehaviour {
    // private InventoryRegistry _inventoryRegistry;
    private DisposableBag _bag = new();
    [Inject] public void Construct (InventoryRegistry inventoryRegistry) {
        inventoryRegistry.Inventory.ObserveAdd().Subscribe(item => HandleAdd(item.Value)).AddTo(ref _bag);
        inventoryRegistry.Inventory.ObserveRemove().Subscribe(item => HandleRemove(item.Value)).AddTo(ref _bag);
        inventoryRegistry.Inventory.ForEach(item => {
            Debug.Log(item.ItemData.name);
        });
        inventoryRegistry.Inventory.ObserveChanged().Subscribe(_ => DebugItems(inventoryRegistry.Inventory)).AddTo(ref _bag);
    }
    private void DebugItems(ICollection<ItemContext> items) {
        // foreach (var item in items) {
        //     Debug.Log(item.ItemData.name);
        // }
    }
    public void HandleAdd(ItemContext item) {
        Debug.Log($"added {item.ItemData.name}");
    }
    public void HandleRemove(ItemContext item) {
        Debug.Log($"removed {item.ItemData.name}");
    }
    public void OnDisable() {
        _bag.Dispose();
    }
}