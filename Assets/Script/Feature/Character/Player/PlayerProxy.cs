using System;
using System.Linq;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
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
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SpriteRenderer sr;
    
    private float _currentRotation;
    private Vector3 _moveDir = Vector3.zero;
    private DisposableBag _bag;
    
    private IItemSystem _itemSystem;
    [Inject]
    public void Construct(InputProcessor inputProcessor, IItemSystem itemSystem) {

        inputProcessor.MoveEvent.Subscribe(UpdateMoveDir).AddTo(ref _bag);
        inputProcessor.InteractEvent.Subscribe(_ => Interact()).AddTo(ref _bag);

        _itemSystem = itemSystem;
    }

    private void UpdateMoveDir(Vector2 val) {
        _moveDir.x = val.x;
        _moveDir.z = val.y;
    }

    private void FixedUpdate() {
        if (_moveDir == Vector3.zero) return;
        gameObject.transform.position += (Vector3) _moveDir * (Time.fixedDeltaTime * speed);

        var angle = _moveDir.x > 0 ? 0f : 180f;
        
        rotatingContainer.rotation = Quaternion.Euler(0, angle, 0);
        sr.flipX = _moveDir.x < 0;
    }
    private void Interact() {
        var hit = Physics.OverlapSphere(gameObject.transform.position, range, interactableLayer);
        if (hit.Count() == 0) return;

        foreach (var h in hit) {
            if (h.TryGetComponent<IInteractable>(out var interactable)) {
                Debug.Log("interaction started");
                interactable.Interact();
                return;
            }
        }
    }

    private void OnDisable() {
        _bag.Dispose();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
        Gizmos.DrawWireSphere(pointer.position, range);
    }
}
}