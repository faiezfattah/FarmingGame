using Script.Core.Interface;
using Script.Core.Model.Soil;

namespace Script.Core.Model.Item{
public class WaterCanContext : ToolContext, IUseable<SoilContext> {
    public WaterCanContext(WaterCanData data) : base(data) {}

    public void Use(SoilContext context) {
        if (context.State.Value == SoilState.Tilled) {
            context.State.Value = SoilState.Watered;
        }
    }
}
}