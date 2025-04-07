using ObservableCollections;
using R3;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry, IToolbarRegistry {
    public ObservableList<ItemContext> registry = new();
    public IReadOnlyObservableList<ItemContext> ReadonlyRegistry => registry;
    public  ReactiveProperty<ItemContext> activeItem = new();

    public ObservableList<ToolContext> toolbarRegistry = new(){};
    public ReactiveProperty<ToolContext> activeTool = new();
    public ToolContext tool => activeTool.Value;
}