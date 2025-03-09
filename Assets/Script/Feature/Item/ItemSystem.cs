using Script.Core.Interface;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class ItemSystem : IItemSystem {
    private ItemPool _pool;
    private ItemRegistry _itemRegistry;
    public ItemSystem(ItemPool pool, ItemRegistry itemRegistry) {
        _pool = pool;
        _itemRegistry = itemRegistry;
    }

    public void SpawnItem(ItemData itemData, Vector3 position) {
        var item = _pool.Get();
        item.transform.position = position;
        Debug.Log($"item: {itemData.name}");

        var context = itemData.CreateContext();
        
        _itemRegistry.ItemsList.Add(context);
        item.Initialize(context, () => {
            HandlePickup(context);
            _pool.Release(item);
        });
    }

    private void HandlePickup(ItemContext itemContext) {
        Debug.Log("picked");
        _itemRegistry.ItemsList.Remove(itemContext);
    }
}
}