using Script.Core.Interface.Systems;
using Script.Feature.Character.Player;
using Script.Feature.DayTime;
using Script.Feature.Farm.Crop;
using Script.Feature.Farm.Soil;
using Script.Feature.Input;
using Script.Feature.Inventory;
using Script.Feature.Item;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Script.Game {
    public class MainInjector : LifetimeScope {
        [SerializeField] private ItemPool itemPool;
        [SerializeField] private CropSystem cropSystem;
        [SerializeField] private PlayerController playerProxy;

        protected override void Configure(IContainerBuilder builder) {
            builder.Register<InputProcessor>(Lifetime.Singleton);

            // Systems
            builder.Register<TimeSystem>(Lifetime.Singleton).As<ITimeSystem>();
            builder.Register<SoilSystem>(Lifetime.Singleton);
            builder.Register<ItemSystem>(Lifetime.Singleton).As<IItemSystem>();
            builder.Register<InventorySystem>(Lifetime.Singleton).As<IInventorySystem>();
            builder.RegisterInstance(cropSystem).As<CropSystem>();
            builder.Register<MoneySystem>(Lifetime.Singleton).As<IMoneySystem>();

            // Registry
            builder.Register<InventoryRegistry>(Lifetime.Singleton).As<IInventoryRegistry>().AsSelf();
            builder.Register<ItemRegistry>(Lifetime.Singleton).As<IItemRegistry>().AsSelf();
            builder.Register<SoilRegistry>(Lifetime.Singleton).As<ISoilRegistry>().AsSelf();
            builder.Register<CropRegistry>(Lifetime.Singleton).As<ICropRegistry>().AsSelf();

            //factory
            builder.Register<ItemContextFactory>(Lifetime.Transient).As<IItemContextFactory>().AsSelf();

            builder.RegisterInstance(itemPool);

            // player... idk what else to do. the ui need controll to the player movement
            builder.RegisterInstance(playerProxy).As<PlayerController>();
        }
        protected override void Awake() {
            base.Awake();
            Game();
        }
        protected void Game() {
            var playerInputBridge = new PlayerInputBridge(
                Container.Resolve<InputProcessor>(),
                Container.Resolve<PlayerController>()
            );
        }
    }
}