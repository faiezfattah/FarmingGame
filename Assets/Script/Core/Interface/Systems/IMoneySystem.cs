using R3;

namespace Script.Core.Interface.Systems{
public interface IMoneySystem{
    public ReadOnlyReactiveProperty<int> Money { get; }
    public bool TryTransfer(int amount);
}
}