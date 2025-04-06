using System.Linq;
using Codice.Client.Common.GameUI;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Input;
using TriInspector;
using Unity.Android.Gradle.Manifest;
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
    [Inject] public void Construct(InputProcessor input, InventoryRegistry inventoryRegistry) {
        input.ToolbarEvent.Subscribe(_ => ToggleVisibility()).AddTo(ref _bag);
        _inventoryRegistry = inventoryRegistry;
    }
    private void Start() {
        uIDocument.enabled = false;
        foreach (var data in toolDatas) {
            var context = data.CreateContext();
            _inventoryRegistry.toolbarRegistry.Add(context);
            Debug.Log("added context: " + context.ItemData.name);
        }
    }
    private void HandleClick(ClickEvent e) {
        var btn = e.currentTarget as Button;
        _inventoryRegistry.activeTool.Value = _inventoryRegistry.toolbarRegistry.Where(x => x.ItemData.name == btn.text).First();
        Debug.Log("equipped: " + _inventoryRegistry.activeTool.CurrentValue.ItemData.name);
        ToggleVisibility();
    }
    private void ToggleVisibility() {
        uIDocument.enabled = !uIDocument.enabled;

        if (!uIDocument.enabled) return;

        _root = uIDocument.rootVisualElement;
        var rootChildrens = _root.Query<Button>().ToList();

        foreach (var item in rootChildrens) {
            if (item is Button button) {
                Debug.Log("registered" + button.name);
                button.RegisterCallback<ClickEvent>(HandleClick);
            }
        }
    }
    public void OnDisable() {
        _bag.Dispose();
    }
}
}