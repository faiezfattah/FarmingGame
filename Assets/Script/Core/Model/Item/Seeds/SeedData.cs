using System;
using Script.Core.Interface;
using Script.Core.Model.Crop;
using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Seeds Data", menuName = "Item/Seeds")]
public class SeedData : ItemData<SeedContext> {
    public CropData CropData;
    public override SeedContext CreateContext() => new(this);
    
}
}