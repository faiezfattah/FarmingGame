using System.Collections.Generic;
using System.Linq;
using R3;
using Script.Core;

namespace Script.Feature.Farm.Soil {
public class SoilRegistry : IRegistry<SoilContext> {
    public ReactiveProperty<HashSet<SoilContext>> SoilContexts = new();

    public SoilContext TryGet(string id) {
        return SoilContexts.Value.First(item => item.Id.ToString() == id);
    }
    public bool TryAdd(SoilContext value) {
        return SoilContexts.Value.Add(value);
    }

    public bool TryRemove(SoilContext value) {
        return SoilContexts.Value.Remove(value);
    }

    public IEnumerable<SoilContext> GetAll() {
        return SoilContexts.Value;
    }
}
}