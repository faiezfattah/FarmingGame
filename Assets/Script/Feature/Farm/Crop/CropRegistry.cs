using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using Script.Core;
using Script.Core.Base;
using Script.Core.Model.Crop;

namespace Script.Feature.Farm.Crop {
public class CropRegistry : ICropRegistry {
    public ObservableList<CropContext> _registry = new();
    public IReadOnlyObservableList<CropContext> ReadonlyRegistry => _registry;
}
}