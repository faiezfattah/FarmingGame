using System;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class ToolButton : Button {
    private Sprite _itemSprite;
    public ToolContext toolContext;
    VisualElement _icon = new();

    [UxmlAttribute]
    public Sprite itemSprite {
        get => _itemSprite;
        set {
            _itemSprite = value;
            UpdateDisplaySprite();
        }
    }

    private ItemContext contextData;
    public ToolButton() {
        // text = _toolName;
        this.text = "";

        _icon.style.width = 32;
        _icon.style.height = 32;
        Add(_icon);

        AddToClassList("tool-button");
    }

    private void UpdateDisplaySprite() {
        _icon.style.backgroundImage = new StyleBackground(_itemSprite);
    }
    public ToolButton SetToolContext(ToolContext context) {
        if (context == null) {
            style.opacity = 0;
            return this;
        }

        toolContext = context;
        itemSprite = context.BaseData.itemSprite;

        return this;
    }
}