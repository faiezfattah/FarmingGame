using System;
using Script.Core;
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
        _currentRotation = _moveDir.y * 90f + (Mathf.Approximately(_moveDir.x, 1) ? 0f : 180f);
        rotatingContainer.rotation = Quaternion.Euler(0, 0, _currentRotation);
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
    }
}
}