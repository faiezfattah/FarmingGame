using System;
using Script.Feature.DayTime;
using Script.Feature.Farm.Crop;
using Script.Feature.Farm.Soil;
using Script.Feature.Item;
using Script.Core.Model.Soil;
using Script.Core.Model.Crop;
using ObservableCollections;
using R3;
using UnityEngine;

public class FarmingManager : IDisposable {
    IDisposable _disposeable;
    
    public FarmingManager(
        CropSystem cropSystem,
        TimeSystem timeSystem,
        ItemSystem itemSystem
    ) {
        DisposableBuilder builder = new();

        timeSystem.DayCount.Subscribe(_ => {
            cropSystem.UpdateCrop();
        }).AddTo(ref builder);

        cropSystem.OnHarvest.Subscribe(tuple => {
            var (itemData, position) = tuple;
            itemSystem.SpawnItem(itemData, position);
        }).AddTo(ref builder);



        _disposeable = builder.Build();
    }


    public void Dispose() {
        _disposeable?.Dispose();
    }
}