using System;
using Script.Core.Model.Soil;
using UnityEngine;

namespace Script.Feature.Farm.Soil {
public class SoilSystem : MonoBehaviour {
    [SerializeField] private GameObject farmContainer;
    [SerializeField] private SoilData soilData;
    private SoilRegistry _soilRegistry;
    private void Start() {
        var farmTiles = farmContainer.GetComponentsInChildren<Soil>();

        foreach (var tile in farmTiles) {
            var context = soilData.CreateContext()
                                  .SetPosition(tile.transform.position);
            tile.SetContext(context, () => HandleSelect(context));
        }
    }
    private void HandleSelect(SoilContext context) {
        switch (context.State.Value) {
            case SoilState.Initial:
                context.State.Value = SoilState.Tilled;
                break;
            case SoilState.Tilled:
                context.State.Value = SoilState.Watered;
                break;
            case SoilState.Watered:
                context.State.Value = SoilState.Planted;
                break;
            case SoilState.Planted:
                context.State.Value = SoilState.Initial;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(context.State), context.State.Value, null);
        }
    }
}
}