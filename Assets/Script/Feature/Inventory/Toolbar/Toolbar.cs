using System;
using System.Linq;
using PrimeTween;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Toolbar {
    public class Toolbar : MonoBehaviour {
        [SerializeField] private UIDocument uIDocument;
        [SerializeField] private Core.Utils.Logger logger;
        private VisualElement _root;
        private DisposableBag _bag;
        private InventoryRegistry _inventoryRegistry;
        [SerializeField] private ToolData[] toolDatas;
        public static Subject<ToolContext> OnSelect = new();

        [Inject]
        public void Construct(InputProcessor input, InventoryRegistry inventoryRegistry) {
            input.ToolbarEvent.Subscribe(ToggleVisibility).AddTo(ref _bag);
            _inventoryRegistry = inventoryRegistry;
        }
        private void Start() {
            uIDocument.enabled = false;
            foreach (var data in toolDatas) {
                var context = data.CreateContext();
                _inventoryRegistry.toolbarRegistry.Add(context);
                // Debug.Log("added context: " + context.BaseData.name);
            }
        }
        private void HandleClick(ClickEvent e) {
            var btn = e.currentTarget as ToolButton;
            _inventoryRegistry.activeItem.Value = btn.toolContext;
            OnSelect.OnNext(btn.toolContext);
            
            logger.Log("equipped: " + _inventoryRegistry.activeItem.CurrentValue.BaseData.name);
        }
        private async void ToggleVisibility(bool value) {
            uIDocument.enabled = value;

            if (!value) return;

            _root = uIDocument.rootVisualElement;

            var buttons = _root.Query<ToolButton>().ToList();
            var tools = _inventoryRegistry.toolbarRegistry.ToList();

            foreach (var button in buttons) { // animation setup
                button.transform.scale = Vector3.zero;
            }

            for (int i = 0; i < buttons.Count; i++) {
                var tool = i < _inventoryRegistry.toolbarRegistry.Count
                    ? _inventoryRegistry.toolbarRegistry[i] 
                    : null;


                if (tool is not null) {
                    buttons[i].SetToolContext(tool);
                    buttons[i].RegisterCallback<ClickEvent>(HandleClick);
                    Disposable.Create(() => buttons[i].UnregisterCallback<ClickEvent>(HandleClick)).AddTo(ref _bag);
                }
            }

            // animation
            foreach (var button in buttons) {
                _ = Tween.Scale(button.transform, Vector3.zero, Vector3.one, 0.2f);
                _ = Tween.Rotation(button.transform, Quaternion.Euler(0, 0, 180), Quaternion.Euler(0, 0, 0), 0.2f, Ease.OutSine);
                await Tween.Delay(0.01f);
            }
        }
        public void OnDisable() {
            _bag.Dispose();
        }
    }
}