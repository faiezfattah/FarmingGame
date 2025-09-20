using ObservableCollections;
using Script.Core.Model.Crop;

namespace Script.Feature.Farm.Crop {
public class CropRegistry : ICropRegistry {
    public ObservableList<CropContext> registry = new();
    public IReadOnlyObservableList<CropContext> ReadonlyRegistry => registry;
}
}