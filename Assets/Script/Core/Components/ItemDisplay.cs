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
    public ItemContext itemContext;

    // events will not fire if there no item context
    private Subject<ItemDisplay> _pointerEnter = new();
    public Observable<ItemDisplay> PointerEnter => _pointerEnter;
    private Subject<ItemDisplay> _pointerExit = new();
    public Observable<ItemDisplay> PointerExit => _pointerExit;

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
    public ItemDisplay() {
        AddToClassList("item--display");
        
         _countLabel = new Label();
        _countLabel.AddToClassList("item--count-label");
        // _countLabel.style.position = Position.Absolute;
        // _countLabel.style.bottom = 0;
        // _countLabel.style.right = 0;

        
        
        Add(_countLabel);
        UpdateCountLabel();
    }
    public ItemDisplay SetContextBinding(PackedItemContext packedItemContext) {
        itemSprite = packedItemContext.ItemContext.BaseData.itemSprite;
        itemContext = packedItemContext.ItemContext;
        subscription = packedItemContext.Count.Subscribe(x => itemCount = x);

        RegisterCallback<PointerEnterEvent>(_ => _pointerEnter.OnNext(this));
        RegisterCallback<PointerLeaveEvent>(_ => _pointerExit.OnNext(this));


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
        _pointerEnter.Dispose();
    }
}