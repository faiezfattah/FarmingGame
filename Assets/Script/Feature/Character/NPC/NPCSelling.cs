using System;
using System.Linq;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using Script.Feature.Character.Player;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Character.NPC {
    public class NPCSelling : MonoBehaviour, IInteractable {
        [SerializeField] private UIDocument uIDocument;
        [Inject] IMoneySystem _moneySystem;
        [Inject] InventoryRegistry _inventoryRegistry;
        [Inject] IInventorySystem _inventorySystem;
        [Inject] InputProcessor _inputProcessor;
        [Inject] PlayerProxy _player;
        DisposableBag _bag;
        public void Interact() {
            uIDocument.enabled = !uIDocument.enabled; // toggle

            if (!uIDocument.enabled) { // if the current switch to off
                _bag.Dispose();
                _player.EnableMovement();
                return;
            }
            _player.DisableMovement();
            _bag = new();

            _inputProcessor.EscapeEvent.Subscribe(_ => Interact()).AddTo(ref _bag);

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
                     .Subscribe(x => HandleSell(x.ItemContext.BaseData, x.Count.Value))
                     .AddTo(ref _bag);
        }
        private void HandleSell(ItemData itemData, int amount = 1) {
            var price = itemData.price * amount;

            if (_moneySystem.TryTransfer(price)) {
                _inventorySystem.RemoveItem(itemData, amount);
            }
        }
        private void HandleLabel(ItemContext hoveredContext, Label label) {
            label.text = $"Sell {hoveredContext.BaseData.name} for {hoveredContext.BaseData.price} each?";
        }
    }
}