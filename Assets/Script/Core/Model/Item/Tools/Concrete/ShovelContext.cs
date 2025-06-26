using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;
namespace Script.Core.Model.Item{
public class ShovelContext : ToolContext, IUseable<SoilContext> {
    public ShovelContext(ShovelData data) : base(data) {}

    public void Use(SoilContext context) {
        // Debug.Log("trying to modify context");
        if (context.State.Value == SoilState.Initial) {
            context.State.Value = SoilState.Tilled;
        }
    }
}
}