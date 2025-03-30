using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Soil {
[CreateAssetMenu(fileName = "New Soil Data", menuName = "Soil/Data")]
public class SoilData : ScriptableObject {
    public Mesh initial;
    public Mesh tilled;
    public Mesh watered;

    public SoilContext CreateContext() => new(this);
}
}