using Script.Core.Interface;
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
    
    protected override void Configure(IContainerBuilder builder) {
        builder.Register<InputProcessor>(Lifetime.Singleton);

        builder.Register<TimeSystem>(Lifetime.Singleton);
        
        builder.Register<SoilSystem>(Lifetime.Singleton);
        builder.Register<SoilRegistry>(Lifetime.Singleton);

        builder.Register<CropRegistry>(Lifetime.Singleton);

        builder.Register<ItemSystem>(Lifetime.Singleton).As<IItemSystem>();
        builder.Register<ItemRegistry>(Lifetime.Singleton);
        builder.RegisterInstance(itemPool);
    }
}
}