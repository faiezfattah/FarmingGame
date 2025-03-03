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