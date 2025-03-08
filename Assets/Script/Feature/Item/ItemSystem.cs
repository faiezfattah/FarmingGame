using Script.Core.Interface;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class ItemSystem : IItemSystem {
    private ItemPool _pool;
    public ItemSystem(ItemPool pool) {
        _pool = pool;
    }

    public void SpawnItem(ItemData itemData, Vector3 position) {
        var item = _pool.Get();
        item.transform.position = position;
        item.Initialize(itemData.Create());
    }
}
}