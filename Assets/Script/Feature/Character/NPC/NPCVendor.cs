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
    [SerializeField] private SeedData[] seedData;
    [SerializeField] private int price;
    private IInventorySystem _inventorySystem;
    private IItemContextFactory _itemContextFactory;
    private InputProcessor _inputProcessor;
    private IMoneySystem _moneySystem;
    private DisposableBag _bag = new();
    [Inject] public void Construct(
        IInventorySystem inventorySystem,
        InputProcessor inputProcessor,
        IItemContextFactory itemContextFactory,
        IMoneySystem moneySystem) {

        _inventorySystem = inventorySystem;
        _inputProcessor = inputProcessor;
        _itemContextFactory = itemContextFactory;
        _moneySystem = moneySystem;
    }
    private void Start() {
        uIDocument.enabled = false;
    }
    private void GenerateShop() {
        var container = uIDocument.rootVisualElement.Q<VisualElement>("container");
        container.Clear();

        foreach (var seed in seedData) {
            var slider = new SliderWithButton();
            slider.SetInputDisplay(true)
                  .SetMaxValue(seed.MaxStackable)
                  .SetMinValue(1)
                  .SetLabel(seed.name);

            slider.OnClick.Subscribe(x => HandleBuy(seed, x)).AddTo(ref _bag);
            container.Add(slider);
        }
    }
    private void HandleBuy(SeedData seedData, int amount) {
        if (_moneySystem.TryTransfer(-amount * price)) {
            // Debug.Log("Sold!: " + itemCount);
            _inventorySystem.AddItem(_itemContextFactory.Create(seedData), amount);
        } else {
            Debug.Log("not enough money :<");
        }
    }
    public void Interact() {
        uIDocument.enabled = true;
        _inputProcessor.EscapeEvent.Subscribe(_ => Close()).AddTo(ref _bag);
        GenerateShop();
    }
    public void Close() {
        _bag.Dispose();
        _bag = new();
        uIDocument.enabled = false;
    }
}
}