using Script.Core.Base;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Feature.Item {
public class ItemSystem {
    private ItemPool _pool;
    public ItemSystem(ItemPool pool) {
        _pool = pool;
    }

    public void SpawnItem(Vector3 position) {
        var item = _pool.Get();
        item.transform.position = position;
        item.Initialize(new ItemContext());
    }
}
}