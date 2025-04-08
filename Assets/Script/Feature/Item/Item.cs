using System;
using Script.Core;
using Script.Core.Interface;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Feature.Item {
public class Item : MonoBehaviour, IEntity<ItemContext> {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SphereCollider sphereRollider;
    
    [SerializeField] private float itemPickupRadius = 1;

    private ItemContext _itemContext;
    private Action _onPickup;
    
    public void Initialize(ItemContext context, Action onPickup) {
        _itemContext = context;
        _onPickup = onPickup;
        
        spriteRenderer.sprite = context.BaseData.itemSprite;
        sphereRollider.radius = itemPickupRadius;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _onPickup?.Invoke();
        }
    }

        private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, itemPickupRadius);
    }
}
}