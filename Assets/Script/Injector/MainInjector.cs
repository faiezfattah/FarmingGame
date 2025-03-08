using Script.Feature.DayTime;
using Script.Feature.Farm.Soil;
using Script.Feature.Input;
using Script.Registry.Farm.Crop;
using Script.Registry.Farm.Soil;
using VContainer;
using VContainer.Unity;

namespace Script.Injector {
public class MainInjector : LifetimeScope {
    protected override void Configure(IContainerBuilder builder) {
        builder.Register<InputProcessor>(Lifetime.Singleton);
        
        builder.Register<TimeSystem>(Lifetime.Singleton);
        builder.Register<SoilSystem>(Lifetime.Singleton);
        
        builder.Register<CropRegistry>(Lifetime.Singleton);
        builder.Register<SoilRegistry>(Lifetime.Singleton);
    }
}
}