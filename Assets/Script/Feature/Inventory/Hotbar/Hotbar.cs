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
    private DisposableBag _bag = new();
    private ItemDisplay activeDisplay;
    private InventoryRegistry _inventoryRegistry;
    [Inject] public void Construct(InventoryRegistry inventoryRegistry) {
        inventoryRegistry.ReadonlyRegistry.ObserveChanged().Subscribe(_ => Refresh(inventoryRegistry.registry)).AddTo(ref _bag);
        inventoryRegistry.activeItem.Subscribe(x => HandleSelect(x)).AddTo(ref _bag);
    }
    private void Start() {
        _container = _hotbarDocument.rootVisualElement.Q<VisualElement>("container");
        _container.Clear();
        for (int i = 0; i < 5; i++) {
            var slot = new ItemDisplay();
            slot.AddToClassList("hotbaritem");
            slot.name = "empty";
            _slots.Add(slot);
            _container.Add(slot);
        }
    }
    private void HandleSelect(ItemContext context) {
        if (activeDisplay != null) {
            activeDisplay.RemoveFromClassList("active-tool");
        }
        activeDisplay = _slots.Where(x => x.contextData == context).FirstOrDefault();
        activeDisplay.AddToClassList("active-tool");
    }
    private void Refresh(IReadOnlyObservableList<PackedItemContext> readonlyRegistry) {
        Debug.Log("refreshing");

        for (var i = 0; i < Mathf.Min(readonlyRegistry.Count, 5); i++) {
            var item = readonlyRegistry[i];
            _slots[i].itemSprite = item.ItemContext.BaseData.itemSprite;
            _slots[i].itemCount = readonlyRegistry.Count(x => x.ItemContext.BaseData.name == item.ItemContext.BaseData.name);
            _slots[i].contextData = item.ItemContext;   
        }
    }
    private void OnDisable() {
        _bag.Dispose();
    }
}