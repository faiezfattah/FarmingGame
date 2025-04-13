using MessagePipe;
using Script.Core.Interface;
using Script.Core.Model.Item.Seeds;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;

namespace Script.Core.Model.Item{
public class SeedContext : ItemContext<SeedData>, IUseable<SoilContext> {
    public SeedContext(SeedData data) : base(data) {}
    private IPublisher<SeedUsed> usedEvent;
    [Inject] public void Construct(IPublisher<SeedUsed> usedEvent) {
        this.usedEvent = usedEvent;
        Debug.Log("injected");
    }

    public void Use(SoilContext context) {
        var data = (SeedData) BaseData;
        context.CropPlanted.Value = data.CropData.CreateContext();
        usedEvent.Publish(new(this));
    }
}
}