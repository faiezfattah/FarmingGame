using System;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;
using R3;

[UxmlElement]
public partial class ItemDisplay : VisualElement, IDisposable {
    
    private Sprite _itemSprite;
    private int _itemCount;
    private Label _countLabel;
    private string _itemName;
    private IDisposable subscription;

    [UxmlAttribute]
    public Sprite itemSprite { 
        get => _itemSprite;
        set {
            _itemSprite = value;
            UpdateDisplaySprite();
        }
    }

    [UxmlAttribute]
    public int itemCount {
        get => _itemCount;
        set {
            _itemCount = value;
            UpdateCountLabel();
        }
    }

    [UxmlAttribute] 
    public string itemName {
        get => _itemName;
        set {
            _itemName = value;
        }
    }
    public ItemContext contextData;
    public ItemDisplay() {
        AddToClassList("item-display");
        
         _countLabel = new Label();
        _countLabel.AddToClassList("item-count");
        _countLabel.style.position = Position.Absolute;
        _countLabel.style.bottom = 0;
        _countLabel.style.right = 0;
        
        Add(_countLabel);
        
        UpdateCountLabel();
    }
    public ItemDisplay SetContextBinding(PackedItemContext packedItemContext) {
        itemSprite = packedItemContext.ItemContext.BaseData.itemSprite;
        // itemCount = packedItemContext.Count.Value;
        contextData = packedItemContext.ItemContext;
        subscription = packedItemContext.Count.Subscribe(x => itemCount = x);
        return this;
    }
    private void UpdateCountLabel() {
        if (_itemCount <= 1) {
            _countLabel.style.display = DisplayStyle.None;
        }
        else {
            _countLabel.style.display = DisplayStyle.Flex;
            _countLabel.text = _itemCount.ToString();
        }
    }
    private void UpdateDisplaySprite() {
        if (_itemSprite != null) {
            style.backgroundImage = new StyleBackground(_itemSprite);
        } else {
            style.backgroundImage = new StyleBackground();
        }
    }

    public void Dispose() {
        subscription.Dispose();
    }
}