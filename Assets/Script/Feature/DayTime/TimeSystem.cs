using System;
using R3;
using Script.Core.Interface.Systems;
using UnityEngine;

namespace Script.Feature.DayTime {
public class TimeSystem : IDisposable, ITimeSystem {
    private ReactiveProperty<int> _dayCount = new(0);
    public ReadOnlyReactiveProperty<int> DayCount => _dayCount;

    private float _dayTimeInSeconds = 2f;
    private IDisposable _subscription;
    
    public TimeSystem() {
        _subscription = Observable
                       .Interval(TimeSpan.FromSeconds(_dayTimeInSeconds))
                       .Subscribe(_ => { _dayCount.Value++; });
    }

    public void Dispose() {
        _subscription?.Dispose();
    }
}
}