using System;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using R3;
using Script.Core.Model.Item;
using Script.Core.Interface;
using Script.Core.Interface.Systems;

namespace Script.Feature.Character.NPC {
public class NPCVendor : MonoBehaviour, IInteractable {
    [SerializeField] private UIDocument uIDocument;
    [SerializeField] private SeedData seedData;
    [SerializeField] private int price;
    private InputProcessor _inputSystem;
    private IInventorySystem _inventorySystem;
    private IDisposable subscription;
    private IItemContextFactory _itemContextFactory;
    private IMoneySystem _moneySystem;
    [Inject] public void Construct(
        InputProcessor inputSystem, 
        IInventorySystem inventorySystem, 
        IItemContextFactory itemContextFactory,
        IMoneySystem moneySystem) {

        _inputSystem = inputSystem;
        _inventorySystem = inventorySystem;
        _itemContextFactory = itemContextFactory;
        _moneySystem = moneySystem;
    }
    private void Start() {
        uIDocument.enabled = false;
    }
    private int GetValue() {
        var slider = uIDocument.rootVisualElement.Q<SliderInt>();
        return slider.value;
    }

    public void Interact() {
        uIDocument.enabled = true;
        subscription = _inputSystem.DebugEvent.Subscribe(_ => Close());
        uIDocument.rootVisualElement.Q<Button>("Buy")
                                    .RegisterCallback<ClickEvent>(_ => Close());
    }
    public void Close() {
        var itemCount = GetValue();
        
        if (_moneySystem.TryTransfer(-itemCount * price)) {
            // Debug.Log("Sold!: " + itemCount);
            _inventorySystem.AddItem(_itemContextFactory.Create(seedData), GetValue());
        } else {
            Debug.Log("not enough money :<");
        }

        subscription.Dispose();
        uIDocument.enabled = false;
    }
}
}