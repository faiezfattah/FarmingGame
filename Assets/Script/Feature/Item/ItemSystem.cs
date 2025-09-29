using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using UnityEngine;
namespace Script.Feature.Item {
public class ItemSystem : IItemSystem {
    private ItemPool _pool;
    private ItemRegistry _itemRegistry;
    private IInventorySystem _inventorySystem;
    private ItemContextFactory _factory;
    public ItemSystem(
        ItemPool pool, 
        ItemRegistry itemRegistry, 
        IInventorySystem inventorySystem, 
        ItemContextFactory factory) {
        _pool = pool;
        _itemRegistry = itemRegistry;
        _inventorySystem = inventorySystem;
        _factory = factory;
    }
    public void SpawnItem(ItemData itemData, Vector3 position) {
        var item = _pool.Get();
        item.transform.position = position;

        var context = _factory.Create(itemData);

        _itemRegistry.Registry.Add(context);
        
        item.Initialize(context, () => {
            HandlePickup(context);
            _pool.Release(item);
        });
    }

    private void HandlePickup(ItemContext itemContext) {
        _itemRegistry.Registry.Remove(itemContext);
        _inventorySystem.AddItem(new PackedItemContext(itemContext, 1)); // Todo: change later to handle entity cramming
    }
}
}