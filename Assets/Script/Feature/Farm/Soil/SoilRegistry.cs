using System.Collections.Generic;
using System.Linq;
using ObservableCollections;
using R3;
using Script.Core;
using Script.Core.Base;
using Script.Core.Model.Soil;

namespace Script.Feature.Farm.Soil {
public class SoilRegistry : ISoilRegistry {
    public ObservableList<SoilContext> Registry = new();
    public IReadOnlyObservableList<SoilContext> ReadonlyRegistry { get => Registry; }
}
}