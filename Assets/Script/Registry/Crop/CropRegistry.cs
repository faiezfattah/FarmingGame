using System.Collections.Generic;
using System.Linq;
using Script.Core;

namespace Script.Registry.Crop {
public class CropRegistry : IRegistry<CropContext> {
    private HashSet<CropContext> _cropContexts = new();
    
    public CropContext TryGet(string id) {
        return _cropContexts.First(item => item.Id.ToString() == id);
    }
    public bool TryAdd(CropContext value) {
        return _cropContexts.Add(value);
    }

    public bool TryRemove(CropContext value) {
        return _cropContexts.Remove(value);
    }

    public IEnumerable<CropContext> GetAll() {
        return _cropContexts;
    }
}
}