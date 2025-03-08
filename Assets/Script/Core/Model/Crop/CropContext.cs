using System;
using R3;

namespace Script.Core.Model.Crop {
public class CropContext : IDisposable {
    public Guid Id;
    public int DayPlanted;
    public ReactiveProperty<int> Growth;
    public ReactiveProperty<int> Level;
    public Action OnHarvest;

    private DisposableBag _bag = new();

    public CropContext(Action<CropContext> onHarvest) {
        Id = Guid.NewGuid();
        OnHarvest = () => onHarvest(this);

        Growth = new ReactiveProperty<int>(0);
        Level = new ReactiveProperty<int>(0);
    }

    public void Dispose() {
        _bag.Dispose();
    }
}
}