using System;
using UnityEngine;

namespace Script.Core.Model.Item {
public class ItemContext {
    public Sprite Sprite;
    public int Price;

    public Action OnPickup;

    public ItemContext(Action<ItemContext> onPickup) {
        OnPickup = () => onPickup?.Invoke(this);
    }
}
}