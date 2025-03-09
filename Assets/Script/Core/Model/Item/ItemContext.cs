using System;
using UnityEngine;

namespace Script.Core.Model.Item {
public class ItemContext {
    public ItemData ItemData;

    public ItemContext(ItemData itemData) {
        ItemData = itemData;
    }
}
}