using System;
using UnityEngine;

namespace Script.Registry.Farm.Soil {
public class SoilContext {
    public Guid Id;
    public Vector2Int Position;
       
    public SoilContext(Vector2Int pos) {
        Position = pos;
    }
}
}