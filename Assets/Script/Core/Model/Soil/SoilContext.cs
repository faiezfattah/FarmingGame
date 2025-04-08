using System;
using R3;
using Script.Core.Model.Crop;
using Script.Core.Model.Item;
using UnityEngine;

namespace Script.Core.Model.Soil {
public class SoilContext : IDisposable {
    public Guid Id;
    public Vector3 Position;
    public SoilData Data;
    public CropContext CropPlanted;
    public ReactiveProperty<SoilState> State = new(SoilState.Initial);
    public SoilContext(SoilData data) {
        Data = data;
    }

    public SoilContext SetPosition(Vector3 position) {
        Position = position;   
        return this;
    }

    public void Dispose() {
        State.Dispose();
    }
}
}