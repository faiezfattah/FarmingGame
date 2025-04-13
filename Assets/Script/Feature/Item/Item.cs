using System;
using Script.Core;
using Script.Core.Interface;
using Script.Core.Model.Item;
using UnityEngine;
using PrimeTween;

namespace Script.Feature.Item {
public class Item : MonoBehaviour, IEntity<ItemContext> {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SphereCollider sphereRollider;
    
    [SerializeField] private float itemPickupRadius = 1;
    private bool _animationEnd = false;
    private bool _hit = false;
    private ItemContext _itemContext;
    private Action _onPickup;
    
    public void Initialize(ItemContext context, Action onPickup) {
        _itemContext = context;
        _onPickup = onPickup;
        
        spriteRenderer.sprite = context.BaseData.itemSprite;
        sphereRollider.radius = itemPickupRadius;

        _animationEnd = false;
        _hit = false;

        Tween.PositionY(transform, endValue: 2.5f, duration: 0.25f)
             .OnComplete(OnAnimationEnd);
    }
    private void OnAnimationEnd() {
        _animationEnd = true;
        var ray = Physics.Raycast(transform.position, Vector3.down, out var hit);
        
        Tween.PositionY(transform, endValue: hit.point.y + itemPickupRadius/2, duration: 0.25f)
             .OnComplete(() => { if (_hit) _onPickup?.Invoke(); });
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _hit = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, itemPickupRadius);
    }
}
}