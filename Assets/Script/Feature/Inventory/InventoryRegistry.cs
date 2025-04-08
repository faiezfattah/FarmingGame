using ObservableCollections;
using R3;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry, IToolbarRegistry {

    // actual inventory
    // public ObservableList<ItemContext> registry = new();
    public ObservableList<PackedItemContext> registry = new();
    public IReadOnlyObservableList<PackedItemContext> ReadonlyRegistry => registry;
    
    // hotbar located active item
    public  ReactiveProperty<ItemContext> activeItem = new();

    // toolbar
    public ObservableList<ToolContext> toolbarRegistry = new(){};
    public ReactiveProperty<ToolContext> activeTool = new();
    public ToolContext tool => activeTool.Value;
}