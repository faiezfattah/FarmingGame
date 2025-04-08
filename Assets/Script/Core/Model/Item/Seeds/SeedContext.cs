using Script.Core.Interface;

namespace Script.Core.Model.Item{
public class SeedContext : ItemContext<SeedData>, IUseable {
    public SeedContext(SeedData data) : base(data) {}

    public void Use(IActionable actionable) {
        actionable.Action(this);
    }
}
}