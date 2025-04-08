using Script.Core.Interface;

namespace Script.Core.Model.Item{
public class GenericContext : ItemContext<GenericData> {
    public GenericContext(GenericData data) : base(data) {}
}
}