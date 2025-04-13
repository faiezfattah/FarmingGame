using System.Runtime.InteropServices.WindowsRuntime;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using UnityEngine;
using VContainer;

namespace Script.Feature.Item {
public class ItemContextFactory : IItemContextFactory {
    private IObjectResolver _resolver;

    public ItemContextFactory(IObjectResolver resolver) {
        _resolver = resolver;
    }

    public ItemContext Create(ItemData itemData) {
        Debug.Log("created: " + itemData.name);
        return ProcessContext(itemData);
    }

    public TContext Create<TContext>(ItemData itemData) where TContext : ItemContext {
        Debug.Log("created: " + itemData.name);
        return ProcessContext(itemData) as TContext;
    }

    private ItemContext ProcessContext(ItemData itemData) {
        var context = itemData.CreateBaseContext();
        return context switch {
            SeedContext s => InjectHelper(s),
            _ => context
        };
    }

    private T InjectHelper<T>(T context) {
        _resolver.Inject(context);
        return context;
    }
}
}
