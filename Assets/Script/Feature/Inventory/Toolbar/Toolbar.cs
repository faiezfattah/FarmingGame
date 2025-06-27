using System.Linq;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Toolbar {
    public class Toolbar : MonoBehaviour {
        [SerializeField] private UIDocument uIDocument;
        private VisualElement _root;
        private DisposableBag _bag;
        private InventoryRegistry _inventoryRegistry;
        [SerializeField] private ToolData[] toolDatas;
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
            var btn = e.currentTarget as Button;
            _inventoryRegistry.activeTool.Value = _inventoryRegistry.toolbarRegistry.Where(x => x.BaseData.name == btn.text).First();
            Debug.Log("equipped: " + _inventoryRegistry.activeTool.CurrentValue.BaseData.name);
        }
        private void ToggleVisibility(bool value) {
            uIDocument.enabled = value;

            if (!value) return;

            _root = uIDocument.rootVisualElement;

            var buttons = _root.Query<ToolButton>().ToList();
            var tools = _inventoryRegistry.toolbarRegistry.ToList();

            for (int i = 0; i < _inventoryRegistry.toolbarRegistry.Count; i++) {
                var tool = _inventoryRegistry.toolbarRegistry[i];

                if (tool is not null) {
                    buttons[i].SetData(tool);
                    buttons[i].RegisterCallback<ClickEvent>(HandleClick);
                    buttons[i].toolName = tools[i].BaseData.name;
                }
            }
        }
        public void OnDisable() {
            _bag.Dispose();
        }
    }
}