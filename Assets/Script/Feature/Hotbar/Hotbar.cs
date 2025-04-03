using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class Hotbar : MonoBehaviour {
    [SerializeField] private UIDocument _hotbarDocument;
    private VisualElement _container;
    private List<ItemDisplay> _slots = new();
    private InventoryRegistry _inventory;
    private DisposableBag _bag = new();

    [Inject] public void Construct(InventoryRegistry inventoryRegistry) {
        inventoryRegistry.Inventory.ObserveChanged().Subscribe(_ => Refresh()).AddTo(ref _bag);
        _inventory = inventoryRegistry;
    }
    private void Start() {
        _container = _hotbarDocument.rootVisualElement.Q<VisualElement>("container");
        _container.Clear();
        for (int i = 0; i < 5; i++) {
            var slot = new ItemDisplay();
            slot.AddToClassList("hotbaritem");
            _slots.Add(slot);
            _container.Add(slot);
        }
    }
    public void Refresh() {
        Debug.Log("refreshing");

        var item = _inventory.Inventory[0];

        _slots[0].itemSprite = item.ItemData.itemSprite;
        _slots[0].itemCount = _inventory.Inventory.Count(x => x.ItemData == item.ItemData);
        
    }
    private void OnDisable() {
        _bag.Dispose();
    }
}