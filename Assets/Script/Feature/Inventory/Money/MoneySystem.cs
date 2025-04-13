using R3;
using Script.Core.Interface.Systems;
using UnityEngine;

namespace Script.Feature.Inventory {
public class MoneySystem : IMoneySystem {
    private ReactiveProperty<int> _money = new(20);
    public ReadOnlyReactiveProperty<int> Money => _money;
    public bool TryTransfer(int amount) {
        var projectedBalance = _money.Value + amount;
        if (projectedBalance < 0) return false; // en guard!
        
        _money.Value = projectedBalance;
        return true;
    }
}
}