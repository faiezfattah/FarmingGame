using R3;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Script.Feature.Inventory {
public class Money : MonoBehaviour {
    [SerializeField] private UIDocument uIDocument;
    private ReactiveProperty<int> _testMoney = new(10);
    private string _labelString => $"Money: {_testMoney.Value}";
    private Label _label;
    [Inject] public void Construct () {

    }
    private void Start() {
        _label = uIDocument.rootVisualElement.Q<Label>("MoneyLabel");
        _testMoney.Subscribe(_ => UpdateLabel());
    }
    private void UpdateLabel() {
        _label.text = _labelString;
    }
}
}