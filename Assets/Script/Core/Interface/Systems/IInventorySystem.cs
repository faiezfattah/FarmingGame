using Script.Core.Model.Item;

public interface IInventorySystem {
    public void AddItem(ItemContext item);
    public void RemoveItem(ItemContext item);
}