using System;
using UnityEngine;

namespace Script.Core.Model.Soil {
public class SoilContext {
    public Guid Id;
    public Vector3 Position;
    public SoilData Data;
       
    public SoilContext(SoilData data) {
        Data = data;
    }
    public SoilContext SetPosition(Vector3 position) {
        Position = position;   
        return this;
    }
}
}