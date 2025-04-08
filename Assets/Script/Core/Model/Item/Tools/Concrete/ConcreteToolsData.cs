using System;
using System.Collections.Generic;
using Script.Core.Interface;
using TriInspector;
using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Shovel Data", menuName = "Item/Tools/Shovel")]
public class ShovelData : ToolData {
    public override ToolContext CreateContext() => new ShovelContext(this);
}
[CreateAssetMenu(fileName = "New Watering Can Data", menuName = "Item/Tools/Watering Can")]
public class WaterCanData : ToolData {
    public override ToolContext CreateContext() => new WaterCanContext(this);
}
}