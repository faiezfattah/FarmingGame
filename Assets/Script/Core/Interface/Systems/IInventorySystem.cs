using Script.Core.Model.Item;

public interface IInventorySystem {
    public void AddItem(ItemContext item, int amount = 1);
    public void RemoveItem(ItemContext item, int amount = 1);
}