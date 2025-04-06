using Script.Core.Interface;

namespace Script.Core.Model.Item{
public class GenericContext : ItemContext<GenericData>, IUseable {
    public GenericContext(GenericData data) : base(data) {}
}
}