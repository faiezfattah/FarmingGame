using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ItemDisplay : VisualElement {
    
    private Sprite _itemSprite;
    private int _itemCount;
    private Label _countLabel;
    private string _itemName;

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
        AddToClassList("hotbaritem");
        
         _countLabel = new Label();
        _countLabel.AddToClassList("item-count");
        _countLabel.style.position = Position.Absolute;
        _countLabel.style.bottom = 0;
        _countLabel.style.right = 0;
        
        Add(_countLabel);
        
        UpdateCountLabel();
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
}