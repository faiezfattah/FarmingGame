using Script.Core.Interface;

namespace Script.Core.Model.Item{
public class ToolContext : ItemContext<ToolData>, IUseable {
    public ToolContext(ToolData data) : base(data) {}

}
}