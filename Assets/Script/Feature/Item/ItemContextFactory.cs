using System.Runtime.InteropServices.WindowsRuntime;
using Script.Core.Interface.Systems;
using Script.Core.Model.Item;
using UnityEngine;
using Reflex.Core;
using Reflex.Injectors;

namespace Script.Feature.Item {
public class ItemContextFactory : IItemContextFactory {
    private Container _container;

    public ItemContextFactory(Container container) {
        _container = container;
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
        AttributeInjector.Inject(context, _container);
        return context;
    }
}
}
