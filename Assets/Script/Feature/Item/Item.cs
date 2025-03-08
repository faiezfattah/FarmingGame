using Script.Core;
using UnityEngine;

namespace Script.Feature.Item {
public class Item : MonoBehaviour, IEntity<ItemContext> {
    private ItemContext _itemContext;
    
    public void Initialize(ItemContext context) {
        _itemContext = context;
    }
}
}