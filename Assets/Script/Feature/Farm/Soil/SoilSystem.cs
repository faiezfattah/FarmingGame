using System;
using ObservableCollections;
using R3;
using Script.Core.Interface;
using Script.Core.Interface.Systems;
using Script.Core.Model.Crop;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;
using Script.Core.Utils;
using Script.Feature.Farm.Crop;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Script.Feature.Farm.Soil {
    public class SoilSystem : MonoBehaviour {
        [SerializeField] private GameObject farmContainer;
        [SerializeField] private SoilData soilData;
        [Inject] private SoilRegistry _soilRegistry;
        [Inject] private CropSystem _cropSystem;
        [Inject] private ICropRegistry _cropRegistry;
        [Inject] private ITimeSystem _timeSystem;
        DisposableBag _bag = new();
        private void Start() {
            var farmTiles = farmContainer.GetComponentsInChildren<Soil>();

            foreach (var tile in farmTiles) {
                var context = soilData.CreateContext()
                                      .SetPosition(tile.transform.position);
                context.CropPlanted.WhereNotNull().Subscribe(_ => HandlePlanting(context)).AddTo(ref _bag);
                tile.SetContext(context, (useable) => HandleSelect(context, useable));
                _soilRegistry.Registry.Add(context);
            }
            _timeSystem.DayCount.Subscribe(_ => HandleChangeDay()).AddTo(ref _bag);
        }
        [Button]
        private void DebugUnwatered() {
            _soilRegistry.Registry.ForEach(soil => {
                Debug.Log("prev soil state: " + soil.State.Value.ToString() + "context: " + soil.Position);
                if (soil.State.Value != SoilState.Watered) return;

                soil.State.Value = SoilState.Tilled;
                Debug.Log("current soil state: " + soil.State.Value.ToString() + "context: " + soil.Position);
            });
            // f
        }
        private void HandleChangeDay() {
            _soilRegistry.Registry.ForEach(soil => {
                if (soil.State.Value != SoilState.Watered) return;
                if (!soil.Data.ShouldUnwatered(soil)) return;
                soil.State.Value = SoilState.Tilled;
            });
        }
        private void HandlePlanting(SoilContext context) {
            _cropSystem.SpawnCrop(context.CropPlanted.Value, context.Position + Vector3.up * 0.25f); // 0.25f above the ground. may need to change to raycast later

            // reset context when cropcontext is removed from the cropregistry
            _cropRegistry.ReadonlyRegistry.ObserveRemove()
                                  .Where(x => x.Value == context.CropPlanted.Value)
                                  .Subscribe(_ => context.HarvestReset())
                                  .AddTo(ref context.bag);

        }
        private void HandleSelect(SoilContext context, IUseable useable) {
            if (useable is IUseable<SoilContext> item) {
                item.Use(context);
            }
        }
    }
}