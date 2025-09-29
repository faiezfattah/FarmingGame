using System;
using R3;
using Script.Core.Interface.Systems;
using UnityEngine;
using UnityEngine.UIElements;
using Reflex.Attributes;

namespace Script.Feature.Inventory {
public class Money : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    private Label _label;
    private IDisposable _subscription;
    private IMoneySystem _moneySystem;
    [Inject] public void Construct (IMoneySystem moneySystem) {
        _moneySystem = moneySystem;
        _subscription = _moneySystem.Money.Subscribe(_ => UpdateLabel());
    }
    private string labelString => $"Money: {_moneySystem.Money.CurrentValue}";
    private void Start() {
        _label = uIDocument.rootVisualElement.Q<Label>("MoneyLabel");
        UpdateLabel();
    }
    private void UpdateLabel() {
        if (_label == null) return;
        _label.text = labelString;
    }
    private void OnDisable() {
        _subscription?.Dispose();       
    }
}
}