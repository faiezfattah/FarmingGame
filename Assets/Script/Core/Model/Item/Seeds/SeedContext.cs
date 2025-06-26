using MessagePipe;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Soil;
using UnityEngine;
using VContainer;

namespace Script.Core.Model.Item {
    public class SeedContext : ItemContext<SeedData>, IUseable<SoilContext> {
        public SeedContext(SeedData data) : base(data) { }

        public void Use(SoilContext context) {
            if (context.State.Value != SoilState.Watered) return;
            if (context.CropPlanted.Value != null) return;

            var data = (SeedData)BaseData;
            context.CropPlanted.Value = data.CropData.CreateContext();
            Event.OnUsed.Publish(this);
        }

        public static class Event {
            public static Subject<SeedContext> OnUsed = new();
        }
    }
}