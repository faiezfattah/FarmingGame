using System.Collections.Generic;
using System.Linq;
using Script.Core;
using Script.Core.Base;
using Script.Core.Model.Crop;

namespace Script.Feature.Farm.Crop {
public class CropRegistry : IRegistry<CropContext> {
    public HashSet<CropContext> cropContexts = new();
    
    public CropContext TryGet(string id) {
        return cropContexts.First(item => item.Id.ToString() == id);
    }
    public bool TryAdd(CropContext value) {
        return cropContexts.Add(value);
    }

    public bool TryRemove(CropContext value) {
        return cropContexts.Remove(value);
    }

    public IEnumerable<CropContext> GetAll() {
        return cropContexts;
    }
}
}