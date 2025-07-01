using System;
using ObservableCollections;
using R3;
using Script.Core.Interface;
using Script.Core.Model.Crop;
using Script.Core.Model.Item;
using Script.Core.Model.Soil;
using Script.Feature.Farm.Crop;
using UnityEngine;
using VContainer;

namespace Script.Feature.Farm.Soil {
public class SoilSystem : MonoBehaviour {
    [SerializeField] private GameObject farmContainer;
    [SerializeField] private SoilData soilData;
    private SoilRegistry _soilRegistry;
    private CropSystem _cropSystem;
    private CropRegistry _cropRegistry;
    [Inject] public void Construct(CropSystem cropSystem, CropRegistry cropRegistry) {
        _cropSystem = cropSystem;
        _cropRegistry = cropRegistry;
    }
    private void Start() {
        var farmTiles = farmContainer.GetComponentsInChildren<Soil>();

        foreach (var tile in farmTiles) {
            var context = soilData.CreateContext()
                                  .SetPosition(tile.transform.position);
            context.CropPlanted.WhereNotNull().Subscribe(_ => HandlePlanting(context));
            tile.SetContext(context, (useable) => HandleSelect(context, useable));
        }
    }
    private void HandlePlanting(SoilContext context) {
        _cropSystem.SpawnCrop(context.CropPlanted.Value, context.Position + Vector3.up * 0.25f); // 0.25f above the ground. may need to change to raycast later

        // reset context when cropcontext is removed from the cropregistry
        _cropRegistry.registry.ObserveRemove()
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