using ObservableCollections;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry {
    // inventory
    public ObservableList<PackedItemContext> inventory = new();
    IReadOnlyObservableList<PackedItemContext> IInventoryRegistry.InventoryRegistry => inventory;

    // toolbar
    public ObservableList<ToolContext> toolbarRegistry = new();
    public IReadOnlyObservableList<ToolContext> ToolRegistry => throw new System.NotImplementedException();

    // active item
    public ReactiveProperty<ItemContext> activeItem = new();
    public Observable<ItemContext> ActiveItem => activeItem;
}

