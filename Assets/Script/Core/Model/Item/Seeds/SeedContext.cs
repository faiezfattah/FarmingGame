using MessagePipe;
using Script.Core.Interface;
using Script.Core.Model.Item.Seeds;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;

namespace Script.Core.Model.Item{
public class SeedContext : ItemContext<SeedData>, IUseable<SoilContext> {
    public SeedContext(SeedData data) : base(data) {}
    [Inject] private IPublisher<SeedUsed> usedEvent;

    public void Use(SoilContext context) {
        if (context.State.Value != SoilState.Watered) return;
        if (context.CropPlanted.Value != null) return;
        
        var data = (SeedData) BaseData;
        context.CropPlanted.Value = data.CropData.CreateContext();
        usedEvent.Publish(new(this));
    }
}
}