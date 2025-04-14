using System;
using System.Collections.Generic;
using ObservableCollections;
using Script.Core.Model.Item;
using UnityEngine.UIElements;
using R3;
using UnityEngine;

[UxmlElement]
public partial class InventoryDisplay : VisualElement, IDisposable {
    private HashSet<ItemDisplay> _itemDisplay = new();
    private ISynchronizedView<PackedItemContext, PackedItemContext> _inventoryView;
    private int _tileCount = 20;
    
    [UxmlAttribute]
    public int TileCount { 
        get => _tileCount;
        set {
            _tileCount = value;
            UpdateTile();
        }
    }
    private int _tilePerRow = 5;
    
    [UxmlAttribute]
    public int TilePerRow { 
        get => _tilePerRow;
        set {
            _tilePerRow = value;
            UpdateTile();
        }
    }
    public InventoryDisplay() {
        AddToClassList("inventory-container");
    }
    public InventoryDisplay SetInventoryBinding(IInventoryRegistry inventoryRegistry) {
        _inventoryView = inventoryRegistry.ReadonlyRegistry.CreateView(x => x);
        _inventoryView.ObserveChanged().Subscribe(_ => UpdateTile());
        return this;
    }
    public InventoryDisplay SetTileCount(int value) {
        TileCount = value;
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

        for (int i = 0; i < _tileCount / _tilePerRow; i++) {
            var container = new VisualElement();
            container.AddToClassList("item-group");
            Add(container);

            for (int j = 0; j < _tilePerRow; j++) {
                var item = new ItemDisplay();

                _itemDisplay.Add(item);
                container.Add(item);
            }
        }
    }
    private void FillGrid() {
        foreach(var item in _inventoryView) {
            foreach(var display in _itemDisplay) {
                if (display.contextData == null) {
                    display.SetContextBinding(item);
                    break;
                }
            }
        }
    }
    public void Dispose() {
        _inventoryView.Dispose();
        _itemDisplay.Clear();
    }
}
