using System;
using System.Linq;
using OneiricFarming.Core.Utils.EventBinding;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Reflex.Attributes;

namespace Script.Feature.Character.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private LayerMask interactableLayer;

        [SerializeField] private Transform rotatingContainer;
        [SerializeField] private Transform pointer;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private InputActionReference moveAction;

        private float _currentSpeed;
        private Vector3 _moveDir = Vector3.zero;
        private IDisposable _bag;

        private IItemSystem _itemSystem;
        [Inject]
        public void Construct(IItemSystem itemSystem) {
            _itemSystem = itemSystem;
        }

        private void UpdateMoveDir(InputAction.CallbackContext ctx) {
            if (ctx.performed) {
                var val = ctx.ReadValue<Vector2>();
                _moveDir = new Vector3(val.x, 0, val.y).normalized;
            }
            else _moveDir = Vector3.zero;
            
        }
        private void Start() {
            EnableMovement();

            _bag = new EventBinding(
                () => moveAction.action.performed += UpdateMoveDir,
                () => moveAction.action.performed -= UpdateMoveDir
            );
        }
        private void FixedUpdate() {
            if (_moveDir == Vector3.zero) return;
            gameObject.transform.position += (Vector3)_moveDir * (Time.fixedDeltaTime * _currentSpeed);

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
        public void DisableMovement() {
            _currentSpeed = 0;
        }
        public void EnableMovement() {
            _currentSpeed = speed;
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