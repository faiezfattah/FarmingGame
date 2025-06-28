using Script.Core.Model.Item;

public interface IInventorySystem {
    public void AddItem(ItemData item, int amount = 1);
    public void AddItem(PackedItemContext packedItem, int amount = 1);
    // public void AddItem(ItemContext item, int amount = 1);
    public void RemoveItem(ItemData item, int amount = 1);
}