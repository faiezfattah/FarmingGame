using System.Collections.Generic;
using ObservableCollections;
using Script.Core.Model.Item;

namespace Script.Feature.Item {
public class ItemRegistry {
    public ObservableList<ItemContext> ItemsList = new();
}
}