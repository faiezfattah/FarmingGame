using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Item {
// [CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Data")]
public abstract class ItemData : ScriptableObject {
    // public abstract ItemContext CreateContext();
    public Sprite itemSprite;
    public int MaxStackable = 64;
    public abstract ItemContext CreateBaseContext();
}

public abstract class ItemData<TContext> : ItemData where TContext : ItemContext   {
    public override ItemContext CreateBaseContext() => CreateContext();
    public abstract TContext CreateContext();
}
}