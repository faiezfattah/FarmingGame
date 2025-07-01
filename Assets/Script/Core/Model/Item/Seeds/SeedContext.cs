using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;


namespace Script.Core.Model.Item {
    public class SeedContext : ItemContext<SeedData>, IUseable<SoilContext> {
        public SeedContext(SeedData data) : base(data) { }

        public void Use(SoilContext soilContext) {
            if (soilContext.State.Value == SoilState.Initial) return;
            if (soilContext.CropPlanted.Value != null) return;

            var data = (SeedData) BaseData;
            var cropContext = data.CropData.CreateContext();
            soilContext.CropPlanted.Value = cropContext;
            cropContext.SetSoil(soilContext);

            Event.OnUsed.OnNext(this);
        }

        public static class Event {
            public static Subject<SeedContext> OnUsed = new();
        }
    }
}