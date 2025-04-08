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
    [Inject] public void Construct (IInventoryRegistry inventoryRegistry) {
        inventoryRegistry.ReadonlyRegistry.ObserveAdd().Subscribe(item => HandleAdd(item.Value)).AddTo(ref _bag);
        inventoryRegistry.ReadonlyRegistry.ObserveRemove().Subscribe(item => HandleRemove(item.Value)).AddTo(ref _bag);
    }
    public void HandleAdd(ItemContext item) {
        Debug.Log($"added {item.BaseData.name}");
    }
    public void HandleRemove(ItemContext item) {
        Debug.Log($"removed {item.BaseData.name}");
    }
    public void OnDisable() {
        _bag.Dispose();
    }
}