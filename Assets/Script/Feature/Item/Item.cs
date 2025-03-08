using Script.Core;
using Script.Core.Interface;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class Item : MonoBehaviour, IEntity<ItemContext> {
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private ItemContext _itemContext;
    
    public void Initialize(ItemContext context) {
        _itemContext = context;
        spriteRenderer.sprite = context.Sprite;
    }
}
}