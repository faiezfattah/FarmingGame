using UnityEngine;

namespace Script.Core.Model.Item {

[CreateAssetMenu(fileName = "New Watering Can Data", menuName = "Item/Tools/Watering Can")]
public class WaterCanData : ToolData {
    public override ToolContext CreateContext() => new WaterCanContext(this);
}
}