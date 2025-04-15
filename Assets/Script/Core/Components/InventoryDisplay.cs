using System;
using System.Collections.Generic;
using ObservableCollections;
using Script.Core.Model.Item;
using UnityEngine.UIElements;
using R3;
using UnityEngine;
using System.Linq;

[UxmlElement]
public partial class InventoryDisplay : VisualElement, IDisposable {
    private HashSet<ItemDisplay> _itemDisplay = new();
    private ISynchronizedView<PackedItemContext, PackedItemContext> _inventoryView;
    private int _rowCount = 4;
    private int _tilePerRow = 5;
    private DisposableBag _bag = new();
    public ItemDisplay CurrentHover { private set; get; }
    public ItemDisplay CurrentSelected { private set; get; }
    [UxmlAttribute]
    public int RowCount { 
        get => _rowCount;
        set {
            _rowCount = value;
        }
    }
    
    [UxmlAttribute]
    public int TilePerRow { 
        get => _tilePerRow;
        set {
            _tilePerRow = value;
        }
    }
    public InventoryDisplay() {
        AddToClassList("inventory--container");
    }
    public InventoryDisplay SetInventoryBinding(IInventoryRegistry inventoryRegistry) {
        _inventoryView = inventoryRegistry.ReadonlyRegistry.CreateView(x => x);
        _inventoryView.ObserveChanged().Subscribe(_ => UpdateTile()).AddTo(ref _bag);
        UpdateTile();
        return this;
    }
    public InventoryDisplay SetRowCount(int value) {
        RowCount = value;
        return this;
    }
    public InventoryDisplay SetTilePerRow(int value = 5) {
        TilePerRow = value;
        return this;
    }
    private void UpdateTile() {
        Debug.Log("refreshed!");
        ResetGrid();

        if (_inventoryView == null) return;
        FillGrid();

    }
    private void ResetGrid() {
        Clear();
        _itemDisplay.Clear();

        for (int i = 0; i < _rowCount; i++) {
            var container = new VisualElement();
            container.AddToClassList("item--group");
            Add(container);

            for (int j = 0; j < _tilePerRow; j++) {
                var item = new ItemDisplay();

                item.PointerEnter.Subscribe(HandleHoverEnter).AddTo(ref _bag);
                item.PointerExit.Subscribe(HandleHoverExit).AddTo(ref _bag);

                _itemDisplay.Add(item);
                container.Add(item);
            }
        }
    }
    private void HandleHoverEnter(ItemDisplay display) {
        CurrentHover = display;
        CurrentHover.AddToClassList("item--active");
    }
    
    private void HandleHoverExit(ItemDisplay display) {
        CurrentHover.RemoveFromClassList("item--active");
        CurrentHover = CurrentHover == display ? null : CurrentHover;
    }
    private void FillGrid() {
        foreach(var item in _inventoryView) {
            foreach(var display in _itemDisplay) {
                if (display.itemContext == null) {
                    display.SetContextBinding(item);
                    break;
                }
            }
        }
    }
    public void Dispose() {
        _inventoryView.Dispose();
        _itemDisplay.Clear();
        _bag.Dispose();
    }
}
