using Cysharp.Threading.Tasks;
using R3;
using Script.Core.Model.Item;
using Script.Feature.Toolbar;
using UnityEngine;
using UnityEngine.UIElements;

public class ActiveTool : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    DisposableBag _bag = new();
    private void Start() {
        Toolbar.OnSelect.Subscribe(HandleSelect).AddTo(ref _bag);
    }
    private void HandleSelect(ToolContext context) {
        var display = uIDocument.rootVisualElement.Q<VisualElement>("tool");
        display.style.backgroundImage = new StyleBackground(context.BaseData.itemSprite);
    }
    void OnDisable() {
        _bag.Dispose();
    }
}