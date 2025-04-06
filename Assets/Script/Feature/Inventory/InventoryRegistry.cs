using ObservableCollections;
using R3;
using Script.Core.Model.Item;

public class InventoryRegistry : IInventoryRegistry, IToolbarRegistry {
    public ObservableList<ItemContext> registry = new();
    public IReadOnlyObservableList<ItemContext> ReadonlyRegistry => registry;


    public ObservableList<string> toolbarRegistry = new(){
        "Shovel", "None", "None", "None", "None", "None"
    };
    public ReactiveProperty<string> activeTool = new();
    public string tool => activeTool.Value;
}