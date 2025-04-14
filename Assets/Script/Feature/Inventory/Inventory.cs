using UnityEngine;
using VContainer;
using R3;
using ObservableCollections;
using Script.Core.Model.Item;
using UnityEngine.UIElements;
public class Inventory : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    private IInventoryRegistry _inventoryRegistry;
    [Inject] public void Construct (IInventoryRegistry inventoryRegistry) {
        _inventoryRegistry = inventoryRegistry;
    }
    private void Start() {
        var root = uIDocument.rootVisualElement;
        var inventory = root.Query<InventoryDisplay>().First();
        inventory.SetInventoryBinding(_inventoryRegistry);
    }
}