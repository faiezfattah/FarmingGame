using System.Collections.Generic;
using ObservableCollections;
using R3.Collections;
using Script.Core.Base;
using Script.Core.Model.Item;

public class InventoryRegistry {
    public ObservableList<ItemContext> Inventory = new();

    
    // public IEnumerable<ItemContext> GetAll() {
    //     throw new System.NotImplementedException();
    // }

    // public bool TryAdd(ItemContext value) {
    //     throw new System.NotImplementedException();
    // }

    // public ItemContext TryGet(string id) {
    //     throw new System.NotImplementedException();
    // }

    // public bool TryRemove(ItemContext value) {
    //     throw new System.NotImplementedException();
    // }
}