using UnityEngine;

namespace Script.Core.Model.Item {
[CreateAssetMenu(fileName = "New Shovel Data", menuName = "Item/Tools/Shovel")]
public class ShovelData : ToolData {
    public override ToolContext CreateContext() => new ShovelContext(this);
}
}