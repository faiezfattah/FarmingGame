using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Data")]
public class ItemData : ScriptableObject, IContextFactory<ItemContext> {
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int sellPrice;

    public ItemContext CreateContext(Action<ItemContext> onPickup) => new(onPickup) {
        Sprite = itemSprite,
        Price = sellPrice,
    };
}
}