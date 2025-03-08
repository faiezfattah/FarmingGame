using System;
using UnityEngine;

namespace Script.Core.Model.Item {
public class ItemContext {
    public ItemData ItemData;
    public Action OnPickup;

    public ItemContext(ItemData itemData, Action<ItemContext> onPickup) {
        ItemData = itemData;
        OnPickup = () => onPickup?.Invoke(this);
    }
}
}