using ObservableCollections;
using Script.Core.Model.Soil;

namespace Script.Feature.Farm.Soil {
public class SoilRegistry : ISoilRegistry {
    public ObservableList<SoilContext> Registry = new();
    public IReadOnlyObservableList<SoilContext> ReadonlyRegistry { get => Registry; }
}
}