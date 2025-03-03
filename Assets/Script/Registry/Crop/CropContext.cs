using System;
using R3;
using Script.Feature.DayTime;

namespace Script.Registry.Crop {
public class CropContext : IDisposable {
    public Guid Id;
    public int DayPlanted;
    public ReactiveProperty<int> Growth;
    public ReactiveProperty<int> Level;

    private DisposableBag _bag = new();

    public CropContext(TimeSystem timeSystem) {
        Id = Guid.NewGuid();
        DayPlanted = timeSystem.DayCount.Value;
            
        Growth = new ReactiveProperty<int>(0);
        Level = new ReactiveProperty<int>(0);
            
        timeSystem.DayCount
                  .Subscribe(_ => Growth.Value++)
                  .AddTo(ref _bag);
    }

    public void Dispose() {
        _bag.Dispose();
    }
}
}