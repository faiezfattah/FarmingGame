using MessagePipe;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Feature.DayTime;
using Script.Feature.Farm.Crop;
using Script.Feature.Farm.Soil;
using Script.Feature.Input;
using Script.Feature.Item;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Script.Injector {
public class MainInjector : LifetimeScope {
    [SerializeField] private ItemPool itemPool;
    [SerializeField] private CropSystem cropSystem;
    protected override void Configure(IContainerBuilder builder) {
        var options = builder.RegisterMessagePipe(/* configure option */);
        builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        
        // mine
        builder.Register<InputProcessor>(Lifetime.Singleton);

        // Systems
        builder.Register<TimeSystem>(Lifetime.Singleton).As<ITimeSystem>();
        builder.Register<SoilSystem>(Lifetime.Singleton);
        builder.Register<ItemSystem>(Lifetime.Singleton).As<IItemSystem>();
        builder.Register<InventorySystem>(Lifetime.Singleton).As<IInventorySystem>();
        builder.RegisterInstance(cropSystem).As<CropSystem>();

        // Registry
        builder.Register<InventoryRegistry>(Lifetime.Singleton).As<IInventoryRegistry, IToolbarRegistry>().AsSelf();
        builder.Register<ItemRegistry>(Lifetime.Singleton).As<IItemRegistry>().AsSelf();
        builder.Register<SoilRegistry>(Lifetime.Singleton).As<ISoilRegistry>().AsSelf();
        builder.Register<CropRegistry>(Lifetime.Singleton).As<ICropRegistry>().AsSelf();

        //factory
        builder.Register<ItemContextFactory>(Lifetime.Transient).As<IItemContextFactory>().AsSelf();

        builder.RegisterInstance(itemPool);
    }
}
}