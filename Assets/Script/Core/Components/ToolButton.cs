using System;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement] 
public partial class ToolButton : Button {
    private Sprite _itemSprite;
    private string _toolName;

    [UxmlAttribute]
    public Sprite itemSprite { 
        get => _itemSprite;
        set {
            _itemSprite = value;
            UpdateDisplaySprite();
        }
    }

    [UxmlAttribute] 
    public string toolName {
        get => _toolName;
        set {
            _toolName = value;
            UpdateDisplayText();
        }
    }
    private ItemContext contextData;
    public ToolButton() {
        text = _toolName;
    }
    public void SetData(ItemContext context) {
        contextData = context;
    }
    
    private void UpdateDisplaySprite() {
        if (_itemSprite != null) {
            style.backgroundImage = new StyleBackground(_itemSprite);
        } else {
            style.backgroundImage = new StyleBackground();
        }
    }
    private void UpdateDisplayText() {
        text = _toolName;
    }
}