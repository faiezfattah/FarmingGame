using System;
using Script.Core.Interface;
using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Generic Data", menuName = "Item/Generic")]
public class GenericData : ItemData<GenericContext> {
    public override GenericContext CreateContext() => new(this);
}
}