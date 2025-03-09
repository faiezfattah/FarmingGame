using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Data")]
public class ItemData : ScriptableObject {
    public Sprite itemSprite;
    public int sellPrice;

    public ItemContext CreateContext() => new(this);
}
}