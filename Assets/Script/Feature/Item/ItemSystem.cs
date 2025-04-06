using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class ItemSystem : IItemSystem {
    private ItemPool _pool;
    private ItemRegistry _itemRegistry;
    private IInventorySystem _inventorySystem;
    public ItemSystem(ItemPool pool, ItemRegistry itemRegistry, IInventorySystem inventorySystem) {
        _pool = pool;
        _itemRegistry = itemRegistry;
        _inventorySystem = inventorySystem;
    }

    public void SpawnItem(ItemData itemData, Vector3 position) {
        var item = _pool.Get();
        item.transform.position = position;
        Debug.Log($"item: {itemData.name}");

        var context = itemData.CreateBaseContext();

        _itemRegistry.Registry.Add(context);
        
        item.Initialize(context, () => {
            HandlePickup(context);
            _pool.Release(item);
        });
    }

    private void HandlePickup(ItemContext itemContext) {
        _itemRegistry.Registry.Remove(itemContext);
        _inventorySystem.AddItem(itemContext);
    }
}
}