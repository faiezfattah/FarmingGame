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
    private IEnumerable<PackedItemContext> hotbarView;
    [Inject] public void Construct(InventoryRegistry inventoryRegistry) {
        inventoryRegistry.ReadonlyRegistry.ObserveAdd().Subscribe(_ => Refresh(inventoryRegistry.registry)).AddTo(ref _bag); 
        inventoryRegistry.ReadonlyRegistry.ObserveRemove().Subscribe(_ => Refresh(inventoryRegistry.registry)).AddTo(ref _bag);
        inventoryRegistry.activeItem.Subscribe(x => HandleSelect(x)).AddTo(ref _bag);
    }
    private void Start() {
        _container = _hotbarDocument.rootVisualElement.Q<VisualElement>("container");
        _container.Clear();
        for (int i = 0; i < 5; i++) {
            var slot = new ItemDisplay();
            _slots.Add(slot);
            _container.Add(slot);
        }
    }  
    private void ResetHotbar() {
        _slots.ForEach(item => {
            item.itemSprite = null;
            item.itemCount = 0;
            item.itemContext = null;
            item.RemoveFromClassList("item--active");
        });
    }
    private void HandleSelect(ItemContext context) {
        if (context == null) { // basically un equiping
            if (activeDisplay != null) {
                activeDisplay.RemoveFromClassList("item--active");
            }

            if (activeDisplay == null) {}
            return;
        }
        if (context != null) { // switching the active item
            if (activeDisplay != null) {
                activeDisplay.RemoveFromClassList("item--active");
                
                activeDisplay = _slots.Where(x => x.itemContext == context).First();
                activeDisplay.AddToClassList("item--active");
            }

            if (activeDisplay == null) {
                activeDisplay = _slots.Where(x => x.itemContext == context).First();
                activeDisplay.AddToClassList("item--active");
            }
        }
    }
    private void Refresh(IReadOnlyObservableList<PackedItemContext> readonlyRegistry) {
        ResetHotbar();
        
        for (var i = 0; i < Mathf.Min(readonlyRegistry.Count, 5); i++) {
            var item = readonlyRegistry[i];
            // _slots[i].itemSprite = item.ItemContext.BaseData.itemSprite;
            // _slots[i].itemCount = item.Count.Value;
            // _slots[i].contextData = item.ItemContext;   
            _slots[i].SetContextBinding(item);
        }
    }
    private void OnDisable() {
        _bag.Dispose();
    }
}