using System;
using Script.Core;
using Script.Core.Interface;
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

    private float _currentRotation;
    private InputProcessor _inputProcessor;
    private Vector2 _moveDir;
    private IDisposable _subscription;

    [Inject]
    public void Construct(InputProcessor inputProcessor) {
        _inputProcessor = inputProcessor;
        _inputProcessor.MoveEvent += UpdateMoveDir;
        _inputProcessor.InteractEvent += Interact;
    }

    private void UpdateMoveDir(Vector2 val) => _moveDir = val;

    private void FixedUpdate() {
        if (_moveDir == Vector2.zero) return;
        gameObject.transform.position += (Vector3)_moveDir * (Time.fixedDeltaTime * speed);
        
        // rotate le container
        if (_moveDir.x != 0 || _moveDir.y != 0) {
            float angle = 0f;
        
            // Primary directions - more efficient than Atan2
            if (Mathf.Abs(_moveDir.x) > Mathf.Abs(_moveDir.y)) {
                // Horizontal movement is dominant
                angle = _moveDir.x > 0 ? 0f : 180f;
            } else {
                // Vertical movement is dominant
                angle = _moveDir.y > 0 ? 90f : 270f;
            }
        
            rotatingContainer.rotation = Quaternion.Euler(0, 0, angle);
        }
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
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
        Gizmos.DrawWireSphere(pointer.position, range);
    }
}
}