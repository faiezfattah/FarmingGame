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
        inventoryRegistry.ReadonlyRegistry.ObserveChanged().Subscribe(_ => Refresh(inventoryRegistry.ReadonlyRegistry)).AddTo(ref _bag);
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
    private void Refresh(IReadOnlyObservableList<ItemContext> readonlyRegistry) {
        Debug.Log("refreshing");

        var item = readonlyRegistry[0];

        _slots[0].itemSprite = item.BaseData.itemSprite;
        _slots[0].itemCount = readonlyRegistry.Count(x => x.BaseData == item.BaseData);
        _slots[0].contextData = item;
    }
    private void OnDisable() {
        _bag.Dispose();
    }
}