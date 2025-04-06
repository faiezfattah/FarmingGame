using R3;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Toolbar {
public class Toolbar : MonoBehaviour {
    [SerializeField] public UIDocument uIDocument;
    private VisualElement _root;
    private DisposableBag _bag;
    private InventoryRegistry _inventoryRegistry;
    [Inject] public void Construct(InputProcessor input, InventoryRegistry inventoryRegistry) {
        input.ToolbarEvent.Subscribe(_ => ToggleVisibility()).AddTo(ref _bag);
        _inventoryRegistry = inventoryRegistry;
    }
    private void Start() {
        uIDocument.enabled = false;
    }
    private void HandleClick(ClickEvent e) {
        var btn = e.currentTarget as Button;
        _inventoryRegistry.activeTool.Value = btn.text;
        Debug.Log("equipped: " + _inventoryRegistry.activeTool.CurrentValue);
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