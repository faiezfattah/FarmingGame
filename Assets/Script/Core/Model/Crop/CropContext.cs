using System;
using R3;
using Script.Core.Model.Item;

namespace Script.Core.Model.Crop {
public class CropContext : IDisposable {
    public Guid Id;
    public int DayPlanted;
    public ReactiveProperty<int> Growth;
    public ReactiveProperty<int> Level;
    public Action OnHarvest;
    public CropData CropData;

    private DisposableBag _bag = new();

    public CropContext(CropData cropData, Action<CropContext> onHarvest) {
        CropData = cropData;
        OnHarvest = () => onHarvest(this);

        Id = Guid.NewGuid();
        Growth = new ReactiveProperty<int>(0);
        Level = new ReactiveProperty<int>(0);
    }

    public void Dispose() {
        _bag.Dispose();
    }
}
}