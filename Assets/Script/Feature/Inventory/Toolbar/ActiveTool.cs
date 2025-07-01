using System;
using R3;
using Script.Core.Model.Item;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

public class ActiveTool : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    [Inject] private IInventoryRegistry inventory;
    IDisposable _subs;
    private void Start() {
        _subs = inventory.ActiveItem.WhereNotNull().Subscribe(HandleSelect);
    }
    private void HandleSelect(ItemContext itemContext) {
        var display = uIDocument.rootVisualElement.Q<VisualElement>("tool");
        display.style.backgroundImage = new StyleBackground(itemContext.BaseData.itemSprite);
    }
    void OnDisable() {
        _subs.Dispose();
    }
}