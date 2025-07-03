using System;
using Cysharp.Threading.Tasks;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VContainer;

namespace Script.Feature.Character.Player {
    public class PlayerPointer : MonoBehaviour {
        [SerializeField] private GameObject indicator;
        [SerializeField] private float range;
        private IActionable _currentSelection;
        private DisposableBag _bag;
        private InventoryRegistry _inventoryRegistry;
        private PlayerController _playerController;
        [Inject]
        public void Construct(InputProcessor inputProcessor, InventoryRegistry inventoryRegistry, PlayerController playerController) {
            inputProcessor.ActionEvent.Subscribe(_ => Action()).AddTo(ref _bag);
            IActionable.Event.OnPointerHovered.Subscribe(HandleEnter).AddTo(ref _bag);
            IActionable.Event.OnPointerExited.Subscribe(HandleExit).AddTo(ref _bag);
            _inventoryRegistry = inventoryRegistry;
            _playerController = playerController;
        }
        private void Action() {
            if (_currentSelection == null) return;
            if (_inventoryRegistry.activeItem.Value == null) return;

            if (_inventoryRegistry.activeItem.Value is IUseable useable) {
                _currentSelection.Action(useable);
            }
        }
        private bool Check(IActionable actionable) {
            var dist = Vector3.Distance(_playerController.transform.position, actionable.GetPointerPosition());

            return dist < range;
        }
        private void HandleEnter(IActionable actionable) {
            if (!Check(actionable)) return;
            
            if (_currentSelection == null) {
                _currentSelection = actionable;
                indicator.SetActive(true);
                indicator.transform.position = actionable.GetPointerPosition();
            }
            else {
                _currentSelection = actionable;
                indicator.transform.position = actionable.GetPointerPosition();
            }
        }
        private void HandleExit(IActionable actionable) {

            _currentSelection = null;
            indicator.SetActive(false);
        }
        private async UniTaskVoid HandleExitInternal(IActionable actionable) {
            await UniTask.WhenAny(
                UniTask.DelayFrame(3),
                UniTask.WaitUntil(() => _currentSelection != actionable)
            );

            _currentSelection = null;
            indicator.SetActive(false);
        }
        // private void OnTriggerEnter(Collider other) {
        //     other.TryGetComponent<IActionable>(out var actionable);
        //     if (actionable == null) return;

        //     if (_currentSelection == null) {
        //         _currentSelection = actionable;
        //         indicator.SetActive(true);
        //         indicator.transform.position = actionable.GetPointerPosition();
        //     }
        //     else {
        //         _currentSelection = actionable;
        //         indicator.transform.position = actionable.GetPointerPosition();
        //     }
        // }
        // private void OnTriggerExit(Collider other) {
        //     other.TryGetComponent<IActionable>(out var selectable);
        //     if (selectable == null) return;
        //     _currentSelection = null;
        //     indicator.SetActive(false);
        // }
        private void OnDisable() {
            _bag.Dispose();
        }
    }
}