using System.Linq;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.NPC {
public class NPCSelling : MonoBehaviour, IInteractable {
    private IMoneySystem _moneySystem;
    private InventoryRegistry _inventoryRegistry;
    private IInventorySystem _inventorySystem;
    [Inject] public void Construct(IMoneySystem moneySystem, InventoryRegistry inventoryRegistry, IInventorySystem inventorySystem) {
        _moneySystem = moneySystem;
        _inventoryRegistry = inventoryRegistry;
        _inventorySystem = inventorySystem;
    }
    public void Interact() {
        var inventory = _inventoryRegistry.ReadonlyRegistry.ToList();
        var price = 0;
        inventory.ForEach(x => {
            price += x.ItemContext.BaseData.price * x.Count.Value;
            _inventorySystem.RemoveItem(x.ItemContext, x.Count.Value);
        });
        _moneySystem.TryTransfer(price);
    }
}
}