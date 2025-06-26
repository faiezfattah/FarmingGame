using System;
using System.Linq;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Character.NPC {
public class NPCSelling : MonoBehaviour, IInteractable {
    [SerializeField] private UIDocument uIDocument;
    IMoneySystem _moneySystem;
    InventoryRegistry _inventoryRegistry;
    IInventorySystem _inventorySystem;
    DisposableBag _bag;
    InputProcessor _inputProcessor;
    [Inject] public void Construct(IMoneySystem moneySystem, InputProcessor inputProcessor, InventoryRegistry inventoryRegistry, IInventorySystem inventorySystem) {
        _moneySystem = moneySystem;
        _inventoryRegistry = inventoryRegistry;
        _inventorySystem = inventorySystem;
        _inputProcessor = inputProcessor;
    }
    public void Interact() {
        uIDocument.enabled = true;
        _inputProcessor.EscapeEvent.Subscribe(_ => CloseUI()).AddTo(ref _bag);

        var inventory = uIDocument.rootVisualElement.Q<InventoryDisplay>();
        var label = uIDocument.rootVisualElement.Q<Label>();

        inventory.SetInventoryBinding(_inventoryRegistry);
        inventory.OnHover
                 .WhereNotNull()
                 .Where(x => x.itemContext != null)
                 .Subscribe(x => HandleLabel(x.itemContext, label))
                 .AddTo(ref _bag);

        inventory.OnSelected
                 .WhereNotNull()
                 .Subscribe(x => HandleSell(x));
    }
    private void HandleSell(PackedItemContext pic) {
        var price = pic.ItemContext.BaseData.price * pic.Count.Value;
        
        if (_moneySystem.TryTransfer(price)) {
            _inventorySystem.RemoveItem(pic, pic.Count.Value);
            // pic.Count.Value = 0;
        }
    }
    private void HandleLabel(ItemContext hoveredContext, Label label) {
        label.text = $"Sell {hoveredContext.BaseData.name} for {hoveredContext.BaseData.price} each?";
    }
    private void CloseUI() {
        uIDocument.enabled = false;
        _bag.Dispose();
    }
}
}