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
    public static class Event {
        internal static Subject<ItemDisplay> onPointerEnter = new();
        internal static Subject<ItemDisplay> onPointerExit = new();
        internal static Subject<ItemDisplay> onClick = new();
        public static Observable<ItemDisplay> OnPointerEnter => onPointerEnter;
        public static Observable<ItemDisplay> OnPointerExit => onPointerExit;
        public static Observable<ItemDisplay> OnClick => onClick;
    }

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
    public ItemDisplay Reset() {
        _itemCount = 0;
        _itemSprite = null;
        _itemName = null;

        return this;
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

        RegisterCallback<PointerEnterEvent>(_ => Event.onPointerEnter.OnNext(this));
        RegisterCallback<PointerLeaveEvent>(_ => Event.onPointerExit.OnNext(this));
        RegisterCallback<ClickEvent>(_ => Event.onClick.OnNext(this));

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