using Script.Core.Model.Item;

namespace Script.Core.Interface.Systems {
public interface IItemContextFactory {
    public ItemContext Create(ItemData itemData);
    public TContext Create<TContext>(ItemData itemData) where TContext : ItemContext;
}
}