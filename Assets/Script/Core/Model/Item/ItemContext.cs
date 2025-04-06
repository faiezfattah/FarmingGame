using System;
using UnityEngine;

namespace Script.Core.Model.Item {
public class ItemContext { // wrapper for registry
    public ItemData ItemData;
}

public class ItemContext<TData> : ItemContext where TData : ItemData {
    public ItemContext(TData itemData) : base() {
        ItemData =  itemData;
    }
}
}