using Reflex.Core;
using Script.Core.Interface.Systems;
using Script.Feature.Character.Player;
using Script.Feature.DayTime;
using Script.Feature.Farm.Crop;
using Script.Feature.Farm.Soil;
using Script.Feature.Input;
using Script.Feature.Inventory;
using Script.Feature.Item;
using UnityEngine;

namespace Script.Game {
    public class ReflexInjector : MonoBehaviour, IInstaller {
        [SerializeField] private ItemPool itemPool;
        [SerializeField] private CropSystem cropSystem;
        [SerializeField] private PlayerController playerProxy;
        // IDisposable _disposeable;
        public void InstallBindings(ContainerBuilder builder) {
            builder.AddSingleton(new InputProcessor());

            // // Systems
            // builder.Register<TimeSystem>(Lifetime.Singleton).As<ITimeSystem>().AsSelf();
            // builder.Register<SoilSystem>(Lifetime.Singleton);
            // builder.Register<ItemSystem>(Lifetime.Singleton).As<IItemSystem>().AsSelf();
            // builder.Register<InventorySystem>(Lifetime.Singleton).As<IInventorySystem>().AsSelf();
            // builder.RegisterInstance(cropSystem).As<CropSystem>().AsSelf();
            // builder.Register<MoneySystem>(Lifetime.Singleton).As<IMoneySystem>().AsSelf();

            builder.AddSingleton(new TimeSystem(),typeof(TimeSystem), typeof(ITimeSystem));
            builder.AddSingleton(typeof(SoilSystem), typeof(SoilSystem));
            builder.AddSingleton(typeof(ItemSystem), typeof(ItemSystem),typeof(IItemSystem));
            builder.AddSingleton(typeof(InventorySystem), typeof(InventorySystem), typeof(IInventorySystem));
            builder.AddSingleton(cropSystem, typeof(CropSystem));
            builder.AddSingleton(typeof(MoneySystem), typeof(MoneySystem), typeof(IMoneySystem));

            // // Registry
            // builder.Register<InventoryRegistry>(Lifetime.Singleton).As<IInventoryRegistry>().AsSelf();
            // builder.Register<ItemRegistry>(Lifetime.Singleton).As<IItemRegistry>().AsSelf();
            // builder.Register<SoilRegistry>(Lifetime.Singleton).As<ISoilRegistry>().AsSelf();
            // builder.Register<CropRegistry>(Lifetime.Singleton).As<ICropRegistry>().AsSelf();

            builder.AddSingleton(new InventoryRegistry(), typeof(InventoryRegistry), typeof(IInventoryRegistry));
            builder.AddSingleton(new ItemRegistry(), typeof(ItemRegistry), typeof(IItemRegistry));
            builder.AddSingleton(new SoilRegistry(), typeof(SoilRegistry), typeof(ISoilRegistry));
            builder.AddSingleton(new CropRegistry(), typeof(CropRegistry), typeof(ICropRegistry));

            // //factory
            // builder.Register<ItemContextFactory>(Lifetime.Transient).As<IItemContextFactory>().AsSelf();

            builder.AddTransient(typeof(ItemContextFactory), typeof(ItemContextFactory), typeof(IItemContextFactory));

            // builder.RegisterInstance(itemPool);
            builder.AddSingleton(itemPool);

            // // player... idk what else to do. the ui need controll to the player movement
            // builder.RegisterInstance(playerProxy).As<PlayerController>();
            // containerBuilder.AddSingleton
            builder.AddSingleton(playerProxy);
        }
    }
}