using Script.Feature.DayTime;
using Script.Feature.Input;
using Script.Registry.Crop;
using VContainer;
using VContainer.Unity;

namespace Script.Injector {
public class MainInjector : LifetimeScope {
    protected override void Configure(IContainerBuilder builder) {
        builder.Register<InputProcessor>(Lifetime.Singleton);
        builder.Register<TimeSystem>(Lifetime.Singleton);
        builder.Register<CropRegistry>(Lifetime.Singleton);
    }
}
}