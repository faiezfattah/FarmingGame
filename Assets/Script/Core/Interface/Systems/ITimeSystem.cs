using R3;

namespace Script.Core.Interface.Systems {
public interface ITimeSystem {
    public ReadOnlyReactiveProperty<int> DayCount { get; }
}
}