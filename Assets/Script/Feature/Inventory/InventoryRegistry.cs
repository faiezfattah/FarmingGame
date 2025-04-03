using ObservableCollections;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry {
    public ObservableList<ItemContext> registry = new();

    public IReadOnlyObservableList<ItemContext> ReadonlyRegistry => registry;
}