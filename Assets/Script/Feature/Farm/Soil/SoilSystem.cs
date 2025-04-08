using System;
using ObservableCollections;
using R3;
using Script.Core.Interface;
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
        Debug.Log("injected");
    }
    private void Start() {
        var farmTiles = farmContainer.GetComponentsInChildren<Soil>();

        foreach (var tile in farmTiles) {
            var context = soilData.CreateContext()
                                  .SetPosition(tile.transform.position);
            tile.SetContext(context, (useable) => HandleSelect(context, useable));
        }
    }
    private void HandleSelect(SoilContext context, IUseable useable) {
        switch (context.State.Value) {
            case SoilState.Initial:
                if (useable is ToolContext) {
                    context.State.Value = SoilState.Tilled;
                }
                break;
            case SoilState.Tilled:
                if (useable is ToolContext) {
                    context.State.Value = SoilState.Watered;
                }
                break;
            case SoilState.Watered:
                if (useable is SeedContext seed && context.CropPlanted == null) {
                    var data = (SeedData) seed.ItemData;
                    context.CropPlanted = _cropSystem.SpawnCrop(data.CropData, context.Position);
                    _cropRegistry.registry
                                          .ObserveRemove()
                                          .Where(x => x.Value == context.CropPlanted)
                                          .Subscribe(_ => context.CropPlanted = null);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(context.State), context.State.Value, null);
        }
    }
}
}