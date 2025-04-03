using Script.Core.Model.Item;

public class InventorySystem : IInventorySystem {
    private InventoryRegistry _inventoryRegistry;
    public InventorySystem(InventoryRegistry inventoryRegistry) {
        _inventoryRegistry = inventoryRegistry;
    }

    public void AddItem(ItemContext item) {
        _inventoryRegistry.registry.Add(item);
    }

    public void RemoveItem(ItemContext item) {
        _inventoryRegistry.registry.Remove(item);
    }
}