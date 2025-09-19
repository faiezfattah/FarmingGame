using Cysharp.Threading.Tasks;
using R3;
using Script.Core.Interface;
using Script.Core.Utils;
using Script.Feature.Input;
using UnityEngine;
using VContainer;

namespace Script.Feature.Character.Player {
    public class PlayerPointer : MonoBehaviour {
        [SerializeField] private GameObject indicator;
        [SerializeField] private float range;
        private ReactiveProperty<IActionable> _currentSelection = new();
        private DisposableBag _bag;
        private InventoryRegistry _inventoryRegistry;
        private PlayerController _playerController;
        [Inject]
        public void Construct(InputProcessor inputProcessor, InventoryRegistry inventoryRegistry, PlayerController playerController) {
            // inputProcessor.ActionEvent.Subscribe(_ => Action()).AddTo(ref _bag);

            var hasItemEquipped = inventoryRegistry.activeItem.Select(x => x != null);
            inputProcessor.ActionEvent
                .WithLatestFrom(hasItemEquipped, (_, equipped) => equipped)
                .Where(equipped => equipped)
                .Subscribe(_ => Action())
                .AddTo(ref _bag);
            
            _inventoryRegistry = inventoryRegistry;
            _playerController = playerController;
        }
        void Awake() {
            var hovered = IActionable.Event
                .OnPointerHovered
                .Where(Check);
            var exited = IActionable.Event
                .OnPointerExited
                .Select(_ => (IActionable)null);

            hovered.Merge(exited)
                .Subscribe(UpdateSelection)
                .AddTo(ref _bag);
        }
        private void UpdateSelection(IActionable actionable) {
            if (actionable != null) {
                indicator.SetActive(true);
                indicator.transform.position = actionable.GetPointerPosition();
                if (_currentSelection == null) _currentSelection = new();
                _currentSelection.Value = actionable;
            }
            else {
                _currentSelection = null;
                indicator.SetActive(false);
            }
        }

        private void Action() {
            if (_currentSelection == null) return;
            if (_inventoryRegistry.activeItem.Value == null) return;

            if (_inventoryRegistry.activeItem.Value is IUseable useable) {
                _currentSelection.Expect("current selection is null")
                    .CurrentValue.Expect("current value is null")
                    .Action(useable);
            }
        }
        private bool Check(IActionable actionable) {
            var dist = Vector3.Distance(_playerController.transform.position, actionable.GetPointerPosition());

            return dist < range;
        }
        private void OnDisable() {
            _bag.Dispose();
        }
    }
}