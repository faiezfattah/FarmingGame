using Script.Core;
using Script.Registry.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class Item : MonoBehaviour, IEntity<ItemContext> {
    private ItemContext _itemContext;
    
    public void Initialize(ItemContext context) {
        _itemContext = context;
    }
}
}