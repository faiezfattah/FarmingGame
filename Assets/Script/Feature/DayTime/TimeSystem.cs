using System;
using R3;
using UnityEngine;

namespace Script.Feature.DayTime {
public class TimeSystem : IDisposable {
    public ReactiveProperty<int> DayCount = new(0);
    private float _dayTimeInSeconds = 2f;
    private IDisposable _subscription;
    
    public TimeSystem() {
        _subscription = Observable
                       .Interval(TimeSpan.FromSeconds(_dayTimeInSeconds))
                       .Subscribe(_ => { DayCount.Value++; });
    }

    public void Dispose() {
        _subscription?.Dispose();
    }
}
}