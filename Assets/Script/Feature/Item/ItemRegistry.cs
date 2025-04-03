using System.Collections.Generic;
using ObservableCollections;
using Script.Core.Model.Item;

namespace Script.Feature.Item {
public class ItemRegistry : IItemRegistry {
    public ObservableList<ItemContext> Registry = new();
    public IReadOnlyObservableList<ItemContext> ReadonlyRegistry { get => Registry; }
}
}