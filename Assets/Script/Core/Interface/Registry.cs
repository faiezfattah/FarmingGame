using ObservableCollections;
using Script.Core.Model.Crop;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;

public interface IRegistry<T> {
    public IReadOnlyObservableList<T> ReadonlyRegistry { get; }
}

public interface ISoilRegistry : IRegistry<SoilContext> {};
public interface IInventoryRegistry : IRegistry<ItemContext> {};
public interface ICropRegistry : IRegistry<CropContext> {};
public interface IItemRegistry : IRegistry<ItemContext> {};