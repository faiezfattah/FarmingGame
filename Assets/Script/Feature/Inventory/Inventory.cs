using UnityEngine;
using Reflex.Attributes;
using R3;
using UnityEngine.UIElements;
using Script.Feature.Input;
using System;
using ObservableCollections;
using System.Collections.Generic;
using Script.Core.Model.Item;
using TriInspector;
using System.Linq;
public class Inventory : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    private InventoryRegistry _inventoryRegistry;
    private IDisposable _subscription;
    private DisposableBag _bag = new();
    private ISynchronizedView<KeyValuePair<int, PackedItemContext>, KeyValuePair<int, PackedItemContext>> view;

    [Inject]
    public void Construct(InventoryRegistry inventoryRegistry, InputProcessor inputProcessor) {
        _inventoryRegistry = inventoryRegistry;
        _subscription = inputProcessor.InventoryEvent.Subscribe(_ => ToggleInventory());
        inventoryRegistry.inventory2.ObserveChanged().Subscribe(HandleChange).AddTo(ref _bag);
        view = _inventoryRegistry.inventory2
            .CreateView((val) => val)
            .AddTo(ref _bag);
        view.AttachFilter(
                (a, b) => true
            );

        // first 5
        var first5ItemsView = _inventoryRegistry.inventory2
            .CreateView(
                kvp => {
                    return kvp.Key < 5;
                }
            )
            .AddTo(ref _bag);

        view = _inventoryRegistry.inventory2
            .CreateView(kvp => kvp)
            .AddTo(ref _bag);
    }
    private void HandleChange(CollectionChangedEvent<KeyValuePair<int, PackedItemContext>> _) {
        foreach (var (i, value) in view.OrderBy(kvp => kvp.Key)) {
            Debug.Log($"{i} : {value.ItemContext.BaseData.name} {value.Count}");
        }   
    }
    [Button]
    private void AddToInventory2(ItemData itemData, int count, int index) {
        _inventoryRegistry.inventory2.Add(index, new PackedItemContext(itemData.CreateBaseContext(), count));
    }
    private void Start() {
        uIDocument.enabled = false;
    }
    private void ToggleInventory() {
        uIDocument.enabled = !uIDocument.enabled;

        if (!uIDocument.enabled) return;

        var root = uIDocument.rootVisualElement;
        var inventory = root.Query<InventoryDisplay>().First();
        inventory.SetInventoryBinding(_inventoryRegistry);                  
    }
    private void OnDisable() {
        _subscription?.Dispose();
    }
}