using System.Linq;
using ObservableCollections;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry {
    // inventory
    public ObservableList<PackedItemContext> inventory = new();
    IReadOnlyObservableList<PackedItemContext> IInventoryRegistry.InventoryRegistry => inventory;

    // new system
    public ObservableDictionary<int, PackedItemContext> inventory2 = new();
    public void Add(int index, PackedItemContext packedItem) {
        if (inventory2.TryGetValue(index, out var existing) &&
            existing.ItemContext.BaseData == packedItem.ItemContext.BaseData) {
            existing.Count.Value += packedItem.Count.Value;
            return;
        }
        
        inventory2[index] = packedItem;
        inventory2.OrderBy(kvp => kvp.Key);
        
    }
    public void Add(int index, ItemContext itemContext, int value = 1) {
        if (inventory2.TryGetValue(index, out var existing) &&
            existing.ItemContext.BaseData == itemContext.BaseData) {
            existing.Count.Value += value;
            return;
        }
        
        inventory2[index] = new PackedItemContext(itemContext.BaseData.CreateBaseContext(), value);
        inventory2.OrderBy(kvp => kvp.Key);
        
    }
    public void Add(int index, ItemData itemData, int value = 1) {
        if (inventory2.TryGetValue(index, out var existing) &&
            existing.ItemContext.BaseData == itemData) {
            existing.Count.Value += value;
            return;
        }
        
        inventory2[index] = new PackedItemContext(itemData.CreateBaseContext(), value);
        inventory2.OrderBy(kvp => kvp.Key);
        
    }
    
    // toolbar
    public ObservableList<ToolContext> toolbarRegistry = new();
    public IReadOnlyObservableList<ToolContext> ToolRegistry => throw new System.NotImplementedException();

    // active item
    public ReactiveProperty<ItemContext> activeItem = new();
    public Observable<ItemContext> ActiveItem => activeItem;
}

