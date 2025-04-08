using Script.Core.Interface;
using Script.Core.Model.Soil;

namespace Script.Core.Model.Item{
public class SeedContext : ItemContext<SeedData>, IUseable<SoilContext> {
    public SeedContext(SeedData data) : base(data) {}

    public void Use(SoilContext context) {
        var data = (SeedData) BaseData;
        context.CropPlanted.Value = data.CropData.CreateContext();
    }
}
}