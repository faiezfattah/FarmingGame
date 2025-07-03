using PrimeTween;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using Script.Feature.Character.Player;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Character.NPC {
    public class NPCVendor : MonoBehaviour, IInteractable {
        [SerializeField] private UIDocument uIDocument;
        [SerializeField] private SeedData[] seedData;
        [Inject] private IInventorySystem _inventorySystem;
        [Inject] private InputProcessor _inputProcessor;
        [Inject] private IMoneySystem _moneySystem;
        [Inject] private PlayerController _player;
        private DisposableBag _bag = new();

        private void Start() {
            uIDocument.enabled = false;
            System.Array.Sort(seedData, (a, b) => a.price.CompareTo(b.price));
        }
        private void GenerateShop() {
            var container = uIDocument.rootVisualElement.Q<VisualElement>("container");

            Tween.Position(container.transform, new Vector2(0, -1000), Vector2.zero, 0.15f);

            container.Clear();
            foreach (var seed in seedData) {
                var count = _moneySystem.Money.CurrentValue / seed.price > seed.MaxStackable
                            ? seed.MaxStackable 
                            : _moneySystem.Money.CurrentValue / seed.price;

                var slider = new SliderWithButton();
                slider.SetInputDisplay(true)
                      .SetMaxValue(count)
                      .SetMinValue(1)
                      .SetLabel($"{seed.name} ({seed.price}$)");

                slider.OnClick.Subscribe(x => HandleBuy(seed, x)).AddTo(ref _bag);
                container.Add(slider);
            }
        }
        private void HandleBuy(SeedData seedData, int amount) {
            if (_moneySystem.TryTransfer(-amount * seedData.price)) {
                // Debug.Log("Sold!: " + itemCount);
                _inventorySystem.AddItem(seedData, amount);
            }
            else {
                Debug.Log("not enough money :<");
            }
        }
        public void Interact() {
            if (uIDocument.enabled) {
                Close();
                return;
            }

            _player.DisableMovement();
            uIDocument.enabled = true;
            _inputProcessor.EscapeEvent.Subscribe(_ => Close()).AddTo(ref _bag);
            GenerateShop();
        }
        public async void Close() {
            _player.EnableMovement();

            var container = uIDocument.rootVisualElement.Q<VisualElement>("container");
            await Tween.Position(container.transform, new Vector2(0, -1000), 0.25f);

            _bag.Dispose();
            _bag = new();
            uIDocument.enabled = false;
            
        }
    }
}