using System;
using Script.Core;
using Script.Core.Interface;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.Player {
public class PlayerProxy : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private LayerMask interactableLayer;
    
    [SerializeField] private Transform rotatingContainer;
    [SerializeField] private Transform pointer;

    [SerializeField] private ItemData itemData;
    

    private float _currentRotation;
    private InputProcessor _inputProcessor;
    private Vector2 _moveDir;
    private IDisposable _subscription;
    
    private IItemSystem _itemSystem;
    [Inject]
    public void Construct(InputProcessor inputProcessor, IItemSystem itemSystem) {
        _inputProcessor = inputProcessor;
        _inputProcessor.MoveEvent += UpdateMoveDir;
        _inputProcessor.InteractEvent += Interact;
        _inputProcessor.DebugEvent += HandleDebug;
        
        _itemSystem = itemSystem;
    }

    private void UpdateMoveDir(Vector2 val) => _moveDir = val;

    private void FixedUpdate() {
        if (_moveDir == Vector2.zero) return;
        gameObject.transform.position += (Vector3)_moveDir * (Time.fixedDeltaTime * speed);

        if (_moveDir.x == 0 && _moveDir.y == 0) return;
        var angle = 0f;
        if (Mathf.Abs(_moveDir.x) > Mathf.Abs(_moveDir.y)) {
            angle = _moveDir.x > 0 ? 0f : 180f;
        } else {
            angle = _moveDir.y > 0 ? 90f : 270f;
        }
        
        rotatingContainer.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void HandleDebug() {
        _itemSystem.SpawnItem(itemData, transform.position);
    }
    private void Interact() {
        var hit = Physics2D.OverlapCircle(gameObject.transform.position, range, interactableLayer);
        if (!hit) return;

        if (hit.TryGetComponent<IInteractable>(out var interactable)) {
            interactable.Interact();
        }
    }

    private void OnDisable() {
        _subscription?.Dispose();
        _inputProcessor.MoveEvent -= UpdateMoveDir;
        _inputProcessor.InteractEvent -= Interact;
        _inputProcessor.DebugEvent -= HandleDebug;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
        Gizmos.DrawWireSphere(pointer.position, range);
    }
}
}