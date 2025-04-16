using System;
using System.Linq;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Character.NPC {
public class NPCSelling : MonoBehaviour, IInteractable {
    [SerializeField] private UIDocument uIDocument;
    private IMoneySystem _moneySystem;
    private InventoryRegistry _inventoryRegistry;
    private IInventorySystem _inventorySystem;
    private DisposableBag _bag;
    private InputProcessor _inputProcessor;
    private ItemContext _currentItemContext;
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
        var slider = uIDocument.rootVisualElement.Q<SliderWithButton>();

        inventory.SetInventoryBinding(_inventoryRegistry);
        inventory.CurrentHover
                 .Where(x=> x != null)
                 .Where(x => x.itemContext != null)
                 .Subscribe(x => HandleLabel(x.itemContext, slider))
                 .AddTo(ref _bag);
        slider.OnClick.Subscribe(x => HandleSell(x)).AddTo(ref _bag);

        // var inventory = _inventoryRegistry.ReadonlyRegistry.ToList();
        // var price = 0;
        // inventory.ForEach(x => {
        //     price += x.ItemContext.BaseData.price * x.Count.Value;
        //     _inventorySystem.RemoveItem(x.ItemContext, x.Count.Value);
        // });
        // _moneySystem.TryTransfer(price);
    }
    private void HandleSell(int amount) {
        var price = _currentItemContext.BaseData.price * amount;
        
        if (_moneySystem.TryTransfer(price)) {
            _inventorySystem.RemoveItem(_currentItemContext, amount);
        }
    }
    private void HandleLabel(ItemContext hoveredContext, SliderWithButton slider) {
        _currentItemContext = hoveredContext;
        slider.SetLabel(_currentItemContext.BaseData.name);
    }
    private void CloseUI() {
        uIDocument.enabled = false;
        _bag.Dispose();
    }
}
}