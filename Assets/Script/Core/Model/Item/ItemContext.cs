using System;
using UnityEngine;

namespace Script.Core.Model.Item {
public class ItemContext { // wrapper for registry
    public ItemData BaseData;
}

public class ItemContext<TData> : ItemContext where TData : ItemData {
    public TData data;
    public ItemContext(TData itemData) : base() {
        BaseData = itemData;
    }
}
}