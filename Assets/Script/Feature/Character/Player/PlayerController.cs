using System.Linq;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Feature.Input;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.Player {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private LayerMask interactableLayer;

        [SerializeField] private Transform rotatingContainer;
        [SerializeField] private Transform pointer;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private SpriteRenderer sr;

        private float _currentSpeed;
        private Vector3 _moveDir = Vector3.zero;

        public void UpdateMoveDir(Vector2 val) {
            _moveDir.x = val.x;
            _moveDir.z = val.y;
        }
        public void DisableMovement() {
            _currentSpeed = 0;
        }
        public void EnableMovement() {
            _currentSpeed = speed;
        }
        public void Interact() {
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
        void Start() {
            EnableMovement();
        }
        void FixedUpdate() {
            if (_moveDir == Vector3.zero) return;
            gameObject.transform.position += (Vector3)_moveDir * (Time.fixedDeltaTime * _currentSpeed);

            var angle = _moveDir.x > 0 ? 0f : 180f;

            rotatingContainer.rotation = Quaternion.Euler(0, angle, 0);
            sr.flipX = _moveDir.x < 0;
        }
        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(gameObject.transform.position, range);
            Gizmos.DrawWireSphere(pointer.position, range);
        }
    }
}